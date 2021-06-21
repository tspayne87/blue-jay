using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueJay.UI.Components
{
  public class UIComponentCollection
  {
    private List<object> _components { get; set; }

    public UIComponentCollection()
    {
      _components = new List<object>();
    }

    public void Add(object component)
    {
      _components.Add(component);
    }

    public T GetItem<T>()
    {
      return (T)_components.FirstOrDefault(x => x.GetType() == typeof(T));
    }
  }
}
