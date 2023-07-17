namespace BlueJay.UI.Component.Attributes
{
  /// <summary>
  /// Provided property/field/method that needs to be assigned to an injected property
  /// later down the node tree line
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
  public class ProvideAttribute : Attribute
  {
  }
}
