﻿using BlueJay.Shared.Games.Breakout.Factories;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Container;

namespace BlueJay.Shared.Games.Breakout.EventListeners
{
  /// <summary>
  /// Listener to determine what happens after a ball is lost
  /// </summary>
  public class LostBallEventListener : EventListener<LostBallEvent>
  {
    /// <summary>
    /// The layer collection that has all the entities in the game at the moment
    /// </summary>
    private readonly ILayerCollection _layerCollection;

    /// <summary>
    /// The event queue we want to dispatch events too
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// The current service provider
    /// </summary>
    private readonly IServiceProvider _provider;

    /// <summary>
    /// The game service that is meant to process the different states of the game
    /// </summary>
    private readonly BreakoutGameService _service;

    /// <summary>
    /// The current content manager meant to load different type of textures
    /// </summary>
    private readonly ContentManager _contentManager;

    /// <summary>
    /// Constructor is meant to inject various items to be used in this system
    /// </summary>
    /// <param name="layerCollection">The layer collection that has all the entities in the game at the moment</param>
    /// <param name="eventQueue">The event queue we want to dispatch events too</param>
    /// <param name="provider">The service provider we will use to add a new ball from</param>
    /// <param name="service">The game service meant to update the UI based on the different items</param>
    /// <param name="contentManager">The content manager ment to load textures and other content types</param>
    public LostBallEventListener(ILayerCollection layerCollection, IEventQueue eventQueue, IServiceProvider provider, BreakoutGameService service, ContentManager contentManager)
    {
      _layerCollection = layerCollection;
      _eventQueue = eventQueue;
      _provider = provider;
      _service = service;
      _contentManager = contentManager;
    }

    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public override void Process(IEvent<LostBallEvent> evt)
    {
      if (_layerCollection[LayerNames.BallLayer].Count == 1)
      {
        var ball = _layerCollection[LayerNames.BallLayer][0];
        _layerCollection[LayerNames.BallLayer].Remove(ball);
        _service.Balls--;

        if (_service.Balls >= 0)
        {
          _provider.AddBall(_contentManager.Load<ITexture2DContainer>("Circle"));
        }
      }
    }
  }
}
