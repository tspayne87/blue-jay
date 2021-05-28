using BlueJay.Component.System.Addons;
using System.Collections.Generic;

namespace BlueJay.Addons
{
  /// <summary>
  /// Particle addon that handles the list of particles that should be batched
  /// </summary>
  public class ParticleAddon : Addon<ParticleAddon>
  {
    /// <summary>
    /// The batch of particles that were generated from a system
    /// </summary>
    public List<Particle> Particles { get; set; }

    /// <summary>
    /// Constructor to build the particles
    /// </summary>
    /// <param name="particles">The particles that should be rendered to the screen</param>
    public ParticleAddon(List<Particle> particles)
    {
      Particles = particles;
    }
  }
}
