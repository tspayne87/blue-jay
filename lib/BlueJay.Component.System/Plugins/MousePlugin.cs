using System;
using System.Collections.Generic;
using System.Linq;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Enums;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BlueJay.Component.System.Plugins
{
  public enum ButtonType { Left, Right, Middle }

  public class MousePluginOptions
  {
    public ButtonType Button { get; set; } = ButtonType.Left;
    public Point Stagger { get; set; } = new Point(1, 1);
  }

  public class MousePlugin : Plugin
  {
    private readonly IEntityCollection _entityCollection;
    private readonly MousePluginOptions _options;
    private readonly ICamera _camera;
    private readonly long _key;
    private readonly EntityType _uiType;
    private readonly int _clickDelay;

    private MouseAddon.MouseEventArgs _args;

    public MousePlugin(IEntityCollection entityCollection, ICamera camera, MousePluginOptions options)
    {
      _entityCollection = entityCollection;
      _camera = camera;
      _options = options;
      _key = PositionAddon.Identifier | BoundsAddon.Identifier | MouseAddon.Identifier;
      _uiType = EntityType.UI;
      _clickDelay = 100;
    }

    public override void Update(int delta)
    {
      var state = Mouse.GetState();
      _args = new MouseAddon.MouseEventArgs(state, _camera != null ? _camera.ToWorld(state.Position.ToVector2()) : state.Position.ToVector2());

      var entities = _entityCollection.GetByKey(_key)
        .GroupBy(x => x.Type)
        .OrderBy(x => x.Key)
        .SelectMany(x => x.Reverse());

      var containsUI = false;
      foreach (var entity in entities)
      {
        Update(delta, entity);
        containsUI |= (_args.Contains && entity.Type == _uiType);

        if (!_args.ContinuePropegation || (containsUI && entity.Type != _uiType)) break;
      }
    }

    private void Update(int delta, IEntity entity)
    {
      var pc = entity.GetAddon<PositionAddon>();
      var bc = entity.GetAddon<BoundsAddon>();
      var mc = entity.GetAddon<MouseAddon>();
      var stagger = _options?.Stagger ?? new Point(1, 1);

      mc.Change = (_args.State.Position - mc.PreviousPosition + mc.Overflow) / stagger * stagger;
      mc.Overflow = _args.State.Position - mc.PreviousPosition + mc.Overflow - mc.Change;

      var mouseEvents = new List<MouseAddon.MouseEvent>();

      _args.Bounds = bc.Bounds.Add(pc.Position);
      _args.Contains = _args.Bounds.Contains(_args.State.Position);

      var bState = GetButtonState(_args.State);
      if (!mc.ButtonPressed && bState == ButtonState.Pressed)
      {
        mc.Start = 0;
        mc.ButtonPressed = true;
        if (_args.Contains)
        {
          mc.IsMouseDown = true;
          mouseEvents.Add(MouseAddon.MouseEvent.MouseDown);
        }
      }
      else if (mc.ButtonPressed && bState == ButtonState.Released)
      {
        if (mc.IsMouseDown && _args.Contains)
        {
          mouseEvents.Add(MouseAddon.MouseEvent.MouseUp);
          if (mc.Start + delta < _clickDelay)
          {
            mouseEvents.Add(MouseAddon.MouseEvent.Click);
          }
        }

        mc.IsMouseDown = false;
        mc.ButtonPressed = false;
      }
      else
      {
        mouseEvents.Add(MouseAddon.MouseEvent.MouseMove);
        if (mc.IsMouseDown) mc.Start += delta;
      }

      foreach (var evt in mouseEvents) mc.On(evt, _args);
      mc.Hovering = _args.Contains && !mc.IsMouseDown;
      mc.PreviousPosition = _args.State.Position;
    }

    private ButtonState GetButtonState(MouseState state)
    {
      if (_options != null)
      {
        switch (_options.Button)
        {
          case ButtonType.Right: return state.RightButton;
          case ButtonType.Middle: return state.MiddleButton;
        }
      }
      return state.LeftButton;
    }
  }
}
