using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.UI.Events.EventListeners.UIUpdate
{
  internal class UITextUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly ILayerCollection _layers;

    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <summary>
    /// The global sprite font that should be used
    /// </summary>
    private readonly IFontCollection _fonts;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    /// <param name="graphics">The current graphic device we are working with</param>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="fonts">The global sprite font that should be used</param>
    public UITextUIUpdateEventListener(ILayerCollection layers, GraphicsDevice graphics, SpriteBatch batch, IFontCollection fonts)
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
      if (_layers.Contains(UIStatic.LayerName))
      {
        var layer = _layers[UIStatic.LayerName];
        if (layer != null)
          foreach (var entity in layer.AsSpan())
            ProcessEntity(entity);
      }
    }

    /// <summary>
    /// Process method is meant to process the width for each entity
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    /// <param name="evt">The event that triggered the change to the UI</param>
    private void ProcessEntity(IEntity entity)
    {
      if (entity.MatchKey(KeyHelper.Create<TextAddon, StyleAddon>()))
      {
        var txt = entity.GetAddon<TextAddon>();
        var sa = entity.GetAddon<StyleAddon>();

        if (sa.CalculatedBounds.Height == 0 && sa.CalculatedBounds.Width > 0)
        {
          var ta = entity.GetAddon<TextureAddon>();

          if (ta.Texture != null)
            ta.Texture.Dispose();

          if (!string.IsNullOrEmpty(txt.Text))
          {
            var result = entity.FitString(txt.Text.Trim(), sa.CalculatedBounds.Width, _fonts);
            var finalBounds = entity.MeasureString(result, _fonts);
            var pos = Vector2.Zero;

            switch (entity.GetStyle(x => x.TextAlign) ?? TextAlign.Center)
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

            if (sa.CalculatedBounds.Width != 0 && sa.CalculatedBounds.Height != 0)
            {
              // Generate texture and add it to the texture addon so it can be rendered to the screen
              var target = new RenderTarget2D(_graphics, sa.CalculatedBounds.Width, sa.CalculatedBounds.Height);
              _graphics.SetRenderTarget(target);
              _graphics.Clear(Color.Transparent);
              _batch.Begin();

              if (entity.TryGetStyle(x => x.Font, out var font) && font != null)
                _batch.DrawString(_fonts.SpriteFonts[font], result, pos, entity.GetStyle(x => x.TextColor) ?? Color.Black);
              else if (entity.TryGetStyle(x => x.TextureFont, out var textureFont) && textureFont != null)
                _batch.DrawString(_fonts.TextureFonts[textureFont], result, pos, entity.GetStyle(x => x.TextColor) ?? Color.Black, entity.GetStyle(x => x.TextureFontSize) ?? 1);
              _batch.End();
              _graphics.SetRenderTarget(null);

              ta.Texture = target;
            }

            entity.Update(sa);
          }
          entity.Update(ta);
        }
      }
    }
  }
}
