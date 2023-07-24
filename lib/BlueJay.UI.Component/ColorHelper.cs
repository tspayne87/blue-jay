using Microsoft.Xna.Framework;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// Set of helper methods meant to help with color conversion
  /// </summary>
  internal static class ColorHelper
  {
    /// <summary>
    /// Helper method meant to convert an object into a valid argument that can be passed into a color constructor
    /// </summary>
    /// <param name="obj">The object we want to convert into a valid color constructor</param>
    /// <returns>Will return the converted object meant to set for the color</returns>
    public static object ConvertObj(object obj)
    {
      if (obj is Vector3 vec3)
      {
        vec3.X = vec3.X / 255f;
        vec3.Y = vec3.Y / 255f;
        vec3.Z = vec3.Z / 255f;
        return vec3;
      }
      if (obj is Vector4 vec4)
      {
        vec4.X = vec4.X / 255f;
        vec4.Y = vec4.Y / 255f;
        vec4.Z = vec4.Z / 255f;
        vec4.W = vec4.W / 255f;
        return vec4;
      }
      return obj;
    }
  }
}
