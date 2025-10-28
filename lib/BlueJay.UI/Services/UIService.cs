using BlueJay.Component.System.Interfaces;

/// <summary>
/// Global service for UI to track various things based on interactions
/// </summary>
internal class UIService
{
  /// <summary>
  /// The currently focused entity on the UI
  /// </summary>
  public IEntity? FocusedEntity { get; set; }
}
