using Microsoft.Xna.Framework.Content;

namespace BlueJay.Component.System.Interfaces
{
  public interface IEnginePlugin
  {
    void Initialize();
    void LoadContent();
    void Update(int delta);
    void Draw(int delta);
  }
}
