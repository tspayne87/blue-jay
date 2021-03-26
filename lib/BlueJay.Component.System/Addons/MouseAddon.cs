using System;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BlueJay.Component.System.Addons
{
  public abstract class MouseAddon : Addon<MouseAddon>
  {
    public enum MouseEvent
    {
      MouseDown, MouseMove, MouseUp, Click
    }

    internal bool ButtonPressed;

    public bool IsMouseDown;
    public bool Hovering;
    public int Start;

    public Point Change;
    public Point PreviousPosition;
    public Point Overflow;

    public abstract void On(MouseEvent type, MouseEventArgs args);

    public override string ToString()
    {
      return $"Mouse | IsMouseDown: {IsMouseDown}, Hovering: {Hovering}, Start: {Start}, Change: {Change}, Previous: {PreviousPosition}";
    }

    public class MouseEventArgs
    {
      public MouseState State { get; set; }
      public Vector2 Position { get; set; }
      public bool Contains { get; set; }
      public Rectangle Bounds { get; set; }

      internal bool ContinuePropegation { get; set; }

      public MouseEventArgs(MouseState state, Vector2 pos)
      {
        State = state;
        Position = pos;
        ContinuePropegation = true;
      }

      public void StopPropegation()
      {
        ContinuePropegation = false;
      }
    }
  }
}
