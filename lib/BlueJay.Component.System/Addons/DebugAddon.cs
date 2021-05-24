using System;

namespace BlueJay.Component.System.Addons
{
  /// <summary>
  /// Debug addon is meant to set a list of addons we want to debug
  /// </summary>
  public class DebugAddon : Addon<DebugAddon>
  {
    /// <summary>
    /// The key identifier used to track down the correct addons
    /// </summary>
    public long KeyIdentifier;

    /// <summary>
    /// Constructor build out what other addons this debug addon should watch
    /// </summary>
    /// <param name="keyIdentifier">The key identifier to debug</param>
    public DebugAddon(long keyIdentifier)
    {
      KeyIdentifier = keyIdentifier;
    }
  }
}
