using System;

namespace BlueJay.Core
{
  public static class RandomExtensions
  {
    /// <summary>
    /// Will get the next random float from the random object
    /// </summary>
    /// <param name="rand">The random object to generate a float from</param>
    /// <param name="min">The minimum float to go from</param>
    /// <param name="max">The maximum float to go from</param>
    /// <returns>Will return a float in the middle of the minimum and maximum values including the minium and maximum values</returns>
    public static float NextFloat(this Random rand, float min, float max)
    {
      return (float)rand.NextDouble() * (max - min) + min;
    }
  }
}
