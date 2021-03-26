using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Core.Interfaces
{
  public interface ICamera
  {
    Vector2 Position { get; set; }
    float Zoom { get; set; }

    Matrix GetTransformMatrix { get; }
    Matrix GetViewMatrix(Vector2 parallaxFactor);

    Vector2 ToWorld(Vector2 pos);
    Vector2 ToScreen(Vector2 pos);
  }
}
