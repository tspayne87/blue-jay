using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.UI.Interfaces;
using BlueJay.Utils;

namespace BlueJay.UI.Tree;

/// <summary>
/// Represents a text node in the UI tree.
/// </summary>
/// <param name="screen">The current screen viewport.</param>
/// <param name="styleService">The style service.</param>
/// <param name="provider">The service provider.</param>
/// <param name="content">The content manager container.</param>
/// <param name="fontCollection">The font collection.</param>
/// <param name="text">The text content.</param>
internal class TextElement(IScreenViewport screen, IStyleService styleService, IServiceProvider provider, IContentManagerContainer content, IFontCollection fontCollection, string text)
  : Element("Text", screen, styleService, provider, content, fontCollection), ITextElement
{
  /// <summary>
  /// The font collection for the text node.
  /// </summary>
  private readonly IFontCollection _fontCollection = fontCollection;
  /// <summary>
  /// The text content for the text node.
  /// </summary>
  private string _text = text;

  /// <inheritdoc />
  public string Text
  {
    get => _text;
    set
    {
      _text = value;
      UpdateEntity();
    }
  }

  /// <inheritdoc />
  protected override bool NeedsEntity => GetComputedStyle().Font != null;

  /// <inheritdoc />
  protected override void UpdateEntity()
  {
    base.UpdateEntity();
    if (_entity == null)
      return;

    var style = GetComputedStyle();
    var spriteFont = _fontCollection.SpriteFonts.TryGetValue(style.Font!, out var spriteFontValue) ? spriteFontValue : null;
    var textureFont = _fontCollection.TextureFonts.TryGetValue(style.Font!, out var textureFontValue) ? textureFontValue : null;

    _entity!.Upsert(new TextAddon(Text));
    _entity.Upsert(new FontAddon(textureFont, spriteFont, style.FontSize, style.FontColor));
  }

  /// <inheritdoc />
  public override int CalculateHeight()
  {
    if (_cache.Height == null)
    {
      if (TryGetPixelHeight(out var result))
        return result;

      var style = GetComputedStyle();
      var height = 0;
      if (style.Font != null)
      {
        var spriteFont = _fontCollection.SpriteFonts.TryGetValue(style.Font!, out var spriteFontValue) ? spriteFontValue : null;
        var textureFont = _fontCollection.TextureFonts.TryGetValue(style.Font!, out var textureFontValue) ? textureFontValue : null;
        var width = CalculateWidth();

        if (spriteFont != null)
        {
          var text = spriteFont.FitString(Text, width);
          height = (int)spriteFont.MeasureString(text).Y;
        }
        else if (textureFont != null)
        {
          var text = textureFont.FitString(Text, width, style.FontSize ?? 1);
          height = (int)textureFont.MeasureString(text, style.FontSize ?? 1).Y;
        }
      }
      _cache.Height = height;
    }
    return _cache.Height.Value;
  }
}
