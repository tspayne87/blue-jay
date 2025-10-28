namespace BlueJay.UI.Interfaces;

public interface IStyleService
{
  /// <summary>
  /// We want to register a new global style that will be used for all elements.
  /// </summary>
  /// <param name="style">The style to register</param>
  /// <param name="decorators">The current decorators for this style</param>
  public void RegisterStyle(Style style, StyleDecorator decorators = StyleDecorator.None);

  /// <summary>
  /// We want to register a new style for a specific key and decorators.
  /// </summary>
  /// <param name="key">The current key we want to use for the style</param>
  /// <param name="style">The style we want to register</param>
  /// <param name="decorators">The decorators to apply to the style</param>
  public void RegisterStyle(string key, Style style, StyleDecorator decorators = StyleDecorator.None);

  /// <summary>
  /// Gets the style associated with the specified key and decorators.
  /// </summary>
  /// <param name="key">The current key we are searching for a style from</param>
  /// <param name="decorators">The decorators to apply to the style</param>
  /// <returns>The merged style associated with the specified key and decorators</returns>
  public Style GetStyle(string key, StyleDecorator decorators = StyleDecorator.None);

  /// <summary>
  /// Gets the global style associated with the specified decorators.
  /// </summary>
  /// <param name="decorators">The decorators to apply to the style</param>
  /// <returns>The merged global style associated with the specified decorators</returns>
  public Style GetStyle(StyleDecorator decorators = StyleDecorator.None);
}
