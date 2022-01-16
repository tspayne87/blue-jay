# Texture Font
A texture representation of a font that renders areas of the texture for the font

## Width
The current width of the font face

## Height
The current height of the font face

## Texture
The texture for the font face

## Constructor
The constructor to build out a texture font

```csharp
  /// @param texture: The texture that represents the font
  /// @param rows: The rows that exist in the texture
  /// @param cols: The columns that exist in the texture
  /// @param alphabet: The alphabet index order for this texture
  public TextureFont(Texture2D texture, int rows, int cols, string alphabet = "abcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()/.,';\\][=-?><\":|}{+_`");
```
## GetBounds
Method is meant to get the bounds of the texture for rendering the letter in the texture

```csharp
  /// @param letter: The letter we are rendering
  /// @returns The bounds of the letter found, empty if letter was not found
  public Rectangle GetBounds(char letter);
```
## MeasureString
Measures how big the string will be when it is rendered to the screen

```csharp
  /// @param str: The string to measure
  /// @param size: The size of the font being renderered
  /// <returns>Will return the width and hight of the string</returns>
  public Vector2 MeasureString(string str, int size = 1);
```
## FitString
Method is meant to calculate a string based on the width of the space

```csharp
  /// @param str: The string we want to fit
  /// @param width: The width we want to fit into
  /// @param size: The size of the font
  /// @returns Will return a string that fits into the width of its space
  public string FitString(string str, int width, int size);
```
