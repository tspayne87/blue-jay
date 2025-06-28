using BlueJay.Common.Systems;
using BlueJay.Core;
using BlueJay.Shared.Components;
using BlueJay.Shared.Games.Layer;
using BlueJay.Shared.Games.Layer.Systems;
using BlueJay.Shared.Games.LDtk.Factories;
using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Shared.Views
{
  internal class LDtkView : View
  {
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      serviceProvider.AddSystem<ViewportSystem>();
      serviceProvider.AddUISystems();
      serviceProvider.AddUIMouseSupport();
      serviceProvider.AddUIKeyboardSupport();
      serviceProvider.AddUITouchSupport();
      serviceProvider.AddUIComponentSystems();

      serviceProvider.AddSystem<ClearSystem>(Color.White);

      serviceProvider.AddUIRenderSystems();

      serviceProvider.AttachComponent<LayoutViewComponent>();

      serviceProvider.CreateBackground();
    }
  }
}
