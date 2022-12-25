namespace BlueJay.UI.Component.Nodes.Attributes
{
  public class IfAttribute : Attribute
  {
    public Func<UIComponent, object?, Dictionary<string, object>?, object> Condition { get; set; }

    public IfAttribute(Func<UIComponent, object?, Dictionary<string, object>?, object> condition)
      : base("if")
    {
      Condition = condition;
    }
  }
}
