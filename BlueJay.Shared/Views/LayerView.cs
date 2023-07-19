using BlueJay.Common.Systems;
using BlueJay.Core;
using BlueJay.Shared.Components;
using BlueJay.Shared.Games.Layer;
using BlueJay.Shared.Games.Layer.Systems;
using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Shared.Views
{
  internal class LayerView : View
  {
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      serviceProvider.AddSystem<ViewportSystem>();
      serviceProvider.AddUISystems();
      serviceProvider.AddUIMouseSupport();
      serviceProvider.AddUIKeyboardSupport();
      serviceProvider.AddUITouchSupport();

      serviceProvider.AddSystem<ClearSystem>(Color.White);
      serviceProvider.AddSystem<TopRectangleDrawSystem>();
      serviceProvider.AddSystem<BottomRectangleDrawSystem>();

      serviceProvider.AddUIRenderSystems();
      serviceProvider.AddSystem<RenderingSystem>();

      serviceProvider.AddTopEntity(new Size(100, 50), new Vector2(200, 200), Color.Blue);
      serviceProvider.AddBottomEntity(new Size(120, 70), new Vector2(190, 190), Color.Green);

      serviceProvider.AttachComponent<LayoutViewComponent>();
    }
  }
}
