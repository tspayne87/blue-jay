using BlueJay.Component.System.Systems;
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
using BlueJay.Systems;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System;
using BlueJay.UI.Components;
using BlueJay.Content.App.Components;

namespace BlueJay.Content.App.Views
{
  /// <summary>
  /// The title view to switch between sample games for BlueJay
  /// </summary>
  public class TitleView : View
  {
    /// <summary>
    /// The content manger meant to load the one texture used by breakout
    /// </summary>
    public readonly ContentManager _contentManager;

    /// <summary>
    /// Constructor is meant to inject the global content manger into the system
    /// </summary>
    public TitleView(ContentManager contentManager)
    {
      _contentManager = contentManager;
    }

    /// <summary>
    /// Configuration method is meant to add in all the systems that this game will use and bootstrap the game with entities
    /// that will be used by the game and its systems
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to add the entities and systems to</param>
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
      serviceProvider.AddUITouchSupport();

      serviceProvider.AddComponentSystem<RenderingSystem>(serviceProvider.GetRequiredService<RendererCollection>()[RendererName.Default]);
      serviceProvider.AddComponentSystem<FPSSystem>("Default");

      serviceProvider.AddUIComponent<TitleViewComponent>();
    }

    /// <summary>
    /// The basic title view
    /// </summary>
    [View(@"
<container style=""WidthPercentage: 0.66; TopOffset: 50; HorizontalAlign: Center; GridColumns: 3; ColumnGap: 5, 5; NinePatch: Sample_NinePatch; Padding: 13"">
  <text style=""ColumnSpan: 3; Padding: 15; TextureFont: Default; TextureFontSize: 2"">BlueJay Component System</text>

  <button onSelect=""OnBreakoutClick"">{{BreakoutTitle}}</button>
  <button onSelect=""OnTetrisClick"">{{TetrisTitle}}</button>
</container>
    ")]
    [Component(typeof(Button))]
    private class TitleViewComponent
    {
      /// <summary>
      /// The view collection we will use to transition between games
      /// </summary>
      private IViewCollection _views;

      /// <summary>
      /// The breakout title we should be using for breakout
      /// </summary>
      public ReactiveProperty<string> BreakoutTitle;

      /// <summary>
      /// The Tetris title we are using for this component
      /// </summary>
      public ReactiveProperty<string> TetrisTitle;

      /// <summary>
      /// Constructor is meant to bootstrap the component
      /// </summary>
      /// <param name="views">The views collection we need to switch between views</param>
      public TitleViewComponent(IViewCollection views)
      {
        _views = views;

        BreakoutTitle = new ReactiveProperty<string>("Breakout");
        TetrisTitle = new ReactiveProperty<string>("Tetris");
      }

      /// <summary>
      /// Callback method that is triggered when the user clicks the element in the component
      /// </summary>
      /// <param name="evt">The select event</param>
      /// <returns>will return true to continue propegation</returns>
      public bool OnBreakoutClick(SelectEvent evt)
      {
        _views.SetCurrent<BreakOutView>();
        return true;
      }

      /// <summary>
      /// Callback method that is triggered when the user clicks the element in the component
      /// </summary>
      /// <param name="evt">The select event</param>
      /// <returns>will return true to continue propegation</returns>
      public bool OnTetrisClick(SelectEvent evt)
      {
        // TODO: Set Current to Tetris
        return true;
      }
    }
  }
}
