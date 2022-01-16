# IAddon Interface
The addon interface is meant to be implemented by addons, it does not have any implementation requirements
but it is required that a struct is used for this interface.  Addons are meant to be used as data points for
an entity in the system.  For instance, the position were the entity should be draw and the texture what should
be used when drawing to the screen.  Addons are meant to also be immutable, thus the requirement to implement
on a struct.

## Example
```csharp
  public struct PositionAddon : IAddon
  {
    public Vector2 Position { get; set; }

    public PositionAddon(int x, int y)
    {
      Position = new Vector2(x, y);
    }

    public PositionAddon(Vector2 position)
    {
      Position = position;
    }
  }
```