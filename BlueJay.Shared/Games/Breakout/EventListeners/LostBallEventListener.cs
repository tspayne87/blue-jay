using BlueJay.Shared.Games.Breakout.Factories;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Container;
using BlueJay.Utils;
using System.Linq;
using BlueJay.Component.System;

namespace BlueJay.Shared.Games.Breakout.EventListeners
{
  /// <summary>
  /// Listener to determine what happens after a ball is lost
  /// </summary>
  public class LostBallEventListener : EventListener<LostBallEvent>
  {
    /// <summary>
    /// The current query to get the balls in the label
    /// </summary>
    private readonly IQuery _query;

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
    /// Constructor is meant to inject various items to be used in this system
    /// </summary>
    /// <param name="layerCollection">The layer collection that has all the entities in the game at the moment</param>
    /// <param name="provider">The service provider we will use to add a new ball from</param>
    /// <param name="service">The game service meant to update the UI based on the different items</param>
    /// <param name="contentManager">The content manager ment to load textures and other content types</param>
    public LostBallEventListener(IServiceProvider provider, BreakoutGameService service, IContentManagerContainer contentManager, IQuery query)
    {
      _provider = provider;
      _service = service;
      _contentManager = contentManager;
      _query = query.WhereLayer(LayerNames.BallLayer);
    }

    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public override void Process(IEvent<LostBallEvent> evt)
    {
      var ball = _query.FirstOrDefault();
      if (ball != null)
      {
        _provider.RemoveEntity(ball);
        var spawnBall = _service.Balls - 1 >= 0;
        _service.Balls--;

        if (spawnBall)
        {
          _provider.AddBall(_contentManager.Load<ITexture2DContainer>("Circle"));
        }
      }
    }
  }
}
