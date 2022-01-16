# MathHelper
A static class meant to have some basic clamps to handle numbers between other numbers

## Clamp
Clamp between two numbers meaning do not go below the min or above the max

```csharp
  /// @param val: The current value we want to clamp
  /// @param min: The minium clamp value
  /// @param max: The maxium clamp value
  /// @returns Will return the clamped number
  public static float Clamp(float val, float min, float max);

  /// @param val: The current value we want to clamp
  /// @param min: The minium clamp value
  /// @param max: The maxium clamp value
  /// @returns Will return the clamped number
  public static int Clamp(int val, int min, int max);
```