using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System.Plugins
{
  public class ClearPlugin : Plugin
  {
    private readonly GraphicsDevice _graphicsDevice;
    private readonly Color _color;

    public ClearPlugin(GraphicsDevice graphicsDevice, Color color)
    {
      _graphicsDevice = graphicsDevice;
      _color = color;
    }

    public override void Draw(int delta)
    {
      _graphicsDevice.Clear(_color);
    }
  }
}
