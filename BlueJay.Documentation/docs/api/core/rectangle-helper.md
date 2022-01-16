# RectangleHelper
A static helper class meant to add extra logic to help with rectangle objects

## SideIntersection
Set of extension methods meant to calculate the side interaction between two rectangles on
the screen

```csharp
  /// @param self: The entity we want to check against the target
  /// @param target: The target we need to check which side was hit
  /// @returns Will return a side that was hit or none if nothing was hit or if we are inside the rectangle
  public static RectangleSide SideIntersection(Rectangle self, Rectangle target);

  /// @param self: The entity we want to check against the target
  /// @param target: The target we need to check which side was hit
  /// @returns Will return a side that was hit or none if nothing was hit or if we are inside the rectangle
  public static RectangleSide SideIntersection(Rectangle self, Rectangle target, out Rectangle intersection);
```

## RectangleSide
The enumeration of the side that is being intersected with

```csharp
  public enum RectangleSide
  {
    Top, Right, Bottom, Left, None
  }
```