using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.EventListeners.UIUpdate
{
  public class UITextUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly LayerCollection _layers;

    /// <summary>
    /// The renderer so we can render the ninepatch to a renderable target
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The global sprite font that should be used
    /// </summary>
    private readonly FontCollection _fonts;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UITextUIUpdateEventListener(LayerCollection layers, GraphicsDevice graphics, IRenderer renderer, FontCollection fonts)
    {
      _layers = layers;
      _renderer = renderer;
      _graphics = graphics;
      _fonts = fonts;
    }


    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<UIUpdateEvent> evt)
    {
      for (var i = 0; i < _layers[UIStatic.LayerName].Entities.Count; ++i)
      {
        ProcessEntity(_layers[UIStatic.LayerName].Entities[i]);
      }
    }

    /// <summary>
    /// Process method is meant to process the width for each entity
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    /// <param name="evt">The event that triggered the change to the UI</param>
    private void ProcessEntity(IEntity entity)
    {
      var txt = entity.GetAddon<TextAddon>();
      var sa = entity.GetAddon<StyleAddon>();

      if (txt != null && sa.CalculatedBounds.Height == 0 && sa.CalculatedBounds.Width > 0)
      {
        var ta = entity.GetAddon<TextureAddon>();

        if (ta.Texture != null)
        {
          ta.Texture.Dispose();
          ta.Texture = null;
        }

        var spaceBounds = MeasureString(" ", sa.Style);
        var words = txt.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var lines = new List<string>();
        var result = string.Empty;
        for (var i = 0; i < words.Length; ++i)
        {
          var bounds = MeasureString(result + words[i], sa.Style);
          var width = (i - 1 == words.Length) ? bounds.X : bounds.X + spaceBounds.X;
          if (width > sa.CalculatedBounds.Width)
          {
            if (!string.IsNullOrEmpty(result))
              lines.Add(result);
            result = $"{words[i]} ";
          }
          else
          {
            result += $"{words[i]} ";
          }
        }
        if (result.Length > 0)
          lines.Add(result);

        result = string.Join("\n", lines.Select(x => x.Trim()));
        var finalBounds = MeasureString(result, sa.Style);
        var pos = Vector2.Zero;
        if (sa.CurrentStyle.TextAlign != null)
        {
          switch (sa.CurrentStyle.TextAlign.Value)
          {
            case TextAlign.Center:
              pos.X = (sa.CalculatedBounds.Width - finalBounds.X) / 2;
              break;
            case TextAlign.Left:
              pos.X = sa.CalculatedBounds.Width - finalBounds.X;
              break;
          }
        }

        // Calculate the text height based on the bounds of the generate text
        if (sa.CalculatedBounds.Height == 0)
        {
          sa.CalculatedBounds.Height = (int)Math.Ceiling(finalBounds.Y) + ((sa.CurrentStyle.Padding ?? 0) * 2);
        }

        // Generate texture and add it to the texture addon so it can be rendered to the screen
        var target = new RenderTarget2D(_graphics, sa.CalculatedBounds.Width, sa.CalculatedBounds.Height);
        _graphics.SetRenderTarget(target);
        _graphics.Clear(Color.Transparent);
        if (!string.IsNullOrEmpty(sa.Style.Font) && _fonts.SpriteFonts.ContainsKey(sa.Style.Font))
          _renderer.DrawString(_fonts.SpriteFonts[sa.Style.Font], result, pos, sa.CurrentStyle.TextColor ?? Color.Black);
        if (!string.IsNullOrEmpty(sa.Style.TextureFont) && _fonts.TextureFonts.ContainsKey(sa.Style.TextureFont))
          _renderer.DrawString(_fonts.TextureFonts[sa.Style.TextureFont], result, pos, sa.CurrentStyle.TextColor ?? Color.Black, sa.Style.TextureFontSize);
        _graphics.SetRenderTarget(null);

        ta.Texture = target;
      }
    }

    private Vector2 MeasureString(string str, Style style)
    {
      if (!string.IsNullOrEmpty(style.Font) && _fonts.SpriteFonts.ContainsKey(style.Font))
        return _fonts.SpriteFonts[style.Font].MeasureString(str);
      if (!string.IsNullOrEmpty(style.TextureFont) && _fonts.TextureFonts.ContainsKey(style.TextureFont))
        return _fonts.TextureFonts[style.TextureFont].MeasureString(str, style.TextureFontSize);
      return Vector2.Zero;
    }
  }
}
