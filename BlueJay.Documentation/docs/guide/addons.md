# Introduction
Addons are data points that can be added to entities so that systems can filter on those types of addons
to break down all entities in the system to their basic level.  Addons should be small data points that handle
a piece the data for a piece of the game.  An example is the common *ColorAddon* as follows:

```csharp
  /// <summary>
  /// The color addon is for attaching color to an entity, make sure to use a struct because addons should be
  /// immutable
  /// </summary>
  public struct ColorAddon : IAddon
  {
    /// <summary>
    /// The current color that should be used for the entity
    /// </summary>
    public Color Color;

    /// <summary>
    /// Constructor to build out the color addon
    /// </summary>
    /// <param name="color">The current color that should be assigned</param>
    public ColorAddon(Color color)
    {
      Color = color;
    }

    /// <summary>
    /// Overridden to string method is meant to print out a nice version of the
    /// addon for debugging purposes
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Color | R: {Color.R}, G: {Color.G}, B: {Color.B}, A: {Color.A}";
    }
  }
```

_*Note:*_ Only 64 different addons can be used in a game