using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// The particle system that should update and render the particles to the screen
  /// </summary>
  public class ParticleSystem : IUpdateEntitySystem, IDrawSystem, IDrawEntitySystem, IDrawEndSystem
  {
    /// <summary>
    /// The delta service that is meant to be updated every frame
    /// </summary>
    private readonly IDeltaService _deltaService;

    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <summary>
    /// The layers that handle the entities
    /// </summary>
    private readonly LayerCollection _layers;

    /// <inheritdoc />
    public long Key => AddonHelper.Identifier<ParticleAddon, TextureAddon>();

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor for the particle system
    /// </summary>
    /// <param name="layers">The current layers we are working with</param>
    /// <param name="deltaService">The delta service</param>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    public ParticleSystem(LayerCollection layers, IDeltaService deltaService, SpriteBatch batch)
    {
      _layers = layers;
      _deltaService = deltaService;
      _batch = batch;
    }

    /// <inheritdoc />
    public void OnUpdate(IEntity entity)
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

      entity.Update(pa);
    }

    /// <inheritdoc />
    public void OnDraw()
    {
      _batch.Begin();
    }

    /// <inheritdoc />
    public void OnDraw(IEntity entity)
    {
      var pa = entity.GetAddon<ParticleAddon>();
      var ta = entity.GetAddon<TextureAddon>();

      _batch.DrawParticles(ta.Texture, pa.Particles);
    }

    /// <inheritdoc />
    public void OnDrawEnd()
    {
      _batch.End();
    }
  }
}
