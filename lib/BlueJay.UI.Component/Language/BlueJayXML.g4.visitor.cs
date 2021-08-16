using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using BlueJay.UI.Component.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace BlueJay.UI.Component.Language
{
  public class BlueJayXMLVisitor : BlueJayXMLBaseVisitor<object>
  {
    private readonly object _intance;
    private readonly List<Type> _components;
    private readonly IServiceProvider _serviceProvider;
    private ParameterExpression _param;
    private ElementNode _root;

    public ElementNode Root
    {
      get => _root;
      set => _root = value;
    }

    public BlueJayXMLVisitor(IServiceProvider serviceProvider, object instance, List<Type> components)
    {
      _serviceProvider = serviceProvider;
      _intance = instance;
      _components = components ?? new List<Type>();
    }

    #region Node Evaluators
    public override object VisitContainerExpression([NotNull] BlueJayXMLParser.ContainerExpressionContext context)
    {
      var node = new ElementNode(ElementType.Container);
      if (Root == null) Root = node;
      ProcessElement(node, context, new List<BindableProp>());
      return null;
    }

    public override object VisitTextExpression([NotNull] BlueJayXMLParser.TextExpressionContext context)
    {
      var node = new ElementNode(ElementType.Text);

      // TODO: Create the idea of being able to process arguments in here
      node.Props.Add(new ElementProp() { Name = PropNames.Text, Data = context.GetText().Trim() });
      return node;
    }

    public override object VisitSlotExpression([NotNull] BlueJayXMLParser.SlotExpressionContext context)
    {
      return new ElementNode(ElementType.Slot);
    }

    public override object VisitCustomElementExpression([NotNull] BlueJayXMLParser.CustomElementExpressionContext context)
    {
      if (context.children[1].GetText() != context.children[context.ChildCount - 2].GetText())
        return null; // TODO: Need to throw error or something since we cannot find cooresponding tag

      var elementName = context.children[1].GetText();
      var type = _components.FirstOrDefault(x => x.Name == elementName);
      if (type != null)
      {
        var node = _serviceProvider.ParseUIComponet(type, out var instance);
        if (Root == null) Root = node;

        var placeholderNode = new ElementNode(ElementType.Container);
        var bindableProps = instance.GetType()
          .GetFields()
          .Select(x => new BindableProp()
          {
            ReactiveProp = x.GetValue(instance) as IReactiveProperty,
            Attribute = x.GetCustomAttributes(typeof(PropAttribute), false).FirstOrDefault() as PropAttribute,
            Name = x.Name
          })
          .Where(x => x.ReactiveProp != null && x.Attribute != null)
          .ToList();
        ProcessElement(placeholderNode, context, bindableProps);

        node.Props.AddRange(placeholderNode.Props);
        node.Events.AddRange(placeholderNode.Events);
        node.ReactiveProps.AddRange(placeholderNode.ReactiveProps);

        if (node.Slot != null)
        {
          foreach (var child in placeholderNode.Children.ToArray())
          {
            child.Parent = node.Slot.Node.Parent;
          }
          node.Slot.Node.Parent = null;
          node.Slot = null;
        }
        return node;
      }

      return null;
    }

    public override object VisitInnerExpression([NotNull] BlueJayXMLParser.InnerExpressionContext context)
    {
      var nodes = new List<ElementNode>();
      for(var i = 0; i < context.ChildCount; ++i)
      {
        nodes.Add(Visit(context.GetChild(i)) as ElementNode);
      }
      return nodes;
    }

    private void ProcessElement(ElementNode node, IParseTree context, List<BindableProp> bindableProps)
    {
      var i = 2;
      for (; i < context.ChildCount && context.GetChild(i).GetText() != ">"; ++i)
      {
        if (context.GetChild(i).GetText().Trim().Length > 0)
        {
          var obj = Visit(context.GetChild(i));
          var (type, name, value, props) = obj as Tuple<AttributeType, string, object, List<IReactiveProperty>>;

          var bindedProp = bindableProps.FirstOrDefault(x => x.Name == name);
          switch (type)
          {
            case AttributeType.Prop:
              if (bindedProp != null)
                bindedProp.ReactiveProp.Value = value;
              node.Props.Add(new ElementProp() { Name = name, Data = value, ReactiveProps = props, Type = bindedProp?.Attribute.Binding ?? PropBinding.None, InstanceProp = bindedProp?.ReactiveProp });
              break;
            case AttributeType.Binding:
              var dataGetter = value as Func<object, object>;
              if (bindedProp != null && props.Count > 0)
              {
                bindedProp.ReactiveProp.Value = dataGetter(null);
                if (bindedProp.Attribute.Binding == PropBinding.TwoWay)
                {
                  if (props.Count == 1)
                  {
                    bindedProp.ReactiveProp.PropertyChanged += (sender, o) => props[0].Value = bindedProp.ReactiveProp.Value;
                    props[0].PropertyChanged += (sender, o) => bindedProp.ReactiveProp.Value = dataGetter(null);
                  }
                }
                else if (bindedProp.Attribute.Binding == PropBinding.OneWay)
                {
                  foreach (var prop in props)
                  {
                    prop.PropertyChanged += (sender, o) => bindedProp.ReactiveProp.Value = dataGetter(null);
                  }
                }
              }
              node.Props.Add(new ElementProp() { Name = name, DataGetter = dataGetter, ReactiveProps = props, Type = bindedProp?.Attribute.Binding ?? PropBinding.None, InstanceProp = bindedProp?.ReactiveProp });
              break;
            case AttributeType.Event:
              node.Events.Add(value as ElementEvent);
              break;
          }
        }
      }

      var children = Visit(context.GetChild(++i)) as List<ElementNode>;
      foreach(var child in children)
      {
        if (child != null)
        {
          child.Parent = node;

          if (child.Type == ElementType.Slot)
          {
            if (Root.Slot != null) throw new ArgumentOutOfRangeException("Cannot have two slots");
            Root.Slot = new ElementSlot() { Name = "Default", Node = child };
          }
        }
      }
    }
    #endregion

    #region Attribute Evaluators
    public override object VisitStringAttributeExpression([NotNull] BlueJayXMLParser.StringAttributeExpressionContext context)
    {
      var name = context.children[0].GetText();
      var value = context.children[context.ChildCount - 2].GetText();
      return new Tuple<AttributeType, string, object, List<IReactiveProperty>>(AttributeType.Prop, name, value, null);
    }

    public override object VisitBindingAttributeExpression([NotNull] BlueJayXMLParser.BindingAttributeExpressionContext context)
    {
      var name = context.children[1].GetText();
      _param = Expression.Parameter(typeof(object), "x");
      var body = Visit(context.children[context.ChildCount - 2]) as BuilderExpression;
      var expression = Expression.Lambda<Func<object, object>>(Expression.Convert(body.Expression, typeof(object)), _param).Compile();
      return new Tuple<AttributeType, string, object, List<IReactiveProperty>>(AttributeType.Binding, name, expression, body.Data.Select(x => x as IReactiveProperty).Where(x => x != null).ToList());
    }

    public override object VisitEventAttributeExpression([NotNull] BlueJayXMLParser.EventAttributeExpressionContext context)
    {
      var name = context.children[1].GetText();
      _param = Expression.Parameter(typeof(object), "x");
      var body = Visit(context.children[context.ChildCount - 2]) as BuilderExpression;
      var expression = Expression.Lambda<Func<object, object>>(Expression.Convert(body.Expression, typeof(object)), _param).Compile();

      var elementEvent = new ElementEvent() { Name = name, Callback = expression };
      for (var i = 2; i < context.ChildCount && context.GetChild(i).GetText() != ">"; ++i)
      {
        if (context.GetChild(i).GetText().ToLower() == "global")
          elementEvent.IsGlobal = true;
      }
      return new Tuple<AttributeType, string, object, List<IReactiveProperty>>(AttributeType.Event, name, elementEvent, null);
    }
    #endregion

    #region Literal Expressions
    public override object VisitString([NotNull] BlueJayXMLParser.StringContext context)
    {
      var str = context.children[1].GetText();
      return new BuilderExpression(Expression.Constant(str), new List<object>() { str });
    }

    public override object VisitDecimal([NotNull] BlueJayXMLParser.DecimalContext context)
    {
      if (float.TryParse(context.GetText(), out var num))
        return new BuilderExpression(Expression.Constant(num), new List<object>() { num });
      return new BuilderExpression(Expression.Constant(null), null);
    }

    public override object VisitInteger([NotNull] BlueJayXMLParser.IntegerContext context)
    {
      if (int.TryParse(context.GetText(), out var num))
        return new BuilderExpression(Expression.Constant(num), new List<object>() { num });
      return new BuilderExpression(Expression.Constant(null), null);
    }

    public override object VisitBoolean([NotNull] BlueJayXMLParser.BooleanContext context)
    {
      if (bool.TryParse(context.GetText(), out var boolean))
        return new BuilderExpression(Expression.Constant(boolean), new List<object>() { boolean });
      return new BuilderExpression(Expression.Constant(null), null);
    }
    #endregion

    #region Functional Expressions
    public override object VisitFunctionExpression([NotNull] BlueJayXMLParser.FunctionExpressionContext context)
    {
      var methodName = context.children[0].GetText();
      var props = Visit(context.children[2]) as List<BuilderExpression>;

      var method = _intance.GetType().GetMethod(methodName);
      if (method != null && props != null)
      {
        var args = props.Select((x, i) =>
        {
          if (x.Expression == _param) return Expression.Convert(x.Expression, method.GetParameters()[i].ParameterType);
          return x.Expression;
        });
        return new BuilderExpression(Expression.Call(Expression.Constant(_intance), method, args), props.SelectMany(x => x.Data).ToList());
      }
      return new BuilderExpression(Expression.Constant(null), null);
    }

    public override object VisitArgumentExpression([NotNull] BlueJayXMLParser.ArgumentExpressionContext context)
    {
      var props = new List<BuilderExpression>();
      for (var i = 0; i < context.ChildCount; ++i)
      {
        var str = context.children[i].GetText();
        if (str != "," && str.Trim().Length > 0)
        {
          props.Add(Visit(context.children[i]) as BuilderExpression);
        }
      }
      return props;
    }
    #endregion

    #region Identifier Expressions
    public override object VisitIdentifier([NotNull] BlueJayXMLParser.IdentifierContext context)
    {
      var propName = context.GetText();
      var member = _intance.GetType().GetMember(propName)?[0];
      if (member != null)
      {
        var obj = member is FieldInfo ? ((FieldInfo)member).GetValue(_intance) : ((PropertyInfo)member).GetValue(_intance);
        var expression = Expression.PropertyOrField(Expression.Constant(_intance), propName);
        if (obj is IReactiveProperty)
          expression = Expression.PropertyOrField(expression, "Value");

        return new BuilderExpression(expression, new List<object>() { obj });
      }

      return new BuilderExpression(Expression.Constant(null), null);
    }

    public override object VisitContextVarExpression([NotNull] BlueJayXMLParser.ContextVarExpressionContext context)
    {
      return new BuilderExpression(_param, new List<object>());
    }
    #endregion

    private enum AttributeType
    {
      Prop, Event, Binding, If, Foreach
    }

    private class BuilderExpression
    {
      public Expression Expression { get; private set; }
      public List<object> Data { get; private set; }

      public BuilderExpression(Expression expression, List<object> data)
      {
        Expression = expression;
        Data = data;
      }
    }

    private class BindableProp
    {
      public IReactiveProperty ReactiveProp { get; set; }
      public PropAttribute Attribute { get; set; }
      public string Name { get; set; }
    }
  }
}
