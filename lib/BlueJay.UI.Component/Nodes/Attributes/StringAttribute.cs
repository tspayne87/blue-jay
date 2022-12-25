namespace BlueJay.UI.Component.Nodes.Attributes
{
  public class StringAttribute : Attribute
  {
    public string Value { get; set; }

    public StringAttribute(string name, string value)
      : base(name)
    {
      Value = value;
    }
  }
}
