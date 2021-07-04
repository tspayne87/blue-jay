using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Interfaces;
using System.Collections.Generic;

namespace BlueJay.Component.System.Systems
{
  /// <summary>
  /// The particle system that should update and render the particles to the screen
  /// </summary>
  public class ParticleSystem : ComponentSystem
  {
    /// <summary>
    /// The delta service that is meant to be updated every frame
    /// </summary>
    private readonly IDeltaService _deltaService;

    /// <summary>
    /// The renderer to draw textures to the screen
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The layers that handle the entities
    /// </summary>
    private readonly LayerCollection _layers;

    /// <summary>
    /// The Identifier for this system 0 is used if we do not care about the entities
    /// </summary>
    public override long Key => ParticleAddon.Identifier | TextureAddon.Identifier;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor for the particle system
    /// </summary>
    /// <param name="layers">The current layers we are working with</param>
    /// <param name="deltaService">The delta service</param>
    /// <param name="collection">The renderer collection so we can load the correct renderer</param>
    /// <param name="renderer">The renderer we need to load</param>
    public ParticleSystem(LayerCollection layers, IDeltaService deltaService, RendererCollection collection, string renderer)
    {
      _layers = layers;
      _deltaService = deltaService;
      _renderer = collection[renderer];
    }

    /// <summary>
    /// The update event that is called for eeach entity that was selected by the key
    /// for this system.
    /// </summary>
    /// <param name="entity">The current entity that should be updated</param>
    public override void OnUpdate(IEntity entity)
    {
      var pa = entity.GetAddon<ParticleAddon>();

      var remove = new List<Particle>();
      for (var i = 0; i < pa.Particles.Count; ++i)
      {
        pa.Particles[i].LifeSpan -= _deltaService.Delta;
        pa.Particles[i].Position += pa.Particles[i].Velocity;

        if (pa.Particles[i].LifeSpan <= 0)
        {
          remove.Add(pa.Particles[i]);
        }
      }

      // Remove particles that have expended their lifespan and remove the entity if it does not have any more particles
      pa.Particles.RemoveAll(x => remove.Contains(x));
      if (pa.Particles.Count == 0)
      {
        _layers[entity.Layer].Entities.Remove(entity);
      }
    }

    /// <summary>
    /// The draw event that is called for each entity that was selected by the key
    /// for this system
    /// </summary>
    /// <param name="entity">The current entity that should be drawn</param>
    public override void OnDraw(IEntity entity)
    {
      var pa = entity.GetAddon<ParticleAddon>();
      var ta = entity.GetAddon<TextureAddon>();

      _renderer.DrawParticles(ta.Texture, pa.Particles);
    }
  }
}
