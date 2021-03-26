using BlueJay.Component.System.Interfaces;
using System;
using System.Linq;
using BlueJay.Component.System.Enums;

namespace BlueJay.Component.System
{
  public class Engine : IEngine
  {
    /// <summary>
    /// The current entity collection
    /// </summary>
    private readonly IEntityCollection _entityCollection;

    /// <summary>
    /// The current plugin collection
    /// </summary>
    private readonly IPluginCollection _pluginCollection;

    /// <summary>
    /// The current system collection
    /// </summary>
    private readonly ISystemCollection _systemCollection;

    /// <summary>
    /// Constructor is meant to build out an engine, this is meant to be a scoped DI object so that it can get the scoped collections
    /// </summary>
    /// <param name="entityCollection">The scoped entity collection</param>
    /// <param name="pluginCollection">The scoped plugin collection</param>
    /// <param name="systemCollection">The scoped system collection</param>
    public Engine(IEntityCollection entityCollection, IPluginCollection pluginCollection, ISystemCollection systemCollection)
    {
      _entityCollection = entityCollection;
      _pluginCollection = pluginCollection;
      _systemCollection = systemCollection;
    }

    #region Lifecycle Methods
    /// <summary>
    /// Lifecycle hook is meant to update all plugins and systems in the current scoped DI context
    /// </summary>
    /// <param name="delta">The current delta since the last update call</param>
    public void Update(int delta)
    {
      foreach (var plugin in _pluginCollection) plugin.Update(delta);
      foreach(var system in _systemCollection)
      {
        system.Update(delta);

        if (system.Key != 0)
        {
          var entities = _entityCollection.GetByKey(system.Key).ToArray();
          foreach(var entity in system.UpdateOrder == SystemUpdateOrder.Reverse ? entities.Reverse() : entities)
          {
            if (entity.Active)
            {
              system.Update(delta, entity);
            }
          }
        }
      }
    }

    /// <summary>
    /// Lifecycle hook is meant to draw all plugins and systems in the current scoped DI context
    /// </summary>
    /// <param name="delta">The current delta since the last update call</param>
    public void Draw(int delta)
    {
      foreach (var plugin in _pluginCollection) plugin.Draw(delta);
      foreach (var system in _systemCollection)
      {
        system.Draw(delta);
        
        if (system.Key != 0)
        {
          var entities = _entityCollection.GetByKey(system.Key);
          foreach (var entity in system.DrawOrder == SystemDrawOrder.Reverse ? entities.ToArray().Reverse() : entities)
          {
            if (entity.Active)
            {
              system.Draw(delta, entity);
            }
          }
        }
      }
    }
    #endregion
  }
}