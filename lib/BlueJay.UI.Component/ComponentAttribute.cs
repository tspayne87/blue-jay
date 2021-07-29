using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// Component attribute is meant to tell the UI component what other custom components will be
  /// used in the view
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class ComponentAttribute : Attribute
  {
    /// <summary>
    /// The list of components needed in the view
    /// </summary>
    public List<Type> Components { get; private set; }

    /// <summary>
    /// Constructor for defining all custom components used in the view
    /// </summary>
    /// <param name="components">The list of custom components</param>
    public ComponentAttribute(params Type[] components)
    {
      Components = components.ToList();
    }
  }
}
