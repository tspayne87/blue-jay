using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BlueJay.Core
{
  public static class ParticleGenerator
  {
    public static List<Particle> Generate(Vector2 position, int amount, int range, Color color, int? seed = null)
    {
      var rand = seed == null ? new Random() : new Random(seed.Value);

      var particles = new List<Particle>();
      var num = rand.Next(amount - range, amount + range);
      for (var i = 0; i < num; ++i)
      {
        particles.Add(new Particle()
        {
          Position = new Vector2(rand.NextFloat(position.X - range, position.X + range), rand.NextFloat(position.Y - range, position.Y + range)),
          Velocity = new Vector2(rand.NextFloat(-range, range), rand.NextFloat(-(range * 2), 0)),
          LifeSpan = rand.Next(250, 1000),
          Color = color
        });
      }
      return particles;
    }
  }
}
