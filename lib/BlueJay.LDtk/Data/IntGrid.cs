using BlueJay.Core;

namespace BlueJay.LDtk.Data;

/// <summary>
/// Represents a grid of integers used in the LDtk (Level Designer Toolkit) framework.
/// This structure is typically used to store data such as terrain types, object presence, or other grid-based information.
/// </summary>
public struct IntGrid
{
  /// <summary>
  /// A one-dimensional array representing the grid, where each element corresponds to a cell in the grid.
  /// The value of each cell is an integer, which can represent various data such as terrain type, object presence, etc.
  /// </summary>
  public long[] Grid { get; }

  /// <summary>
  /// The width of the grid in cells.
  /// </summary>
  public long Width { get; }

  /// <summary>
  /// The height of the grid in cells.
  /// </summary>
  public long Height { get; }

  /// <summary>
  /// The size of each cell in the grid.
  /// </summary>
  public long CellSize { get; }

  public IntGrid(long[] grid, long width, long height, long cellSize)
  {
    if (grid == null) throw new ArgumentNullException(nameof(grid), "Grid cannot be null");
    if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than zero");
    if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than zero");
    if (cellSize <= 0) throw new ArgumentException("The cell size needs to be bigger than zero in the x and y direction", nameof(cellSize));

    Grid = grid;
    Width = width;
    Height = height;
    CellSize = cellSize;
  }
}
