using BlueJay.Component.System.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlueJay.Component.System
{
  public static class IEnumerableExtensions
  {
    /// <summary>
    /// Method is meant to get an addon by its identifier
    /// </summary>
    /// <typeparam name="TComponent">The current addon we are looking for</typeparam>
    /// <param name="list">The list we are looking in</param>
    /// <returns>Will return the FirstOrDefault addon it finds</returns>
    public static TComponent ByIdentifier<TComponent>(this IEnumerable<IAddon> list)
      where TComponent : IAddon
    {
      var identifier = IdentifierHelper.Addon<TComponent>();
      return (TComponent)list.FirstOrDefault(x => x.Identifier == identifier);
    }
  }
}
