using System;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.UI;

public static class IQueryExtensions
{
  public static IQuery ExcludeUILayer(this IQuery query)
  {
    return query.ExcludeLayer(StaticNames.UILAYER);
  }
}
