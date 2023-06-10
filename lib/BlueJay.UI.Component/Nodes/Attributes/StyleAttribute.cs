using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Nodes.Attributes
{
  internal class StyleAttribute : Attribute
  {
    private readonly List<StyleItem> _styles;

    public List<StyleItem> Styles => _styles;

    public StyleAttribute(List<StyleItem> styles)
      : base("style")
    {
      _styles = styles;
    }

    public enum StyleItemType
    {
      Basic, Hover
    }

    public class StyleItem 
    {
      public string Name { get; set; }
      public StyleItemType Type { get; set; }

      public Func<UIComponent, object?, Dictionary<string, object>?, object> Callback { get; set; }
      public List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty>> ReactiveProperties { get; private set; }

      public StyleItem(string name, StyleItemType type, List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty>> reactiveProperties, Func<UIComponent, object?, Dictionary<string, object>?, object> callback)
      {
        Name = name;
        Type = type;
        Callback = callback;
        ReactiveProperties = reactiveProperties;
      }
    }
  }
}
