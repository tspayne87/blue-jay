using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Component.System.Addons
{
  /// <summary>
  /// Addon meant to handle textures that are meant to be rendered in the scene
  /// </summary>
  public class TextureAddon : Addon<TextureAddon>
  {
    /// <summary>
    /// The content manager that will be loaded if an asset name is used instead
    /// of a pre-loaded texture
    /// </summary>
    private readonly ContentManager _manager;

    /// <summary>
    /// The asset name that should be used to load the texture from the manager
    /// </summary>
    private readonly string _assetName;

    /// <summary>
    /// The current texture that has been loaded for the manager
    /// </summary>
    public virtual Texture2D Texture { get; set; }


    /// <summary>
    /// Constructor to build out the texture addon based on the asset name
    /// </summary>
    /// <param name="assetName">The asset name for the texture</param>
    /// <param name="manager">The manager that will be used to load the texture</param>
    public TextureAddon(string assetName, ContentManager manager)
    {
      _assetName = assetName;
      _manager = manager;
    }

    /// <summary>
    /// Constructor meant to set the texture for this addon
    /// </summary>
    /// <param name="texture">The texture that should be set</param>
    public TextureAddon(Texture2D texture)
    {
      Texture = texture;
      _assetName = texture.Name;
    }

    /// <summary>
    /// The on load event that should load the texture into existance before rendering
    /// </summary>
    public override void OnLoad()
    {
      if (Texture == null && _manager != null)
      {
        Texture = _manager.Load<Texture2D>(_assetName);
      }
    }

    /// <summary>
    /// Overriden to string function is meant to give a quick and easy way to see
    /// how this object looks while debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Texture | {Texture.Name}";
    }
  }
}
