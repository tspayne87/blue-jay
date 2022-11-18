using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Events
{
  /// <summary>
  /// Delete addon event
  /// </summary>
  public sealed class RemoveAddonEvent
  {
    /// <summary>
    /// The addon that is being removed
    /// </summary>
    public IAddon Addon { get; set; }

    /// <summary>
    /// Constructor for the removed addon event
    /// </summary>
    /// <param name="addon">The addon that is being removed</param>
    public RemoveAddonEvent(IAddon addon)
    {
      Addon = addon;
    }
  }
}
