using System;
using System.Collections.Generic;
using System.Linq;
using BlueJay.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.Views
{
  /// <summary>
  /// The collection of views we are working with
  /// </summary>
  internal class ViewCollection : IViewCollection
  {
    /// <summary>
    /// The view provider we will use to find the collection and build out the object with
    /// </summary>
    private readonly IServiceProvider _provider;

    /// <summary>
    /// The list of collections so we can switch between them
    /// </summary>
    private List<IView> _collection = new List<IView>();

    /// <summary>
    /// The current view that was assigned
    /// </summary>
    private IView _current;

    /// <summary>
    /// The current rendering view
    /// </summary>
    public IView Current
    {
      get => _current;
      private set
      {
        _current?.Leave();
        _current = value;
        _current?.Enter();
      }
    }

    /// <summary>
    /// Constructor meant to inject various services into this collection
    /// </summary>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    public ViewCollection(IServiceProvider provider)
    {
      _provider = provider;
    }

    /// <summary>
    /// Helper method is meant to set the current view we are working with
    /// </summary>
    /// <typeparam name="T">The view we want to find</typeparam>
    public T SetCurrent<T>()
      where T : IView
    {
      var item = _collection.FirstOrDefault(x => x.GetType() == typeof(T));
      if (item == null)
      {
        item = ActivatorUtilities.CreateInstance<T>(_provider);
        item.Initialize(_provider);
        _collection.Add(item);
      }
      Current = item;
      return (T)item;
    }
  }
}