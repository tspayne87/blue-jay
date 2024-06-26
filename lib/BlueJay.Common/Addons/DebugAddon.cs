﻿using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Common.Addons
{
  /// <summary>
  /// Debug addon is meant to set a list of addons we want to debug
  /// </summary>
  public struct DebugAddon : IAddon
  {
    /// <summary>
    /// The key identifier used to track down the correct addons
    /// </summary>
    public AddonKey KeyIdentifier;

    /// <summary>
    /// Constructor build out what other addons this debug addon should watch
    /// </summary>
    /// <param name="keyIdentifier">The key identifier to debug</param>
    public DebugAddon(AddonKey keyIdentifier)
    {
      KeyIdentifier = keyIdentifier;
    }
  }
}
