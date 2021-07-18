using BlueJay.Component.System.Interfaces;
using BlueJay.Core;

namespace BlueJay.Common.Addons
{
  /// <summary>
  /// Addon to track the size of the entity
  /// </summary>
  public struct SizeAddon : IAddon
  {
    /// <summary>
    /// The current size of the entity
    /// </summary>
    public Size Size { get; set; }

    /// <summary>
    /// Constructor for building out the size addon
    /// </summary>
    /// <param name="size">The size addon should be assigned with first</param>
    public SizeAddon(Size size)
    {
      Size = size;
    }

    /// <summary>
    /// Constructor to build out a square with the same width/height
    /// </summary>
    /// <param name="size">The size of this Size</param>
    public SizeAddon(int size)
    {
      Size = new Size(size);
    }

    /// <summary>
    /// Constructor to build out a rectangle based on the width and height
    /// </summary>
    /// <param name="width">The width of the Size</param>
    /// <param name="height">The height of the Size</param>
    public SizeAddon(int width, int height)
    {
      Size = new Size(width, height);
    }

    /// <summary>
    /// Overriden to string function is meant to give a quick and easy way to see
    /// how this object looks while debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Size | Width: {Size.Width}, Height: {Size.Height}";
    }
  }
}
