namespace BlueJay.Core
{
  public static class MathHelper
  {
    /// <summary>
    /// Clamp between two numbers
    /// </summary>
    /// <param name="val">The current value we want to clamp</param>
    /// <param name="min">The minium clamp value</param>
    /// <param name="max">The maxium clamp value</param>
    /// <returns>Will return the clamped number</returns>
    public static float Clamp(float val, float min, float max) => val < min ? min : (val > max ? max : val);

    /// <summary>
    /// Clamp between two numbers
    /// </summary>
    /// <param name="val">The current value we want to clamp</param>
    /// <param name="min">The minium clamp value</param>
    /// <param name="max">The maxium clamp value</param>
    /// <returns>Will return the clamped number</returns>
    public static int Clamp(int val, int min, int max) => val < min ? min : (val > max ? max : val);
  }
}
