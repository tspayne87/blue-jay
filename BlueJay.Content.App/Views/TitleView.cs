using BlueJay.Component.System.Systems;
using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using BlueJay.UI;
using Microsoft.Extensions.DependencyInjection;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System;
using BlueJay.Content.App.Components;

namespace BlueJay.Content.App.Views
{
  /// <summary>
  /// The title view to switch between sample games for BlueJay
  /// </summary>
  public class TitleView : View
  {
    /// <summary>
    /// Configuration method is meant to add in all the systems that this game will use and bootstrap the game with entities
    /// that will be used by the game and its systems
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to add the entities and systems to</param>
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Add Processor Systems
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);
      serviceProvider.AddUISystems();
      serviceProvider.AddUIMouseSupport();
      serviceProvider.AddUITouchSupport();

      serviceProvider.AddComponentSystem<RenderingSystem>("Default");
      serviceProvider.AddComponentSystem<FPSSystem>("Default", "Default");

      serviceProvider.AddUIComponent<TitleViewComponent>();
    }
  }
}
