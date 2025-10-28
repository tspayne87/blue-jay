using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Container;
using BlueJay.UI.Interfaces;
using BlueJay.Utils;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Tree;

/// <summary>
/// Initializes a new instance of the <see cref="Node"/> class.
/// </summary>
/// <param name="tag">The tag associated with this node.</param>
/// <param name="screen">The screen viewport associated with this node.</param>
/// <param name="styleService">The style service used for managing styles.</param>
/// <param name="provider">The service provider associated with this node.</param>
/// <param name="content">The content manager container associated with this node.</param>
internal class Element(string tag, IScreenViewport screen, IStyleService styleService, IServiceProvider provider, IContentManagerContainer content, IFontCollection fontCollection) : IElement
{
  /// <summary>
  /// The style service used for managing styles.
  /// </summary>
  private readonly IStyleService _styleService = styleService;

  /// <summary>
  /// The screen viewport associated with this node.
  /// </summary>
  private readonly IScreenViewport _screen = screen;

  /// <summary>
  /// The service provider associated with this node.
  /// </summary>
  private readonly IServiceProvider _provider = provider;

  /// <summary>
  /// The content manager container associated with this node.
  /// </summary>
  private readonly IContentManagerContainer _content = content;

  /// <summary>
  /// The font collection for the text node.
  /// </summary>
  private readonly IFontCollection _fontCollection = fontCollection;

  /// <summary>
  /// The child nodes of this node.
  /// </summary>
  private IElement[] _children = Array.Empty<IElement>();

  /// <summary>
  /// The root node of this tree.
  /// </summary>
  private IElement? _root;

  /// <summary>
  /// The current parent node.
  /// </summary>
  private IElement? _parent;

  /// <summary>
  /// The tag associated with this node.
  /// </summary>
  private string _tag = tag;

  /// <summary>
  /// Indicates whether this node has been mounted or not.
  /// </summary>
  private bool _mounted = false;

  /// <summary>
  /// The style decorators applied to this node.
  /// </summary>
  private StyleDecorator _decorators;

  /// <summary>
  /// The styles applied to this node.
  /// </summary>
  private Dictionary<StyleDecorator, Style> _styles = new Dictionary<StyleDecorator, Style>();

  /// <summary>
  /// The current cache for this node.
  /// </summary>
  protected readonly ElementCache _cache = new ElementCache();

  /// <summary>
  /// The entity associated with this node.
  /// </summary>
  protected IEntity? _entity;

  /// <inheritdoc />
  public IReadOnlyList<IElement> Children => Array.AsReadOnly(_children);

  /// <inheritdoc />
  public IElement? Parent
  {
    get => _parent;
    internal set => _parent = value;
  }

  public IElement? Root
  {
    get => _root;
    internal set => UpdateRoot(value);
  }

  /// <inheritdoc />
  public Style Style
  {
    get => this[StyleDecorator.None];
    set => this[StyleDecorator.None] = value;
  }

  /// <inheritdoc />
  public StyleDecorator Decorators
  {
    get => _decorators;
    set => _decorators = value;
  }

  /// <summary>
  /// Determines whether this node needs to generate an entity or not.
  /// </summary>
  protected virtual bool NeedsEntity
  {
    get
    {
      var style = GetComputedStyle();
      return style.NinePatch != null;
    }
  }

  /// <inheritdoc />
  public Style this[StyleDecorator decorator]
  {
    get
    {
      if (_styles.TryGetValue(decorator, out var style))
        return style;
      _styles[decorator] = new Style();
      return _styles[decorator];
    }
    set
    {
      _styles[decorator] = value;
      UpdateEntity();
    }
  }

  /// <summary>
  /// Updates the entity associated with this node.
  /// </summary>
  protected virtual void UpdateEntity()
  {
    if (_mounted && NeedsEntity)
    {
      var style = GetComputedStyle();

      if (_entity == null)
        _entity = _provider.AddEntity(StaticNames.UILAYER, 10);

      _entity.Upsert(new SizeAddon(CalculateWidth(), CalculateHeight()));
      _entity.Upsert(new PositionAddon(new Vector2(CalculateTopPosition(), CalculateLeftPosition())));
      if (style.NinePatch != null)
        _entity.Upsert(new NinePatchAddon(style.NinePatch));
    }
  }

  #region Calculation Methods
  /// <inheritdoc />
  public virtual int CalculateWidth()
  {
    if (_cache.Width == null)
    {
      var style = GetComputedStyle();
      if (style.Width?.TryGetPixels(out var pixels) == true)
        return pixels;

      var parentStyle = _parent?.GetComputedStyle();
      var parentWidth = _parent?.CalculateWidth() ?? _screen.Width;
      if (parentStyle?.Padding != null)
        parentWidth -= parentStyle.Value.Padding.Value.Left + parentStyle.Value.Padding.Value.Right;

      var direction = parentStyle?.Direction ?? Direction.Horizontal;
      if (direction == Direction.Horizontal)
      {
        if (parentStyle?.Gap != null)
          parentWidth -= parentStyle.Value.Gap.Value * (_parent!.Children.Count - 1);
        var siblingWidth = (int)Math.Floor(parentWidth / (float)(_parent?.Children.Count ?? 1f));
        return style.Width?.ToPixels(siblingWidth) ?? siblingWidth;
      }
      _cache.Width = style.Width?.ToPixels(parentWidth) ?? parentWidth;
    }

    return _cache.Width.Value;
  }

  /// <inheritdoc />
  public virtual int CalculateHeight()
  {
    if (_cache.Height == null)
    {
      var style = GetComputedStyle();
      if (style.Height?.TryGetPixels(out var pixels) == true)
        _cache.Height = pixels;
    }

    return _cache.Height.Value;
  }

