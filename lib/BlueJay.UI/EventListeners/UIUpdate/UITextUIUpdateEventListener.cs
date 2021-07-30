using BlueJay.Common.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
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
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <summary>
    /// The global sprite font that should be used
    /// </summary>
    private readonly FontCollection _fonts;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    /// <param name="graphics">The current graphic device we are working with</param>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="fonts">The global sprite font that should be used</param>
    public UITextUIUpdateEventListener(LayerCollection layers, GraphicsDevice graphics, SpriteBatch batch, FontCollection fonts)
    {
      _layers = layers;
      _batch = batch;
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

      if (!string.IsNullOrEmpty(txt.Text) && sa.CalculatedBounds.Height == 0 && sa.CalculatedBounds.Width > 0)
      {
        var ta = entity.GetAddon<TextureAddon>();

        if (ta.Texture != null)
        {
          ta.Texture.Dispose();
          ta.Texture = null;
        }

        var spaceBounds = MeasureString(" ", entity);
        var words = txt.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var lines = new List<string>();
        var result = string.Empty;
        for (var i = 0; i < words.Length; ++i)
        {
          var bounds = MeasureString(result + words[i], entity);
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
        var finalBounds = MeasureString(result, entity);
        var pos = Vector2.Zero;

        switch (GetStyle(entity, x => x.TextAlign) ?? TextAlign.Center)
        {
          case TextAlign.Center:
            pos.X = (sa.CalculatedBounds.Width - finalBounds.X) / 2;
            break;
          case TextAlign.Right:
            pos.X = sa.CalculatedBounds.Width - finalBounds.X;
            break;
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
        _batch.Begin();
        if (TryGetStyle(entity, x => x.Font, out var font))
          _batch.DrawString(_fonts.SpriteFonts[font], result, pos, GetStyle(entity, x => x.TextColor) ?? Color.Black);
        else if (TryGetStyle(entity, x => x.TextureFont, out var textureFont))
          _batch.DrawString(_fonts.TextureFonts[textureFont], result, pos, GetStyle(entity, x => x.TextColor) ?? Color.Black, GetStyle(entity, x => x.TextureFontSize) ?? 1);
        _batch.End();
        _graphics.SetRenderTarget(null);

        ta.Texture = target;

        entity.Update(sa);
        entity.Update(ta);
      }
    }

    private Vector2 MeasureString(string str, IEntity entity)
    {
      if (TryGetStyle(entity, x => x.Font, out var font))
        return _fonts.SpriteFonts[font].MeasureString(str);
      if (TryGetStyle(entity, x => x.TextureFont, out var textureFont))
        return _fonts.TextureFonts[textureFont].MeasureString(str, GetStyle(entity, x => x.TextureFontSize) ?? 1);
      return Vector2.Zero;
    }

    private bool TryGetStyle<T>(IEntity entity, Func<Style, T> expression, out T output)
    {
      output = GetStyle(entity, expression);
      return output != null;
    }

    private T GetStyle<T>(IEntity entity, Func<Style, T> expression)
    {
      while (entity != null)
      {
        var sa = entity.GetAddon<StyleAddon>();
        var la = entity.GetAddon<LineageAddon>();

        var style = expression(sa.CurrentStyle);
        if (style != null)
          return style;

        entity = la.Parent;
      }
      return default;
    }
  }
}
