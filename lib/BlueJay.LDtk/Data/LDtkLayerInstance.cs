using BlueJay.Core.Container;
using BlueJay.Core.Containers;
using BlueJay.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.LDtk.Data;

internal class LDtkLayerInstance : ILDtkLayerInstance
{
  private readonly LayerInstance _instance;
  private readonly IContentManagerContainer _content;
  private readonly IGraphicsDeviceContainer _graphics;
  private readonly ISpriteBatchContainer _spriteBatch;

  public LDtkLayerInstance(
    LayerInstance instance, IContentManagerContainer content, IGraphicsDeviceContainer graphics, ISpriteBatchContainer spriteBatch)
  {
    _content = content ?? throw new ArgumentNullException(nameof(content), "ContentManagerContainer cannot be null.");
    _graphics = graphics ?? throw new ArgumentNullException(nameof(graphics), "GraphicsDeviceContainer cannot be null.");
    _spriteBatch = spriteBatch ?? throw new ArgumentNullException(nameof(spriteBatch), "SpriteBatchContainer cannot be null.");
    _instance = instance ?? throw new ArgumentNullException(nameof(instance), "LayerInstance cannot be null.");
  }

  public LayerType InstanceType => Enum.Parse<LayerType>(_instance.Type, true);

  /// <inheritdoc />
  public IntGrid CreateIntGrid()
  {
    if (_instance.IntGrid == null)
      throw new InvalidOperationException("IntGrid is not available for this layer instance.");
    if (InstanceType != LayerType.IntGrid)
      throw new InvalidOperationException("This layer instance is not of type IntGrid.");
    return new IntGrid(_instance.IntGridCsv, _instance.CWid, _instance.CHei, _instance.GridSize);
  }

  /// <inheritdoc />
  public ITexture2DContainer CreateTexture(ITexture2DContainer? tileMap = null)
  {
    if (InstanceType != LayerType.AutoLayer && InstanceType != LayerType.Tiles)
      throw new InvalidOperationException("This layer instance is not of type AutoLayer.");
      
    var tiles = _instance.AutoLayerTiles;
    if (tiles == null || tiles.Length == 0)
      tiles = _instance.GridTiles;
      
    if (tiles == null || tiles.Length == 0)
      throw new InvalidOperationException("No tiles available for this layer instance.");

    if (tileMap == null)
    {
      var relPath = _instance.TilesetRelPath?.Split('.').First();

      if (relPath == null)
        throw new InvalidOperationException("TilesetRelPath is not set for this layer instance.");
      tileMap = _content.Load<ITexture2DContainer>(relPath);
    }

    var width = _instance.CWid * _instance.GridSize;
    var height = _instance.CHei * _instance.GridSize;

    var renderTarget = _graphics.CreateRenderTarget2D((int)width, (int)height);
    _graphics.SetRenderTarget(renderTarget);
    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

    foreach (var tile in tiles!)
    {
      var position = new Vector2(tile.Px[0], tile.Px[1]);
      var targetRect = new Rectangle((int)tile.Src[0], (int)tile.Src[1], (int)_instance.GridSize, (int)_instance.GridSize);
      var mirror = (SpriteEffects)tile.F;
      _spriteBatch.Draw(tileMap!, position, targetRect, Color.White * (float)_instance.Opacity, 0, Vector2.Zero, 1f, mirror, 0);
    }
    _spriteBatch.End();
    _graphics.SetRenderTarget(null);

    return renderTarget.AsTexture2DContainer();
  }
}
