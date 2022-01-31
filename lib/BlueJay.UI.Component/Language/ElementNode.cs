using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;

namespace BlueJay.UI.Component.Language
{
  /// <summary>
  /// The element node the represents what should be rendered on the screen and how it should be structured
  /// </summary>
  public sealed class ElementNode
  {
    /// <summary>
    /// The elment parent node
    /// </summary>
    private ElementNode _parent;

    /// <summary>
    /// The parent node to make sure that all the links are kept in tack
    /// </summary>
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

    /// <summary>
    /// The type of element we are working with
    /// </summary>
    public ElementType Type { get; private set; }

    /// <summary>
    /// The children nodes that are attached to this node
    /// </summary>
    public List<ElementNode> Children { get; private set; }

    /// <summary>
    /// The slot that should be used for this node
    /// </summary>
    public ElementSlot Slot { get; set; }

    /// <summary>
    /// The for loop that should be used
    /// </summary>
    public ElementFor For { get; set; }

    /// <summary>
    /// If this node needs to be global and ignore its parent
    /// </summary>
    public bool IsGlobal { get; set; }

    /// <summary>
    /// The props of the node, mainly things like style, text and custom props
    /// </summary>
    public List<ElementProp> Props { get; private set; }

    /// <summary>
    /// The events that need to be binded to the node
    /// </summary>
    public List<ElementEvent> Events { get; private set; }

    /// <summary>
    /// The current refs for this node that need to be assigned
    /// </summary>
    public List<ElementRef> Refs { get; private set; }

    /// <summary>
    /// The current UI Component instance that should be bound to this node
    /// </summary>
    public UIComponent Instance { get; private set; }

    /// <summary>
    /// Constructor is meant to define a basic node
    /// </summary>
    /// <param name="type">The type of node being created</param>
    /// <param name="instance">The current instance of the node</param>
    public ElementNode(ElementType type, UIComponent instance)
    {
      Type = type;
      Instance = instance;
      Children = new List<ElementNode>();
      Props = new List<ElementProp>();
      Events = new List<ElementEvent>();
      Refs = new List<ElementRef>();
    }

    /// <summary>
    /// Helper method is meant to generate a scoped based on the node tree
    /// </summary>
    /// <param name="scope">If we want to bind to a current scope</param>
    /// <returns>Will return the updated or generated scope</returns>
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

  /// <summary>
  /// The element prop for nodes
  /// </summary>
  public sealed class ElementProp
  {
    /// <summary>
    /// The name of the prop
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The getter for the data of this prop
    /// </summary>
    public Func<ReactiveScope, object> DataGetter { get; set; }

    /// <summary>
    /// The scope paths we need to watch on to rebuild the data from the getter
    /// </summary>
    public List<string> ScopePaths { get; set; }
  }

  /// <summary>
  /// The element event for nodes
  /// </summary>
  public sealed class ElementEvent
  {
    /// <summary>
    /// The name of the event
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The callback that should be ran when processing event
    /// </summary>
    public Func<ReactiveScope, object> Callback { get; set; }

    /// <summary>
    /// If this event should be put in the global scope
    /// </summary>
    public bool IsGlobal { get; set; }

    /// <summary>
    /// What type of modifier you want to use for this event process
    /// </summary>
    public string Modifier { get; set; }
  }

  /// <summary>
  /// The element slot for nodes
  /// </summary>
  public sealed class ElementSlot
  {
    /// <summary>
    /// The name of the slot
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The element node that should be used to stich together the slots to what is in the main component
    /// </summary>
    public ElementNode Node { get; set; }
  }

  /// <summary>
  /// The element ref for nodes
  /// </summary>
  public sealed class ElementRef
  {
    /// <summary>
    /// The property name that should be used when assigning the ref
    /// </summary>
    public string PropName { get; set; }
  }

  /// <summary>
  /// The element for for nodes
  /// </summary>
  public sealed class ElementFor
  {
    /// <summary>
    /// The scope name for each element
    /// </summary>
    public string ScopeName { get; set; }

    /// <summary>
    /// The data getter to generate the list for iterating over
    /// </summary>
    public Func<ReactiveScope, object> DataGetter { get; set; }

    /// <summary>
    /// The paths we need to watch on the rebuild the data getter
    /// </summary>
    public List<string> ScopePaths { get; set; }

    /// <summary>
    /// If this for has been processed
    /// </summary>
    public bool Processed { get; set; }
  }

  /// <summary>
  /// The element type that needs to be rendered
  /// </summary>
  public enum ElementType
  {
    /// <summary>
    /// The container object is the basic building block for most items
    /// </summary>
    Container,
    
    /// <summary>
    /// The text object that is meant to render text to the screen
    /// </summary>
    Text,
    
    /// <summary>
    /// The slot entity is meant to stich together everything
    /// </summary>
    Slot
  }

  /// <summary>
  /// The list of static prop names
  /// </summary>
  public static class PropNames
  {
    /// <summary>
    /// The text prop name
    /// </summary>
    public const string Text = "Text";

    /// <summary>
    /// The style prop name
    /// </summary>
    public const string Style = "Style";

    /// <summary>
    /// The hover style prop name
    /// </summary>
    public const string HoverStyle = "HoverStyle";

    /// <summary>
    /// The if prop name
    /// </summary>
    public const string If = "if";

    /// <summary>
    /// The for prop name
    /// </summary>
    public const string For = "for";

    /// <summary>
    /// The event prop name
    /// </summary>
    public const string Event = "evt";

    /// <summary>
    /// The identifier prop name
    /// </summary>
    public const string Identifier = "_identifier";
  }
}
