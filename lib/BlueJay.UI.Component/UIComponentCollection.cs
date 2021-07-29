using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// UI Component collection is meant to add a component to the collection to be loaded at a later time
  /// </summary>
  public class UIComponentCollection
  {
    /// <summary>
    /// The list of components we have created
    /// </summary>
    private List<UIComponent> _components { get; set; }

    /// <summary>
    /// Constructor is meant to bootstrap the component collection
    /// </summary>
    public UIComponentCollection()
    {
      _components = new List<UIComponent>();
    }

    /// <summary>
    /// Method is meant to add a UI component to the collection for further use later
    /// </summary>
    /// <param name="component">The component that we need to add</param>
    public void Add(UIComponent component)
    {
      _components.Add(component);
    }

    /// <summary>
    /// Method is meant to get the first item of the type selected
    /// </summary>
    /// <typeparam name="T">The type that we are looking for</typeparam>
    /// <returns>Will return null or the first of the type found</returns>
    public T GetItem<T>()
      where T : UIComponent
    {
      return (T)_components.FirstOrDefault(x => x.GetType() == typeof(T));
    }
  }
}
