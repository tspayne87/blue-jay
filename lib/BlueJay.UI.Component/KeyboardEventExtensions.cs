using BlueJay.Events.Keyboard;
using Microsoft.Xna.Framework.Input;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// Extension methods to add method so the <see cref="KeyboardEvent" />
  /// </summary>
  public static class KeyboardEventExtensions
  {
    /// <summary>
    /// Helper method will try and get a character out of the keyboard event and give the correct character based on the keys
    /// </summary>
    /// <param name="evt">The keyboard event we need to process</param>
    /// <param name="character">The character we want to return</param>
    /// <returns>Will return true if a character was generated otherwise false</returns>
    public static bool TryGetCharacter(this KeyboardEvent evt, out char character)
    {
      switch (evt.Key)
      {
        case Keys.A:
        case Keys.B:
        case Keys.C:
        case Keys.D:
        case Keys.E:
        case Keys.F:
        case Keys.G:
        case Keys.H:
        case Keys.I:
        case Keys.J:
        case Keys.K:
        case Keys.L:
        case Keys.M:
        case Keys.N:
        case Keys.O:
        case Keys.P:
        case Keys.Q:
        case Keys.R:
        case Keys.S:
        case Keys.T:
        case Keys.U:
        case Keys.V:
        case Keys.W:
        case Keys.X:
        case Keys.Y:
        case Keys.Z:
          var key = evt.Shift ? evt.Key.ToString() : evt.Key.ToString().ToLower();
          character = key[0];
          return true;
        case Keys.Space:
          character = ' ';
          return true;
        case Keys.OemComma:
          character = evt.Shift ? '<' : ',';
          return true;
        case Keys.OemPeriod:
          character = evt.Shift ? '>' : '.';
          return true;
        case Keys.OemQuestion:
          character = evt.Shift ? '?' : '/';
          return true;
        case Keys.OemSemicolon:
          character = evt.Shift ? ':' : ';';
          return true;
        case Keys.OemQuotes:
          character = evt.Shift ? '"' : '\'';
          return true;
        case Keys.OemOpenBrackets:
          character = evt.Shift ? '{' : '[';
          return true;
        case Keys.OemCloseBrackets:
          character = evt.Shift ? '}' : ']';
          return true;
        case Keys.OemPipe:
          character = evt.Shift ? '|' : '\\';
          return true;
        case Keys.OemPlus:
          character = evt.Shift ? '+' : '=';
          return true;
        case Keys.OemMinus:
          character = evt.Shift ? '_' : '-';
          return true;
        case Keys.OemTilde:
          character = evt.Shift ? '~' : '`';
          return true;
        case Keys.D0:
          character = evt.Shift ? ')' : '0';
          return true;
        case Keys.D1:
          character = evt.Shift ? '!' : '1';
          return true;
        case Keys.D2:
          character = evt.Shift ? '@' : '2';
          return true;
        case Keys.D3:
          character = evt.Shift ? '#' : '3';
          return true;
        case Keys.D4:
          character = evt.Shift ? '$' : '4';
          return true;
        case Keys.D5:
          character = evt.Shift ? '%' : '5';
          return true;
        case Keys.D6:
          character = evt.Shift ? '^' : '6';
          return true;
        case Keys.D7:
          character = evt.Shift ? '&' : '7';
          return true;
        case Keys.D8:
          character = evt.Shift ? '*' : '8';
          return true;
        case Keys.D9:
          character = evt.Shift ? '(' : '9';
          return true;
        case Keys.NumPad0:
          character = '0';
          return true;
        case Keys.NumPad1:
          character = '1';
          return true;
        case Keys.NumPad2:
          character = '2';
          return true;
        case Keys.NumPad3:
          character = '3';
          return true;
        case Keys.NumPad4:
          character = '4';
          return true;
        case Keys.NumPad5:
          character = '5';
          return true;
        case Keys.NumPad6:
          character = '6';
          return true;
        case Keys.NumPad7:
          character = '7';
          return true;
        case Keys.NumPad8:
          character = '8';
          return true;
        case Keys.NumPad9:
          character = '9';
          return true;
      }

      character = default;
      return false;
    }
  }
}
