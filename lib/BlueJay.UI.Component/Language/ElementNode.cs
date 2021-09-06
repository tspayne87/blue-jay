using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  public class ElementNode
  {
    private ElementNode _parent;

    public ElementNode Parent
    {
      get => _parent;
      set
      {
        if (_parent != null)
        {
          _parent.Children.Remove(this);
        }

        _parent = value;
        _parent?.Children.Add(this);
      }
    }

    public ElementType Type { get; private set; }
    public List<ElementNode> Children { get; private set; }
    public ElementSlot Slot { get; set; }
    public ElementFor For { get; set; }
    public bool IsGlobal { get; set; }

    public List<ElementProp> Props { get; private set; }
    public List<ElementEvent> Events { get; private set; }
    public List<ElementRef> Refs { get; private set; }

    public UIComponent Instance { get; private set; }

    public ElementNode(ElementType type, UIComponent instance)
    {
      Type = type;
      Instance = instance;
      Children = new List<ElementNode>();
      Props = new List<ElementProp>();
      Events = new List<ElementEvent>();
      Refs = new List<ElementRef>();
    }

    public ReactiveScope GenerateScope(ReactiveScope scope = null)
    {
      scope = scope ?? new ReactiveScope();
      if (!scope.ContainsKey(Instance.Identifier))
        scope[Instance.Identifier] = Instance;

      foreach (var child in Children)
        child.GenerateScope(scope);

      return scope;
    }
  }

  public class ElementProp
  {
    public string Name { get; set; }
    public Func<ReactiveScope, object> DataGetter { get; set; }
    public List<string> ScopePaths { get; set; }
    public int Weight { get; set; }
  }

  public class ElementEvent
  {
    public string Name { get; set; }
    public Func<ReactiveScope, object> Callback { get; set; }
    public bool IsGlobal { get; set; }
  }

  public class ElementSlot
  {
    public string Name { get; set; }
    public ElementNode Node { get; set; }
  }

  public class ElementRef
  {
    public string PropName { get; set; }
  }

  public class ElementFor
  {
    public string ScopeName { get; set; }
    public Func<ReactiveScope, object> DataGetter { get; set; }
    public List<string> ScopePaths { get; set; }
    public bool Processed { get; set; }
  }

  public enum ElementType
  {
    Container, Text, Slot
  }

  public static class PropNames
  {
    public const string Text = "Text";
    public const string Style = "Style";
    public const string HoverStyle = "HoverStyle";
    public const string If = "if";
    public const string For = "for";
    public const string Event = "event";
    public const string Identifier = "_identifier";
  }
}
