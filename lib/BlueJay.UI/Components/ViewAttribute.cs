using System;
using System.Xml;

namespace BlueJay.UI.Components
{
  /// <summary>
  /// The view attribute that will store the xml view information for the UI component
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class ViewAttribute : Attribute
  {
    /// <summary>
    /// The xml document for the view
    /// </summary>
    public XmlDocument View { get; set; }

    /// <summary>
    /// Constructor to get the string for the xml that should be used for this view
    /// </summary>
    /// <param name="xml">The string representation of the view</param>
    public ViewAttribute(string xml)
    {
      View = new XmlDocument();
      View.LoadXml(xml);
    }
  }
}
