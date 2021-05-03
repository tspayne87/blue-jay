using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.Events;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlueJay.UI.Systems
{
  /// <summary>
  /// System is meant to process the bounds based on the style of the entity
  /// </summary>
  public class UIStyleBoundsSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The key that we should filter on for this system to work with
    /// </summary>
    public override long Key => LineageAddon.Identifier | StyleAddon.Identifier;

    /// <summary>
    /// The layers this system should be working on
    /// </summary>
    public override List<string> Layers => new List<string>() { UIStatic.LayerName };

    /// <summary>
    /// Constructor to build out the UI style bounds system
    /// </summary>
    /// <param name="graphics"></param>
    public UIStyleBoundsSystem(GraphicsDevice graphics)
    {
      _graphics = graphics;
    }

    /// <summary>
    /// Update event that will process the bounds object based on the styles given
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    public override void OnUpdate(IEntity entity)
    {
      var la = entity.GetAddon<LineageAddon>();
      var sa = entity.GetAddon<StyleAddon>();
      var psa = la.Parent?.GetAddon<StyleAddon>();

      var pWidth = (psa?.CalculatedBounds.Width ?? _graphics.Viewport.Width) - ((psa?.CurrentStyle.Padding ?? 0) * 2);
      var pHeight = (psa?.CalculatedBounds.Height ?? _graphics.Viewport.Height) - ((psa?.CurrentStyle.Padding ?? 0) * 2);

      // Process Height Properties
      if (sa.CurrentStyle.Height != null) sa.CalculatedBounds.Height = sa.CurrentStyle.Height.Value;
      else if (sa.CurrentStyle.HeightPercentage != null) sa.CalculatedBounds.Height = (int)Math.Floor(pHeight * sa.CurrentStyle.HeightPercentage.Value);

      // Process Width Properties
      if (sa.CurrentStyle.Width != null) sa.CalculatedBounds.Width = sa.CurrentStyle.Width.Value;
      else if (sa.CurrentStyle.WidthPercentage != null) sa.CalculatedBounds.Width = (int)Math.Floor(pWidth * sa.CurrentStyle.WidthPercentage.Value);

      // Calculate against the nine patch
      if (sa.CurrentStyle.NinePatch != null)
      {
        // Add some pixels to fix the mod before rendering the texture so we have a seemless pattern without clipping
        var widthMod = sa.CalculatedBounds.Width % sa.CurrentStyle.NinePatch.Break.X;
        if (widthMod != 0)
          sa.CalculatedBounds.Width += sa.CurrentStyle.NinePatch.Break.X - widthMod;

        // Add some pixels to fix the mod before rendering the texture so we have a seemless patern without clipping
        var heightMod = sa.CalculatedBounds.Height % sa.CurrentStyle.NinePatch.Break.Y;
        if (heightMod != 0)
          sa.CalculatedBounds.Height += sa.CurrentStyle.NinePatch.Break.X - heightMod;
      }

      // Process Top Offset Properties
      if (sa.CurrentStyle.TopOffset != null) sa.CalculatedBounds.Y = sa.CurrentStyle.TopOffset.Value;
      else
      {
        switch (sa.CurrentStyle.VerticalAlign)
        {
          case VerticalAlign.Center:
            sa.CalculatedBounds.Y = (pHeight - sa.CalculatedBounds.Height) / 2;
            break;
          case VerticalAlign.Bottom:
            sa.CalculatedBounds.Y = pHeight - sa.CalculatedBounds.Height;
            break;
        }
      }

      // Process Left Offset Properties
      if (sa.CurrentStyle.LeftOffset != null) sa.CalculatedBounds.X = sa.CurrentStyle.LeftOffset.Value;
      else
      {
        switch(sa.CurrentStyle.HorizontalAlign)
        {
          case HorizontalAlign.Center:
            sa.CalculatedBounds.X = (pWidth - sa.CalculatedBounds.Width) / 2;
            break;
          case HorizontalAlign.Right:
            sa.CalculatedBounds.X = pWidth - sa.CalculatedBounds.Width;
            break;
        }
      }
    }
  }
}
