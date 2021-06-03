using BlueJay.App.Games.Breakout;
using BlueJay.App.Games.Breakout.EventListeners;
using BlueJay.App.Games.Breakout.Factories;
using BlueJay.App.Games.Breakout.Systems;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Keyboard;
using BlueJay.Events.Mouse;
using BlueJay.Interfaces;
using BlueJay.Systems;
using BlueJay.UI;
using BlueJay.UI.Factories;
using BlueJay.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.App.Views
{
  /// <summary>
  /// Breakout game broken up into a view to handle the entities in a scoped manner
  /// </summary>
  public class BreakOutView : View
  {
    /// <summary>
    /// The content manger meant to load the one texture used by breakout
    /// </summary>
    private readonly ContentManager _contentManager;

    /// <summary>
    /// Constructor is meant to inject the global content manger into the system
    /// </summary>
    /// <param name="contentManager">The global content manager</param>
    public BreakOutView(ContentManager contentManager)
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

      // Processing systems
      serviceProvider.AddComponentSystem<KeyboardSystem>();
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);
      serviceProvider.AddUISystems();
      serviceProvider.AddUIMouseSupport();
      serviceProvider.AddComponentSystem<ClampPositionSystem>();
      serviceProvider.AddComponentSystem<BallSystem>();

      // Rendering systems
      serviceProvider.AddComponentSystem<BreakoutRenderingSystem>();
      serviceProvider.AddComponentSystem<RenderingSystem>();
      serviceProvider.AddComponentSystem<ParticleSystem>();

      // Add event listeners that could happen in the system
      serviceProvider.AddEventListener<KeyboardPressEventListener, KeyboardPressEvent>();
      serviceProvider.AddEventListener<StartBallEventListener, StartBallEvent>();
      serviceProvider.AddEventListener<UpdateBoundsEventListener, UpdateBoundsEvent>();
      serviceProvider.AddEventListener<ViewportChangeEventListener, ViewportChangeEvent>();
      serviceProvider.AddEventListener<LostBallEventListener, LostBallEvent>();
      serviceProvider.AddEventListener<NextRoundEventListener, NextRoundEvent>();

      // Add Game Entities
      serviceProvider.AddPaddle();

      // Add the UI elements for the game
      var container = serviceProvider.AddContainer(new Style() { GridColumns = 5, ColumnGap = new Point(5, 5) });
      var views = serviceProvider.GetRequiredService<IViewCollection>();
      AddButton(serviceProvider, "Back To Title", container, evt =>
      {
        views.SetCurrent<TitleView>();
        return true;
      }, new Style() { ColumnSpan = 2 });

      var dataContainer = serviceProvider.AddContainer(new Style() { GridColumns = 3, ColumnGap = new Point(10, 10), ColumnSpan = 3 }, container);
      serviceProvider.AddText("Round:0", new Style() { TextureFont = "Default" }, dataContainer);
      serviceProvider.AddText("Balls:0", new Style() { TextureFont = "Default" }, dataContainer);
      serviceProvider.AddText("Score:0", new Style() { TextureFont = "Default" }, dataContainer);
    }

    /// <summary>
    /// The enter method is meant to trigger when this view is set as the current
    /// </summary>
    public override void Enter()
    {
      // Initialize the game state
      var service = ServiceProvider.GetRequiredService<BreakoutGameService>();
      service.Initialize();

      // Queue for the round to start over
      var queue = ServiceProvider.GetRequiredService<EventQueue>();
      queue.DispatchEvent(new NextRoundEvent());
    }

    /// <summary>
    /// Add Button to the UI
    /// </summary>
    /// <param name="serviceProvider">The service provider where entities should be added</param>
    /// <param name="text">The text of the button</param>
    /// <param name="parent">The current parent of this button</param>
    /// <param name="callback">The callback function that should be triggered if this button is called</param>
    /// <param name="style">The overriding style that should be included for this button</param>
    /// <returns>Will return the entity that was created</returns>
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
