using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Interfaces
{
  /// <summary>
  /// The view colleciton is meant to store all the different views and to be able to switch between them
  /// </summary>
  public interface IViewCollection
  {
    /// <summary>
    /// The current view we are updating/rendering to the screen
    /// </summary>
    IView Current { get; }

    /// <summary>
    /// Helper method is meant to set the current view so that it is updated/rendered
    /// </summary>
    /// <typeparam name="T">The view we need to switch too</typeparam>
    /// <returns>Will return the object based on the type given</returns>
    T SetCurrent<T>()
      where T : IView;
  }
}
