# GraphicsDevice Extension Methods

## CreateRectangle
Method is meant to create a rectangle texture for rendering

```csharp
    /// @param graphics: The graphics device we need to render with
    /// @param width: The width of the rectangle
    /// @param height: The height of the rectangle
    /// @param color: The color that should be used if null is passed Color.Black is used
    /// @returns Will return a texture that was created
    public static Texture2D CreateRectangle(this GraphicsDevice graphics, int width, int height, Color? color = null);
```