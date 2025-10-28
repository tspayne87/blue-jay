using System;
using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Container;
using BlueJay.UI.Interfaces;
using BlueJay.Utils;

namespace BlueJay.UI.Tree;

internal class TextureElement(
  IScreenViewport screen,
  IStyleService styleService,
  IServiceProvider provider,
  IContentManagerContainer content,
  ITexture2DContainer texture,
  IFontCollection fontCollection)
  : Element("Texture", screen, styleService, provider, content, fontCollection), ITextureElement
{
  /// <summary>
  /// The texture used by the element.
  /// </summary>
  private ITexture2DContainer _texture = texture;

  /// <summary>
  /// The number of frames in the texture.
  /// </summary>
  private int _frameCount = 1;

  /// <summary>
  /// The amount of time to wait between frames.
  /// </summary>
  private int _frameTickAmount = 0;

  /// <summary>
  /// The current frame of the animation.
  /// </summary>
  private int _frame = 0;

  /// <summary>
  /// The starting frame of the animation.
  /// </summary>
  private int _startingFrame = 0;

  /// <summary>
  /// The number of rows in the texture.
  /// </summary>
  private int _rows = 1;

  /// <summary>
  /// The number of columns in the texture.
  /// </summary>
  private int _cols = 1;

  /// <inheritdoc />
  public ITexture2DContainer Texture
  {
    get => _texture;
    set
    {
      _texture = value;
      UpdateEntity();
    }
  }

  /// <inheritdoc />
  public int FrameCount
  {
    get => _frameCount;
    set
    {
      _frameCount = value;
      UpdateEntity();
    }
  }

  /// <inheritdoc />
  public int FrameTickAmount
  {
    get => _frameTickAmount;
    set
    {
      _frameTickAmount = value;
      UpdateEntity();
    }
  }

  /// <inheritdoc />
  public int Frame
  {
    get => _frame;
    set
    {
      _frame = value;
      UpdateEntity();
    }
  }

  /// <inheritdoc />
  public int StartingFrame
  {
    get => _startingFrame;
    set
    {
      _startingFrame = value;
      UpdateEntity();
    }
  }

  /// <inheritdoc />
  public int Rows
  {
    get => _rows;
    set
    {
      _rows = value;
      UpdateEntity();
    }
  }

  /// <inheritdoc />
  public int Cols
  {
    get => _cols;
    set
    {
      _cols = value;
      UpdateEntity();
    }
  }

  /// <inheritdoc />
  protected override bool NeedsEntity => true;

  /// <inheritdoc />
  protected override void UpdateEntity()
  {
    base.UpdateEntity();
    if (_entity == null)
      return;

    _entity!.Upsert(new TextureAddon(_texture));
    _entity!.Upsert(new SpriteSheetAddon(_rows, _cols));
    _entity!.Upsert(new FrameAddon(_frameCount, _frameTickAmount, _frame, _startingFrame));
  }

  /// <inheritdoc />
  public override int CalculateHeight()
  {
    if (_cache.Height == null)
    {
      if (TryGetPixelHeight(out var result))
        return result;
      _cache.Height = _texture.Height;
    }
    return _cache.Height.Value;
  }
}
