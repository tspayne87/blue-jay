﻿namespace BlueJay.LDtk
{
  using System;
  using System.Collections.Generic;

  using System.Text.Json;
  using System.Text.Json.Serialization;
  using System.Globalization;

  /// <summary>
  /// This file is a JSON schema of files created by LDtk level editor (https://ldtk.io).
  ///
  /// This is the root of any Project JSON file. It contains:  - the project settings, - an
  /// array of levels, - a group of definitions (that can probably be safely ignored for most
  /// users).
  /// </summary>
  public class LDtkObject
  {
    /// <summary>
    /// 
    /// This object is not actually used by LDtk. It ONLY exists to force explicit references to
    /// all types, to make sure QuickType finds them and integrate all of them. Otherwise,
    /// Quicktype will drop types that are not explicitely used.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("__FORCED_REFS")]
    public ForcedRefs ForcedRefs { get; set; }

    /// <summary>
    /// LDtk application build identifier.<br/>  This is only used to identify the LDtk version
    /// that generated this particular project file, which can be useful for specific bug fixing.
    /// Note that the build identifier is just the date of the release, so it's not unique to
    /// each user (one single global ID per LDtk public release), and as a result, completely
    /// anonymous.
    /// </summary>
    [JsonPropertyName("appBuildId")]
    public double AppBuildId { get; set; }

    /// <summary>
    /// Number of backup files to keep, if the `backupOnSave` is TRUE
    /// </summary>
    [JsonPropertyName("backupLimit")]
    public long BackupLimit { get; set; }

    /// <summary>
    /// If TRUE, an extra copy of the project will be created in a sub folder, when saving.
    /// </summary>
    [JsonPropertyName("backupOnSave")]
    public bool BackupOnSave { get; set; }

    /// <summary>
    /// Target relative path to store backup files
    /// </summary>
    [JsonPropertyName("backupRelPath")]
    public string BackupRelPath { get; set; }

    /// <summary>
    /// Project background color
    /// </summary>
    [JsonPropertyName("bgColor")]
    public string BgColor { get; set; }

    /// <summary>
    /// An array of command lines that can be ran manually by the user
    /// </summary>
    [JsonPropertyName("customCommands")]
    public LdtkCustomCommand[] CustomCommands { get; set; }

    /// <summary>
    /// Default height for new entities
    /// </summary>
    [JsonPropertyName("defaultEntityHeight")]
    public long DefaultEntityHeight { get; set; }

    /// <summary>
    /// Default width for new entities
    /// </summary>
    [JsonPropertyName("defaultEntityWidth")]
    public long DefaultEntityWidth { get; set; }

    /// <summary>
    /// Default grid size for new layers
    /// </summary>
    [JsonPropertyName("defaultGridSize")]
    public long DefaultGridSize { get; set; }

    /// <summary>
    /// Default background color of levels
    /// </summary>
    [JsonPropertyName("defaultLevelBgColor")]
    public string DefaultLevelBgColor { get; set; }

    /// <summary>
    /// **WARNING**: this field will move to the `worlds` array after the "multi-worlds" update.
    /// It will then be `null`. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/><br/>  Default new level height
    /// </summary>
    [JsonPropertyName("defaultLevelHeight")]
    public long? DefaultLevelHeight { get; set; }

    /// <summary>
    /// **WARNING**: this field will move to the `worlds` array after the "multi-worlds" update.
    /// It will then be `null`. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/><br/>  Default new level width
    /// </summary>
    [JsonPropertyName("defaultLevelWidth")]
    public long? DefaultLevelWidth { get; set; }

    /// <summary>
    /// Default X pivot (0 to 1) for new entities
    /// </summary>
    [JsonPropertyName("defaultPivotX")]
    public double DefaultPivotX { get; set; }

    /// <summary>
    /// Default Y pivot (0 to 1) for new entities
    /// </summary>
    [JsonPropertyName("defaultPivotY")]
    public double DefaultPivotY { get; set; }

    /// <summary>
    /// A structure containing all the definitions of this project
    /// </summary>
    [JsonPropertyName("defs")]
    public Definitions Defs { get; set; }

    /// <summary>
    /// If the project isn't in MultiWorlds mode, this is the IID of the internal "dummy" World.
    /// </summary>
    [JsonPropertyName("dummyWorldIid")]
    public string DummyWorldIid { get; set; }

    /// <summary>
    /// If TRUE, the exported PNGs will include the level background (color or image).
    /// </summary>
    [JsonPropertyName("exportLevelBg")]
    public bool ExportLevelBg { get; set; }

    /// <summary>
    /// **WARNING**: this deprecated value is no longer exported since version 0.9.3  Replaced
    /// by: `imageExportMode`
    /// </summary>
    [JsonPropertyName("exportPng")]
    public bool? ExportPng { get; set; }

    /// <summary>
    /// If TRUE, a Tiled compatible file will also be generated along with the LDtk JSON file
    /// (default is FALSE)
    /// </summary>
    [JsonPropertyName("exportTiled")]
    public bool ExportTiled { get; set; }

    /// <summary>
    /// If TRUE, one file will be saved for the project (incl. all its definitions) and one file
    /// in a sub-folder for each level.
    /// </summary>
    [JsonPropertyName("externalLevels")]
    public bool ExternalLevels { get; set; }

    /// <summary>
    /// An array containing various advanced flags (ie. options or other states). Possible
    /// values: `DiscardPreCsvIntGrid`, `ExportOldTableOfContentData`,
    /// `ExportPreCsvIntGridFormat`, `IgnoreBackupSuggest`, `PrependIndexToLevelFileNames`,
    /// `MultiWorlds`, `UseMultilinesType`
    /// </summary>
    [JsonPropertyName("flags")]
    public Flag[] Flags { get; set; }

    /// <summary>
    /// Naming convention for Identifiers (first-letter uppercase, full uppercase etc.) Possible
    /// values: `Capitalize`, `Uppercase`, `Lowercase`, `Free`
    /// </summary>
    [JsonPropertyName("identifierStyle")]
    public IdentifierStyle IdentifierStyle { get; set; }

    /// <summary>
    /// Unique project identifier
    /// </summary>
    [JsonPropertyName("iid")]
    public string Iid { get; set; }

    /// <summary>
    /// "Image export" option when saving project. Possible values: `None`, `OneImagePerLayer`,
    /// `OneImagePerLevel`, `LayersAndLevels`
    /// </summary>
    [JsonPropertyName("imageExportMode")]
    public ImageExportMode ImageExportMode { get; set; }

    /// <summary>
    /// File format version
    /// </summary>
    [JsonPropertyName("jsonVersion")]
    public string JsonVersion { get; set; }

    /// <summary>
    /// The default naming convention for level identifiers.
    /// </summary>
    [JsonPropertyName("levelNamePattern")]
    public string LevelNamePattern { get; set; }

    /// <summary>
    /// All levels. The order of this array is only relevant in `LinearHorizontal` and
    /// `linearVertical` world layouts (see `worldLayout` value).<br/>  Otherwise, you should
    /// refer to the `worldX`,`worldY` coordinates of each Level.
    /// </summary>
    [JsonPropertyName("levels")]
    public Level[] Levels { get; set; }

    /// <summary>
    /// If TRUE, the Json is partially minified (no indentation, nor line breaks, default is
    /// FALSE)
    /// </summary>
    [JsonPropertyName("minifyJson")]
    public bool MinifyJson { get; set; }

    /// <summary>
    /// Next Unique integer ID available
    /// </summary>
    [JsonPropertyName("nextUid")]
    public long NextUid { get; set; }

    /// <summary>
    /// File naming pattern for exported PNGs
    /// </summary>
    [JsonPropertyName("pngFilePattern")]
    public string PngFilePattern { get; set; }

    /// <summary>
    /// If TRUE, a very simplified will be generated on saving, for quicker & easier engine
    /// integration.
    /// </summary>
    [JsonPropertyName("simplifiedExport")]
    public bool SimplifiedExport { get; set; }

    /// <summary>
    /// All instances of entities that have their `exportToToc` flag enabled are listed in this
    /// array.
    /// </summary>
    [JsonPropertyName("toc")]
    public LdtkTableOfContentEntry[] Toc { get; set; }

    /// <summary>
    /// This optional description is used by LDtk Samples to show up some informations and
    /// instructions.
    /// </summary>
    [JsonPropertyName("tutorialDesc")]
    public string TutorialDesc { get; set; }

    /// <summary>
    /// **WARNING**: this field will move to the `worlds` array after the "multi-worlds" update.
    /// It will then be `null`. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/><br/>  Height of the world grid in pixels.
    /// </summary>
    [JsonPropertyName("worldGridHeight")]
    public long? WorldGridHeight { get; set; }

    /// <summary>
    /// **WARNING**: this field will move to the `worlds` array after the "multi-worlds" update.
    /// It will then be `null`. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/><br/>  Width of the world grid in pixels.
    /// </summary>
    [JsonPropertyName("worldGridWidth")]
    public long? WorldGridWidth { get; set; }

    /// <summary>
    /// **WARNING**: this field will move to the `worlds` array after the "multi-worlds" update.
    /// It will then be `null`. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/><br/>  An enum that describes how levels are organized in
    /// this project (ie. linearly or in a 2D space). Possible values: &lt;`null`&gt;, `Free`,
    /// `GridVania`, `LinearHorizontal`, `LinearVertical`
    /// </summary>
    [JsonPropertyName("worldLayout")]
    public WorldLayout? WorldLayout { get; set; }

    /// <summary>
    /// This array will be empty, unless you enable the Multi-Worlds in the project advanced
    /// settings.<br/><br/> - in current version, a LDtk project file can only contain a single
    /// world with multiple levels in it. In this case, levels and world layout related settings
    /// are stored in the root of the JSON.<br/> - with "Multi-worlds" enabled, there will be a
    /// `worlds` array in root, each world containing levels and layout settings. Basically, it's
    /// pretty much only about moving the `levels` array to the `worlds` array, along with world
    /// layout related values (eg. `worldGridWidth` etc).<br/><br/>If you want to start
    /// supporting this future update easily, please refer to this documentation:
    /// https://github.com/deepnight/ldtk/issues/231
    /// </summary>
    [JsonPropertyName("worlds")]
    public World[] Worlds { get; set; }
  }

  public class LdtkCustomCommand
  {
    [JsonPropertyName("command")]
    public string Command { get; set; }

    /// <summary>
    /// Possible values: `Manual`, `AfterLoad`, `BeforeSave`, `AfterSave`
    /// </summary>
    [JsonPropertyName("when")]
    public When When { get; set; }
  }

  /// <summary>
  /// If you're writing your own LDtk importer, you should probably just ignore *most* stuff in
  /// the `defs` section, as it contains data that are mostly important to the editor. To keep
  /// you away from the `defs` section and avoid some unnecessary JSON parsing, important data
  /// from definitions is often duplicated in fields prefixed with a double underscore (eg.
  /// `__identifier` or `__type`).  The 2 only definition types you might need here are
  /// **Tilesets** and **Enums**.
  ///
  /// A structure containing all the definitions of this project
  /// </summary>
  public class Definitions
  {
    /// <summary>
    /// All entities definitions, including their custom fields
    /// </summary>
    [JsonPropertyName("entities")]
    public EntityDefinition[] Entities { get; set; }

    /// <summary>
    /// All internal enums
    /// </summary>
    [JsonPropertyName("enums")]
    public EnumDefinition[] Enums { get; set; }

    /// <summary>
    /// Note: external enums are exactly the same as `enums`, except they have a `relPath` to
    /// point to an external source file.
    /// </summary>
    [JsonPropertyName("externalEnums")]
    public EnumDefinition[] ExternalEnums { get; set; }

    /// <summary>
    /// All layer definitions
    /// </summary>
    [JsonPropertyName("layers")]
    public LayerDefinition[] Layers { get; set; }

    /// <summary>
    /// All custom fields available to all levels.
    /// </summary>
    [JsonPropertyName("levelFields")]
    public FieldDefinition[] LevelFields { get; set; }

    /// <summary>
    /// All tilesets
    /// </summary>
    [JsonPropertyName("tilesets")]
    public TilesetDefinition[] Tilesets { get; set; }
  }

  public class EntityDefinition
  {
    /// <summary>
    /// If enabled, this entity is allowed to stay outside of the current level bounds
    /// </summary>
    [JsonPropertyName("allowOutOfBounds")]
    public bool AllowOutOfBounds { get; set; }

    /// <summary>
    /// Base entity color
    /// </summary>
    [JsonPropertyName("color")]
    public string Color { get; set; }

    /// <summary>
    /// User defined documentation for this element to provide help/tips to level designers.
    /// </summary>
    [JsonPropertyName("doc")]
    public string Doc { get; set; }

    /// <summary>
    /// If enabled, all instances of this entity will be listed in the project "Table of content"
    /// object.
    /// </summary>
    [JsonPropertyName("exportToToc")]
    public bool ExportToToc { get; set; }

    /// <summary>
    /// Array of field definitions
    /// </summary>
    [JsonPropertyName("fieldDefs")]
    public FieldDefinition[] FieldDefs { get; set; }

    [JsonPropertyName("fillOpacity")]
    public double FillOpacity { get; set; }

    /// <summary>
    /// Pixel height
    /// </summary>
    [JsonPropertyName("height")]
    public long Height { get; set; }

    [JsonPropertyName("hollow")]
    public bool Hollow { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Only applies to entities resizable on both X/Y. If TRUE, the entity instance width/height
    /// will keep the same aspect ratio as the definition.
    /// </summary>
    [JsonPropertyName("keepAspectRatio")]
    public bool KeepAspectRatio { get; set; }

    /// <summary>
    /// Possible values: `DiscardOldOnes`, `PreventAdding`, `MoveLastOne`
    /// </summary>
    [JsonPropertyName("limitBehavior")]
    public LimitBehavior LimitBehavior { get; set; }

    /// <summary>
    /// If TRUE, the maxCount is a "per world" limit, if FALSE, it's a "per level". Possible
    /// values: `PerLayer`, `PerLevel`, `PerWorld`
    /// </summary>
    [JsonPropertyName("limitScope")]
    public LimitScope LimitScope { get; set; }

    [JsonPropertyName("lineOpacity")]
    public double LineOpacity { get; set; }

    /// <summary>
    /// Max instances count
    /// </summary>
    [JsonPropertyName("maxCount")]
    public long MaxCount { get; set; }

    /// <summary>
    /// Max pixel height (only applies if the entity is resizable on Y)
    /// </summary>
    [JsonPropertyName("maxHeight")]
    public long? MaxHeight { get; set; }

    /// <summary>
    /// Max pixel width (only applies if the entity is resizable on X)
    /// </summary>
    [JsonPropertyName("maxWidth")]
    public long? MaxWidth { get; set; }

    /// <summary>
    /// Min pixel height (only applies if the entity is resizable on Y)
    /// </summary>
    [JsonPropertyName("minHeight")]
    public long? MinHeight { get; set; }

    /// <summary>
    /// Min pixel width (only applies if the entity is resizable on X)
    /// </summary>
    [JsonPropertyName("minWidth")]
    public long? MinWidth { get; set; }

    /// <summary>
    /// An array of 4 dimensions for the up/right/down/left borders (in this order) when using
    /// 9-slice mode for `tileRenderMode`.<br/>  If the tileRenderMode is not NineSlice, then
    /// this array is empty.<br/>  See: https://en.wikipedia.org/wiki/9-slice_scaling
    /// </summary>
    [JsonPropertyName("nineSliceBorders")]
    public long[] NineSliceBorders { get; set; }

    /// <summary>
    /// Pivot X coordinate (from 0 to 1.0)
    /// </summary>
    [JsonPropertyName("pivotX")]
    public double PivotX { get; set; }

    /// <summary>
    /// Pivot Y coordinate (from 0 to 1.0)
    /// </summary>
    [JsonPropertyName("pivotY")]
    public double PivotY { get; set; }

    /// <summary>
    /// Possible values: `Rectangle`, `Ellipse`, `Tile`, `Cross`
    /// </summary>
    [JsonPropertyName("renderMode")]
    public RenderMode RenderMode { get; set; }

    /// <summary>
    /// If TRUE, the entity instances will be resizable horizontally
    /// </summary>
    [JsonPropertyName("resizableX")]
    public bool ResizableX { get; set; }

    /// <summary>
    /// If TRUE, the entity instances will be resizable vertically
    /// </summary>
    [JsonPropertyName("resizableY")]
    public bool ResizableY { get; set; }

    /// <summary>
    /// Display entity name in editor
    /// </summary>
    [JsonPropertyName("showName")]
    public bool ShowName { get; set; }

    /// <summary>
    /// An array of strings that classifies this entity
    /// </summary>
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }

    /// <summary>
    /// **WARNING**: this deprecated value is no longer exported since version 1.2.0  Replaced
    /// by: `tileRect`
    /// </summary>
    [JsonPropertyName("tileId")]
    public long? TileId { get; set; }

    [JsonPropertyName("tileOpacity")]
    public double TileOpacity { get; set; }

    /// <summary>
    /// An object representing a rectangle from an existing Tileset
    /// </summary>
    [JsonPropertyName("tileRect")]
    public TilesetRectangle TileRect { get; set; }

    /// <summary>
    /// An enum describing how the the Entity tile is rendered inside the Entity bounds. Possible
    /// values: `Cover`, `FitInside`, `Repeat`, `Stretch`, `FullSizeCropped`,
    /// `FullSizeUncropped`, `NineSlice`
    /// </summary>
    [JsonPropertyName("tileRenderMode")]
    public TileRenderMode TileRenderMode { get; set; }

    /// <summary>
    /// Tileset ID used for optional tile display
    /// </summary>
    [JsonPropertyName("tilesetId")]
    public long? TilesetId { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }

    /// <summary>
    /// This tile overrides the one defined in `tileRect` in the UI
    /// </summary>
    [JsonPropertyName("uiTileRect")]
    public TilesetRectangle UiTileRect { get; set; }

    /// <summary>
    /// Pixel width
    /// </summary>
    [JsonPropertyName("width")]
    public long Width { get; set; }
  }

  /// <summary>
  /// This section is mostly only intended for the LDtk editor app itself. You can safely
  /// ignore it.
  /// </summary>
  public class FieldDefinition
  {
    /// <summary>
    /// Human readable value type. Possible values: `Int, Float, String, Bool, Color,
    /// ExternEnum.XXX, LocalEnum.XXX, Point, FilePath`.<br/>  If the field is an array, this
    /// field will look like `Array<...>` (eg. `Array<Int>`, `Array<Point>` etc.)<br/>  NOTE: if
    /// you enable the advanced option **Use Multilines type**, you will have "*Multilines*"
    /// instead of "*String*" when relevant.
    /// </summary>
    [JsonPropertyName("__type")]
    public string Type { get; set; }

    /// <summary>
    /// Optional list of accepted file extensions for FilePath value type. Includes the dot:
    /// `.ext`
    /// </summary>
    [JsonPropertyName("acceptFileTypes")]
    public string[] AcceptFileTypes { get; set; }

    /// <summary>
    /// Possible values: `Any`, `OnlySame`, `OnlyTags`, `OnlySpecificEntity`
    /// </summary>
    [JsonPropertyName("allowedRefs")]
    public AllowedRefs AllowedRefs { get; set; }

    [JsonPropertyName("allowedRefsEntityUid")]
    public long? AllowedRefsEntityUid { get; set; }

    [JsonPropertyName("allowedRefTags")]
    public string[] AllowedRefTags { get; set; }

    [JsonPropertyName("allowOutOfLevelRef")]
    public bool AllowOutOfLevelRef { get; set; }

    /// <summary>
    /// Array max length
    /// </summary>
    [JsonPropertyName("arrayMaxLength")]
    public long? ArrayMaxLength { get; set; }

    /// <summary>
    /// Array min length
    /// </summary>
    [JsonPropertyName("arrayMinLength")]
    public long? ArrayMinLength { get; set; }

    [JsonPropertyName("autoChainRef")]
    public bool AutoChainRef { get; set; }

    /// <summary>
    /// TRUE if the value can be null. For arrays, TRUE means it can contain null values
    /// (exception: array of Points can't have null values).
    /// </summary>
    [JsonPropertyName("canBeNull")]
    public bool CanBeNull { get; set; }

    /// <summary>
    /// Default value if selected value is null or invalid.
    /// </summary>
    [JsonPropertyName("defaultOverride")]
    public object DefaultOverride { get; set; }

    /// <summary>
    /// User defined documentation for this field to provide help/tips to level designers about
    /// accepted values.
    /// </summary>
    [JsonPropertyName("doc")]
    public string Doc { get; set; }

    [JsonPropertyName("editorAlwaysShow")]
    public bool EditorAlwaysShow { get; set; }

    [JsonPropertyName("editorCutLongValues")]
    public bool EditorCutLongValues { get; set; }

    [JsonPropertyName("editorDisplayColor")]
    public string EditorDisplayColor { get; set; }

    /// <summary>
    /// Possible values: `Hidden`, `ValueOnly`, `NameAndValue`, `EntityTile`, `LevelTile`,
    /// `Points`, `PointStar`, `PointPath`, `PointPathLoop`, `RadiusPx`, `RadiusGrid`,
    /// `ArrayCountWithLabel`, `ArrayCountNoLabel`, `RefLinkBetweenPivots`,
    /// `RefLinkBetweenCenters`
    /// </summary>
    [JsonPropertyName("editorDisplayMode")]
    public EditorDisplayMode EditorDisplayMode { get; set; }

    /// <summary>
    /// Possible values: `Above`, `Center`, `Beneath`
    /// </summary>
    [JsonPropertyName("editorDisplayPos")]
    public EditorDisplayPos EditorDisplayPos { get; set; }

    [JsonPropertyName("editorDisplayScale")]
    public double EditorDisplayScale { get; set; }

    /// <summary>
    /// Possible values: `ZigZag`, `StraightArrow`, `CurvedArrow`, `ArrowsLine`, `DashedLine`
    /// </summary>
    [JsonPropertyName("editorLinkStyle")]
    public EditorLinkStyle EditorLinkStyle { get; set; }

    [JsonPropertyName("editorShowInWorld")]
    public bool EditorShowInWorld { get; set; }

    [JsonPropertyName("editorTextPrefix")]
    public string EditorTextPrefix { get; set; }

    [JsonPropertyName("editorTextSuffix")]
    public string EditorTextSuffix { get; set; }

    /// <summary>
    /// If TRUE, the field value will be exported to the `toc` project JSON field. Only applies
    /// to Entity fields.
    /// </summary>
    [JsonPropertyName("exportToToc")]
    public bool ExportToToc { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// TRUE if the value is an array of multiple values
    /// </summary>
    [JsonPropertyName("isArray")]
    public bool IsArray { get; set; }

    /// <summary>
    /// Max limit for value, if applicable
    /// </summary>
    [JsonPropertyName("max")]
    public double? Max { get; set; }

    /// <summary>
    /// Min limit for value, if applicable
    /// </summary>
    [JsonPropertyName("min")]
    public double? Min { get; set; }

    /// <summary>
    /// Optional regular expression that needs to be matched to accept values. Expected format:
    /// `/some_reg_ex/g`, with optional "i" flag.
    /// </summary>
    [JsonPropertyName("regex")]
    public string Regex { get; set; }

    /// <summary>
    /// If enabled, this field will be searchable through LDtk command palette
    /// </summary>
    [JsonPropertyName("searchable")]
    public bool Searchable { get; set; }

    [JsonPropertyName("symmetricalRef")]
    public bool SymmetricalRef { get; set; }

    /// <summary>
    /// Possible values: &lt;`null`&gt;, `LangPython`, `LangRuby`, `LangJS`, `LangLua`, `LangC`,
    /// `LangHaxe`, `LangMarkdown`, `LangJson`, `LangXml`, `LangLog`
    /// </summary>
    [JsonPropertyName("textLanguageMode")]
    public TextLanguageMode? TextLanguageMode { get; set; }

    /// <summary>
    /// UID of the tileset used for a Tile
    /// </summary>
    [JsonPropertyName("tilesetUid")]
    public long? TilesetUid { get; set; }

    /// <summary>
    /// Internal enum representing the possible field types. Possible values: F_Int, F_Float,
    /// F_String, F_Text, F_Bool, F_Color, F_Enum(...), F_Point, F_Path, F_EntityRef, F_Tile
    /// </summary>
    [JsonPropertyName("type")]
    public string FieldDefinitionType { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }

    /// <summary>
    /// If TRUE, the color associated with this field will override the Entity or Level default
    /// color in the editor UI. For Enum fields, this would be the color associated to their
    /// values.
    /// </summary>
    [JsonPropertyName("useForSmartColor")]
    public bool UseForSmartColor { get; set; }
  }

  /// <summary>
  /// This object represents a custom sub rectangle in a Tileset image.
  /// </summary>
  public class TilesetRectangle
  {
    /// <summary>
    /// Height in pixels
    /// </summary>
    [JsonPropertyName("h")]
    public long H { get; set; }

    /// <summary>
    /// UID of the tileset
    /// </summary>
    [JsonPropertyName("tilesetUid")]
    public long TilesetUid { get; set; }

    /// <summary>
    /// Width in pixels
    /// </summary>
    [JsonPropertyName("w")]
    public long W { get; set; }

    /// <summary>
    /// X pixels coordinate of the top-left corner in the Tileset image
    /// </summary>
    [JsonPropertyName("x")]
    public long X { get; set; }

    /// <summary>
    /// Y pixels coordinate of the top-left corner in the Tileset image
    /// </summary>
    [JsonPropertyName("y")]
    public long Y { get; set; }
  }

  public class EnumDefinition
  {
    [JsonPropertyName("externalFileChecksum")]
    public string ExternalFileChecksum { get; set; }

    /// <summary>
    /// Relative path to the external file providing this Enum
    /// </summary>
    [JsonPropertyName("externalRelPath")]
    public string ExternalRelPath { get; set; }

    /// <summary>
    /// Tileset UID if provided
    /// </summary>
    [JsonPropertyName("iconTilesetUid")]
    public long? IconTilesetUid { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// An array of user-defined tags to organize the Enums
    /// </summary>
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }

    /// <summary>
    /// All possible enum values, with their optional Tile infos.
    /// </summary>
    [JsonPropertyName("values")]
    public EnumValueDefinition[] Values { get; set; }
  }

  public class EnumValueDefinition
  {
    /// <summary>
    /// **WARNING**: this deprecated value is no longer exported since version 1.4.0  Replaced
    /// by: `tileRect`
    /// </summary>
    [JsonPropertyName("__tileSrcRect")]
    public long[] TileSrcRect { get; set; }

    /// <summary>
    /// Optional color
    /// </summary>
    [JsonPropertyName("color")]
    public long Color { get; set; }

    /// <summary>
    /// Enum value
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// **WARNING**: this deprecated value is no longer exported since version 1.4.0  Replaced
    /// by: `tileRect`
    /// </summary>
    [JsonPropertyName("tileId")]
    public long? TileId { get; set; }

    /// <summary>
    /// Optional tileset rectangle to represents this value
    /// </summary>
    [JsonPropertyName("tileRect")]
    public TilesetRectangle TileRect { get; set; }
  }

  public class LayerDefinition
  {
    /// <summary>
    /// Type of the layer (*IntGrid, Entities, Tiles or AutoLayer*)
    /// </summary>
    [JsonPropertyName("__type")]
    public string Type { get; set; }

    /// <summary>
    /// Contains all the auto-layer rule definitions.
    /// </summary>
    [JsonPropertyName("autoRuleGroups")]
    public AutoLayerRuleGroup[] AutoRuleGroups { get; set; }

    [JsonPropertyName("autoSourceLayerDefUid")]
    public long? AutoSourceLayerDefUid { get; set; }

    /// <summary>
    /// **WARNING**: this deprecated value is no longer exported since version 1.2.0  Replaced
    /// by: `tilesetDefUid`
    /// </summary>
    [JsonPropertyName("autoTilesetDefUid")]
    public long? AutoTilesetDefUid { get; set; }

    [JsonPropertyName("autoTilesKilledByOtherLayerUid")]
    public long? AutoTilesKilledByOtherLayerUid { get; set; }

    [JsonPropertyName("biomeFieldUid")]
    public long? BiomeFieldUid { get; set; }

    /// <summary>
    /// Allow editor selections when the layer is not currently active.
    /// </summary>
    [JsonPropertyName("canSelectWhenInactive")]
    public bool CanSelectWhenInactive { get; set; }

    /// <summary>
    /// Opacity of the layer (0 to 1.0)
    /// </summary>
    [JsonPropertyName("displayOpacity")]
    public double DisplayOpacity { get; set; }

    /// <summary>
    /// User defined documentation for this element to provide help/tips to level designers.
    /// </summary>
    [JsonPropertyName("doc")]
    public string Doc { get; set; }

    /// <summary>
    /// An array of tags to forbid some Entities in this layer
    /// </summary>
    [JsonPropertyName("excludedTags")]
    public string[] ExcludedTags { get; set; }

    /// <summary>
    /// Width and height of the grid in pixels
    /// </summary>
    [JsonPropertyName("gridSize")]
    public long GridSize { get; set; }

    /// <summary>
    /// Height of the optional "guide" grid in pixels
    /// </summary>
    [JsonPropertyName("guideGridHei")]
    public long GuideGridHei { get; set; }

    /// <summary>
    /// Width of the optional "guide" grid in pixels
    /// </summary>
    [JsonPropertyName("guideGridWid")]
    public long GuideGridWid { get; set; }

    [JsonPropertyName("hideFieldsWhenInactive")]
    public bool HideFieldsWhenInactive { get; set; }

    /// <summary>
    /// Hide the layer from the list on the side of the editor view.
    /// </summary>
    [JsonPropertyName("hideInList")]
    public bool HideInList { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Alpha of this layer when it is not the active one.
    /// </summary>
    [JsonPropertyName("inactiveOpacity")]
    public double InactiveOpacity { get; set; }

    /// <summary>
    /// An array that defines extra optional info for each IntGrid value.<br/>  WARNING: the
    /// array order is not related to actual IntGrid values! As user can re-order IntGrid values
    /// freely, you may value "2" before value "1" in this array.
    /// </summary>
    [JsonPropertyName("intGridValues")]
    public IntGridValueDefinition[] IntGridValues { get; set; }

    /// <summary>
    /// Group informations for IntGrid values
    /// </summary>
    [JsonPropertyName("intGridValuesGroups")]
    public IntGridValueGroupDefinition[] IntGridValuesGroups { get; set; }

    /// <summary>
    /// Parallax horizontal factor (from -1 to 1, defaults to 0) which affects the scrolling
    /// speed of this layer, creating a fake 3D (parallax) effect.
    /// </summary>
    [JsonPropertyName("parallaxFactorX")]
    public double ParallaxFactorX { get; set; }

    /// <summary>
    /// Parallax vertical factor (from -1 to 1, defaults to 0) which affects the scrolling speed
    /// of this layer, creating a fake 3D (parallax) effect.
    /// </summary>
    [JsonPropertyName("parallaxFactorY")]
    public double ParallaxFactorY { get; set; }

    /// <summary>
    /// If true (default), a layer with a parallax factor will also be scaled up/down accordingly.
    /// </summary>
    [JsonPropertyName("parallaxScaling")]
    public bool ParallaxScaling { get; set; }

    /// <summary>
    /// X offset of the layer, in pixels (IMPORTANT: this should be added to the `LayerInstance`
    /// optional offset)
    /// </summary>
    [JsonPropertyName("pxOffsetX")]
    public long PxOffsetX { get; set; }

    /// <summary>
    /// Y offset of the layer, in pixels (IMPORTANT: this should be added to the `LayerInstance`
    /// optional offset)
    /// </summary>
    [JsonPropertyName("pxOffsetY")]
    public long PxOffsetY { get; set; }

    /// <summary>
    /// If TRUE, the content of this layer will be used when rendering levels in a simplified way
    /// for the world view
    /// </summary>
    [JsonPropertyName("renderInWorldView")]
    public bool RenderInWorldView { get; set; }

    /// <summary>
    /// An array of tags to filter Entities that can be added to this layer
    /// </summary>
    [JsonPropertyName("requiredTags")]
    public string[] RequiredTags { get; set; }

    /// <summary>
    /// If the tiles are smaller or larger than the layer grid, the pivot value will be used to
    /// position the tile relatively its grid cell.
    /// </summary>
    [JsonPropertyName("tilePivotX")]
    public double TilePivotX { get; set; }

    /// <summary>
    /// If the tiles are smaller or larger than the layer grid, the pivot value will be used to
    /// position the tile relatively its grid cell.
    /// </summary>
    [JsonPropertyName("tilePivotY")]
    public double TilePivotY { get; set; }

    /// <summary>
    /// Reference to the default Tileset UID being used by this layer definition.<br/>
    /// **WARNING**: some layer *instances* might use a different tileset. So most of the time,
    /// you should probably use the `__tilesetDefUid` value found in layer instances.<br/>  Note:
    /// since version 1.0.0, the old `autoTilesetDefUid` was removed and merged into this value.
    /// </summary>
    [JsonPropertyName("tilesetDefUid")]
    public long? TilesetDefUid { get; set; }

    /// <summary>
    /// Type of the layer as Haxe Enum Possible values: `IntGrid`, `Entities`, `Tiles`,
    /// `AutoLayer`
    /// </summary>
    [JsonPropertyName("type")]
    public TypeEnum LayerDefinitionType { get; set; }

    /// <summary>
    /// User defined color for the UI
    /// </summary>
    [JsonPropertyName("uiColor")]
    public string UiColor { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }

    /// <summary>
    /// Display tags
    /// </summary>
    [JsonPropertyName("uiFilterTags")]
    public string[] UiFilterTags { get; set; }

    /// <summary>
    /// Asynchronous rendering option for large/complex layers
    /// </summary>
    [JsonPropertyName("useAsyncRender")]
    public bool UseAsyncRender { get; set; }
  }

  public class AutoLayerRuleGroup
  {
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("biomeRequirementMode")]
    public long BiomeRequirementMode { get; set; }

    /// <summary>
    /// *This field was removed in 1.0.0 and should no longer be used.*
    /// </summary>
    [JsonPropertyName("collapsed")]
    public bool? Collapsed { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }

    [JsonPropertyName("icon")]
    public TilesetRectangle Icon { get; set; }

    [JsonPropertyName("isOptional")]
    public bool IsOptional { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("requiredBiomeValues")]
    public string[] RequiredBiomeValues { get; set; }

    [JsonPropertyName("rules")]
    public AutoLayerRuleDefinition[] Rules { get; set; }

    [JsonPropertyName("uid")]
    public long Uid { get; set; }

    [JsonPropertyName("usesWizard")]
    public bool UsesWizard { get; set; }
  }

  /// <summary>
  /// This complex section isn't meant to be used by game devs at all, as these rules are
  /// completely resolved internally by the editor before any saving. You should just ignore
  /// this part.
  /// </summary>
  public class AutoLayerRuleDefinition
  {
    /// <summary>
    /// If FALSE, the rule effect isn't applied, and no tiles are generated.
    /// </summary>
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("alpha")]
    public double Alpha { get; set; }

    /// <summary>
    /// When TRUE, the rule will prevent other rules to be applied in the same cell if it matches
    /// (TRUE by default).
    /// </summary>
    [JsonPropertyName("breakOnMatch")]
    public bool BreakOnMatch { get; set; }

    /// <summary>
    /// Chances for this rule to be applied (0 to 1)
    /// </summary>
    [JsonPropertyName("chance")]
    public double Chance { get; set; }

    /// <summary>
    /// Checker mode Possible values: `None`, `Horizontal`, `Vertical`
    /// </summary>
    [JsonPropertyName("checker")]
    public Checker Checker { get; set; }

    /// <summary>
    /// If TRUE, allow rule to be matched by flipping its pattern horizontally
    /// </summary>
    [JsonPropertyName("flipX")]
    public bool FlipX { get; set; }

    /// <summary>
    /// If TRUE, allow rule to be matched by flipping its pattern vertically
    /// </summary>
    [JsonPropertyName("flipY")]
    public bool FlipY { get; set; }

    /// <summary>
    /// If TRUE, then the rule should be re-evaluated by the editor at one point
    /// </summary>
    [JsonPropertyName("invalidated")]
    public bool Invalidated { get; set; }

    /// <summary>
    /// Default IntGrid value when checking cells outside of level bounds
    /// </summary>
    [JsonPropertyName("outOfBoundsValue")]
    public long? OutOfBoundsValue { get; set; }

    /// <summary>
    /// Rule pattern (size x size)
    /// </summary>
    [JsonPropertyName("pattern")]
    public long[] Pattern { get; set; }

    /// <summary>
    /// If TRUE, enable Perlin filtering to only apply rule on specific random area
    /// </summary>
    [JsonPropertyName("perlinActive")]
    public bool PerlinActive { get; set; }

    [JsonPropertyName("perlinOctaves")]
    public double PerlinOctaves { get; set; }

    [JsonPropertyName("perlinScale")]
    public double PerlinScale { get; set; }

    [JsonPropertyName("perlinSeed")]
    public double PerlinSeed { get; set; }

    /// <summary>
    /// X pivot of a tile stamp (0-1)
    /// </summary>
    [JsonPropertyName("pivotX")]
    public double PivotX { get; set; }

    /// <summary>
    /// Y pivot of a tile stamp (0-1)
    /// </summary>
    [JsonPropertyName("pivotY")]
    public double PivotY { get; set; }

    /// <summary>
    /// Pattern width & height. Should only be 1,3,5 or 7.
    /// </summary>
    [JsonPropertyName("size")]
    public long Size { get; set; }

    /// <summary>
    /// **WARNING**: this deprecated value is no longer exported since version 1.5.0  Replaced
    /// by: `tileRectsIds`
    /// </summary>
    [JsonPropertyName("tileIds")]
    public long[] TileIds { get; set; }

    /// <summary>
    /// Defines how tileIds array is used Possible values: `Single`, `Stamp`
    /// </summary>
    [JsonPropertyName("tileMode")]
    public TileMode TileMode { get; set; }

    /// <summary>
    /// Max random offset for X tile pos
    /// </summary>
    [JsonPropertyName("tileRandomXMax")]
    public long TileRandomXMax { get; set; }

    /// <summary>
    /// Min random offset for X tile pos
    /// </summary>
    [JsonPropertyName("tileRandomXMin")]
    public long TileRandomXMin { get; set; }

    /// <summary>
    /// Max random offset for Y tile pos
    /// </summary>
    [JsonPropertyName("tileRandomYMax")]
    public long TileRandomYMax { get; set; }

    /// <summary>
    /// Min random offset for Y tile pos
    /// </summary>
    [JsonPropertyName("tileRandomYMin")]
    public long TileRandomYMin { get; set; }

    /// <summary>
    /// Array containing all the possible tile IDs rectangles (picked randomly).
    /// </summary>
    [JsonPropertyName("tileRectsIds")]
    public long[][] TileRectsIds { get; set; }

    /// <summary>
    /// Tile X offset
    /// </summary>
    [JsonPropertyName("tileXOffset")]
    public long TileXOffset { get; set; }

    /// <summary>
    /// Tile Y offset
    /// </summary>
    [JsonPropertyName("tileYOffset")]
    public long TileYOffset { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }

    /// <summary>
    /// X cell coord modulo
    /// </summary>
    [JsonPropertyName("xModulo")]
    public long XModulo { get; set; }

    /// <summary>
    /// X cell start offset
    /// </summary>
    [JsonPropertyName("xOffset")]
    public long XOffset { get; set; }

    /// <summary>
    /// Y cell coord modulo
    /// </summary>
    [JsonPropertyName("yModulo")]
    public long YModulo { get; set; }

    /// <summary>
    /// Y cell start offset
    /// </summary>
    [JsonPropertyName("yOffset")]
    public long YOffset { get; set; }
  }

  /// <summary>
  /// IntGrid value definition
  /// </summary>
  public class IntGridValueDefinition
  {
    [JsonPropertyName("color")]
    public string Color { get; set; }

    /// <summary>
    /// Parent group identifier (0 if none)
    /// </summary>
    [JsonPropertyName("groupUid")]
    public long GroupUid { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    [JsonPropertyName("tile")]
    public TilesetRectangle Tile { get; set; }

    /// <summary>
    /// The IntGrid value itself
    /// </summary>
    [JsonPropertyName("value")]
    public long Value { get; set; }
  }

  /// <summary>
  /// IntGrid value group definition
  /// </summary>
  public class IntGridValueGroupDefinition
  {
    /// <summary>
    /// User defined color
    /// </summary>
    [JsonPropertyName("color")]
    public string Color { get; set; }

    /// <summary>
    /// User defined string identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Group unique ID
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }
  }

  /// <summary>
  /// The `Tileset` definition is the most important part among project definitions. It
  /// contains some extra informations about each integrated tileset. If you only had to parse
  /// one definition section, that would be the one.
  /// </summary>
  public class TilesetDefinition
  {
    /// <summary>
    /// Grid-based height
    /// </summary>
    [JsonPropertyName("__cHei")]
    public long CHei { get; set; }

    /// <summary>
    /// Grid-based width
    /// </summary>
    [JsonPropertyName("__cWid")]
    public long CWid { get; set; }

    /// <summary>
    /// The following data is used internally for various optimizations. It's always synced with
    /// source image changes.
    /// </summary>
    [JsonPropertyName("cachedPixelData")]
    public Dictionary<string, object> CachedPixelData { get; set; }

    /// <summary>
    /// An array of custom tile metadata
    /// </summary>
    [JsonPropertyName("customData")]
    public TileCustomMetadata[] CustomData { get; set; }

    /// <summary>
    /// If this value is set, then it means that this atlas uses an internal LDtk atlas image
    /// instead of a loaded one. Possible values: &lt;`null`&gt;, `LdtkIcons`
    /// </summary>
    [JsonPropertyName("embedAtlas")]
    public EmbedAtlas? EmbedAtlas { get; set; }

    /// <summary>
    /// Tileset tags using Enum values specified by `tagsSourceEnumId`. This array contains 1
    /// element per Enum value, which contains an array of all Tile IDs that are tagged with it.
    /// </summary>
    [JsonPropertyName("enumTags")]
    public EnumTagValue[] EnumTags { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Distance in pixels from image borders
    /// </summary>
    [JsonPropertyName("padding")]
    public long Padding { get; set; }

    /// <summary>
    /// Image height in pixels
    /// </summary>
    [JsonPropertyName("pxHei")]
    public long PxHei { get; set; }

    /// <summary>
    /// Image width in pixels
    /// </summary>
    [JsonPropertyName("pxWid")]
    public long PxWid { get; set; }

    /// <summary>
    /// Path to the source file, relative to the current project JSON file<br/>  It can be null
    /// if no image was provided, or when using an embed atlas.
    /// </summary>
    [JsonPropertyName("relPath")]
    public string RelPath { get; set; }

    /// <summary>
    /// Array of group of tiles selections, only meant to be used in the editor
    /// </summary>
    [JsonPropertyName("savedSelections")]
    public Dictionary<string, object>[] SavedSelections { get; set; }

    /// <summary>
    /// Space in pixels between all tiles
    /// </summary>
    [JsonPropertyName("spacing")]
    public long Spacing { get; set; }

    /// <summary>
    /// An array of user-defined tags to organize the Tilesets
    /// </summary>
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }

    /// <summary>
    /// Optional Enum definition UID used for this tileset meta-data
    /// </summary>
    [JsonPropertyName("tagsSourceEnumUid")]
    public long? TagsSourceEnumUid { get; set; }

    [JsonPropertyName("tileGridSize")]
    public long TileGridSize { get; set; }

    /// <summary>
    /// Unique Intidentifier
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }
  }

  /// <summary>
  /// In a tileset definition, user defined meta-data of a tile.
  /// </summary>
  public class TileCustomMetadata
  {
    [JsonPropertyName("data")]
    public string Data { get; set; }

    [JsonPropertyName("tileId")]
    public long TileId { get; set; }
  }

  /// <summary>
  /// In a tileset definition, enum based tag infos
  /// </summary>
  public class EnumTagValue
  {
    [JsonPropertyName("enumValueId")]
    public string EnumValueId { get; set; }

    [JsonPropertyName("tileIds")]
    public long[] TileIds { get; set; }
  }

  /// <summary>
  /// This object is not actually used by LDtk. It ONLY exists to force explicit references to
  /// all types, to make sure QuickType finds them and integrate all of them. Otherwise,
  /// Quicktype will drop types that are not explicitely used.
  /// </summary>
  public class ForcedRefs
  {
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("AutoLayerRuleGroup")]
    public AutoLayerRuleGroup AutoLayerRuleGroup { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("AutoRuleDef")]
    public AutoLayerRuleDefinition AutoRuleDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("CustomCommand")]
    public LdtkCustomCommand CustomCommand { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("Definitions")]
    public Definitions Definitions { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EntityDef")]
    public EntityDefinition EntityDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EntityInstance")]
    public EntityInstance EntityInstance { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EntityReferenceInfos")]
    public ReferenceToAnEntityInstance EntityReferenceInfos { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EnumDef")]
    public EnumDefinition EnumDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EnumDefValues")]
    public EnumValueDefinition EnumDefValues { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EnumTagValue")]
    public EnumTagValue EnumTagValue { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("FieldDef")]
    public FieldDefinition FieldDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("FieldInstance")]
    public FieldInstance FieldInstance { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("GridPoint")]
    public GridPoint GridPoint { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("IntGridValueDef")]
    public IntGridValueDefinition IntGridValueDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("IntGridValueGroupDef")]
    public IntGridValueGroupDefinition IntGridValueGroupDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("IntGridValueInstance")]
    public IntGridValueInstance IntGridValueInstance { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("LayerDef")]
    public LayerDefinition LayerDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("LayerInstance")]
    public LayerInstance LayerInstance { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("Level")]
    public Level Level { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("LevelBgPosInfos")]
    public LevelBackgroundPosition LevelBgPosInfos { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("NeighbourLevel")]
    public NeighbourLevel NeighbourLevel { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("TableOfContentEntry")]
    public LdtkTableOfContentEntry TableOfContentEntry { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("Tile")]
    public TileInstance Tile { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("TileCustomMetadata")]
    public TileCustomMetadata TileCustomMetadata { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("TilesetDef")]
    public TilesetDefinition TilesetDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("TilesetRect")]
    public TilesetRectangle TilesetRect { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("TocInstanceData")]
    public LdtkTocInstanceData TocInstanceData { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("World")]
    public World World { get; set; }
  }

  public class EntityInstance
  {
    /// <summary>
    /// Grid-based coordinates (`[x,y]` format)
    /// </summary>
    [JsonPropertyName("__grid")]
    public long[] Grid { get; set; }

    /// <summary>
    /// Entity definition identifier
    /// </summary>
    [JsonPropertyName("__identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Pivot coordinates  (`[x,y]` format, values are from 0 to 1) of the Entity
    /// </summary>
    [JsonPropertyName("__pivot")]
    public double[] Pivot { get; set; }

    /// <summary>
    /// The entity "smart" color, guessed from either Entity definition, or one its field
    /// instances.
    /// </summary>
    [JsonPropertyName("__smartColor")]
    public string SmartColor { get; set; }

    /// <summary>
    /// Array of tags defined in this Entity definition
    /// </summary>
    [JsonPropertyName("__tags")]
    public string[] Tags { get; set; }

    /// <summary>
    /// Optional TilesetRect used to display this entity (it could either be the default Entity
    /// tile, or some tile provided by a field value, like an Enum).
    /// </summary>
    [JsonPropertyName("__tile")]
    public TilesetRectangle Tile { get; set; }

    /// <summary>
    /// X world coordinate in pixels. Only available in GridVania or Free world layouts.
    /// </summary>
    [JsonPropertyName("__worldX")]
    public long? WorldX { get; set; }

    /// <summary>
    /// Y world coordinate in pixels Only available in GridVania or Free world layouts.
    /// </summary>
    [JsonPropertyName("__worldY")]
    public long? WorldY { get; set; }

    /// <summary>
    /// Reference of the **Entity definition** UID
    /// </summary>
    [JsonPropertyName("defUid")]
    public long DefUid { get; set; }

    /// <summary>
    /// An array of all custom fields and their values.
    /// </summary>
    [JsonPropertyName("fieldInstances")]
    public FieldInstance[] FieldInstances { get; set; }

    /// <summary>
    /// Entity height in pixels. For non-resizable entities, it will be the same as Entity
    /// definition.
    /// </summary>
    [JsonPropertyName("height")]
    public long Height { get; set; }

    /// <summary>
    /// Unique instance identifier
    /// </summary>
    [JsonPropertyName("iid")]
    public string Iid { get; set; }

    /// <summary>
    /// Pixel coordinates (`[x,y]` format) in current level coordinate space. Don't forget
    /// optional layer offsets, if they exist!
    /// </summary>
    [JsonPropertyName("px")]
    public long[] Px { get; set; }

    /// <summary>
    /// Entity width in pixels. For non-resizable entities, it will be the same as Entity
    /// definition.
    /// </summary>
    [JsonPropertyName("width")]
    public long Width { get; set; }
  }

  public class FieldInstance
  {
    /// <summary>
    /// Field definition identifier
    /// </summary>
    [JsonPropertyName("__identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Optional TilesetRect used to display this field (this can be the field own Tile, or some
    /// other Tile guessed from the value, like an Enum).
    /// </summary>
    [JsonPropertyName("__tile")]
    public TilesetRectangle Tile { get; set; }

    /// <summary>
    /// Type of the field, such as `Int`, `Float`, `String`, `Enum(my_enum_name)`, `Bool`,
    /// etc.<br/>  NOTE: if you enable the advanced option **Use Multilines type**, you will have
    /// "*Multilines*" instead of "*String*" when relevant.
    /// </summary>
    [JsonPropertyName("__type")]
    public string Type { get; set; }

    /// <summary>
    /// Actual value of the field instance. The value type varies, depending on `__type`:<br/>
    /// - For **classic types** (ie. Integer, Float, Boolean, String, Text and FilePath), you
    /// just get the actual value with the expected type.<br/>   - For **Color**, the value is an
    /// hexadecimal string using "#rrggbb" format.<br/>   - For **Enum**, the value is a String
    /// representing the selected enum value.<br/>   - For **Point**, the value is a
    /// [GridPoint](#ldtk-GridPoint) object.<br/>   - For **Tile**, the value is a
    /// [TilesetRect](#ldtk-TilesetRect) object.<br/>   - For **EntityRef**, the value is an
    /// [EntityReferenceInfos](#ldtk-EntityReferenceInfos) object.<br/><br/>  If the field is an
    /// array, then this `__value` will also be a JSON array.
    /// </summary>
    [JsonPropertyName("__value")]
    public object Value { get; set; }

    /// <summary>
    /// Reference of the **Field definition** UID
    /// </summary>
    [JsonPropertyName("defUid")]
    public long DefUid { get; set; }

    /// <summary>
    /// Editor internal raw values
    /// </summary>
    [JsonPropertyName("realEditorValues")]
    public object[] RealEditorValues { get; set; }
  }

  /// <summary>
  /// This object describes the "location" of an Entity instance in the project worlds.
  ///
  /// IID information of this instance
  /// </summary>
  public class ReferenceToAnEntityInstance
  {
    /// <summary>
    /// IID of the refered EntityInstance
    /// </summary>
    [JsonPropertyName("entityIid")]
    public string EntityIid { get; set; }

    /// <summary>
    /// IID of the LayerInstance containing the refered EntityInstance
    /// </summary>
    [JsonPropertyName("layerIid")]
    public string LayerIid { get; set; }

    /// <summary>
    /// IID of the Level containing the refered EntityInstance
    /// </summary>
    [JsonPropertyName("levelIid")]
    public string LevelIid { get; set; }

    /// <summary>
    /// IID of the World containing the refered EntityInstance
    /// </summary>
    [JsonPropertyName("worldIid")]
    public string WorldIid { get; set; }
  }

  /// <summary>
  /// This object is just a grid-based coordinate used in Field values.
  /// </summary>
  public class GridPoint
  {
    /// <summary>
    /// X grid-based coordinate
    /// </summary>
    [JsonPropertyName("cx")]
    public long Cx { get; set; }

    /// <summary>
    /// Y grid-based coordinate
    /// </summary>
    [JsonPropertyName("cy")]
    public long Cy { get; set; }
  }

  /// <summary>
  /// IntGrid value instance
  /// </summary>
  public class IntGridValueInstance
  {
    /// <summary>
    /// Coordinate ID in the layer grid
    /// </summary>
    [JsonPropertyName("coordId")]
    public long CoordId { get; set; }

    /// <summary>
    /// IntGrid value
    /// </summary>
    [JsonPropertyName("v")]
    public long V { get; set; }
  }

  public class LayerInstance
  {
    /// <summary>
    /// Grid-based height
    /// </summary>
    [JsonPropertyName("__cHei")]
    public long CHei { get; set; }

    /// <summary>
    /// Grid-based width
    /// </summary>
    [JsonPropertyName("__cWid")]
    public long CWid { get; set; }

    /// <summary>
    /// Grid size
    /// </summary>
    [JsonPropertyName("__gridSize")]
    public long GridSize { get; set; }

    /// <summary>
    /// Layer definition identifier
    /// </summary>
    [JsonPropertyName("__identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Layer opacity as Float [0-1]
    /// </summary>
    [JsonPropertyName("__opacity")]
    public double Opacity { get; set; }

    /// <summary>
    /// Total layer X pixel offset, including both instance and definition offsets.
    /// </summary>
    [JsonPropertyName("__pxTotalOffsetX")]
    public long PxTotalOffsetX { get; set; }

    /// <summary>
    /// Total layer Y pixel offset, including both instance and definition offsets.
    /// </summary>
    [JsonPropertyName("__pxTotalOffsetY")]
    public long PxTotalOffsetY { get; set; }

    /// <summary>
    /// The definition UID of corresponding Tileset, if any.
    /// </summary>
    [JsonPropertyName("__tilesetDefUid")]
    public long? TilesetDefUid { get; set; }

    /// <summary>
    /// The relative path to corresponding Tileset, if any.
    /// </summary>
    [JsonPropertyName("__tilesetRelPath")]
    public string TilesetRelPath { get; set; }

    /// <summary>
    /// Layer type (possible values: IntGrid, Entities, Tiles or AutoLayer)
    /// </summary>
    [JsonPropertyName("__type")]
    public string Type { get; set; }

    /// <summary>
    /// An array containing all tiles generated by Auto-layer rules. The array is already sorted
    /// in display order (ie. 1st tile is beneath 2nd, which is beneath 3rd etc.).<br/><br/>
    /// Note: if multiple tiles are stacked in the same cell as the result of different rules,
    /// all tiles behind opaque ones will be discarded.
    /// </summary>
    [JsonPropertyName("autoLayerTiles")]
    public TileInstance[] AutoLayerTiles { get; set; }

    [JsonPropertyName("entityInstances")]
    public EntityInstance[] EntityInstances { get; set; }

    [JsonPropertyName("gridTiles")]
    public TileInstance[] GridTiles { get; set; }

    /// <summary>
    /// Unique layer instance identifier
    /// </summary>
    [JsonPropertyName("iid")]
    public string Iid { get; set; }

    /// <summary>
    /// **WARNING**: this deprecated value is no longer exported since version 1.0.0  Replaced
    /// by: `intGridCsv`
    /// </summary>
    [JsonPropertyName("intGrid")]
    public IntGridValueInstance[] IntGrid { get; set; }

    /// <summary>
    /// A list of all values in the IntGrid layer, stored in CSV format (Comma Separated
    /// Values).<br/>  Order is from left to right, and top to bottom (ie. first row from left to
    /// right, followed by second row, etc).<br/>  `0` means "empty cell" and IntGrid values
    /// start at 1.<br/>  The array size is `__cWid` x `__cHei` cells.
    /// </summary>
    [JsonPropertyName("intGridCsv")]
    public long[] IntGridCsv { get; set; }

    /// <summary>
    /// Reference the Layer definition UID
    /// </summary>
    [JsonPropertyName("layerDefUid")]
    public long LayerDefUid { get; set; }

    /// <summary>
    /// Reference to the UID of the level containing this layer instance
    /// </summary>
    [JsonPropertyName("levelId")]
    public long LevelId { get; set; }

    /// <summary>
    /// An Array containing the UIDs of optional rules that were enabled in this specific layer
    /// instance.
    /// </summary>
    [JsonPropertyName("optionalRules")]
    public long[] OptionalRules { get; set; }

    /// <summary>
    /// This layer can use another tileset by overriding the tileset UID here.
    /// </summary>
    [JsonPropertyName("overrideTilesetUid")]
    public long? OverrideTilesetUid { get; set; }

    /// <summary>
    /// X offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
    /// the `LayerDef` optional offset, so you should probably prefer using `__pxTotalOffsetX`
    /// which contains the total offset value)
    /// </summary>
    [JsonPropertyName("pxOffsetX")]
    public long PxOffsetX { get; set; }

    /// <summary>
    /// Y offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
    /// the `LayerDef` optional offset, so you should probably prefer using `__pxTotalOffsetX`
    /// which contains the total offset value)
    /// </summary>
    [JsonPropertyName("pxOffsetY")]
    public long PxOffsetY { get; set; }

    /// <summary>
    /// Random seed used for Auto-Layers rendering
    /// </summary>
    [JsonPropertyName("seed")]
    public long Seed { get; set; }

    /// <summary>
    /// Layer instance visibility
    /// </summary>
    [JsonPropertyName("visible")]
    public bool Visible { get; set; }
  }

  /// <summary>
  /// This structure represents a single tile from a given Tileset.
  /// </summary>
  public class TileInstance
  {
    /// <summary>
    /// Alpha/opacity of the tile (0-1, defaults to 1)
    /// </summary>
    [JsonPropertyName("a")]
    public double A { get; set; }

    /// <summary>
    /// Internal data used by the editor.<br/>  For auto-layer tiles: `[ruleId, coordId]`.<br/>
    /// For tile-layer tiles: `[coordId]`.
    /// </summary>
    [JsonPropertyName("d")]
    public long[] D { get; set; }

    /// <summary>
    /// "Flip bits", a 2-bits integer to represent the mirror transformations of the tile.<br/>
    /// - Bit 0 = X flip<br/>   - Bit 1 = Y flip<br/>   Examples: f=0 (no flip), f=1 (X flip
    /// only), f=2 (Y flip only), f=3 (both flips)
    /// </summary>
    [JsonPropertyName("f")]
    public long F { get; set; }

    /// <summary>
    /// Pixel coordinates of the tile in the **layer** (`[x,y]` format). Don't forget optional
    /// layer offsets, if they exist!
    /// </summary>
    [JsonPropertyName("px")]
    public long[] Px { get; set; }

    /// <summary>
    /// Pixel coordinates of the tile in the **tileset** (`[x,y]` format)
    /// </summary>
    [JsonPropertyName("src")]
    public long[] Src { get; set; }

    /// <summary>
    /// The *Tile ID* in the corresponding tileset.
    /// </summary>
    [JsonPropertyName("t")]
    public long T { get; set; }
  }

  /// <summary>
  /// This section contains all the level data. It can be found in 2 distinct forms, depending
  /// on Project current settings:  - If "*Separate level files*" is **disabled** (default):
  /// full level data is *embedded* inside the main Project JSON file, - If "*Separate level
  /// files*" is **enabled**: level data is stored in *separate* standalone `.ldtkl` files (one
  /// per level). In this case, the main Project JSON file will still contain most level data,
  /// except heavy sections, like the `layerInstances` array (which will be null). The
  /// `externalRelPath` string points to the `ldtkl` file.  A `ldtkl` file is just a JSON file
  /// containing exactly what is described below.
  /// </summary>
  public class Level
  {
    /// <summary>
    /// Background color of the level (same as `bgColor`, except the default value is
    /// automatically used here if its value is `null`)
    /// </summary>
    [JsonPropertyName("__bgColor")]
    public string BgColor { get; set; }

    /// <summary>
    /// Position informations of the background image, if there is one.
    /// </summary>
    [JsonPropertyName("__bgPos")]
    public LevelBackgroundPosition BgPos { get; set; }

    /// <summary>
    /// An array listing all other levels touching this one on the world map. Since 1.4.0, this
    /// includes levels that overlap in the same world layer, or in nearby world layers.<br/>
    /// Only relevant for world layouts where level spatial positioning is manual (ie. GridVania,
    /// Free). For Horizontal and Vertical layouts, this array is always empty.
    /// </summary>
    [JsonPropertyName("__neighbours")]
    public NeighbourLevel[] Neighbours { get; set; }

    /// <summary>
    /// The "guessed" color for this level in the editor, decided using either the background
    /// color or an existing custom field.
    /// </summary>
    [JsonPropertyName("__smartColor")]
    public string SmartColor { get; set; }

    /// <summary>
    /// Background color of the level. If `null`, the project `defaultLevelBgColor` should be
    /// used.
    /// </summary>
    [JsonPropertyName("bgColor")]
    public string LevelBgColor { get; set; }

    /// <summary>
    /// Background image X pivot (0-1)
    /// </summary>
    [JsonPropertyName("bgPivotX")]
    public double BgPivotX { get; set; }

    /// <summary>
    /// Background image Y pivot (0-1)
    /// </summary>
    [JsonPropertyName("bgPivotY")]
    public double BgPivotY { get; set; }

    /// <summary>
    /// An enum defining the way the background image (if any) is positioned on the level. See
    /// `__bgPos` for resulting position info. Possible values: &lt;`null`&gt;, `Unscaled`,
    /// `Contain`, `Cover`, `CoverDirty`, `Repeat`
    /// </summary>
    [JsonPropertyName("bgPos")]
    public BgPos? LevelBgPos { get; set; }

    /// <summary>
    /// The *optional* relative path to the level background image.
    /// </summary>
    [JsonPropertyName("bgRelPath")]
    public string BgRelPath { get; set; }

    /// <summary>
    /// This value is not null if the project option "*Save levels separately*" is enabled. In
    /// this case, this **relative** path points to the level Json file.
    /// </summary>
    [JsonPropertyName("externalRelPath")]
    public string ExternalRelPath { get; set; }

    /// <summary>
    /// An array containing this level custom field values.
    /// </summary>
    [JsonPropertyName("fieldInstances")]
    public FieldInstance[] FieldInstances { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Unique instance identifier
    /// </summary>
    [JsonPropertyName("iid")]
    public string Iid { get; set; }

    /// <summary>
    /// An array containing all Layer instances. **IMPORTANT**: if the project option "*Save
    /// levels separately*" is enabled, this field will be `null`.<br/>  This array is **sorted
    /// in display order**: the 1st layer is the top-most and the last is behind.
    /// </summary>
    [JsonPropertyName("layerInstances")]
    public LayerInstance[] LayerInstances { get; set; }

    /// <summary>
    /// Height of the level in pixels
    /// </summary>
    [JsonPropertyName("pxHei")]
    public long PxHei { get; set; }

    /// <summary>
    /// Width of the level in pixels
    /// </summary>
    [JsonPropertyName("pxWid")]
    public long PxWid { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }

    /// <summary>
    /// If TRUE, the level identifier will always automatically use the naming pattern as defined
    /// in `Project.levelNamePattern`. Becomes FALSE if the identifier is manually modified by
    /// user.
    /// </summary>
    [JsonPropertyName("useAutoIdentifier")]
    public bool UseAutoIdentifier { get; set; }

    /// <summary>
    /// Index that represents the "depth" of the level in the world. Default is 0, greater means
    /// "above", lower means "below".<br/>  This value is mostly used for display only and is
    /// intended to make stacking of levels easier to manage.
    /// </summary>
    [JsonPropertyName("worldDepth")]
    public long WorldDepth { get; set; }

    /// <summary>
    /// World X coordinate in pixels.<br/>  Only relevant for world layouts where level spatial
    /// positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the
    /// value is always -1 here.
    /// </summary>
    [JsonPropertyName("worldX")]
    public long WorldX { get; set; }

    /// <summary>
    /// World Y coordinate in pixels.<br/>  Only relevant for world layouts where level spatial
    /// positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the
    /// value is always -1 here.
    /// </summary>
    [JsonPropertyName("worldY")]
    public long WorldY { get; set; }
  }

  /// <summary>
  /// Level background image position info
  /// </summary>
  public class LevelBackgroundPosition
  {
    /// <summary>
    /// An array of 4 float values describing the cropped sub-rectangle of the displayed
    /// background image. This cropping happens when original is larger than the level bounds.
    /// Array format: `[ cropX, cropY, cropWidth, cropHeight ]`
    /// </summary>
    [JsonPropertyName("cropRect")]
    public double[] CropRect { get; set; }

    /// <summary>
    /// An array containing the `[scaleX,scaleY]` values of the **cropped** background image,
    /// depending on `bgPos` option.
    /// </summary>
    [JsonPropertyName("scale")]
    public double[] Scale { get; set; }

    /// <summary>
    /// An array containing the `[x,y]` pixel coordinates of the top-left corner of the
    /// **cropped** background image, depending on `bgPos` option.
    /// </summary>
    [JsonPropertyName("topLeftPx")]
    public long[] TopLeftPx { get; set; }
  }

  /// <summary>
  /// Nearby level info
  /// </summary>
  public class NeighbourLevel
  {
    /// <summary>
    /// A lowercase string tipping on the level location (`n`orth, `s`outh, `w`est,
    /// `e`ast).<br/>  Since 1.4.0, this value can also be `<` (neighbour depth is lower), `>`
    /// (neighbour depth is greater) or `o` (levels overlap and share the same world
    /// depth).<br/>  Since 1.5.3, this value can also be `nw`,`ne`,`sw` or `se` for levels only
    /// touching corners.
    /// </summary>
    [JsonPropertyName("dir")]
    public string Dir { get; set; }

    /// <summary>
    /// Neighbour Instance Identifier
    /// </summary>
    [JsonPropertyName("levelIid")]
    public string LevelIid { get; set; }

    /// <summary>
    /// **WARNING**: this deprecated value is no longer exported since version 1.2.0  Replaced
    /// by: `levelIid`
    /// </summary>
    [JsonPropertyName("levelUid")]
    public long? LevelUid { get; set; }
  }

  public class LdtkTableOfContentEntry
  {
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// **WARNING**: this deprecated value will be *removed* completely on version 1.7.0+
    /// Replaced by: `instancesData`
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("instances")]
    public ReferenceToAnEntityInstance[] Instances { get; set; }

    [JsonPropertyName("instancesData")]
    public LdtkTocInstanceData[] InstancesData { get; set; }
  }

  public class LdtkTocInstanceData
  {
    /// <summary>
    /// An object containing the values of all entity fields with the `exportToToc` option
    /// enabled. This object typing depends on actual field value types.
    /// </summary>
    [JsonPropertyName("fields")]
    public object Fields { get; set; }

    [JsonPropertyName("heiPx")]
    public long HeiPx { get; set; }

    /// <summary>
    /// IID information of this instance
    /// </summary>
    [JsonPropertyName("iids")]
    public ReferenceToAnEntityInstance Iids { get; set; }

    [JsonPropertyName("widPx")]
    public long WidPx { get; set; }

    [JsonPropertyName("worldX")]
    public long WorldX { get; set; }

    [JsonPropertyName("worldY")]
    public long WorldY { get; set; }
  }

  /// <summary>
  /// **IMPORTANT**: this type is available as a preview. You can rely on it to update your
  /// importers, for when it will be officially available.  A World contains multiple levels,
  /// and it has its own layout settings.
  /// </summary>
  public class World
  {
    /// <summary>
    /// Default new level height
    /// </summary>
    [JsonPropertyName("defaultLevelHeight")]
    public long DefaultLevelHeight { get; set; }

    /// <summary>
    /// Default new level width
    /// </summary>
    [JsonPropertyName("defaultLevelWidth")]
    public long DefaultLevelWidth { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Unique instance identifer
    /// </summary>
    [JsonPropertyName("iid")]
    public string Iid { get; set; }

    /// <summary>
    /// All levels from this world. The order of this array is only relevant in
    /// `LinearHorizontal` and `linearVertical` world layouts (see `worldLayout` value).
    /// Otherwise, you should refer to the `worldX`,`worldY` coordinates of each Level.
    /// </summary>
    [JsonPropertyName("levels")]
    public Level[] Levels { get; set; }

    /// <summary>
    /// Height of the world grid in pixels.
    /// </summary>
    [JsonPropertyName("worldGridHeight")]
    public long WorldGridHeight { get; set; }

    /// <summary>
    /// Width of the world grid in pixels.
    /// </summary>
    [JsonPropertyName("worldGridWidth")]
    public long WorldGridWidth { get; set; }

    /// <summary>
    /// An enum that describes how levels are organized in this project (ie. linearly or in a 2D
    /// space). Possible values: `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`, `null`
    /// </summary>
    [JsonPropertyName("worldLayout")]
    public WorldLayout? WorldLayout { get; set; }
  }

  /// <summary>
  /// Possible values: `Manual`, `AfterLoad`, `BeforeSave`, `AfterSave`
  /// </summary>
  public enum When { AfterLoad, AfterSave, BeforeSave, Manual };

  /// <summary>
  /// Possible values: `Any`, `OnlySame`, `OnlyTags`, `OnlySpecificEntity`
  /// </summary>
  public enum AllowedRefs { Any, OnlySame, OnlySpecificEntity, OnlyTags };

  /// <summary>
  /// Possible values: `Hidden`, `ValueOnly`, `NameAndValue`, `EntityTile`, `LevelTile`,
  /// `Points`, `PointStar`, `PointPath`, `PointPathLoop`, `RadiusPx`, `RadiusGrid`,
  /// `ArrayCountWithLabel`, `ArrayCountNoLabel`, `RefLinkBetweenPivots`,
  /// `RefLinkBetweenCenters`
  /// </summary>
  public enum EditorDisplayMode { ArrayCountNoLabel, ArrayCountWithLabel, EntityTile, Hidden, LevelTile, NameAndValue, PointPath, PointPathLoop, PointStar, Points, RadiusGrid, RadiusPx, RefLinkBetweenCenters, RefLinkBetweenPivots, ValueOnly };

  /// <summary>
  /// Possible values: `Above`, `Center`, `Beneath`
  /// </summary>
  public enum EditorDisplayPos { Above, Beneath, Center };

  /// <summary>
  /// Possible values: `ZigZag`, `StraightArrow`, `CurvedArrow`, `ArrowsLine`, `DashedLine`
  /// </summary>
  public enum EditorLinkStyle { ArrowsLine, CurvedArrow, DashedLine, StraightArrow, ZigZag };

  public enum TextLanguageMode { LangC, LangHaxe, LangJs, LangJson, LangLog, LangLua, LangMarkdown, LangPython, LangRuby, LangXml };

  /// <summary>
  /// Possible values: `DiscardOldOnes`, `PreventAdding`, `MoveLastOne`
  /// </summary>
  public enum LimitBehavior { DiscardOldOnes, MoveLastOne, PreventAdding };

  /// <summary>
  /// If TRUE, the maxCount is a "per world" limit, if FALSE, it's a "per level". Possible
  /// values: `PerLayer`, `PerLevel`, `PerWorld`
  /// </summary>
  public enum LimitScope { PerLayer, PerLevel, PerWorld };

  /// <summary>
  /// Possible values: `Rectangle`, `Ellipse`, `Tile`, `Cross`
  /// </summary>
  public enum RenderMode { Cross, Ellipse, Rectangle, Tile };

  /// <summary>
  /// An enum describing how the the Entity tile is rendered inside the Entity bounds. Possible
  /// values: `Cover`, `FitInside`, `Repeat`, `Stretch`, `FullSizeCropped`,
  /// `FullSizeUncropped`, `NineSlice`
  /// </summary>
  public enum TileRenderMode { Cover, FitInside, FullSizeCropped, FullSizeUncropped, NineSlice, Repeat, Stretch };

  /// <summary>
  /// Checker mode Possible values: `None`, `Horizontal`, `Vertical`
  /// </summary>
  public enum Checker { Horizontal, None, Vertical };

  /// <summary>
  /// Defines how tileIds array is used Possible values: `Single`, `Stamp`
  /// </summary>
  public enum TileMode { Single, Stamp };

  /// <summary>
  /// Type of the layer as Haxe Enum Possible values: `IntGrid`, `Entities`, `Tiles`,
  /// `AutoLayer`
  /// </summary>
  public enum TypeEnum { AutoLayer, Entities, IntGrid, Tiles };

  public enum EmbedAtlas { LdtkIcons };

  public enum Flag { DiscardPreCsvIntGrid, ExportOldTableOfContentData, ExportPreCsvIntGridFormat, IgnoreBackupSuggest, MultiWorlds, PrependIndexToLevelFileNames, UseMultilinesType };

  public enum BgPos { Contain, Cover, CoverDirty, Repeat, Unscaled };

  public enum WorldLayout { Free, GridVania, LinearHorizontal, LinearVertical };

  /// <summary>
  /// Naming convention for Identifiers (first-letter uppercase, full uppercase etc.) Possible
  /// values: `Capitalize`, `Uppercase`, `Lowercase`, `Free`
  /// </summary>
  public enum IdentifierStyle { Capitalize, Free, Lowercase, Uppercase };

  /// <summary>
  /// "Image export" option when saving project. Possible values: `None`, `OneImagePerLayer`,
  /// `OneImagePerLevel`, `LayersAndLevels`
  /// </summary>
  public enum ImageExportMode { LayersAndLevels, None, OneImagePerLayer, OneImagePerLevel };

  internal static class Converter
  {
    public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
    {
      Converters =
            {
                CheckerConverter.Singleton,
                TileModeConverter.Singleton,
                WhenConverter.Singleton,
                AllowedRefsConverter.Singleton,
                EditorDisplayModeConverter.Singleton,
                EditorDisplayPosConverter.Singleton,
                EditorLinkStyleConverter.Singleton,
                TextLanguageModeConverter.Singleton,
                LimitBehaviorConverter.Singleton,
                LimitScopeConverter.Singleton,
                RenderModeConverter.Singleton,
                TileRenderModeConverter.Singleton,
                TypeEnumConverter.Singleton,
                EmbedAtlasConverter.Singleton,
                BgPosConverter.Singleton,
                WorldLayoutConverter.Singleton,
                FlagConverter.Singleton,
                IdentifierStyleConverter.Singleton,
                ImageExportModeConverter.Singleton,
                new DateOnlyConverter(),
                new TimeOnlyConverter(),
                IsoDateTimeOffsetConverter.Singleton
            },
    };
  }

  public class CheckerConverter : JsonConverter<Checker>
  {
    public override bool CanConvert(Type t) => t == typeof(Checker);

    public override Checker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "Horizontal":
          return Checker.Horizontal;
        case "None":
          return Checker.None;
        case "Vertical":
          return Checker.Vertical;
      }
      throw new Exception("Cannot unmarshal type Checker");
    }

    public override void Write(Utf8JsonWriter writer, Checker value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case Checker.Horizontal:
          JsonSerializer.Serialize(writer, "Horizontal", options);
          return;
        case Checker.None:
          JsonSerializer.Serialize(writer, "None", options);
          return;
        case Checker.Vertical:
          JsonSerializer.Serialize(writer, "Vertical", options);
          return;
      }
      throw new Exception("Cannot marshal type Checker");
    }

    public static readonly CheckerConverter Singleton = new CheckerConverter();
  }

  public class TileModeConverter : JsonConverter<TileMode>
  {
    public override bool CanConvert(Type t) => t == typeof(TileMode);

    public override TileMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "Single":
          return TileMode.Single;
        case "Stamp":
          return TileMode.Stamp;
      }
      throw new Exception("Cannot unmarshal type TileMode");
    }

    public override void Write(Utf8JsonWriter writer, TileMode value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case TileMode.Single:
          JsonSerializer.Serialize(writer, "Single", options);
          return;
        case TileMode.Stamp:
          JsonSerializer.Serialize(writer, "Stamp", options);
          return;
      }
      throw new Exception("Cannot marshal type TileMode");
    }

    public static readonly TileModeConverter Singleton = new TileModeConverter();
  }

  public class WhenConverter : JsonConverter<When>
  {
    public override bool CanConvert(Type t) => t == typeof(When);

    public override When Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "AfterLoad":
          return When.AfterLoad;
        case "AfterSave":
          return When.AfterSave;
        case "BeforeSave":
          return When.BeforeSave;
        case "Manual":
          return When.Manual;
      }
      throw new Exception("Cannot unmarshal type When");
    }

    public override void Write(Utf8JsonWriter writer, When value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case When.AfterLoad:
          JsonSerializer.Serialize(writer, "AfterLoad", options);
          return;
        case When.AfterSave:
          JsonSerializer.Serialize(writer, "AfterSave", options);
          return;
        case When.BeforeSave:
          JsonSerializer.Serialize(writer, "BeforeSave", options);
          return;
        case When.Manual:
          JsonSerializer.Serialize(writer, "Manual", options);
          return;
      }
      throw new Exception("Cannot marshal type When");
    }

    public static readonly WhenConverter Singleton = new WhenConverter();
  }

  public class AllowedRefsConverter : JsonConverter<AllowedRefs>
  {
    public override bool CanConvert(Type t) => t == typeof(AllowedRefs);

    public override AllowedRefs Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "Any":
          return AllowedRefs.Any;
        case "OnlySame":
          return AllowedRefs.OnlySame;
        case "OnlySpecificEntity":
          return AllowedRefs.OnlySpecificEntity;
        case "OnlyTags":
          return AllowedRefs.OnlyTags;
      }
      throw new Exception("Cannot unmarshal type AllowedRefs");
    }

    public override void Write(Utf8JsonWriter writer, AllowedRefs value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case AllowedRefs.Any:
          JsonSerializer.Serialize(writer, "Any", options);
          return;
        case AllowedRefs.OnlySame:
          JsonSerializer.Serialize(writer, "OnlySame", options);
          return;
        case AllowedRefs.OnlySpecificEntity:
          JsonSerializer.Serialize(writer, "OnlySpecificEntity", options);
          return;
        case AllowedRefs.OnlyTags:
          JsonSerializer.Serialize(writer, "OnlyTags", options);
          return;
      }
      throw new Exception("Cannot marshal type AllowedRefs");
    }

    public static readonly AllowedRefsConverter Singleton = new AllowedRefsConverter();
  }

  public class EditorDisplayModeConverter : JsonConverter<EditorDisplayMode>
  {
    public override bool CanConvert(Type t) => t == typeof(EditorDisplayMode);

    public override EditorDisplayMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "ArrayCountNoLabel":
          return EditorDisplayMode.ArrayCountNoLabel;
        case "ArrayCountWithLabel":
          return EditorDisplayMode.ArrayCountWithLabel;
        case "EntityTile":
          return EditorDisplayMode.EntityTile;
        case "Hidden":
          return EditorDisplayMode.Hidden;
        case "LevelTile":
          return EditorDisplayMode.LevelTile;
        case "NameAndValue":
          return EditorDisplayMode.NameAndValue;
        case "PointPath":
          return EditorDisplayMode.PointPath;
        case "PointPathLoop":
          return EditorDisplayMode.PointPathLoop;
        case "PointStar":
          return EditorDisplayMode.PointStar;
        case "Points":
          return EditorDisplayMode.Points;
        case "RadiusGrid":
          return EditorDisplayMode.RadiusGrid;
        case "RadiusPx":
          return EditorDisplayMode.RadiusPx;
        case "RefLinkBetweenCenters":
          return EditorDisplayMode.RefLinkBetweenCenters;
        case "RefLinkBetweenPivots":
          return EditorDisplayMode.RefLinkBetweenPivots;
        case "ValueOnly":
          return EditorDisplayMode.ValueOnly;
      }
      throw new Exception("Cannot unmarshal type EditorDisplayMode");
    }

    public override void Write(Utf8JsonWriter writer, EditorDisplayMode value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case EditorDisplayMode.ArrayCountNoLabel:
          JsonSerializer.Serialize(writer, "ArrayCountNoLabel", options);
          return;
        case EditorDisplayMode.ArrayCountWithLabel:
          JsonSerializer.Serialize(writer, "ArrayCountWithLabel", options);
          return;
        case EditorDisplayMode.EntityTile:
          JsonSerializer.Serialize(writer, "EntityTile", options);
          return;
        case EditorDisplayMode.Hidden:
          JsonSerializer.Serialize(writer, "Hidden", options);
          return;
        case EditorDisplayMode.LevelTile:
          JsonSerializer.Serialize(writer, "LevelTile", options);
          return;
        case EditorDisplayMode.NameAndValue:
          JsonSerializer.Serialize(writer, "NameAndValue", options);
          return;
        case EditorDisplayMode.PointPath:
          JsonSerializer.Serialize(writer, "PointPath", options);
          return;
        case EditorDisplayMode.PointPathLoop:
          JsonSerializer.Serialize(writer, "PointPathLoop", options);
          return;
        case EditorDisplayMode.PointStar:
          JsonSerializer.Serialize(writer, "PointStar", options);
          return;
        case EditorDisplayMode.Points:
          JsonSerializer.Serialize(writer, "Points", options);
          return;
        case EditorDisplayMode.RadiusGrid:
          JsonSerializer.Serialize(writer, "RadiusGrid", options);
          return;
        case EditorDisplayMode.RadiusPx:
          JsonSerializer.Serialize(writer, "RadiusPx", options);
          return;
        case EditorDisplayMode.RefLinkBetweenCenters:
          JsonSerializer.Serialize(writer, "RefLinkBetweenCenters", options);
          return;
        case EditorDisplayMode.RefLinkBetweenPivots:
          JsonSerializer.Serialize(writer, "RefLinkBetweenPivots", options);
          return;
        case EditorDisplayMode.ValueOnly:
          JsonSerializer.Serialize(writer, "ValueOnly", options);
          return;
      }
      throw new Exception("Cannot marshal type EditorDisplayMode");
    }

    public static readonly EditorDisplayModeConverter Singleton = new EditorDisplayModeConverter();
  }

  public class EditorDisplayPosConverter : JsonConverter<EditorDisplayPos>
  {
    public override bool CanConvert(Type t) => t == typeof(EditorDisplayPos);

    public override EditorDisplayPos Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "Above":
          return EditorDisplayPos.Above;
        case "Beneath":
          return EditorDisplayPos.Beneath;
        case "Center":
          return EditorDisplayPos.Center;
      }
      throw new Exception("Cannot unmarshal type EditorDisplayPos");
    }

    public override void Write(Utf8JsonWriter writer, EditorDisplayPos value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case EditorDisplayPos.Above:
          JsonSerializer.Serialize(writer, "Above", options);
          return;
        case EditorDisplayPos.Beneath:
          JsonSerializer.Serialize(writer, "Beneath", options);
          return;
        case EditorDisplayPos.Center:
          JsonSerializer.Serialize(writer, "Center", options);
          return;
      }
      throw new Exception("Cannot marshal type EditorDisplayPos");
    }

    public static readonly EditorDisplayPosConverter Singleton = new EditorDisplayPosConverter();
  }

  public class EditorLinkStyleConverter : JsonConverter<EditorLinkStyle>
  {
    public override bool CanConvert(Type t) => t == typeof(EditorLinkStyle);

    public override EditorLinkStyle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "ArrowsLine":
          return EditorLinkStyle.ArrowsLine;
        case "CurvedArrow":
          return EditorLinkStyle.CurvedArrow;
        case "DashedLine":
          return EditorLinkStyle.DashedLine;
        case "StraightArrow":
          return EditorLinkStyle.StraightArrow;
        case "ZigZag":
          return EditorLinkStyle.ZigZag;
      }
      throw new Exception("Cannot unmarshal type EditorLinkStyle");
    }

    public override void Write(Utf8JsonWriter writer, EditorLinkStyle value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case EditorLinkStyle.ArrowsLine:
          JsonSerializer.Serialize(writer, "ArrowsLine", options);
          return;
        case EditorLinkStyle.CurvedArrow:
          JsonSerializer.Serialize(writer, "CurvedArrow", options);
          return;
        case EditorLinkStyle.DashedLine:
          JsonSerializer.Serialize(writer, "DashedLine", options);
          return;
        case EditorLinkStyle.StraightArrow:
          JsonSerializer.Serialize(writer, "StraightArrow", options);
          return;
        case EditorLinkStyle.ZigZag:
          JsonSerializer.Serialize(writer, "ZigZag", options);
          return;
      }
      throw new Exception("Cannot marshal type EditorLinkStyle");
    }

    public static readonly EditorLinkStyleConverter Singleton = new EditorLinkStyleConverter();
  }

  public class TextLanguageModeConverter : JsonConverter<TextLanguageMode>
  {
    public override bool CanConvert(Type t) => t == typeof(TextLanguageMode);

    public override TextLanguageMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "LangC":
          return TextLanguageMode.LangC;
        case "LangHaxe":
          return TextLanguageMode.LangHaxe;
        case "LangJS":
          return TextLanguageMode.LangJs;
        case "LangJson":
          return TextLanguageMode.LangJson;
        case "LangLog":
          return TextLanguageMode.LangLog;
        case "LangLua":
          return TextLanguageMode.LangLua;
        case "LangMarkdown":
          return TextLanguageMode.LangMarkdown;
        case "LangPython":
          return TextLanguageMode.LangPython;
        case "LangRuby":
          return TextLanguageMode.LangRuby;
        case "LangXml":
          return TextLanguageMode.LangXml;
      }
      throw new Exception("Cannot unmarshal type TextLanguageMode");
    }

    public override void Write(Utf8JsonWriter writer, TextLanguageMode value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case TextLanguageMode.LangC:
          JsonSerializer.Serialize(writer, "LangC", options);
          return;
        case TextLanguageMode.LangHaxe:
          JsonSerializer.Serialize(writer, "LangHaxe", options);
          return;
        case TextLanguageMode.LangJs:
          JsonSerializer.Serialize(writer, "LangJS", options);
          return;
        case TextLanguageMode.LangJson:
          JsonSerializer.Serialize(writer, "LangJson", options);
          return;
        case TextLanguageMode.LangLog:
          JsonSerializer.Serialize(writer, "LangLog", options);
          return;
        case TextLanguageMode.LangLua:
          JsonSerializer.Serialize(writer, "LangLua", options);
          return;
        case TextLanguageMode.LangMarkdown:
          JsonSerializer.Serialize(writer, "LangMarkdown", options);
          return;
        case TextLanguageMode.LangPython:
          JsonSerializer.Serialize(writer, "LangPython", options);
          return;
        case TextLanguageMode.LangRuby:
          JsonSerializer.Serialize(writer, "LangRuby", options);
          return;
        case TextLanguageMode.LangXml:
          JsonSerializer.Serialize(writer, "LangXml", options);
          return;
      }
      throw new Exception("Cannot marshal type TextLanguageMode");
    }

    public static readonly TextLanguageModeConverter Singleton = new TextLanguageModeConverter();
  }

  public class LimitBehaviorConverter : JsonConverter<LimitBehavior>
  {
    public override bool CanConvert(Type t) => t == typeof(LimitBehavior);

    public override LimitBehavior Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "DiscardOldOnes":
          return LimitBehavior.DiscardOldOnes;
        case "MoveLastOne":
          return LimitBehavior.MoveLastOne;
        case "PreventAdding":
          return LimitBehavior.PreventAdding;
      }
      throw new Exception("Cannot unmarshal type LimitBehavior");
    }

    public override void Write(Utf8JsonWriter writer, LimitBehavior value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case LimitBehavior.DiscardOldOnes:
          JsonSerializer.Serialize(writer, "DiscardOldOnes", options);
          return;
        case LimitBehavior.MoveLastOne:
          JsonSerializer.Serialize(writer, "MoveLastOne", options);
          return;
        case LimitBehavior.PreventAdding:
          JsonSerializer.Serialize(writer, "PreventAdding", options);
          return;
      }
      throw new Exception("Cannot marshal type LimitBehavior");
    }

    public static readonly LimitBehaviorConverter Singleton = new LimitBehaviorConverter();
  }

  public class LimitScopeConverter : JsonConverter<LimitScope>
  {
    public override bool CanConvert(Type t) => t == typeof(LimitScope);

    public override LimitScope Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "PerLayer":
          return LimitScope.PerLayer;
        case "PerLevel":
          return LimitScope.PerLevel;
        case "PerWorld":
          return LimitScope.PerWorld;
      }
      throw new Exception("Cannot unmarshal type LimitScope");
    }

    public override void Write(Utf8JsonWriter writer, LimitScope value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case LimitScope.PerLayer:
          JsonSerializer.Serialize(writer, "PerLayer", options);
          return;
        case LimitScope.PerLevel:
          JsonSerializer.Serialize(writer, "PerLevel", options);
          return;
        case LimitScope.PerWorld:
          JsonSerializer.Serialize(writer, "PerWorld", options);
          return;
      }
      throw new Exception("Cannot marshal type LimitScope");
    }

    public static readonly LimitScopeConverter Singleton = new LimitScopeConverter();
  }

  public class RenderModeConverter : JsonConverter<RenderMode>
  {
    public override bool CanConvert(Type t) => t == typeof(RenderMode);

    public override RenderMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "Cross":
          return RenderMode.Cross;
        case "Ellipse":
          return RenderMode.Ellipse;
        case "Rectangle":
          return RenderMode.Rectangle;
        case "Tile":
          return RenderMode.Tile;
      }
      throw new Exception("Cannot unmarshal type RenderMode");
    }

    public override void Write(Utf8JsonWriter writer, RenderMode value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case RenderMode.Cross:
          JsonSerializer.Serialize(writer, "Cross", options);
          return;
        case RenderMode.Ellipse:
          JsonSerializer.Serialize(writer, "Ellipse", options);
          return;
        case RenderMode.Rectangle:
          JsonSerializer.Serialize(writer, "Rectangle", options);
          return;
        case RenderMode.Tile:
          JsonSerializer.Serialize(writer, "Tile", options);
          return;
      }
      throw new Exception("Cannot marshal type RenderMode");
    }

    public static readonly RenderModeConverter Singleton = new RenderModeConverter();
  }

  public class TileRenderModeConverter : JsonConverter<TileRenderMode>
  {
    public override bool CanConvert(Type t) => t == typeof(TileRenderMode);

    public override TileRenderMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "Cover":
          return TileRenderMode.Cover;
        case "FitInside":
          return TileRenderMode.FitInside;
        case "FullSizeCropped":
          return TileRenderMode.FullSizeCropped;
        case "FullSizeUncropped":
          return TileRenderMode.FullSizeUncropped;
        case "NineSlice":
          return TileRenderMode.NineSlice;
        case "Repeat":
          return TileRenderMode.Repeat;
        case "Stretch":
          return TileRenderMode.Stretch;
      }
      throw new Exception("Cannot unmarshal type TileRenderMode");
    }

    public override void Write(Utf8JsonWriter writer, TileRenderMode value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case TileRenderMode.Cover:
          JsonSerializer.Serialize(writer, "Cover", options);
          return;
        case TileRenderMode.FitInside:
          JsonSerializer.Serialize(writer, "FitInside", options);
          return;
        case TileRenderMode.FullSizeCropped:
          JsonSerializer.Serialize(writer, "FullSizeCropped", options);
          return;
        case TileRenderMode.FullSizeUncropped:
          JsonSerializer.Serialize(writer, "FullSizeUncropped", options);
          return;
        case TileRenderMode.NineSlice:
          JsonSerializer.Serialize(writer, "NineSlice", options);
          return;
        case TileRenderMode.Repeat:
          JsonSerializer.Serialize(writer, "Repeat", options);
          return;
        case TileRenderMode.Stretch:
          JsonSerializer.Serialize(writer, "Stretch", options);
          return;
      }
      throw new Exception("Cannot marshal type TileRenderMode");
    }

    public static readonly TileRenderModeConverter Singleton = new TileRenderModeConverter();
  }

  public class TypeEnumConverter : JsonConverter<TypeEnum>
  {
    public override bool CanConvert(Type t) => t == typeof(TypeEnum);

    public override TypeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "AutoLayer":
          return TypeEnum.AutoLayer;
        case "Entities":
          return TypeEnum.Entities;
        case "IntGrid":
          return TypeEnum.IntGrid;
        case "Tiles":
          return TypeEnum.Tiles;
      }
      throw new Exception("Cannot unmarshal type TypeEnum");
    }

    public override void Write(Utf8JsonWriter writer, TypeEnum value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case TypeEnum.AutoLayer:
          JsonSerializer.Serialize(writer, "AutoLayer", options);
          return;
        case TypeEnum.Entities:
          JsonSerializer.Serialize(writer, "Entities", options);
          return;
        case TypeEnum.IntGrid:
          JsonSerializer.Serialize(writer, "IntGrid", options);
          return;
        case TypeEnum.Tiles:
          JsonSerializer.Serialize(writer, "Tiles", options);
          return;
      }
      throw new Exception("Cannot marshal type TypeEnum");
    }

    public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
  }

  public class EmbedAtlasConverter : JsonConverter<EmbedAtlas>
  {
    public override bool CanConvert(Type t) => t == typeof(EmbedAtlas);

    public override EmbedAtlas Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      if (value == "LdtkIcons")
      {
        return EmbedAtlas.LdtkIcons;
      }
      throw new Exception("Cannot unmarshal type EmbedAtlas");
    }

    public override void Write(Utf8JsonWriter writer, EmbedAtlas value, JsonSerializerOptions options)
    {
      if (value == EmbedAtlas.LdtkIcons)
      {
        JsonSerializer.Serialize(writer, "LdtkIcons", options);
        return;
      }
      throw new Exception("Cannot marshal type EmbedAtlas");
    }

    public static readonly EmbedAtlasConverter Singleton = new EmbedAtlasConverter();
  }

  public class BgPosConverter : JsonConverter<BgPos>
  {
    public override bool CanConvert(Type t) => t == typeof(BgPos);

    public override BgPos Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "Contain":
          return BgPos.Contain;
        case "Cover":
          return BgPos.Cover;
        case "CoverDirty":
          return BgPos.CoverDirty;
        case "Repeat":
          return BgPos.Repeat;
        case "Unscaled":
          return BgPos.Unscaled;
      }
      throw new Exception("Cannot unmarshal type BgPos");
    }

    public override void Write(Utf8JsonWriter writer, BgPos value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case BgPos.Contain:
          JsonSerializer.Serialize(writer, "Contain", options);
          return;
        case BgPos.Cover:
          JsonSerializer.Serialize(writer, "Cover", options);
          return;
        case BgPos.CoverDirty:
          JsonSerializer.Serialize(writer, "CoverDirty", options);
          return;
        case BgPos.Repeat:
          JsonSerializer.Serialize(writer, "Repeat", options);
          return;
        case BgPos.Unscaled:
          JsonSerializer.Serialize(writer, "Unscaled", options);
          return;
      }
      throw new Exception("Cannot marshal type BgPos");
    }

    public static readonly BgPosConverter Singleton = new BgPosConverter();
  }

  public class WorldLayoutConverter : JsonConverter<WorldLayout>
  {
    public override bool CanConvert(Type t) => t == typeof(WorldLayout);

    public override WorldLayout Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "Free":
          return WorldLayout.Free;
        case "GridVania":
          return WorldLayout.GridVania;
        case "LinearHorizontal":
          return WorldLayout.LinearHorizontal;
        case "LinearVertical":
          return WorldLayout.LinearVertical;
      }
      throw new Exception("Cannot unmarshal type WorldLayout");
    }

    public override void Write(Utf8JsonWriter writer, WorldLayout value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case WorldLayout.Free:
          JsonSerializer.Serialize(writer, "Free", options);
          return;
        case WorldLayout.GridVania:
          JsonSerializer.Serialize(writer, "GridVania", options);
          return;
        case WorldLayout.LinearHorizontal:
          JsonSerializer.Serialize(writer, "LinearHorizontal", options);
          return;
        case WorldLayout.LinearVertical:
          JsonSerializer.Serialize(writer, "LinearVertical", options);
          return;
      }
      throw new Exception("Cannot marshal type WorldLayout");
    }

    public static readonly WorldLayoutConverter Singleton = new WorldLayoutConverter();
  }

  public class FlagConverter : JsonConverter<Flag>
  {
    public override bool CanConvert(Type t) => t == typeof(Flag);

    public override Flag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "DiscardPreCsvIntGrid":
          return Flag.DiscardPreCsvIntGrid;
        case "ExportOldTableOfContentData":
          return Flag.ExportOldTableOfContentData;
        case "ExportPreCsvIntGridFormat":
          return Flag.ExportPreCsvIntGridFormat;
        case "IgnoreBackupSuggest":
          return Flag.IgnoreBackupSuggest;
        case "MultiWorlds":
          return Flag.MultiWorlds;
        case "PrependIndexToLevelFileNames":
          return Flag.PrependIndexToLevelFileNames;
        case "UseMultilinesType":
          return Flag.UseMultilinesType;
      }
      throw new Exception("Cannot unmarshal type Flag");
    }

    public override void Write(Utf8JsonWriter writer, Flag value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case Flag.DiscardPreCsvIntGrid:
          JsonSerializer.Serialize(writer, "DiscardPreCsvIntGrid", options);
          return;
        case Flag.ExportOldTableOfContentData:
          JsonSerializer.Serialize(writer, "ExportOldTableOfContentData", options);
          return;
        case Flag.ExportPreCsvIntGridFormat:
          JsonSerializer.Serialize(writer, "ExportPreCsvIntGridFormat", options);
          return;
        case Flag.IgnoreBackupSuggest:
          JsonSerializer.Serialize(writer, "IgnoreBackupSuggest", options);
          return;
        case Flag.MultiWorlds:
          JsonSerializer.Serialize(writer, "MultiWorlds", options);
          return;
        case Flag.PrependIndexToLevelFileNames:
          JsonSerializer.Serialize(writer, "PrependIndexToLevelFileNames", options);
          return;
        case Flag.UseMultilinesType:
          JsonSerializer.Serialize(writer, "UseMultilinesType", options);
          return;
      }
      throw new Exception("Cannot marshal type Flag");
    }

    public static readonly FlagConverter Singleton = new FlagConverter();
  }

  public class IdentifierStyleConverter : JsonConverter<IdentifierStyle>
  {
    public override bool CanConvert(Type t) => t == typeof(IdentifierStyle);

    public override IdentifierStyle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "Capitalize":
          return IdentifierStyle.Capitalize;
        case "Free":
          return IdentifierStyle.Free;
        case "Lowercase":
          return IdentifierStyle.Lowercase;
        case "Uppercase":
          return IdentifierStyle.Uppercase;
      }
      throw new Exception("Cannot unmarshal type IdentifierStyle");
    }

    public override void Write(Utf8JsonWriter writer, IdentifierStyle value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case IdentifierStyle.Capitalize:
          JsonSerializer.Serialize(writer, "Capitalize", options);
          return;
        case IdentifierStyle.Free:
          JsonSerializer.Serialize(writer, "Free", options);
          return;
        case IdentifierStyle.Lowercase:
          JsonSerializer.Serialize(writer, "Lowercase", options);
          return;
        case IdentifierStyle.Uppercase:
          JsonSerializer.Serialize(writer, "Uppercase", options);
          return;
      }
      throw new Exception("Cannot marshal type IdentifierStyle");
    }

    public static readonly IdentifierStyleConverter Singleton = new IdentifierStyleConverter();
  }

  public class ImageExportModeConverter : JsonConverter<ImageExportMode>
  {
    public override bool CanConvert(Type t) => t == typeof(ImageExportMode);

    public override ImageExportMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "LayersAndLevels":
          return ImageExportMode.LayersAndLevels;
        case "None":
          return ImageExportMode.None;
        case "OneImagePerLayer":
          return ImageExportMode.OneImagePerLayer;
        case "OneImagePerLevel":
          return ImageExportMode.OneImagePerLevel;
      }
      throw new Exception("Cannot unmarshal type ImageExportMode");
    }

    public override void Write(Utf8JsonWriter writer, ImageExportMode value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case ImageExportMode.LayersAndLevels:
          JsonSerializer.Serialize(writer, "LayersAndLevels", options);
          return;
        case ImageExportMode.None:
          JsonSerializer.Serialize(writer, "None", options);
          return;
        case ImageExportMode.OneImagePerLayer:
          JsonSerializer.Serialize(writer, "OneImagePerLayer", options);
          return;
        case ImageExportMode.OneImagePerLevel:
          JsonSerializer.Serialize(writer, "OneImagePerLevel", options);
          return;
      }
      throw new Exception("Cannot marshal type ImageExportMode");
    }

    public static readonly ImageExportModeConverter Singleton = new ImageExportModeConverter();
  }

  public class DateOnlyConverter : JsonConverter<DateOnly>
  {
    private readonly string serializationFormat;
    public DateOnlyConverter() : this(null) { }

    public DateOnlyConverter(string? serializationFormat)
    {
      this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      return DateOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
  }

  public class TimeOnlyConverter : JsonConverter<TimeOnly>
  {
    private readonly string serializationFormat;

    public TimeOnlyConverter() : this(null) { }

    public TimeOnlyConverter(string? serializationFormat)
    {
      this.serializationFormat = serializationFormat ?? "HH:mm:ss.fff";
    }

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      return TimeOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
  }

  public class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
  {
    public override bool CanConvert(Type t) => t == typeof(DateTimeOffset);

    private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

    private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
    private string? _dateTimeFormat;
    private CultureInfo? _culture;

    public DateTimeStyles DateTimeStyles
    {
      get => _dateTimeStyles;
      set => _dateTimeStyles = value;
    }

    public string? DateTimeFormat
    {
      get => _dateTimeFormat ?? string.Empty;
      set => _dateTimeFormat = (string.IsNullOrEmpty(value)) ? null : value;
    }

    public CultureInfo Culture
    {
      get => _culture ?? CultureInfo.CurrentCulture;
      set => _culture = value;
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
      string text;


      if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
              || (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
      {
        value = value.ToUniversalTime();
      }

      text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

      writer.WriteStringValue(text);
    }

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      string? dateText = reader.GetString();

      if (string.IsNullOrEmpty(dateText) == false)
      {
        if (!string.IsNullOrEmpty(_dateTimeFormat))
        {
          return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
        }
        else
        {
          return DateTimeOffset.Parse(dateText, Culture, _dateTimeStyles);
        }
      }
      else
      {
        return default(DateTimeOffset);
      }
    }

    public static readonly IsoDateTimeOffsetConverter Singleton = new IsoDateTimeOffsetConverter();
  }
}
