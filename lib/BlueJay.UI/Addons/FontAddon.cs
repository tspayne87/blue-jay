using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Container;
using BlueJay.Core.Containers;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Addons;

/// <summary>
/// Represents a font addon.
/// </summary>
/// <param name="textureFont">The texture font that will be used when rendering text.</param>
/// <param name="spriteFont">The sprite font that will be used when rendering text.</param>
/// <param name="size">The size of the font.</param>
/// <param name="color">The color of the font.</param>
public struct FontAddon(TextureFont? textureFont, ISpriteFontContainer? spriteFont, int? size, Color? color) : IAddon
{
  /// <summary>
  /// The texture font that will be used when rendering text.
  /// </summary>
  public TextureFont? TextureFont = textureFont;

  /// <summary>
  /// The sprite font that will be used when rendering text.
  /// </summary>
  public ISpriteFontContainer? SpriteFont = spriteFont;

  /// <summary>
  /// The size of the font.
  /// </summary>
  public int Size = size ?? 1;

  /// <summary>
  /// The color of the font.
  /// </summary>
  public Color Color = color ?? Color.Black;

  /// <summary>
  /// Indicates whether the font addon is valid, it needs to have either a texture font or sprite font, but not both.
  /// </summary>
  public bool IsValid => (TextureFont == null && SpriteFont != null) || (TextureFont != null && SpriteFont == null);

}
