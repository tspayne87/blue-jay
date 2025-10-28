using BlueJay.UI.Interfaces;

namespace BlueJay.UI.Services;

public class StyleService : IStyleService
{
  /// <summary>
  /// A dictionary that holds all registered styles.
  /// </summary>
  private readonly Dictionary<string, Dictionary<StyleDecorator, Style>> _styles;

  /// <summary>
  /// Initializes a new instance of the <see cref="StyleService"/> class.
  /// </summary>
  public StyleService()
  {
    _styles = new Dictionary<string, Dictionary<StyleDecorator, Style>>();
  }

  /// <inheritdoc />
  public void RegisterStyle(Style style, StyleDecorator decorators = StyleDecorator.None)
  {
    RegisterStyle(Style.GLOBAL, style, decorators);
  }

  /// <inheritdoc />
  public void RegisterStyle(string key, Style style, StyleDecorator decorators = StyleDecorator.None)
  {
    if (!_styles.ContainsKey(key))
      _styles[key] = new Dictionary<StyleDecorator, Style>();
    _styles[key][decorators] = style;
  }

  /// <inheritdoc />
  public Style GetStyle(string key, StyleDecorator decorators = StyleDecorator.None)
  {
    var result = new Style();
    if (_styles.TryGetValue(key, out var styles))
      foreach (var (styleDecorator, style) in styles)
        if ((styleDecorator & decorators) == StyleDecorator.None)
          result += style;
    return result;
  }

  /// <inheritdoc />
  public Style GetStyle(StyleDecorator decorators = StyleDecorator.None)
  {
    return GetStyle(Style.GLOBAL, decorators);
  }
}
