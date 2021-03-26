using BlueJay.Component.System.Services;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using static BlueJay.Component.System.Services.MouseService;

namespace BlueJay.Component.System.Systems
{
  public class MouseSystem : ComponentSystem
  {
    private readonly MouseService _mouseService;
    private readonly Dictionary<MouseButton, bool> _states = new Dictionary<MouseButton, bool>() {
      { MouseButton.Right, false },
      { MouseButton.Middle, false },
      { MouseButton.Left, false }
    };

    public override long Key => 0;

    public MouseSystem(MouseService mouseService)
    {
      _mouseService = mouseService;
    }

    public override void Update(int delta)
    {
      _mouseService.State = Mouse.GetState();

      UpdateClickState(MouseButton.Right);
      UpdateClickState(MouseButton.Middle);
      UpdateClickState(MouseButton.Left);
    }

    private void UpdateClickState(MouseButton button)
    {
      var state = _mouseService.GetButtonState(button);
      SetButtonClick(button, false);
      if (state == ButtonState.Pressed && !_states[button])
      {
        _states[button] = true;
      }
      else if (state == ButtonState.Released && _states[button])
      {
        _states[button] = false;
        SetButtonClick(button, true);
      }
    }

    private void SetButtonClick(MouseButton button, bool state)
    {
      switch(button)
      {
        case MouseButton.Right:
          _mouseService.RightButtonClick = state;
          break;
        case MouseButton.Middle:
          _mouseService.MiddleButtonClick = state;
          break;
        case MouseButton.Left:
          _mouseService.LeftButtonClick = state;
          break;
      }
    }
  }
}
