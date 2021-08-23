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

    public List<ElementProp> Props { get; private set; }
    public List<ElementEvent> Events { get; private set; }
    public List<IReactiveProperty> ReactiveProps { get; private set; }

    public UIComponent Instance { get; private set; }

    public ElementNode(ElementType type, UIComponent instance)
    {
      Type = type;
      Instance = instance;
      Children = new List<ElementNode>();
      Props = new List<ElementProp>();
      Events = new List<ElementEvent>();
      ReactiveProps = new List<IReactiveProperty>();
    }
  }

  public class ElementProp
  {
    public string Name { get; set; }
    public Func<object, object> DataGetter { get; set; }
    public List<IReactiveProperty> ReactiveProps { get; set; }
  }

  public class ElementEvent
  {
    public string Name { get; set; }
    public Func<object, object> Callback { get; set; }
    public bool IsGlobal { get; set; }
  }

  public class ElementSlot
  {
    public string Name { get; set; }
    public ElementNode Node { get; set; }
  }

  public class ElementFor
  {
    public string ScopeName { get; set; }
    public Func<object, object> DataGetter { get; set; }
    public List<IReactiveProperty> ReactiveProps { get; set; }
  }

  public enum ElementType
  {
    Container, Text, Slot
  }

  public static class PropNames
  {
    public const string Text = "Text";
    public const string Style = "Style";
    public const string If = "if";
  }
}
