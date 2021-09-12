using BlueJay.Component.System.Interfaces;

namespace BlueJay.UI.Services
{
  /// <summary>
  /// Global service for UI to track various things based on interactions
  /// </summary>
  public class UIService
  {
    /// <summary>
    /// The currently focused entity on the UI
    /// </summary>
    public IEntity FocusedEntity { get; set; }
  }
}
