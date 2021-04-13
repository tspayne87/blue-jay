using Microsoft.Xna.Framework;

namespace BlueJay.Core.Interfaces
{
  /// <summary>
  /// Camera to determine where we should be rendering in the scene
  /// </summary>
  public interface ICamera
  {
    /// <summary>
    /// The current position of the camera
    /// </summary>
    Vector2 Position { get; set; }

    /// <summary>
    /// The current zoom value for the camera
    /// </summary>
    float Zoom { get; set; }

    /// <summary>
    /// The current transform matrix for this camera
    /// </summary>
    Matrix GetTransformMatrix { get; }

    /// <summary>
    /// The view matrix based on the parallax factor
    /// </summary>
    /// <param name="parallaxFactor">The current parallax factor</param>
    /// <returns></returns>
    Matrix GetViewMatrix(Vector2 parallaxFactor);

    /// <summary>
    /// Convert a vector to the world based on the screen of the camera
    /// </summary>
    /// <param name="pos">The current position on the screen</param>
    /// <returns>Will return the current position in the world based on the screen</returns>
    Vector2 ToWorld(Vector2 pos);

    /// <summary>
    /// Convert a vector to the screen based on the world
    /// </summary>
    /// <param name="pos">The current position of the world</param>
    /// <returns>Will return the screen position based on the world coords</returns>
    Vector2 ToScreen(Vector2 pos);
  }
}
