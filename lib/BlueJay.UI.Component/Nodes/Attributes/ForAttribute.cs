namespace BlueJay.UI.Component.Nodes.Attributes
{
  public class ForAttribute : Attribute
  {
    public string ScopeName { get; set; }
    public Func<UIComponent, object?, Dictionary<string, object>?, object> Value { get; set; }

    public ForAttribute(string scopeName, Func<UIComponent, object?, Dictionary<string, object>?, object> value)
      : base("for")
    {
      ScopeName = scopeName;
      Value = value;
    }
  }
}
