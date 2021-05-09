using BlueJay.Component.System.Interfaces;

namespace BlueJay.UI
{
  public class StyleUpdateEvent
  {
    public IEntity Entity { get; set; }

    public StyleUpdateEvent(IEntity entity)
    {
      Entity = entity;
    }
  }
}
