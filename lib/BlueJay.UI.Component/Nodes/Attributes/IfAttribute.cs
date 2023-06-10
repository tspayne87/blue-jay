using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Nodes.Attributes
{
  public class IfAttribute : Attribute
  {
    public Func<UIComponent, object?, Dictionary<string, object>?, object> Condition { get; private set; }
    public List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty>> ReactiveProperties { get; private set; }

    public IfAttribute(Func<UIComponent, object?, Dictionary<string, object>?, object> condition, List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty>> reactiveProperties)
      : base("if")
    {
      Condition = condition;
      ReactiveProperties = reactiveProperties;
    }
  }
}
