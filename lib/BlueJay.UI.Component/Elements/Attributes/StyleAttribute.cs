using BlueJay.UI.Component.Nodes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Elements.Attributes
{
  /// <summary>
  /// A style attribute to configure how the element should look on the screen
  /// </summary>
  internal class StyleAttribute : UIElementAttribute
  {
    /// <summary>
    /// The list of underling styles that have been attached to this style attribute
    /// </summary>
    private readonly List<StyleItem> _styles;

    /// <summary>
    /// The list of style attributes
    /// </summary>
    public List<StyleItem> Styles => _styles;

    /// <inheritdoc />
    public override bool UseParentScope
    {
      get => base.UseParentScope;
      set
      {
        if (_styles != null)
          foreach (var style in _styles)
            style.UseParentScope = value;
        base.UseParentScope = value;
      }
    }

    public StyleAttribute(List<StyleItem> styles)
      : base("style")
    {
      _styles = styles;
      foreach (var style in _styles)
        style.UseParentScope = UseParentScope;
    }

    /// <summary>
    /// The type of style attribute meant to determine where this style should be assigned when configuring it
    /// to the underlining entity
    /// </summary>
    public enum StyleItemType
    {
      Basic, Hover
    }

    /// <summary>
    /// The style item that is how the underlining style is configured by the callback and reactive properties
    /// </summary>
    public class StyleItem : ICallableType
    {
      /// <summary>
      /// The name of the style we are configuring
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// The type of style that this item should assign too
      /// </summary>
      public StyleItemType Type { get; set; }

      /// <inheritdoc />
      public Func<UIComponent, object?, Dictionary<string, object>?, object> Callback { get; set; }

      /// <inheritdoc />
      public Func<UIComponent, object?, Dictionary<string, object>?, List<IReactiveProperty?>> ReactiveProperties { get; private set; }

      /// <inheritdoc />
      public bool UseParentScope { get; set; }

      /// <summary>
      /// Constructor to build out the style item for the style attribute
      /// </summary>
      /// <param name="name">The name of the style we are configuring</param>
      /// <param name="type">The type of style that this item should assign too</param>
      /// <param name="callback">The callback function meant to get the underlining object for this style</param>
      /// <param name="reactiveProperties">The reactive properties that were found in the callback function</param>
      public StyleItem(string name, StyleItemType type, Func<UIComponent, object?, Dictionary<string, object>?, object> callback, List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty?>> reactiveProperties)
      {
        Name = name;
        Type = type;
        Callback = callback;
        ReactiveProperties = (component, evt, scope) => reactiveProperties.Select(x => x(component, evt, scope)).ToList();
      }
    }
  }
}
