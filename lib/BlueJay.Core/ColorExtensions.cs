using System;
using Microsoft.Xna.Framework;

namespace BlueJay.Core;

public static class ColorExtensions
{
  /// <summary>
  /// Converts a hexadecimal color string to a Color object.
  /// The string can be in the format "#RRGGBB" or "#RRGGBBAA".
  /// </summary>
  /// <param name="hex">The hex format for</param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  public static Color FromRGBAHex(this string hex)
  {
    if (string.IsNullOrEmpty(hex))
      throw new ArgumentException("Hex string cannot be null or empty.", nameof(hex));

    hex = hex.TrimStart('#');
    if (hex.Length != 6 && hex.Length != 8)
      throw new ArgumentException("Hex string must be 6 or 8 characters long.", nameof(hex));

    return new Color(
      Convert.ToByte(hex.Substring(0, 2), 16),
      Convert.ToByte(hex.Substring(2, 2), 16),
      Convert.ToByte(hex.Substring(4, 2), 16),
      hex.Length == 8 ? Convert.ToByte(hex.Substring(6, 2), 16) : 255
    );
  }
}
