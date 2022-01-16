using System;
using System.Collections.Generic;

namespace BlueJay.Interfaces
{
  /// <summary>
  /// Interface to determine the current view that is being rendered.
  /// </summary>
  public interface IView
  {
    /// <summary>
    /// Initialization method is meant to set a new scope for the DI and have the view configure that provider once it has been created
    /// </summary>
    /// <param name="serviceProvider">The current service provider we are working with</param>
    void Initialize(IServiceProvider serviceProvider);

    /// <summary>
    /// The enter method is meant to trigger when this view is set as the current
    /// </summary>
    void Enter();

    /// <summary>
    /// The leave method is meant to trigger when a new current view is set
    /// </summary>
    void Leave();

    /// <summary>
    /// The draw method is meant to draw data to the screen
    /// </summary>
    void Draw();

    /// <summary>
    /// The update method is meant to update data to the screen
    /// </summary>
    void Update();

    /// <summary>
    /// Handle the activate event for the event queue
    /// </summary>
    void Activate();

    /// <summary>
    /// Handle the deactivate event for the event queue
    /// </summary>
    void Deactivate();

    /// <summary>
    /// Handle the exit event for the event queue
    /// </summary>
    void Exit();
  }
}