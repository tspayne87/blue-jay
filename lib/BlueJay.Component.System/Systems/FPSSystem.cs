﻿using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace BlueJay.Component.System.Systems
{
  /// <summary>
  /// FPS system is meant to print out on the screen the current FPS for the game
  /// </summary>
  public class FPSSystem : ComponentSystem
  {
    /// <summary>
    /// The renderer to print the fps to
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The delta service that is meant to be updated every frame
    /// </summary>
    private readonly IDeltaService _deltaService;

    /// <summary>
    /// The current fps for the system
    /// </summary>
    private int _fps = 0;

    /// <summary>
    /// How many updates have happened
    /// </summary>
    private int _updates = 0;

    /// <summary>
    /// The count down to a second based on the delta
    /// </summary>
    private int _countdown = 1000;

    /// <summary>
    /// Do not specify an entity and just use the based draw and update steps
    /// </summary>
    public override long Key => 0;

    /// <summary>
    /// Constructor to build out the fps system
    /// </summary>
    /// <param name="renderer">The current renderer to print stuff on the screen</param>
    /// <param name="deltaService">The current delta that gets updated every frame</param>
    public FPSSystem(IRenderer renderer, IDeltaService deltaService)
    {
      _renderer = renderer;
      _deltaService = deltaService;
    }

    /// <summary>
    /// Update event is meant to track how many times this method is called in a second
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    public override void OnUpdate()
    {
      _updates++;
      _countdown -= _deltaService.Delta;
      if (_countdown <= 0)
      {
        _fps = _updates;
        _updates = 0;
        _countdown += 1000;
      }
    }

    /// <summary>
    /// Draw event is meant to print the current fps to the screen
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    public override void OnDraw()
    {
      _renderer.DrawString($"fps: {_fps}", new Vector2(200, 10), Color.Black);
    }
  }
}
