using BlueJay.Component.System.Addons;
using BlueJay.Core.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlueJay.UI.EventListeners
{
  /// <summary>
  /// Listener is meant to watch for changes in the bounds and update the internal text to fit the bounds that is currently in
  /// </summary>
  public class UITextStyleUpdateListener : EventListener<StyleUpdateEvent>
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The renderer so we can render the ninepatch to a renderable target
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The global sprite font that should be used
    /// </summary>
    private readonly SpriteFont _font;

    /// <summary>
    /// Constructor to build out the event listener to update the texture for the ninepatch
    /// </summary>
    /// <param name="renderer">The renderer to add things to the graphics device</param>
    public UITextStyleUpdateListener(GraphicsDevice graphics, IRenderer renderer, SpriteFont font)
    {
      _graphics = graphics;
      _renderer = renderer;
      _font = font;
    }

    /// <summary>
    /// Processor to update the ninepatch if the bounds changed for an entity
    /// </summary>
    /// <param name="evt">The event that triggered this listener</param>
    public override void Process(IEvent<StyleUpdateEvent> evt)
    {
      var txt = evt.Data.Entity.GetAddon<TextAddon>();
      if (txt != null)
      {
        var ba = evt.Data.Entity.GetAddon<BoundsAddon>();
        var ta = evt.Data.Entity.GetAddon<TextureAddon>();
        var sa = evt.Data.Entity.GetAddon<StyleAddon>();

        if (ta.Texture != null)
        {
          ta.Texture.Dispose();
          ta.Texture = null;
        }

        var spaceBounds = _font.MeasureString(" ");

        var target = new RenderTarget2D(_graphics, ba.Bounds.Width, ba.Bounds.Height);
        _graphics.SetRenderTarget(target);
        _graphics.Clear(Color.Transparent);

        var words = txt.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var lines = new List<string>();
        var result = string.Empty;
        for(var i = 0; i < words.Length; ++i)
        {
          var bounds = _font.MeasureString(result + words[i]);
          var width = (i - 1 == words.Length) ? bounds.X : bounds.X + spaceBounds.X;
          if (width > ba.Bounds.Width)
          {
            lines.Add(result);
            result = string.Empty;
          }
          else
          {
            result += $"{words[i]} ";
          }
        }
        if (result.Length > 0)
          lines.Add(result);

        result = string.Join("\n", lines);
        var finalBounds = _font.MeasureString(result);
        var pos = Vector2.Zero;
        if (sa.CurrentStyle.TextAlign != null)
        {
          switch (sa.CurrentStyle.TextAlign.Value)
          {
            case TextAlign.Center:
              pos.X = (ba.Bounds.Width - finalBounds.X) / 2;
              break;
            case TextAlign.Left:
              pos.X = ba.Bounds.Width - finalBounds.X;
              break;
          }
        }

        if (sa.CurrentStyle.TextBaseline != null)
        {
          switch (sa.CurrentStyle.TextBaseline.Value)
          {
            case TextBaseline.Center:
              pos.Y = (ba.Bounds.Height - finalBounds.Y) / 2;
              break;
            case TextBaseline.Bottom:
              pos.Y = ba.Bounds.Height - finalBounds.Y;
              break;
          }
        }
        _renderer.DrawString(result, pos, sa.CurrentStyle.TextColor ?? Color.Black);
        _graphics.SetRenderTarget(null);

        ta.Texture = target;
      }
    }
  }
}
