using BlueJay.Component.System.Systems;
using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using BlueJay.UI;
using BlueJay.UI.Factories;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

      serviceProvider.AddComponentSystem<RenderingSystem>();
      serviceProvider.AddComponentSystem<FPSSystem>();

      serviceProvider.AddNinePatch(_contentManager.Load<Texture2D>("Sample_NinePatch"), 400, 50, Color.Red);
    }
  }
}
