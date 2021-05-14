using BlueJay.Component.System.Systems;
using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using BlueJay.UI;
using BlueJay.UI.Factories;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BlueJay.Core;

namespace BlueJay.App.Views
{
  public class TitleView : View
  {
    public readonly ContentManager _contentManager;

    public TitleView(ContentManager contentManager)
    {
      _contentManager = contentManager;
    }

    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);
      serviceProvider.AddUISystems();
      serviceProvider.AddUIMouseSupport();

      serviceProvider.AddComponentSystem<RenderingSystem>();
      serviceProvider.AddComponentSystem<FPSSystem>();

      // Create layout and a button
      // <div><div><btn></btn></div></div>
      var parent = serviceProvider.AddContainer(new Style() { WidthPercentage = 0.33f, TopOffset = 50, HorizontalAlign = HorizontalAlign.Center });
      var button = serviceProvider.AddContainer(
        new Style() { NinePatch = new NinePatch(_contentManager.Load<Texture2D>("Sample_NinePatch")), WidthPercentage = 1, Height = 50, Padding = 13 },
        new Style() { NinePatch = new NinePatch(_contentManager.Load<Texture2D>("Sample_Hover_NinePatch")) },
        parent
      );
      serviceProvider.AddText("Hello World", button);

      // Create a second layout with a button
      var parent2 = serviceProvider.AddContainer(new Style() { WidthPercentage = 0.5f, TopOffset = 200, HorizontalAlign = HorizontalAlign.Center });
      var button2 = serviceProvider.AddContainer(new Style() { NinePatch = new NinePatch(_contentManager.Load<Texture2D>("Sample_NinePatch")), WidthPercentage = 1, Height = 150, Padding = 13 }, parent2);
      serviceProvider.AddText("Hello World With a Long Set Of Words to Cause an Overflow", button2);
    }
  }
}
