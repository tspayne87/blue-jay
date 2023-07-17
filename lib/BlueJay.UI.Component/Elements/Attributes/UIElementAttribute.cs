namespace BlueJay.UI.Component.Elements.Attributes
{
  /// <summary>
  /// The UIElement attribute that is extracted from the view elements
  /// </summary>
  internal class UIElementAttribute
  {
    /// <summary>
    /// The name of the attribute for this item
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// If this attribute should use the parent component scope when calling callback functions
    /// </summary>
    public virtual bool UseParentScope { get; set; }

    /// <summary>
    /// The basic constructor the the UIElement attribute
    /// </summary>
    /// <param name="name">The name of the attribute for this item</param>
    public UIElementAttribute(string name)
    {
      Name = name;
      UseParentScope = false;
    }
  }
}
