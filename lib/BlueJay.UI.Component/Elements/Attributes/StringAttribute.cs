namespace BlueJay.UI.Component.Elements.Attributes
{
  /// <summary>
  /// Basic string attribute that should be used as a string to push basic data into attributes
  /// </summary>
  internal class StringAttribute : UIElementAttribute
  {
    /// <summary>
    /// The value of the string attribute
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Constructor meant to build out the string attribute
    /// </summary>
    /// <param name="name">The name of the attribute being assigned</param>
    /// <param name="value">The value of the string attribute</param>
    public StringAttribute(string name, string value)
      : base(name)
    {
      Value = value;
    }
  }
}
