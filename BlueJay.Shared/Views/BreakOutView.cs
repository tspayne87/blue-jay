﻿using BlueJay.Shared.Games.Breakout;
using BlueJay.Shared.Games.Breakout.EventListeners;
using BlueJay.Shared.Games.Breakout.Factories;
using BlueJay.Shared.Games.Breakout.Systems;
using BlueJay.Events;
using BlueJay.Common.Events.Keyboard;
using BlueJay.Common.Events.Touch;
using BlueJay.Common.Systems;
using BlueJay.UI;
using BlueJay.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using System;
using BlueJay.UI.Component;
using BlueJay.Shared.Components;
using BlueJay.Events.Interfaces;
using BlueJay.Common.Events;

namespace BlueJay.Shared.Views
{
  /// <summary>
  /// Breakout game broken up into a view to handle the entities in a scoped manner
  /// </summary>
  public class BreakOutView : View
  {
    /// <summary>
    /// Configuration method is meant to add in all the systems that this game will use and bootstrap the game with entities
    /// that will be used by the game and its systems
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to add the entities and systems to</param>
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Processing systems
      serviceProvider.AddSystem<KeyboardSystem>();
      serviceProvider.AddSystem<ViewportSystem>();
      serviceProvider.AddSystem<ClearSystem>(Color.White);
      serviceProvider.AddUISystems();
      serviceProvider.AddUITouchSupport();
      serviceProvider.AddUIKeyboardSupport();
      serviceProvider.AddUIMouseSupport();
      serviceProvider.AddUIComponentSystems();
      serviceProvider.AddSystem<ClampPositionSystem>();
      serviceProvider.AddSystem<BallSystem>();

      // Rendering systems
      serviceProvider.AddSystem<BreakoutRenderingSystem>();
      serviceProvider.AddSystem<RenderingSystem>();

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
      serviceProvider.AttachComponent<BreakoutViewComponent>();
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
      var queue = ServiceProvider.GetRequiredService<IEventQueue>();
      queue.DispatchEvent(new NextRoundEvent());
    }
  }
}
