using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Lifecycle;
using System.Linq;

namespace BlueJay
{
  /// <summary>
  /// Main lifecycle listener to hook into the draw event
  /// </summary>
  public class DrawEventListener : IEventListener<DrawEvent>
  {
    /// <summary>
    /// The current entity collection
    /// </summary>
    private readonly IEntityCollection _entityCollection;

    /// <summary>
    /// The current system collection
    /// </summary>
    private readonly ISystemCollection _systemCollection;

    /// <summary>
    /// Constructor to build out the update event
    /// </summary>
    /// <param name="entityCollection">The entity collection we are working with</param>
    /// <param name="systemCollection">The current system collection we are working with</param>
    public DrawEventListener(IEntityCollection entityCollection, ISystemCollection systemCollection)
    {
      _entityCollection = entityCollection;
      _systemCollection = systemCollection;
    }

    /// <summary>
    /// Process method is meant to handle the update event and send that update to all systems in the system
    /// </summary>
    /// <param name="evt">The current update event we are working with</param>
    public void Process(IEvent<DrawEvent> evt)
    {
      foreach (var system in _systemCollection)
      {
        system.OnDraw();

        if (system.Key != 0)
        {
          var entities = _entityCollection.GetByKey(system.Key).ToArray();
          for (var i = 0; i < entities.Length; ++i)
          {
            if (entities[i].Active)
            {
              system.OnDraw(entities[i]);
            }
          }
        }
      }
    }
  }
}
