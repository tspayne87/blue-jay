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
    public static void AddText(this IServiceProvider provider, string text)
    {
      provider.AddText(text, new Style(), null);
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="style">The styles that should be included on this text node</param>
    public static void AddText(this IServiceProvider provider, string text, Style style)
    {
      provider.AddText(text, style, null);
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="parent">The parent this text node should have</param>
    public static void AddText(this IServiceProvider provider, string text, IEntity parent)
    {
      provider.AddText(text, new Style(), parent);
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="style">The styles that should be included on this text node</param>
    /// <param name="parent">The parent this text node should have</param>
    public static void AddText(this IServiceProvider provider, string text, Style style, IEntity parent)
    {
      var entity = provider.AddUIEntity<Entity>(parent);
      style.HorizontalAlign = style.HorizontalAlign ?? HorizontalAlign.Center;
      style.HeightPercentage = style.HeightPercentage ?? 1;
      style.WidthPercentage = style.WidthPercentage ?? 1;
      style.TextAlign = style.TextAlign ?? TextAlign.Center;
      style.TextBaseline = style.TextBaseline ?? TextBaseline.Center;

      entity.Add<StyleAddon>(style);
      entity.Add<TextAddon>(text);
      entity.Add<TextureAddon>();
      entity.Add<PositionAddon>();
      entity.Add<ColorAddon>(Color.Black);
      entity.Add<BoundsAddon>();
    }
  }
}
