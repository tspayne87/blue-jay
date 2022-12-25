using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component.Nodes.Elements
{
  internal class UIElement : IDisposable
  {
    private readonly ILayerCollection _layers;
    private readonly IEntity _entity;
    private readonly List<IDisposable> _disposables;
    private UIElement? _parent;
    private List<UIElement> _children;
    private bool disposedValue;

    public IEntity Entity => _entity;
    public List<IDisposable> Disposables => _disposables;

    public Node Node { get; private set; }
    public List<UIElement> Children => _children;
    public UIElement? Parent
    {
      get => _parent;
      set
      {
        // Remove this parent if it already exists on a different parent
        if (_parent != null)
          _parent._children.Remove(this);

        // Add the parent to the new child and update the parent that it has a new child
        _parent = value;
        if (_parent != null)
          _parent._children.Add(this);
      }
    }

    public UIElement(ILayerCollection layers, Node node, IEntity entity, List<IDisposable> disposables)
    {
      Node = node;

      _layers = layers;
      _entity = entity;
      _disposables = disposables;

      _children = new List<UIElement>();
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          foreach (var child in _children)
            child.Dispose();

          _layers.Remove(_entity);
          foreach (var item in _disposables)
            item.Dispose();
        }
        disposedValue = true;
      }
    }

    public void Dispose()
    {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
