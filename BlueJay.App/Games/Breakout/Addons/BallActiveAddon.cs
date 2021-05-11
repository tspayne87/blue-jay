using BlueJay.Component.System.Addons;

namespace BlueJay.App.Games.Breakout.Addons
{
  public class BallActiveAddon : Addon<BallActiveAddon>
  {
    public bool IsActive { get; set; }

    public BallActiveAddon()
    {
      IsActive = false;
    }
  }
}
