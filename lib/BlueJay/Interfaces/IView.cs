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
    /// The draw method is meant to draw data to the screen
    /// </summary>
    void Draw();

    /// <summary>
    /// The update method is meant to update data to the screen
    /// </summary>
    void Update();
  }

  /// <summary>
  /// The view colleciton is meant to store all the different views and to be able to switch between them
  /// </summary>
  public interface IViewCollection : ICollection<IView>
  {
    /// <summary>
    /// The current view we are updating/rendering to the screen
    /// </summary>
    IView Current { get; }

    /// <summary>
    /// Helper method is meant to set the current view so that it is updated/rendered
    /// </summary>
    /// <typeparam name="T">The view we need to switch too</typeparam>
    void SetCurrent<T>()
      where T : IView;
  }
}