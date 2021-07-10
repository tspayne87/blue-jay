using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using System.Collections.Generic;

namespace BlueJay.Common.Addons
{
  /// <summary>
  /// Particle addon that handles the list of particles that should be batched
  /// </summary>
  public struct ParticleAddon : IAddon
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
