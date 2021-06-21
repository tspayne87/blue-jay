using BlueJay.UI.Components;
using System;

namespace BlueJay.Content.App.Games.Breakout
{
  /// <summary>
  /// Service is meant to handle the current state of the game
  /// </summary>
  public class BreakoutGameService
  {
    /// <summary>
    /// The UI component that has all the data needed to update on the component
    /// </summary>
    private BreakoutViewComponent _uiComponent;

    /// <summary>
    /// The current score the player has
    /// </summary>
    public int Score { get => _uiComponent?.Score.Value ?? 0; set { if (_uiComponent != null) _uiComponent.Score.Value = value; } }

    /// <summary>
    /// The current round the player is on
    /// </summary>
    public int Round { get => _uiComponent?.Round.Value ?? 0; set { if (_uiComponent != null) _uiComponent.Round.Value = value; } }

    /// <summary>
    /// The number of balls that the player currently has available to them
    /// </summary>
    public int Balls { get => _uiComponent?.Balls.Value ?? 0; set { if (_uiComponent != null) _uiComponent.Balls.Value = Math.Max(value, 0); } }

    /// <summary>
    /// Constructor meant to be a wrapper around the UI component so that we can update the various things
    /// </summary>
    /// <param name="collection">The UI Collection</param>
    public void Initialize(UIComponentCollection collection)
    {
      _uiComponent = collection.GetItem<BreakoutViewComponent>();
    }
  }
}
