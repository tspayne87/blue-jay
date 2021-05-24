using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay
{
  public class Particle
  {
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public int LifeSpan { get; set; }
  }
}
