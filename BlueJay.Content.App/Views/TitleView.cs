using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using BlueJay.UI;
using BlueJay.Content.App.Components;
using BlueJay.Common.Systems;

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
      serviceProvider.AddSystem<ClearSystem>(Color.White);
      serviceProvider.AddUISystems();
      serviceProvider.AddUIMouseSupport();
      serviceProvider.AddUITouchSupport();

      serviceProvider.AddSystem<RenderingSystem>();
      serviceProvider.AddSystem<FPSSystem>("Default");

      serviceProvider.AddUIComponent<TitleViewComponent>();
    }
  }
}
