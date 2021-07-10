using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.Factories
{
  public static class ParticleFactory
  {
    public static IEntity AddParticles(this IServiceProvider provider, Texture2D texture, Vector2 position, int amount, int range, Color color)
    {
      var entity = provider.AddEntity<Entity>(LayerNames.Particles);
      entity.Add(new TextureAddon(texture));
      entity.Add(new ParticleAddon(ParticleGenerator.Generate(position, amount, range, color)));
      return entity;
    }
  }
}
