using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Core
{
  public class Camera : ICamera
  {
    private float _zoom = 1f;

    public Vector2 Position { get; set; } = new Vector2(-32f);
    public float Zoom
    {
      get => _zoom;
      set => _zoom = MathHelper.Clamp(value, 0.35f, 3f);
    }

    public virtual Matrix GetTransformMatrix => GetViewMatrix(Vector2.One);
    public Matrix GetViewMatrix(Vector2 parallaxFactor) => Matrix.CreateTranslation(new Vector3(-Position * parallaxFactor, 0)) * Matrix.CreateScale(Zoom);
    public Vector2 ToWorld(Vector2 pos) => Vector2.Transform(pos, Matrix.Invert(GetTransformMatrix));
    public Vector2 ToScreen(Vector2 pos) => Vector2.Transform(pos, GetTransformMatrix);
  }
}
