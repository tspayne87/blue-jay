using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace BlueJay.Core
{
  /// <summary>
  /// Basic camera meant to be a starting position
  /// </summary>
  public class Camera : ICamera
  {
    /// <summary>
    /// The current zoom for the camera
    /// </summary>
    private float _zoom = 1f;

    /// <summary>
    /// The current position of the camera
    /// </summary>
    public Vector2 Position { get; set; } = new Vector2(-32f);

    /// <summary>
    /// The current zoom of the camera with a clamp so that it does not zoom in or out to much
    /// </summary>
    public float Zoom
    {
      get => _zoom;
      set => _zoom = MathHelper.Clamp(value, 0.35f, 3f);
    }

    /// <summary>
    /// The current transform matrix for this camera
    /// </summary>
    public virtual Matrix GetTransformMatrix => GetViewMatrix(Vector2.One);

    /// <summary>
    /// The view matrix based on the parallax factor
    /// </summary>
    /// <param name="parallaxFactor">The current parallax factor</param>
    /// <returns></returns>
    public Matrix GetViewMatrix(Vector2 parallaxFactor) => Matrix.CreateTranslation(new Vector3(-Position * parallaxFactor, 0)) * Matrix.CreateScale(Zoom);

    /// <summary>
    /// Convert a vector to the world based on the screen of the camera
    /// </summary>
    /// <param name="pos">The current position on the screen</param>
    /// <returns>Will return the current position in the world based on the screen</returns>
    public Vector2 ToWorld(Vector2 pos) => Vector2.Transform(pos, Matrix.Invert(GetTransformMatrix));

    /// <summary>
    /// Convert a vector to the screen based on the world
    /// </summary>
    /// <param name="pos">The current position of the world</param>
    /// <returns>Will return the screen position based on the world coords</returns>
    public Vector2 ToScreen(Vector2 pos) => Vector2.Transform(pos, GetTransformMatrix);
  }
}
