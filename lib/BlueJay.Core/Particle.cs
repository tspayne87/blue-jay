using Microsoft.Xna.Framework;

namespace BlueJay.Core
{
  /// <summary>
  /// The particle that should be rendered
  /// </summary>
  public class Particle
  {
    /// <summary>
    /// The position of the particle
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// The velocity of the particle
    /// </summary>
    public Vector2 Velocity { get; set; }

    /// <summary>
    /// The lifespan of the particle
    /// </summary>
    public int LifeSpan { get; set; }

    /// <summary>
    /// The color of the particle
    /// </summary>
    public Color Color { get; set; }
  }
}
