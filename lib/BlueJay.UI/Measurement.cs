namespace BlueJay.UI;

public struct Measurement : IEquatable<Measurement>
{
  /// <summary>
  /// The current measurement in pixels
  /// </summary>
  private readonly int? _pixels;

  /// <summary>
  /// The current measurement as a percentage, of the width that it can be
  /// </summary>
  private readonly float? _percent;

  /// <summary>
  /// Gets the current measurement type that should be used for fitting the element
  /// </summary>
  public FitType Type => _pixels.HasValue ? FitType.Fixed : FitType.Percentage;

  /// <summary>
  /// Initializes a new instance of the <see cref="Measurement"/> struct. With 100% of the container width.
  /// </summary>
  public Measurement()
  {
    _pixels = null;
    _percent = 1f;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Measurement"/> struct.
  /// </summary>
  /// <param name="measurement">The current measurement string can be a basic number or ended with a percentage sign, to get the appropriate value.</param>
  public Measurement(string measurement)
  {
    if (measurement.EndsWith("%"))
    {
      _percent = float.Parse(measurement.TrimEnd('%')) / 100f;
      _pixels = null;
    }
    else
    {
      _pixels = int.Parse(measurement);
      _percent = null;
    }
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Measurement"/> struct. With a fixed pixel value.
  /// </summary>
  /// <param name="pixels">The pixel value to set.</param>
  public Measurement(int pixels)
  {
    _pixels = pixels;
    _percent = null;
  }

  /// <summary>
  /// Determines whether the specified <see cref="Measurement"/> is equal to the current <see cref="Measurement"/>.
  /// </summary>
  /// <param name="other">The other measurement to compare with.</param>
  /// <returns>Will return true if the measurements are equal, otherwise false.</returns>
  public bool Equals(Measurement other)
  {
    return _pixels == other._pixels && _percent == other._percent;
  }

  /// <inheritdoc />
  public override bool Equals(object? obj)
  {
    return obj is Measurement other && Equals(other);
  }

  /// <inheritdoc />
  public override int GetHashCode()
  {
    return HashCode.Combine(_pixels, _percent);
  }

  /// <summary>
  /// Converts the measurement to pixels.
  /// </summary>
  /// <param name="containerWidth">The current container value in pixels</param>
  /// <returns>Will return the measurement in pixels</returns>
  /// <exception cref="InvalidOperationException">Will throw if the measurement is not set</exception>
  public int ToPixels(int containerWidth)
  {
    if (_pixels.HasValue)
      return Math.Min(_pixels.Value, containerWidth);
    else if (_percent.HasValue)
      return (int)(_percent.Value * containerWidth);
    throw new InvalidOperationException("Measurement is not set.");
  }

  /// <summary>
  /// Tries to get the pixels to short circuit out of trying to calculate pixels based on a percentage.
  /// </summary>
  /// <param name="pixels">The pixels that were extracted</param>
  /// <returns>Will return true if the pixels were successfully extracted, otherwise false.</returns>
  public bool TryGetPixels(out int pixels)
  {
    pixels = _pixels ?? 0;
    return _pixels.HasValue;
  }

  /// <summary>
  /// The implicit conversion operator from string to Measurement.
  /// </summary>
  /// <param name="value">Would convert a string representation of a measurement to a Measurement struct.</param>
  public static implicit operator Measurement(string value) => new Measurement(value);

  /// <summary>
  /// Determines whether the specified <see cref="Measurement"/> is equal to the current <see cref="Measurement"/>.
  /// </summary>
  /// <param name="a">The first measurement to compare.</param>
  /// <param name="b">The second measurement to compare.</param>
  /// <returns>Will return true if the measurements are equal, otherwise false.</returns>
  public static bool operator ==(Measurement a, Measurement b) => a.Equals(b);

  /// <summary>
  /// Determines whether the specified <see cref="Measurement"/> is not equal to the current <see cref="Measurement"/>.
  /// </summary>
  /// <param name="a">The first measurement to compare.</param>
  /// <param name="b">The second measurement to compare.</param>
  /// <returns>Will return true if the measurements are not equal, otherwise false.</returns>
  public static bool operator !=(Measurement a, Measurement b) => !a.Equals(b);
}
