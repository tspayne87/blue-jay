namespace BlueJay.UI.Component.Attributes
{
  /// <summary>
  /// Injectable attribute meant to set a property/field as something that should inject
  /// a provided element on the node tree structure
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class InjectAttribute : Attribute
  {
  }
}
