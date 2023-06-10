using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component.Nodes.Elements
{
  /// <summary>
  /// Basic element that handles the entity as well as all the connections to that entity like,
  /// events and reactive elements that need to either be called or watched on to update the entity in
  /// one way or the other
  /// </summary>
  internal class UIElement : IDisposable
  {
    /// <summary>
    /// The layer collection meant to allow to remove this entity when the ui element is disposed
    /// </summary>
    private readonly ILayerCollection _layers;

    /// <summary>
    /// The parent ui element that was created for this ui element
    /// </summary>
    private UIElement? _parent;

    /// <summary>
    /// If this ui element is disposed
    /// </summary>
    private bool disposedValue;

    /// <summary>
    /// The current entity owned by this ui element
    /// </summary>
    public IEntity Entity { get; private set; }

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
    public List<UIElement> Children { get; private set; }

    /// <summary>
    /// The parent that should exist for this ui element is also meant to attach itself to the children and keep
    /// the node tree intact
    /// </summary>
    public UIElement? Parent
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
    /// Basic constructor to build out the UI element
    /// </summary>
    /// <param name="layers">The layer collection meant to allow to remove this entity when the ui element is disposed</param>
    /// <param name="node">The current node this element was generated from</param>
    /// <param name="entity">The current entity owned by this ui element</param>
    /// <param name="disposables">The list of listeners we want to dispose of when this element is disposed, this is mainly connections to reactive properties</param>
    public UIElement(ILayerCollection layers, Node node, IEntity entity, List<IDisposable> disposables)
    {
      _layers = layers;

      Node = node;
      Entity = entity;
      Disposables = disposables;
      Children = new List<UIElement>();
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

          _layers.Remove(Entity);
          foreach (var item in Disposables)
            item.Dispose();
        }
        disposedValue = true;
      }
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
