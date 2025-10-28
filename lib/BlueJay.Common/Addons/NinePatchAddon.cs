using BlueJay.Component.System.Interfaces;
using BlueJay.Core;

namespace BlueJay.Common.Addons;

/// <summary>
/// Initializes a new instance of the <see cref="NinePatchAddon"/> struct.
/// </summary>
/// <param name="texture">The nine-patch texture associated with this addon.</param>
public struct NinePatchAddon(NinePatch texture) : IAddon
{
  /// <summary>
  /// The nine-patch texture associated with this addon.
  /// </summary>
  public NinePatch Texture { get; set; } = texture;
}
