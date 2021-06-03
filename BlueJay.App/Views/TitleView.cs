﻿using BlueJay.Component.System.Systems;
using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using BlueJay.UI;
using BlueJay.UI.Factories;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BlueJay.Core;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Mouse;
using Microsoft.Extensions.DependencyInjection;
using BlueJay.Interfaces;

namespace BlueJay.App.Views
{
  public class TitleView : View
  {
    public readonly ContentManager _contentManager;

    public TitleView(ContentManager contentManager)
    {
      _contentManager = contentManager;
    }

    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Add Fonts
      serviceProvider.AddSpriteFont("Default", _contentManager.Load<SpriteFont>("TestFont"));
      var fontTexture = _contentManager.Load<Texture2D>("Bitmap-Font");
      serviceProvider.AddTextureFont("Default", new TextureFont(fontTexture, 3, 24));

      // Add Processor Systems
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);
      serviceProvider.AddUISystems();
      serviceProvider.AddUIMouseSupport();

      // Add Rendering Systems
      serviceProvider.AddComponentSystem<RenderingSystem>();
      serviceProvider.AddComponentSystem<FPSSystem>("Default");

      // Create layout and a button
      var container = serviceProvider.AddContainer(new Style() { WidthPercentage = 0.66f, TopOffset = 50, HorizontalAlign = HorizontalAlign.Center, GridColumns = 3, ColumnGap = new Point(5, 5), NinePatch = new NinePatch(_contentManager.Load<Texture2D>("Sample_NinePatch")), Padding = 13 });

      serviceProvider.AddText("BlueJay Component System", new Style() { ColumnSpan = 3, Padding = 15, TextureFont = "Default", TextureFontSize = 2 }, container);


      var views = serviceProvider.GetRequiredService<IViewCollection>();
      AddButton(serviceProvider, "Breakout", container, evt =>
      {
        views.SetCurrent<BreakOutView>();
        return true;
      });
      AddButton(serviceProvider, "Tetris", container, evt =>
      {
        // TODO: Set Current to Tetris
        return true;
      }, new Style() { ColumnOffset = 1 });
    }

    private IEntity AddButton(IServiceProvider serviceProvider, string text, IEntity parent, Func<MouseDownEvent, bool> callback, Style style = null)
    {
      var baseStyle = new Style() { NinePatch = new NinePatch(_contentManager.Load<Texture2D>("Sample_NinePatch")), Padding = 13 };
      baseStyle.Parent = style;
      var btn = serviceProvider.AddContainer(
        baseStyle,
        new Style() { NinePatch = new NinePatch(_contentManager.Load<Texture2D>("Sample_Hover_NinePatch")) },
        parent
      );
      var txt = serviceProvider.AddText(text, new Style() { TextureFont = "Default" }, btn);

      // Add Event Listeners to both
      serviceProvider.AddEventListener(callback, btn);
      serviceProvider.AddEventListener(callback, txt);
      return btn;
    }
  }
}
