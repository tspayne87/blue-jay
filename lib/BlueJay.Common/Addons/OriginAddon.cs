using System;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;

namespace BlueJay.Common.Addons;

/// <summary>
/// Represents the origin point for a node in 2D space.
/// </summary>
/// <param name="origin">The current origin for the node</param>
public struct OriginAddon(Vector2 origin) : IAddon
{
  /// <summary>
  /// The current origin for this node
  /// </summary>
  public Vector2 Origin { get; set; } = origin;
}
