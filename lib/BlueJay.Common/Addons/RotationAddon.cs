using BlueJay.Component.System.Interfaces;

namespace BlueJay.Common.Addons;

/// <summary>
/// Represents the rotation for a node in 2D space.
/// </summary>
/// <param name="rotation">The current rotation for the node</param>
public struct RotationAddon(float rotation) : IAddon
{
  /// <summary>
  /// The current rotation for this node
  /// </summary>
  public float Rotation { get; set; } = rotation;
}
