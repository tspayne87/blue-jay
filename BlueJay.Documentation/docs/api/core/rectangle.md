# Rectangle Extension Methods

## Add
Set of extension methods meant to add the position to a rectangle and return the rectangle
that is the result

```csharp
  /// @param pos: The position that should be added to the current rectangle
  /// @returns Will return a new rectangle that has the pos added to its position
  public static Rectangle Add(Vector2 pos);

  /// @param pos: The point that should be added to the current rectangle
  /// @returns Will return a new rectangle that has the point added to its position
  public static Rectangle Add(Point pos);

  /// @param x: The x position we want to add by
  /// @param y: The y position we want to add by
  /// @returns Will return a new rectangle that has the x and y coords added to its position
  public static Rectangle Add(int x, int y);
```
## AddX
Set of extension methods meant to add to the x position of a rectangle objec tand return the
rectangle that is the result

```csharp
  /// @param x: The x position we want to add by
  /// @returns Will return a new rectangle that has the x coord added to its position
  public static Rectangle AddX(int x);

  /// @param x: The x position we want to add by
  /// @returns Will return a new rectangle that has the x coord added to its position
  public static Rectangle AddX(float x);
```
## AddY
Set of extension methods meant to add to the y position of a rectangle objec tand return the
rectangle that is the result

```csharp
  /// @param y: The y position we want to add by
  /// @returns Will return a new rectangle that has the y coord added to its position
  public static Rectangle AddY(int y);

  /// @param y: The y position we want to add by
  /// @returns Will return a new rectangle that has the y coord added to its position
  public static Rectangle AddY(float y);
```