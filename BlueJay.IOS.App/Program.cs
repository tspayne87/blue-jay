using System;
using BlueJay.Common.App;
using Foundation;
using UIKit;

namespace BlueJay.IOS.App
{
  [Register("AppDelegate")]
  class Program : UIApplicationDelegate
  {
    private static BlueJayAppGame game;

    internal static void RunGame()
    {
      game = new BlueJayAppGame();
      game.Run();
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
      UIApplication.Main(args, null, "AppDelegate");
    }

    public override void FinishedLaunching(UIApplication app)
    {
      RunGame();
    }
  }
}
