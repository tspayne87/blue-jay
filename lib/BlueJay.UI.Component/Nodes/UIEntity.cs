using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// Basic element that handles the entity as well as all the connections to that entity like,
  /// events and reactive elements that need to either be called or watched on to update the entity in
  /// one way or the other
  /// </summary>
  internal class UIEntity : IDisposable
  {
    /// <summary>
    /// The layer collection meant to allow to remove this entity when the ui element is disposed
    /// </summary>
    private readonly ILayerCollection? _layers;

    /// <summary>
    /// The stored entity for the UIElement
    /// </summary>
    private IEntity? _entity;

    /// <summary>
    /// The parent ui element that was created for this ui element
    /// </summary>
    private UIEntity? _parent;

    /// <summary>
    /// If this ui element is disposed
    /// </summary>
    private bool disposedValue;

    /// <summary>
    /// The internal scope id to load the correct component for this ui element
    /// </summary>
    private Guid? _scopeKey;

    /// <summary>
    /// The entity scope for items that need to be bound to the structure of the nodes and
    /// not the component nodes
    /// </summary>
    private UIEntityScope? _entityScope;

    /// <summary>
    /// The scope for the UI Element, will get the closes scope key based on the UIContainer
    /// </summary>
    public Guid? ScopeKey => _scopeKey.HasValue ? _scopeKey : _parent == null ? null : _parent.ScopeKey;

    /// <summary>
    /// Gets the parent scope id for the UIElement tree
    /// </summary>
    public Guid? ParentScopeKey => ScopeKey.HasValue ? GetFirstParentScopeKey(ScopeKey.Value) : null;

    /// <summary>
    /// The current entity owned by this ui element
    /// </summary>
    public IEntity? Entity => _entity ?? Parent?.Entity;

    /// <summary>
    /// The list of listeners we want to dispose of when this element is disposed, this is mainly connections
    /// to reactive properties
    /// </summary>
    public List<IDisposable> Disposables { get; private set; }

    /// <summary>
    /// The current node this element was generated from
    /// </summary>
    public Node Node { get; private set; }

    /// <summary>
    /// All the children that is under this ui element, we mainly keep a reference to this because when this
    /// ui element is disposed of we also need to remove these elements
    /// </summary>
    public List<UIEntity> Children { get; set; }

    /// <summary>
    /// The parent that should exist for this ui element is also meant to attach itself to the children and keep
    /// the node tree intact
    /// </summary>
    public UIEntity? Parent
    {
      get => _parent;
      set
      {
        // Remove this parent if it already exists on a different parent
        if (_parent != null)
          _parent.Children.Remove(this);

        // Add the parent to the new child and update the parent that it has a new child
        _parent = value;
        if (_parent != null)
          _parent.Children.Add(this);
      }
    }

    /// <summary>
    /// The current entity scope that should exist for the current UIEntity
    /// </summary>
    public UIEntityScope EntityScope
    {
      get
      {
        if (_entityScope != null)
          return _entityScope;

        if (_parent == null)
          throw new ArgumentNullException(nameof(EntityScope));
        return _parent.EntityScope;
      }
    }

    /// <summary>
    /// The constructor mainly meant for root nodes for the component
    /// </summary>
    /// <param name="node">The root node for this UIElement</param>
    /// <param name="scopeKey">The scope key that will be used to get the correct component that is created by the scope</param>
    /// <param name="entityScope">The entity scope that the following children elements need to follow</param>
    public UIEntity(Node node, Guid scopeKey, UIEntityScope? entityScope)
    {
      _scopeKey = scopeKey;
      _entityScope = entityScope;

      Node = node;
      Children = new List<UIEntity>();
      Disposables = new List<IDisposable>();
    }

    /// <summary>
    /// Basic constructor to build out the UI element
    /// </summary>
    /// <param name="layers">The layer collection meant to allow to remove this entity when the ui element is disposed</param>
    /// <param name="node">The current node this element was generated from</param>
    /// <param name="entity">The current entity owned by this ui element</param>
    /// <param name="disposables">The list of listeners we want to dispose of when this element is disposed, this is mainly connections to reactive properties</param>
    public UIEntity(ILayerCollection layers, Node node, IEntity entity, List<IDisposable> disposables)
    {
      _layers = layers;

      Node = node;
      _entity = entity;
      Disposables = disposables;
      Children = new List<UIEntity>();
    }

    /// <summary>
    /// Determines if this entity is using the same key as a parent
    /// </summary>
    /// <param name="scopeKey">The scope key to check if a parent is using</param>
    /// <returns>Will return true/false if a parent is using the scope key given</returns>
    public bool IsParentUsingScopeKey(Guid scopeKey)
    {
      return (_parent != null && _parent.ScopeKey == ScopeKey) || (_parent?.IsParentUsingScopeKey(scopeKey) ?? false);
    }

    /// <inheritdoc />
    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          foreach (var child in Children)
            child.Dispose();

          if (_layers != null && Entity != null)
            _layers.Remove(Entity);

          foreach (var item in Disposables)
            item.Dispose();
        }
        disposedValue = true;
      }
    }

    /// <summary>
    /// Gets the first scope key found up the entity parent structure that is not the same as the given scope key
    /// </summary>
    /// <param name="scopeKey">The scope key we want to find a difference from</param>
    /// <returns>Will return the first different scope key from the current UIEntities parents</returns>
    private Guid? GetFirstParentScopeKey(Guid scopeKey)
    {
      if (_parent == null)
        return null;

      if (_parent._scopeKey.HasValue && _parent._scopeKey.Value != scopeKey)
        return _parent._scopeKey.Value;

      return _parent.GetFirstParentScopeKey(scopeKey);
    }

    /// <inheritdoc />
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
