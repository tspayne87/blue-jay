using System;
using BlueJay.Core;
using BlueJay.Core.Container;
using BlueJay.LDtk.Fields;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace BlueJay.LDtk.Data;

internal class LDtkFieldInstance : ILDtkFieldInstance
{
  private readonly LDtkObject _object;
  private readonly FieldInstance _field;
  private readonly IContentManagerContainer _content;
  private readonly IServiceProvider _service;

  public LDtkFieldInstance(LDtkObject ldtkObject, FieldInstance field, IContentManagerContainer content, IServiceProvider service)
  {
    _object = ldtkObject ?? throw new ArgumentNullException(nameof(ldtkObject), "LDtkObject cannot be null.");
    _field = field ?? throw new ArgumentNullException(nameof(field), "FieldInstance cannot be null.");
    _content = content ?? throw new ArgumentNullException(nameof(content), "ContentManagerContainer cannot be null.");
    _service = service ?? throw new ArgumentNullException(nameof(service), "ServiceProvider cannot be null.");
  }

  /// <inheritdoc />
  public Field AsField()
  {
    var type = _field.Type.Split('.').First();
    switch (type)
    {
      case "Integer":
        return new IntegerField(_field.Identifier, (int)_field.Value);
      case "Float":
        return new FloatField(_field.Identifier, (float)_field.Value);
      case "Boolean":
        return new BooleanField(_field.Identifier, (bool)_field.Value);
      case "String":
        return new StringField(_field.Identifier, (string)_field.Value);
      case "Multilines":
        return new MultilinesField(_field.Identifier, ((string)_field.Value).Split('\n'));
      case "Color":
        return new ColorField(_field.Identifier, ((string)_field.Value).FromRGBAHex());
      case "LocalEnum":
        return AsEnumField();
      case "FilePath":
        return new FilePathField(_field.Identifier, (string)_field.Value);
      case "Tile":
        return AsTileField();
      case "EntityRef":
        return AsEntityRefField();
      case "Point":
        var value = (GridPoint)_field.Value;
        return new PointField(_field.Identifier, new Point((int)value.Cx, (int)value.Cy));
    }
    throw new NotSupportedException($"Field type '{_field.Type}' is not supported.");
  }

  /// <summary>
  /// Converts the field instance to an EnumField.
  /// </summary>
  /// <returns>Will return the enumeration field</returns>
  private EnumField AsEnumField()
  {
    var value = (string)_field.Value;
    var enumName = _field.Type.Split('.').Last();
    var definition = _object.Defs.Enums.FirstOrDefault(x => x.Identifier == enumName);

    if (definition == null)
      throw new InvalidOperationException($"Enum '{enumName}' not found in LDtk definitions.");

    var enumValue = definition.Values.FirstOrDefault(x => x.Id == value);
    if (enumValue == null)
      throw new InvalidOperationException($"Enum value '{value}' not found in enum '{enumName}'.");

    ITexture2DContainer? texture = null;
    Rectangle? bounds = null;
    if (enumValue.TileRect != null)
    {
      var tileset = _object.Defs.Tilesets.FirstOrDefault(x => x.Uid == enumValue.TileRect.TilesetUid);
      if (tileset == null)
        throw new InvalidOperationException($"Tileset with UID {enumValue.TileRect.TilesetUid} not found in LDtk definitions.");

      var path = tileset.RelPath.Split('.').First();
      texture = _content.Load<ITexture2DContainer>(path);
      bounds = new Rectangle((int)enumValue.TileRect.X, (int)enumValue.TileRect.Y, (int)enumValue.TileRect.W, (int)enumValue.TileRect.H);
    }

    return new EnumField(_field.Identifier, enumName, texture, bounds);
  }

  /// <summary>
  /// Coverts to a tile field
  /// </summary>
  /// <returns>Will return a tile filed</returns>
  private TileField AsTileField()
  {
    var value = (TilesetRectangle)_field.Value;

    var tileset = _object.Defs.Tilesets.FirstOrDefault(x => x.Uid == value.TilesetUid);
    if (tileset == null)
      throw new InvalidOperationException($"Tileset with UID {value.TilesetUid} not found in LDtk definitions.");

    var path = tileset.RelPath.Split('.').First();
    var texture = _content.Load<ITexture2DContainer>(path);
    var bounds = new Rectangle((int)value.X, (int)value.Y, (int)value.W, (int)value.H);

    return new TileField(_field.Identifier, texture, bounds);
  }

  /// <summary>
  /// Converts the field instance to an EntityRefField.
  /// </summary>
  /// <returns>Will return an entity reference field</returns>
  private EntityRefField AsEntityRefField()
  {
    var entityRefValue = (ReferenceToAnEntityInstance)_field.Value;
    var worldInstance = _object.Worlds.FirstOrDefault(x => x.Iid == entityRefValue.WorldIid);
    var levelInstance = worldInstance?.Levels.FirstOrDefault(x => x.Iid == entityRefValue.LayerIid);
    var layerInstance = levelInstance?.LayerInstances.FirstOrDefault(x => x.Iid == entityRefValue.LayerIid);
    var entityInstance = layerInstance?.EntityInstances.FirstOrDefault(x => x.Iid == entityRefValue.EntityIid);

    var world = worldInstance == null ? null : ActivatorUtilities.CreateInstance<LDtkWorld>(_service, worldInstance);
    var level = levelInstance == null || worldInstance == null ? null : ActivatorUtilities.CreateInstance<LDtkLevel>(_service, worldInstance, levelInstance);
    var layer = levelInstance == null || worldInstance == null || layerInstance == null ? null : ActivatorUtilities.CreateInstance<LDtkLayerInstance>(_service, worldInstance, levelInstance, layerInstance);
    var entity = levelInstance == null || worldInstance == null || layerInstance == null || entityInstance == null ? null : ActivatorUtilities.CreateInstance<LDtkEntityInstance>(_service, worldInstance, levelInstance, layerInstance, entityInstance);
    

    return new EntityRefField(_field.Identifier, world, level, layer, entity);
  }
}
