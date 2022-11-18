using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Events
{
  /// <summary>
  /// Add Addon Event
  /// </summary>
  public sealed class AddAddonEvent
  {
    /// <summary>
    /// The addon that is being added
    /// </summary>
    public IAddon Addon { get; set; }

    /// <summary>
    /// Constructor for the add addon event
    /// </summary>
    /// <param name="addon">The addon that is being added</param>
    public AddAddonEvent(IAddon addon)
    {
      Addon = addon;
    }
  }
}
