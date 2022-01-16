# Size
A size struct to be a bounds without the x/y properties

## Width
The width of the size
## Height
The height of the size

## Constructors
A set of constructors to build out the size struct
```csharp
    /// @param size: The size of this Size
    public Size(int size);

    /// @param width: The width of the Size
    /// @param height: The height of the Size
    public Size(int width, int height);

    /// @param point: The point we need to build this size out of
    public Size(Point point);

    /// @param position: The vector we need to build this size out of
    public Size(Vector2 position);
```
## ToPoint
Converts the size to a point

```csharp
    /// @returns Will return a point version of this size
    public Point ToPoint();
```
## ToVector2
Converts the size to a vector

```csharp
    /// @returns Will return a vector version of this size
    public Vector2 ToVector2();
```
## Static Methods
This is a list of static methods that exist on the Size struct

### Empty
An empty version of the size or both width/height being set to 0