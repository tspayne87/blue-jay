using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System.Services
{
  public class MouseService
  {
    public MouseState State { get; internal set; }

    public bool RightButtonClick { get; internal set; }
    public bool MiddleButtonClick { get; internal set; }
    public bool LeftButtonClick { get; internal set; }

    internal ButtonState GetButtonState(MouseButton button)
    {
      switch (button)
      {
        case MouseButton.Right: return State.RightButton;
        case MouseButton.Middle: return State.MiddleButton;
        case MouseButton.Left: return State.LeftButton;
      }
      return ButtonState.Released;
    }

    internal enum MouseButton
    {
      Right, Middle, Left
    }
  }
}
