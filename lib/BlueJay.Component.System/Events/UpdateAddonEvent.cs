using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Events
{
  /// <summary>
  /// The update addon event
  /// </summary>
  public sealed class UpdateAddonEvent
  {
    /// <summary>
    /// The addon that is being updated
    /// </summary>
    public IAddon Addon { get; set; }

    /// <summary>
    /// Constructor for the updated addon event
    /// </summary>
    /// <param name="addon">The addon that is being updated</param>
    public UpdateAddonEvent(IAddon addon)
    {
      Addon = addon;
    }
  }
}
