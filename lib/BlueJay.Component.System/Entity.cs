using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Events;
using BlueJay.Events.Interfaces;

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
    private readonly ILayerCollection _layerCollection;

    /// <summary>
    /// The event queue
    /// </summary>
    private readonly IEventQueue _eventQueue;

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
    /// <param name="eventQueue">The event queue</param>
    public Entity(ILayerCollection layerCollection, IEventQueue eventQueue)
    {
      _layerCollection = layerCollection;
      _eventQueue = eventQueue;

      _addonsId = 0;
      _addons = new IAddon[0];
      Active = true;
      Layer = string.Empty;
    }

    #region Lifecycle Methods
    /// <inheritdoc />
    public bool Add<T>(T addon)
      where T : struct, IAddon
    {
      if ((KeyHelper.Create<T>() & _addonsId) == 0)
      {
        Array.Resize(ref _addons, _addons.Length + 1);
        _addons[_addons.Length - 1] = addon;
        _addonsId = KeyHelper.Create(_addons.Select(x => x.GetType()).ToArray());
        _layerCollection[Layer]?.UpdateAddonTree(this);

        _eventQueue.DispatchEvent(new AddAddonEvent(addon), this);
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
        _addonsId = KeyHelper.Create(_addons.Select(x => x.GetType()).ToArray());
        _layerCollection[Layer]?.UpdateAddonTree(this);
        _eventQueue.DispatchEvent(new RemoveAddonEvent(addon), this);
        return true;
      }
      return false;
    }

    /// <inheritdoc />
    public bool Remove<T>()
      where T : struct, IAddon
    {
      if (!Contains<T>()) return false;
      return Remove((T)_addons.First(x => x is T));
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
          _eventQueue.DispatchEvent(new UpdateAddonEvent(addon), this);
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
        var addonKey = KeyHelper.Create(addon.GetType());
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
      var identifier = KeyHelper.Create<T>();
      for (var i = 0; i < _addons.Length; ++i)
      {
        if (KeyHelper.Create(_addons[i].GetType()) == identifier)
        {
          return (T)_addons[i];
        }
      }
      return default(T);
    }
    #endregion

    /// <inheritdoc />
    public virtual void Dispose()
    {

    }

    /// <inheritdoc />
    public bool Contains<A1>() where A1 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1>());

    /// <inheritdoc />
    public bool Contains<A1, A2>() where A1 : struct, IAddon where A2 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1, A2>());

    /// <inheritdoc />
    public bool Contains<A1, A2, A3>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1, A2, A3>());

    /// <inheritdoc />
    public bool Contains<A1, A2, A3, A4>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1, A2, A3, A4>());

    /// <inheritdoc />
    public bool Contains<A1, A2, A3, A4, A5>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1, A2, A3, A4, A5>());

    /// <inheritdoc />
    public bool Contains<A1, A2, A3, A4, A5, A6>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1, A2, A3, A4, A5, A6>());

    /// <inheritdoc />
    public bool Contains<A1, A2, A3, A4, A5, A6, A7>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon where A7 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1, A2, A3, A4, A5, A6, A7>());

    /// <inheritdoc />
    public bool Contains<A1, A2, A3, A4, A5, A6, A7, A8>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon where A7 : struct, IAddon where A8 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1, A2, A3, A4, A5, A6, A7, A8>());

    /// <inheritdoc />
    public bool Contains<A1, A2, A3, A4, A5, A6, A7, A8, A9>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon where A7 : struct, IAddon where A8 : struct, IAddon where A9 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1, A2, A3, A4, A5, A6, A7, A8, A9>());

    /// <inheritdoc />
    public bool Contains<A1, A2, A3, A4, A5, A6, A7, A8, A9, A10>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon where A7 : struct, IAddon where A8 : struct, IAddon where A9 : struct, IAddon where A10 : struct, IAddon
      => MatchKey(KeyHelper.Create<A1, A2, A3, A4, A5, A6, A7, A8, A9, A10>());
  }
}
