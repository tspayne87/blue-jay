using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Keyboard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BlueJay.App.Games.Breakout.EventListeners
{
  public class PlayerKeyboardEventListener : EventListener<KeyboardDownEvent>
  {
    private readonly LayerCollection _layerCollection;


    public PlayerKeyboardEventListener(LayerCollection layerCollection)
    {
      _layerCollection = layerCollection;
    }

    public override void Process(IEvent<KeyboardDownEvent> evt)
    {
      var paddle = _layerCollection[LayerNames.PaddleLayer].Entities[0];
      var pa = paddle.GetAddon<PositionAddon>();

      switch(evt.Data.Key)
      {
        case Keys.A:
          pa.Position += new Vector2(-10, 0);
          break;
        case Keys.D:
          pa.Position += new Vector2(10, 0);
          break;
      }
    }
  }
}
