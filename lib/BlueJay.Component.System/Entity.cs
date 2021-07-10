using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using BlueJay.Component.System.Collections;

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
    private readonly LayerCollection _layerCollection;

    /// <summary>
    /// The list of addons that are bound to this entity
    /// </summary>
    private IAddon[] _addons;

    /// <summary>
    /// The id based on the addons in this entity
    /// </summary>
    private long _addonsId;

    /// <inheritdoc />
    public long Id { get; set; }

    /// <inheritdoc />
    public bool Active { get; set; }

    /// <inheritdoc />
    public string Layer { get; set; }

    /// <summary>
    /// Constructor to build out this entity through DI
    /// </summary>
    /// <param name="layerCollection">The current layer collection</param>
    public Entity(LayerCollection layerCollection)
    {
      _layerCollection = layerCollection;

      _addonsId = 0;
      _addons = new IAddon[0];
      Active = true;
    }

    #region Lifecycle Methods
    /// <inheritdoc />
    public bool Add<T>(T addon)
      where T : struct, IAddon
    {
      if ((AddonHelper.Identifier<T>() & _addonsId) == 0)
      {
        Array.Resize(ref _addons, _addons.Length + 1);
        _addons[_addons.Length - 1] = addon;
        _addonsId = AddonHelper.Identifier(_addons.Select(x => x.GetType()).ToArray());
        _layerCollection[Layer].Entities.UpdateAddonTree(this);
        return true;
      }
      return false;
    }

    /// <inheritdoc />
    public bool Remove<T>(T addon)
      where T : struct, IAddon
    {
      var index = Array.IndexOf(_addons, addon);
      if (index != -1)
      {
        for (var i = index + 1; i < _addons.Length; ++i)
        {
          _addons[i - 1] = _addons[i];
        }
        Array.Resize(ref _addons, _addons.Length - 1);
        _addonsId = AddonHelper.Identifier(_addons.Select(x => x.GetType()).ToArray());
        _layerCollection[Layer].Entities.UpdateAddonTree(this);
        return true;
      }
      return false;
    }

    /// <inheritdoc />
    public bool Update<T>(T addon)
      where T : struct, IAddon
    {
      for (var i = 0; i < _addons.Length; ++i)
      {
        if (_addons[i].GetType() == typeof(T))
        {
          _addons[i] = addon;
          return true;
        }
      }
      return false;
    }

    /// <inheritdoc />
    public bool Upsert<T>(T addon)
      where T : struct, IAddon
    {
      if (!Update(addon))
      {
        Add(addon);
      }
      return true;
    }
    #endregion

    #region Getter Methods
    /// <inheritdoc />
    public bool MatchKey(long key)
    {
      return (_addonsId & key) == key;
    }

    /// <inheritdoc />
    public IEnumerable<IAddon> GetAddons(long key)
    {
      var addons = new List<IAddon>();
      foreach (var addon in _addons)
      {
        var addonKey = AddonHelper.Identifier(addon.GetType());
        if ((key & addonKey) == addonKey)
        {
          addons.Add(addon);
        }
      }
      return addons;
    }

    /// <inheritdoc />
    public T GetAddon<T>()
      where T : struct, IAddon
    {
      var identifier = AddonHelper.Identifier<T>();
      for (var i = 0; i < _addons.Length; ++i)
      {
        if (AddonHelper.Identifier(_addons[i].GetType()) == identifier)
        {
          return (T)_addons[i];
        }
      }
      return default(T);
    }
    #endregion
  }
}
