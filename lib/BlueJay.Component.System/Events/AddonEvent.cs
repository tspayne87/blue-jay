using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Events
{
  /// <summary>
  /// The addon event base
  /// </summary>
  public class AddonEvent
  {
    /// <summary>
    /// The addon that is being added, updated, or deleted
    /// </summary>
    public IAddon Addon { get; set; }
  }
}
