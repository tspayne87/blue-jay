namespace BlueJay.UI.Component.Elements.Attributes
{
  /// <summary>
  /// Reference object that is meant to attach the entity/entities to the underlining UIComponent so that
  /// certain operations could be could happen to that object
  /// </summary>
  internal class RefAttribute : UIElementAttribute
  {
    /// <summary>
    /// Constructor to the ref attribute
    /// </summary>
    /// <param name="name">The name of the property where the entity/entities should be assigned too</param>
    public RefAttribute(string name)
      : base(name) { }
  }
}
