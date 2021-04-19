using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using System.Collections.Generic;

namespace BlueJay.UI.Addons
{
  public class LineageAddon : Addon<LineageAddon>
  {
    /// <summary>
    /// The parent for this lineage
    /// </summary>
    public IEntity Parent { get; set; }

    /// <summary>
    /// The children for this lineage
    /// </summary>
    public List<IEntity> Children { get; set; }

    /// <summary>
    /// The constructor for the lineage
    /// </summary>
    /// <param name="parent">The parent that should be assigned to this addon</param>
    public LineageAddon(IEntity parent)
    {
      Parent = parent;
      Children = new List<IEntity>();
    }
  }
}
