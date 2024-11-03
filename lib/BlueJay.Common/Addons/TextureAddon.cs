using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Container;

namespace BlueJay.Common.Addons
{
  /// <summary>
  /// Addon meant to handle textures that are meant to be rendered in the scene
  /// </summary>
  public struct TextureAddon : IAddon
  {
    /// <summary>
    /// The current texture that has been loaded for the manager
    /// </summary>
    public ITexture2DContainer Texture { get; set; }

    /// <summary>
    /// Constructor meant to set the texture for this addon
    /// </summary>
    /// <param name="texture">The texture that should be set</param>
    public TextureAddon(ITexture2DContainer texture)
    {
      Texture = texture;
    }

    /// <summary>
    /// Overriden to string function is meant to give a quick and easy way to see
    /// how this object looks while debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Texture | {Texture.Current?.Name}";
    }
  }
}
