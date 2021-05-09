using System;

namespace BlueJay.App
{
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      using (var game = new BlueJayAppGame())
        game.Run();
    }
  }
}
