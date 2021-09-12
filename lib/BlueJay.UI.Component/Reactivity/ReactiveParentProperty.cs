namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// The reactive parent property
  /// </summary>
  public class ReactiveParentProperty : IReactiveParentProperty
  {
    /// <summary>
    /// The reactive value for the parent
    /// </summary>
    public IReactiveProperty Value { get; set; }

    /// <summary>
    /// The name that is on the parent
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Constructor to set defaults
    /// </summary>
    /// <param name="value">The reactive value for the parent</param>
    /// <param name="name">The name that is on the parent</param>
    public ReactiveParentProperty(IReactiveProperty value, string name)
    {
      Value = value;
      Name = name;
    }
  }
}