  /// <inheritdoc />
  public virtual int CalculateLeftPosition()
  {
    if (_cache.Left == null)
    {
      var left = 0;
      if (_parent != null)
      {
        left += _parent.CalculateLeftPosition();
        var parentStyle = _parent.GetComputedStyle();
        if (parentStyle.Padding != null)
          left += parentStyle.Padding.Value.Left;

        var direction = parentStyle.Direction ?? Direction.Horizontal;
        if (direction == Direction.Horizontal)
        {
          foreach (var sibling in _parent.Children)
          {
            if (sibling == this)
              break;
            left += sibling.CalculateWidth();
            if (parentStyle.Gap != null)
              left += parentStyle.Gap.Value;
          }
        }
      }

      var style = GetComputedStyle();
      if (style.Offset != null)
      {
        left += style.Offset?.Left ?? 0;
        left -= style.Offset?.Right ?? 0;
      }
      _cache.Left = left;
    }
    return _cache.Left.Value;
  }

  /// <inheritdoc />
  public virtual int CalculateTopPosition()
  {
    if (_cache.Top == null)
    {
      var top = 0;
      if (_parent != null)
      {
        top += _parent.CalculateTopPosition();
        var parentStyle = _parent.GetComputedStyle();
        if (parentStyle.Padding != null)
          top += parentStyle.Padding.Value.Top;

        var direction = parentStyle.Direction ?? Direction.Horizontal;
        if (direction == Direction.Vertical)
        {
          foreach (var sibling in _parent.Children)
          {
            if (sibling == this)
              break;
            top += sibling.CalculateHeight();
            if (parentStyle.Gap != null)
              top += parentStyle.Gap.Value;
          }
        }
      }
      var style = GetComputedStyle();
      if (style.Offset != null)
      {
        top += style.Offset?.Top ?? 0;
        top -= style.Offset?.Bottom ?? 0;
      }
      _cache.Top = top;
    }
    return _cache.Top.Value;
  }

  /// <summary>
  /// Tries to get the pixel height of the node, based on the computed style of this node
  /// </summary>
  /// <param name="height">The pixel height of the node.</param>
  /// <returns>Will return true if the pixel height was successfully retrieved.</returns>
  protected bool TryGetPixelHeight(out int height)
  {
    height = 0;
    var style = GetComputedStyle();
    if (style.Height?.TryGetPixels(out var pixels) == true)
    {
      height = pixels;
      return true;
    }

    if (style.Height != null)
    { // If we have a percentage height we want to use the height of its parent
      var parentStyle = _parent?.GetComputedStyle();
      var parentHeight = _parent?.CalculateHeight() ?? _screen.Height;

      if (parentStyle?.Padding != null)
        parentHeight -= parentStyle.Value.Padding.Value.Top + parentStyle.Value.Padding.Value.Bottom;
      height = style.Height?.ToPixels(parentHeight) ?? parentHeight;
      return true;
    }
    return false;
  }
  #endregion

  /// <inheritdoc />
  public Style GetComputedStyle()
  {
    if (_cache.ComputedStyle == null)
    {
      var result = _styles.TryGetValue(StyleDecorator.None, out var baseStyle) ? baseStyle : new Style();
      foreach (var (decorator, style) in _styles)
        if ((decorator & _decorators) != StyleDecorator.None)
          result += style;
      _cache.ComputedStyle = _styleService.GetStyle(_decorators) + _styleService.GetStyle(_tag, _decorators) + result;
    }
    return _cache.ComputedStyle.Value;
  }

  /// <inheritdoc />
  public void AddChild(IElement child)
  {
    if (child == null) throw new ArgumentNullException(nameof(child));
    Array.Resize(ref _children, _children.Length + 1);
    _children[^1] = child;
    child.Parent = this;
  }

  /// <inheritdoc />
  public void RemoveChild(IElement child)
  {
    if (child == null) throw new ArgumentNullException(nameof(child));
    var index = Array.IndexOf(_children, child);
    if (index == -1) throw new ArgumentException("Child not found", nameof(child));
    Array.Copy(_children, index + 1, _children, index, _children.Length - index - 1);
    Array.Resize(ref _children, _children.Length - 1);
    child.Parent = null;
  }

  /// <inheritdoc />
  public Vector2 MeasureString(string text)
  {
    var style = GetComputedStyle();
    if (style.Font == null)
      return Vector2.Zero;

    var spriteFont = _fontCollection.SpriteFonts.TryGetValue(style.Font!, out var spriteFontValue) ? spriteFontValue : null;
    var textureFont = _fontCollection.TextureFonts.TryGetValue(style.Font!, out var textureFontValue) ? textureFontValue : null;

    if (spriteFont != null)
      return spriteFont.MeasureString(text);
    else if (textureFont != null)
      return textureFont.MeasureString(text, style.FontSize ?? 1);
    return Vector2.Zero;
  }

  /// <inheritdoc />
  public string FitString(string text)
  {
    var style = GetComputedStyle();
    if (style.Font == null)
      return text;

    var spriteFont = _fontCollection.SpriteFonts.TryGetValue(style.Font!, out var spriteFontValue) ? spriteFontValue : null;
    var textureFont = _fontCollection.TextureFonts.TryGetValue(style.Font!, out var textureFontValue) ? textureFontValue : null;
    var width = CalculateWidth();

    if (spriteFont != null)
      return spriteFont.FitString(text, width);
    else if (textureFont != null)
      return textureFont.FitString(text, width, style.FontSize ?? 1);
    return text;
  }

  /// <inheritdoc />
  public void MountElement()
  {
    if (_mounted)
      return;
    _mounted = true;
    UpdateEntity();
  }
  
  private void UpdateRoot(IElement? root)
  {
    _root = root;
    foreach (var child in _children)
      child.UpdateRoot(root);
  }
}
