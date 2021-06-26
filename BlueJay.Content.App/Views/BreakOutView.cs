using BlueJay.Content.App.Games.Breakout;
using BlueJay.Content.App.Games.Breakout.EventListeners;
using BlueJay.Content.App.Games.Breakout.Factories;
using BlueJay.Content.App.Games.Breakout.Systems;
using BlueJay.Component.System;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Systems;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Keyboard;
using BlueJay.Events.Touch;
using BlueJay.Systems;
using BlueJay.UI;
using BlueJay.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using BlueJay.UI.Components;
using BlueJay.Content.App.Components;

namespace BlueJay.Content.App.Views
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
      serviceProvider.AddUITouchSupport();
      serviceProvider.AddUIMouseSupport();
      serviceProvider.AddComponentSystem<ClampPositionSystem>();
      serviceProvider.AddComponentSystem<BallSystem>();

      // Rendering systems
      serviceProvider.AddComponentSystem<BreakoutRenderingSystem>();
      serviceProvider.AddComponentSystem<RenderingSystem>(serviceProvider.GetRequiredService<RendererCollection>()[RendererName.Default]);
      serviceProvider.AddComponentSystem<ParticleSystem>(serviceProvider.GetRequiredService<RendererCollection>()[RendererName.Default]);

      // Add event listeners that could happen in the system
      serviceProvider.AddEventListener<KeyboardPressEventListener, KeyboardPressEvent>();
      serviceProvider.AddEventListener<StartBallEventListener, StartBallEvent>();
      serviceProvider.AddEventListener<UpdateBoundsEventListener, UpdateBoundsEvent>();
      serviceProvider.AddEventListener<ViewportChangeEventListener, ViewportChangeEvent>();
      serviceProvider.AddEventListener<LostBallEventListener, LostBallEvent>();
      serviceProvider.AddEventListener<NextRoundEventListener, NextRoundEvent>();
      serviceProvider.AddEventListener<TouchMoveEventListener, TouchMoveEvent>();
      serviceProvider.AddEventListener<TouchUpEventListener, TouchUpEvent>();

      // Add Game Entities
      serviceProvider.AddPaddle();

      // Add UI Component
      serviceProvider.AddUIComponent<BreakoutViewComponent>();
    }

    /// <summary>
    /// The enter method is meant to trigger when this view is set as the current
    /// </summary>
    public override void Enter()
    {
      // Initialize the service collection
      var service = ServiceProvider.GetRequiredService<BreakoutGameService>();
      service.Initialize(ServiceProvider.GetRequiredService<UIComponentCollection>());

      // Queue for the round to start over
      var queue = ServiceProvider.GetRequiredService<EventQueue>();
      queue.DispatchEvent(new NextRoundEvent());
    }
  }
}
