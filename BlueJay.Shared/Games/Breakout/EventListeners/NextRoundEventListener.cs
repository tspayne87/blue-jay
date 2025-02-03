using BlueJay.Shared.Games.Breakout.Factories;
using BlueJay.Component.System.Collections;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Container;
using BlueJay.Utils;
using BlueJay.Component.System;
using System.Linq;

namespace BlueJay.Shared.Games.Breakout.EventListeners
{
  public class NextRoundEventListener : EventListener<NextRoundEvent>
  {
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
    private readonly IContentManagerContainer _contentManager;

    /// <summary>
    /// The current graphic device for the screen
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The current event queue that is being processed
    /// </summary>
    private readonly IEventQueue _eventQueue;

    private readonly IQuery _query;

    /// <summary>
    /// Constructor is meant to inject various items to be used in this system
    /// </summary>
    /// <param name="provider">The service provider we will use to add a new ball from</param>
    /// <param name="service">The game service meant to update the UI based on the different items</param>
    /// <param name="contentManager">The content manager ment to load textures and other content types</param>
    /// <param name="graphics">The graphics device that is represents the screen</param>
    /// <param name="eventQueue">The event queue that will be used to send out events to the system</param>
    public NextRoundEventListener(IServiceProvider provider, BreakoutGameService service, IContentManagerContainer contentManager, GraphicsDevice graphics, IEventQueue eventQueue, IQuery query)
    {
      _provider = provider;
      _service = service;
      _contentManager = contentManager;
      _graphics = graphics;
      _eventQueue = eventQueue;
      _query = query;
    }

    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public override void Process(IEvent<NextRoundEvent> evt)
    {
      _provider.ClearEntities(LayerNames.BallLayer);
      _provider.ClearEntities(LayerNames.BlockLayer);
 
      // Start the new game with the blocks and ball added
      _service.Round++;
      _provider.AddBall(_contentManager.Load<ITexture2DContainer>("Circle"));
      for (var i = 0; i < 20; ++i)
        _provider.AddBlock(i);

      // Dispatch event to trigger and a re-render of the blocks
      _eventQueue.DispatchEvent(new UpdateBoundsEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
    }
  }
}
