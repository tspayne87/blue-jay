using BlueJay.Shared;
using Foundation;
using UIKit;

namespace BlueJay.IOS.App
{
  [Register("AppDelegate")]
  internal class Program : UIApplicationDelegate
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
      UIApplication.Main(args, null, typeof(Program));
    }

    public override void FinishedLaunching(UIApplication app)
    {
      RunGame();
    }
  }
}
