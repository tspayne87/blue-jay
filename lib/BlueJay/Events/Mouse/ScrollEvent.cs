﻿namespace BlueJay.Events.Mouse
{
  /// <summary>
  /// The mouse scroll event
  /// </summary>
  public class ScrollEvent
  {
    /// <summary>
    /// The current scroll wheel value that was tracked
    /// </summary>
    public int ScrollWheelValue { get; set; }

    /// <summary>
    /// Last frames scroll wheel value that was tracked
    /// </summary>
    public int PreviousScrollWheelValue { get; set; }
  }
}