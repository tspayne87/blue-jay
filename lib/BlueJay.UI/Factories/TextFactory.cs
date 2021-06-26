using BlueJay.Component.System;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.UI.Factories
{
  public static class TextFactory
  {
    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    public static IEntity AddText(this IServiceProvider provider, string text)
    {
      return provider.AddText(text, new Style(), null);
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="style">The styles that should be included on this text node</param>
    public static IEntity AddText(this IServiceProvider provider, string text, Style style)
    {
      return provider.AddText(text, style, null);
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="parent">The parent this text node should have</param>
    public static IEntity AddText(this IServiceProvider provider, string text, IEntity parent)
    {
      return provider.AddText(text, new Style(), parent);
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="style">The styles that should be included on this text node</param>
    /// <param name="parent">The parent this text node should have</param>
    public static IEntity AddText(this IServiceProvider provider, string text, Style style, IEntity parent)
    {
      var parentStyle = parent?.GetAddon<StyleAddon>()?.Style;
      var entity = provider.AddUIEntity<Entity>(parent);
      style.HorizontalAlign = style.HorizontalAlign ?? HorizontalAlign.Center;
      style.TextAlign = style.TextAlign ?? TextAlign.Center;
      style.TextBaseline = style.TextBaseline ?? TextBaseline.Center;
      style.Font = style.Font ?? parentStyle?.Font;
      style.TextureFont = style.TextureFont ?? parentStyle?.TextureFont;
      style.TextureFontSize = style.TextureFontSize ?? parentStyle?.TextureFontSize;

      entity.Add<StyleAddon>(style);
      entity.Add<TextAddon>(text);
      entity.Add<TextureAddon>();
      entity.Add<PositionAddon>();
      entity.Add<ColorAddon>(Color.Black);
      entity.Add<BoundsAddon>();

      return entity;
    }
  }
}
