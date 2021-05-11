using BlueJay.App.Games.Breakout;
using BlueJay.App.Games.Breakout.EventListeners;
using BlueJay.App.Games.Breakout.Factories;
using BlueJay.App.Games.Breakout.Systems;
using BlueJay.Component.System.Systems;
using BlueJay.Events.Keyboard;
using BlueJay.Systems;
using BlueJay.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.App.Views
{
  public class BreakOutView : View
  {
    public readonly ContentManager _contentManager;

    public BreakOutView(ContentManager contentManager)
    {
      _contentManager = contentManager;
    }

    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Processing systems
      serviceProvider.AddComponentSystem<KeyboardSystem>();
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);
      serviceProvider.AddComponentSystem<StretchBoundsSystem>();
      serviceProvider.AddComponentSystem<ClampPositionSystem>();
      serviceProvider.AddComponentSystem<BallSystem>();

      // Rendering systems
      serviceProvider.AddComponentSystem<BreakoutRenderingSystem>();

      // Add event listeners that could happen in the system
      serviceProvider.AddEventListener<KeyboardPressEventListener, KeyboardPressEvent>();
      serviceProvider.AddEventListener<StartBallEventListener, StartBallEvent>();
      serviceProvider.AddEventListener<EndGameEventListener, EndGameEvent>();

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
