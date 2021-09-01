using Antlr4.Runtime.Misc;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Language.Antlr;
using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  public class XMLParserVisitor : XMLParserBaseVisitor<object>
  {
    private readonly UIComponent _intance;
    private readonly List<Type> _components;
    private readonly IServiceProvider _serviceProvider;
    private ElementNode _root;

    public XMLParserVisitor(IServiceProvider serviceProvider, UIComponent instance, List<Type> components)
    {
      _serviceProvider = serviceProvider;
      _intance = instance;
      _components = components ?? new List<Type>();
    }

    public ElementNode Root => _root;

    #region Element Node Processors
    public override object VisitElement([NotNull] XMLParser.ElementContext context)
    {
      if (context.exception != null)
        throw context.exception;

      var isSingle = context.GetChild(context.ChildCount - 1).GetText() == "/>";
      var name = context.GetChild(1).GetText();
      if (!isSingle && name != context.GetChild(context.ChildCount - 2).GetText())
        throw new ArgumentOutOfRangeException("Could not find cooresponding tag");

      var node = new ElementNode(ElementType.Container, _intance);
      UIComponent instance = null;
      if (name != "Container")
      {
        var type = _components.FirstOrDefault(x => x.Name == name);
        if (type == null) return null;
        node = _serviceProvider.ParseUIComponet(type, out instance);
      }

      if (_root == null)
        _root = node;

      var i = 2;
      var events = new List<ElementEvent>();
      for (; i < context.ChildCount && !context.GetChild(i).GetText().Contains(">"); ++i)
      {
        if (context.GetChild(i).GetText() == "Global")
        {
          node.IsGlobal = true;
          continue;
        }
        var attr = Visit(context.GetChild(i));
        if (attr is ElementProp)
        {
          var prop = attr as ElementProp;
          if (prop.Name == PropNames.Style || prop.Name == PropNames.HoverStyle)
          {
            var existingProp = node.Props.FirstOrDefault(x => x.Name == prop.Name);
            if (existingProp != null)
            {
              var oldGetter = existingProp.DataGetter;
              existingProp.DataGetter = x => prop.DataGetter(new ReactiveScope(new Dictionary<string, object>() { { prop.Name, oldGetter(x) } }));
              existingProp.ScopePaths.AddRange(prop.ScopePaths);
              continue;
            }
          }

          node.Props.Add(prop);
        }
        else if (attr is ElementEvent)
        {
          var evt = attr as ElementEvent;
          if (instance != null) events.Add(evt);
          node.Events.Add(evt);
        }
        else if (attr is ElementFor)
        {
          node.For = attr as ElementFor;
        }
        else if (attr is ElementRef)
        {
          node.Refs.Add(attr as ElementRef);
        }
      }

      if (instance != null)
      {
        instance.Initialize(_intance, events);
      }

      if (!isSingle)
      {
        i++;
        for (; i < context.ChildCount && context.GetChild(i).GetText() != "<"; ++i)
        {
          var child = Visit(context.GetChild(i)) as ElementNode;

          if (child != null)
          {
            if (node.Slot != null)
              child.Parent = node.Slot.Node.Parent;
            else
              child.Parent = node;
          }
        }

        if (node.Slot != null)
        {
          node.Slot.Node.Parent = null;
          node.Slot = null;
        }
      }
      return node;
    }

    public override object VisitChardata([NotNull] XMLParser.ChardataContext context)
    {
      var expressions = new List<Func<ReactiveScope, string>>();
      var scopePaths = new List<string>();
      for (var i = 0; i < context.ChildCount; ++i)
      {
        var txt = context.GetChild(i).GetText();
        if (!string.IsNullOrWhiteSpace(txt))
        {
          if (txt.StartsWith("{"))
          {
            var expression = context.GetText();
            var result = _serviceProvider.ParseExpression(txt.Substring(2, txt.Length - 4), _intance);
            expressions.Add(x => result.Callback(x)?.ToString() ?? string.Empty);
            scopePaths.AddRange(result.ScopePaths);
          }
          else
          {
            expressions.Add(x => txt);
          }
        }
      }
      if (expressions.Count == 0) return null;

      var node = new ElementNode(ElementType.Text, _intance);
      node.Props.Add(new ElementProp() { Name = PropNames.Text, DataGetter = x => string.Join(string.Empty, expressions.Select(y => y(x))).Trim(), ScopePaths = scopePaths });
      return node;
    }

    public override object VisitSlotElement([NotNull] XMLParser.SlotElementContext context)
    {
      if (_root != null)
      {
        var slot = new ElementNode(ElementType.Slot, _intance);
        _root.Slot = new ElementSlot() { Name = "Default", Node = slot };
        return slot;
      }
      return null;
    }
    #endregion

    public override object VisitBasicAttribute([NotNull] XMLParser.BasicAttributeContext context)
    {
      var name = context.GetChild(0).GetText();
      var text = context.GetChild(context.ChildCount - 1).GetText();

      if (name == PropNames.Style || name == PropNames.HoverStyle)
      { // Parse the style attribute since it is a special property and we want to parse the data
        var result = _serviceProvider.ParseStyle(text.Substring(1, text.Length - 2), _intance, name);
        return new ElementProp() { Name = name, DataGetter = result.Callback, ScopePaths = result.ScopePaths };
      }
      return new ElementProp() { Name = name, DataGetter = x => text.Substring(1, text.Length - 2) };
    }

    public override object VisitBindAttribute([NotNull] XMLParser.BindAttributeContext context)
    {
      var name = context.GetChild(1).GetText();
      var expression = context.GetChild(context.ChildCount - 1).GetText();
      var result = _serviceProvider.ParseExpression(expression.Substring(1, expression.Length - 2), _intance);
      return new ElementProp() { Name = name, DataGetter = result.Callback, ScopePaths = result.ScopePaths };
    }

    public override object VisitEventAttribute([NotNull] XMLParser.EventAttributeContext context)
    {
      var name = context.GetChild(1).GetText();
      var expression = context.GetChild(context.ChildCount - 1).GetText();
      var result = _serviceProvider.ParseExpression(expression.Substring(1, expression.Length - 2), _intance);
      return new ElementEvent() { Name = name, Callback = result.Callback, IsGlobal = context.ChildCount == 6 };
    }

    public override object VisitIfAttribute([NotNull] XMLParser.IfAttributeContext context)
    {
      var expression = context.GetChild(context.ChildCount - 1).GetText();
      var result = _serviceProvider.ParseExpression(expression.Substring(1, expression.Length - 2), _intance);
      return new ElementProp() { Name = PropNames.If, DataGetter = result.Callback, ScopePaths = result.ScopePaths };
    }

    public override object VisitForAttribute([NotNull] XMLParser.ForAttributeContext context)
    {
      var expression = context.GetChild(context.ChildCount - 1).GetText();
      return _serviceProvider.ParseFor(expression.Substring(1, expression.Length - 2), _intance);
    }

    public override object VisitRefAttribute([NotNull] XMLParser.RefAttributeContext context)
    {
      var propName = context.GetChild(context.ChildCount - 1).GetText();
      return new ElementRef() { PropName = propName.Substring(1, propName.Length - 2) };
    }
  }
}
