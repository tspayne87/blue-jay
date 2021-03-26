using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Plugins
{
  public abstract class Plugin : IEnginePlugin
  {
    public virtual void Initialize()
      { }

    public virtual void LoadContent()
      { }

    public virtual void Update(int delta)
      { }

    public virtual void Draw(int delta)
      { }
  }
}