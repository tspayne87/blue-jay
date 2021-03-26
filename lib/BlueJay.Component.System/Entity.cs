using BlueJay.Component.System.Enums;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.Component.System
{
  /// <summary>
  /// The basic building block for the component system that determines the objects in the game
  /// </summary>
  public class Entity : IEntity
  {
    /// <summary>
    /// The current entity collection 
    /// </summary>
    private readonly IEntityCollection _entityCollection;

    /// <summary>
    /// The service provider to build out objects from to handle DI
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// The current content manager needed to load various assets
    /// </summary>
    private readonly ContentManager _contentManager;

    /// <summary>
    /// The scopped trigger system for this entity
    /// </summary>
    private readonly ITriggerSystem _triggerSystem;

    /// <summary>
    /// The list of addons that are bound to this entity
    /// </summary>
    private List<IAddon> _addons = new List<IAddon>();

    /// <summary>
    /// The current unique identifier for this entity so it can be rebuilt through networking
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Determines if this entity is currently active and should be interacted with
    /// </summary>
    public bool Active { get; set; } = true;

    /// <summary>
    /// The current type of this entity, some very basic and broad entities are used for mouse manipulation
    /// </summary>
    public virtual EntityType Type => EntityType.Basic;

    /// <summary>
    /// Getter to get the trigger system so we can add items to it
    /// </summary>
    public ITriggerSystem Trigger => _triggerSystem;

    /// <summary>
    /// Constructor to build out this entity through DI
    /// </summary>
    /// <param name="entityCollection">The current entity collection</param>
    /// <param name="serviceProvider">The current service provider so we can generate addons through DI</param>
    /// <param name="contentManager">The current content manager so we can load assets through the entity if needed</param>
    public Entity(IEntityCollection entityCollection, IServiceProvider serviceProvider, ITriggerSystem triggerSystem, ContentManager contentManager)
    {
      _entityCollection = entityCollection;
      _serviceProvider = serviceProvider;
      _contentManager = contentManager;
      _triggerSystem = triggerSystem;
    }

    #region Lifecycle Methods
    /// <summary>
    /// Method is meant to add an addon through DI
    /// </summary>
    /// <typeparam name="T">The addon that should be added</typeparam>
    /// <param name="parameters">The construction parameters that do not exist in the DI context</param>
    public void Add<T>(params object[] parameters)
      where T : IAddon
    {
      Add(ActivatorUtilities.CreateInstance<T>(_serviceProvider, parameters));
    }

    /// <summary>
    /// Method is meant to add an addon when the object has already been generated
    /// </summary>
    /// <param name="addon">The addon to append to the list and update the addon trees</param>
    public void Add(IAddon addon)
    {
      if (!_addons.Contains(addon))
      {
        addon.SetTriggerSystem(_triggerSystem);
        addon.LoadContent(_contentManager);
        _addons.Add(addon);
        _entityCollection.UpdateAddonTree(this);
      }
    }

    /// <summary>
    /// Method is meant to remove and addon from the list
    /// </summary>
    /// <param name="addon">The current addon to be removed</param>
    public void Remove(IAddon addon)
    {
      if (_addons.Remove(addon))
      {
        addon.OnRemove();
        _entityCollection.UpdateAddonTree(this);
      }
    }
    
    /// <summary>
    /// Lifecycle hook is meant to load assets for this entity by passing them down to the addons
    /// </summary>
    public void LoadContent()
    {
      foreach (var addon in _addons) addon.LoadContent(_contentManager);
    }
    #endregion

    #region Getter Methods
    /// <summary>
    /// Helper method is meant to match if the key given is able to be sloted into the key
    /// </summary>
    /// <param name="key">The key that is meant to be a bitwise flag to determine the possible addons that exist on this entity</param>
    /// <returns>Will return true if the key matches the list of addons in this entity</returns>
    public bool MatchKey(long key)
    {
      return GetAddons(key).SumOr(x => x.Identifier) == key;
    }

    /// <summary>
    /// Helper method to get a list of addons that represent the key given
    /// </summary>
    /// <param name="key">The key that determines the list of addons we are looking for</param>
    /// <returns>A list of addons based on the key given</returns>
    public IEnumerable<IAddon> GetAddons(long key)
    {
      var addons = new List<IAddon>();
      foreach (var addon in _addons)
      {
        if (key.HasFlag(addon.Identifier))
        {
          addons.Add(addon);
        }
      }

      return addons.OrderBy(x => x.Identifier);
    }

    /// <summary>
    /// Helper method is meant to get a specific addon otherwise null will be given
    /// </summary>
    /// <typeparam name="TAddon">The addon that should be gotten by this entity</typeparam>
    /// <returns>The addon or null if not exist</returns>
    public TAddon GetAddon<TAddon>()
      where TAddon : IAddon
    {
      return _addons.ByIdentifier<TAddon>();
    }
    #endregion
  }
}
