# NinePatch
Nine Patch texture is a way to create rectangles from a single 3x3 texture that can draw any type of rectangle with a texture

## Texture
The texture that will be used in the nine patch

## Break
The breaks for the nine patch texture so we can determine where to draw from

## TopLeft
The top left render source for the ninepatch

## Top
The top render source for the ninepatch

## TopRight
The top right render source for the ninepatch

## MiddleLeft
The middle left render source for the ninepatch

## Middle
The middle render source for the ninepatch

## MiddleRight
The middle right render source for the ninepatch

## BottomLeft
The bottom left render source for the ninepatch

## Bottom
The bottom render source for the ninepatch

## BottomRight
The bottom right render source for the ninepatch

## Constructor
The Ninepatch constructor to build out the ninepatch from a texture

```csharp
  /// @param texture: The texture we are building from
  public NinePatch(Texture2D texture);
```