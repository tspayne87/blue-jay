using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.UI;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.App.Games.Breakout
{
  /// <summary>
  /// Service is meant to handle the current state of the game
  /// </summary>
  public class BreakoutGameService
  {
    /// <summary>
    /// The event queue that will trigger style update events to rerender the UI entity
    /// </summary>
    private readonly EventQueue _eventQueue;

    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly LayerCollection _layers;

    /// <summary>
    /// The graphics device that is connected to the screen that is rendered
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The score UI entity we need to update
    /// </summary>
    private IEntity _scoreEntity;

    /// <summary>
    /// The round UI entity we need to update
    /// </summary>
    private IEntity _roundEntity;

    /// <summary>
    /// The ball UI entity we need to update
    /// </summary>
    private IEntity _ballEntity;

    /// <summary>
    /// The internal score value
    /// </summary>
    private int _score;

    /// <summary>
    /// The internal round value
    /// </summary>
    private int _round;

    /// <summary>
    /// The internal balls value
    /// </summary>
    private int _balls;

    /// <summary>
    /// The current score the player has
    /// </summary>
    public int Score { get => _score; set { _score = value; SetText(_scoreEntity, $"Score: {value}"); } }

    /// <summary>
    /// The current round the player is on
    /// </summary>
    public int Round { get => _round; set { _round = value; SetText(_roundEntity, $"Round: {value}"); } }

    /// <summary>
    /// The number of balls that the player currently has available to them
    /// </summary>
    public int Balls { get => _balls; set { _balls = value; SetText(_ballEntity, $"Balls: {Math.Max(value, 0)}"); } }

    /// <summary>
    /// Constructor to build out the service for the breakout game
    /// </summary>
    /// <param name="layers">The layers that exist in the current game</param>
    /// <param name="eventQueue">The queue so we can trigger and event to update the UI</param>
    /// <param name="graphics">The graphics device that is bound to the screen</param>
    public BreakoutGameService(LayerCollection layers, EventQueue eventQueue, GraphicsDevice graphics)
    {
      _eventQueue = eventQueue;
      _layers = layers;
      _graphics = graphics;
    }

    /// <summary>
    /// Method is meant to initialize the service with the different entities and the start of the game
    /// </summary>
    public void Initialize()
    {
      var lastIndex = _layers[UIStatic.LayerName].Entities.Count - 1;
      _scoreEntity = _layers[UIStatic.LayerName].Entities[lastIndex];
      _ballEntity = _layers[UIStatic.LayerName].Entities[lastIndex - 1];
      _roundEntity = _layers[UIStatic.LayerName].Entities[lastIndex - 2];

      Score = 0;
      Round = 0;
      Balls = 3;
    }

    /// <summary>
    /// Private method is meant to set the text for the UI entity and rerender the updated text to the screen
    /// </summary>
    /// <param name="entity">The entity we are updating the text for</param>
    /// <param name="txt">The current text we need to assign to the entity</param>
    private void SetText(IEntity entity, string txt)
    {
      var ta = entity.GetAddon<TextAddon>();
      if (ta != null)
      {
        ta.Text = txt;
        _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
      }
    }
  }
}
