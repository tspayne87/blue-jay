namespace BlueJay.UI.Component.Nodes.Attributes
{
  public class ExpressionAttribute : Attribute
  {
    public Func<UIComponent, object?, Dictionary<string, object>?, object> Value { get; set; }

    public ExpressionAttribute(string name, Func<UIComponent, object?, Dictionary<string, object>?, object> value)
      : base(name)
    {
      Value = value;
    }
  }
}
