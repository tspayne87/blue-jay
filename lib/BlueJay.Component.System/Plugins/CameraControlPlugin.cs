using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System.Plugins
{
  public class CameraControlPlugin : Plugin
  {
    private readonly ICamera _camera;
    private readonly IRenderer _renderer;

    private bool _pressed = false;
    private Point _previous = Point.Zero;

    public CameraControlPlugin(ICamera camera, IRenderer renderer)
    {
      _camera = camera;
      _renderer = renderer;
    }

    public override void Update(int delta)
    {
      var state = Mouse.GetState();

      if (!_pressed && state.LeftButton == ButtonState.Pressed)
      {
        _pressed = true;
        _previous = state.Position;
      }
      else if (_pressed)
      {
        if (state.LeftButton == ButtonState.Pressed)
        {
          _camera.Position += (_previous - state.Position).ToVector2();
          _previous = state.Position;
        }
        else
        {
          _pressed = false;
        }
      }
    }
  }
}
