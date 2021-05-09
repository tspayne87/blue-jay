using BlueJay.App.Games.Breakout.EventListeners;
using BlueJay.App.Games.Breakout.Factories;
using BlueJay.App.Games.Breakout.Systems;
using BlueJay.Component.System.Systems;
using BlueJay.Events.Keyboard;
using BlueJay.Systems;
using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BlueJay.App.Views
{
  public class BreakOutView : View
  {
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      serviceProvider.AddComponentSystem<KeyboardSystem>();
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);

      serviceProvider.AddComponentSystem<BreakoutRenderingSystem>();
      serviceProvider.AddComponentSystem<FPSSystem>();

      serviceProvider.AddEventListener<PlayerKeyboardEventListener, KeyboardDownEvent>();

      serviceProvider.AddPaddle();

      var colors = new Dictionary<int, Color>()
      {
        { 0, Color.Red },
        { 1, Color.OrangeRed },
        { 2, Color.Orange },
        { 3, Color.DarkGoldenrod }
      };
      for (var i = 0; i < 20; ++i)
      {
        serviceProvider.AddBlock(colors[i / 5], i);
      }
    }
  }
}
