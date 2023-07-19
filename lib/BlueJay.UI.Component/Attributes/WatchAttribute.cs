namespace BlueJay.UI.Component.Attributes
{
  /// <summary>
  /// Attribute is meant to watch on reactive properties on the component and trigger a call when a change is made to
  /// the reactive property
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class WatchAttribute : Attribute
  {
    /// <summary>
    /// The property we need to watch
    /// </summary>
    public string Prop { get; set; }

    /// <summary>
    /// Constructor is meant to gather the property this method should be watching on
    /// </summary>
    /// <param name="prop">The property we are watching on</param>
    public WatchAttribute(string prop)
    {
      Prop = prop;
    }
  }
}
