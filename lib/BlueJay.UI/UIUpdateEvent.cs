using BlueJay.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI
{
  /// <summary>
  /// UI Update event that is meant to determine the size of the screen
  /// </summary>
  public class UIUpdateEvent
  {
    /// <summary>
    /// The current size of the UI screen
    /// </summary>
    public Size Size { get; set; }
  }
}
