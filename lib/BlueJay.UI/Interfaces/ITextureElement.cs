using System;
using BlueJay.Core.Container;

namespace BlueJay.UI.Interfaces;

public interface ITextureElement : IElement
{
  /// <summary>
  /// The texture used by the element.
  /// </summary>
  ITexture2DContainer Texture { get; set; }

  /// <summary>
  /// The number of frames in the texture.
  /// </summary>
  int FrameCount { get; set; }

  /// <summary>
  /// The amount of time to wait between frames.
  /// </summary>
  int FrameTickAmount { get; set; }

  /// <summary>
  /// The current frame of the animation.
  /// </summary>
  int Frame { get; set; }

  /// <summary>
  /// The starting frame of the animation.
  /// </summary>
  int StartingFrame { get; set; }

  /// <summary>
  /// The number of rows in the sprite sheet.
  /// </summary>
  int Rows { get; set; }

  /// <summary>
  /// The number of columns in the sprite sheet.
  /// </summary>
  int Cols { get; set; }
}
