using BlueJay.App.Games.Breakout;
using BlueJay.App.Games.Breakout.EventListeners;
using BlueJay.App.Games.Breakout.Factories;
using BlueJay.App.Games.Breakout.Systems;
using BlueJay.Component.System.Systems;
using BlueJay.Events;
using BlueJay.Events.Keyboard;
using BlueJay.Systems;
using BlueJay.Views;
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
    public readonly ContentManager _contentManager;

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
      // Processing systems
      serviceProvider.AddComponentSystem<KeyboardSystem>();
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);
      serviceProvider.AddComponentSystem<ClampPositionSystem>();
      serviceProvider.AddComponentSystem<BallSystem>();

      // Rendering systems
      serviceProvider.AddComponentSystem<BreakoutRenderingSystem>();

      // Add event listeners that could happen in the system
      serviceProvider.AddEventListener<KeyboardPressEventListener, KeyboardPressEvent>();
      serviceProvider.AddEventListener<StartBallEventListener, StartBallEvent>();
      serviceProvider.AddEventListener<EndGameEventListener, EndGameEvent>();
      serviceProvider.AddEventListener<StretchBoundsViewportChangeListener, ViewportChangeEvent>();

      // Add Game Entities
      serviceProvider.AddPaddle();
      serviceProvider.AddBall(_contentManager.Load<Texture2D>("Circle"));
      for (var i = 0; i < 20; ++i)
      {
        serviceProvider.AddBlock(i);
      }
    }
  }
}
