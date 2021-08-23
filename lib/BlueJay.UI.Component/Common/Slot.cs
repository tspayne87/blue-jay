using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.Component.Common
{
  /// <summary>
  /// Slot component is meant to act as a placeholder for child elements pased into custom components from their parent
  /// and keep the state of the parent instead of the child component
  /// </summary>
  public class Slot : UIComponent
  {
    /// <summary>
    /// The service provider to add event listeners onto
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Constructor to inject the service provider from DI
    /// </summary>
    /// <param name="serviceProvider">The service provider from DI</param>
    public Slot(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Render is meant to iterate over all the child nodes and reprocess them with the correct bindings
    /// </summary>
    /// <param name="parent">The current entity parent we are working with</param>
    /// <returns>The list of entities generated from processing in this way</returns>
    //public override IEntity Render(IEntity parent)
    //{
    //  var parentComponents = (ComponentAttribute)Attribute.GetCustomAttribute(Parent.GetType(), typeof(ComponentAttribute));
    //  var components = ServiceProviderExtension.Globals.Concat(parentComponents?.Components ?? new List<Type>());
    //  for (var i = 0; i < Current.Node.ChildNodes.Count; ++i)
    //  {
    //    // ServiceProviderExtension.GenerateItem(Current.Node.ChildNodes[i], this._serviceProvider, components, Parent, Parent.Parent, parent);
    //  }
    //  return null;
    //}
  }
}
