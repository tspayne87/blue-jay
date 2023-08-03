using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using BlueJay.UI;
using BlueJay.Shared.Components;
using BlueJay.Common.Systems;
using BlueJay.UI.Component;
using BlueJay.UI.Systems;

namespace BlueJay.Shared.Views
{
  public class UIComponentView : View
  {
    /// <summary>
    /// Configuration method is meant to add in all the systems that this game will use and bootstrap the game with entities
    /// that will be used by the game and its systems
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to add the entities and systems to</param>
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Add Processor Systems
      serviceProvider.AddSystem<ViewportSystem>();
      serviceProvider.AddSystem<ClearSystem>(Color.White);
      serviceProvider.AddUISystems();
      serviceProvider.AddUIMouseSupport();
      serviceProvider.AddUIKeyboardSupport();
      serviceProvider.AddUITouchSupport();
      serviceProvider.AddUIComponentSystems();

      serviceProvider.AddUIRenderSystems();

      serviceProvider.AttachComponent<UIComponentTestComponent>();
    }
  }
}
