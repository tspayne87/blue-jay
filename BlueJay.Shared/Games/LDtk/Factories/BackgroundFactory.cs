using System;
using System.Net.Mime;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.LDtk;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.Shared.Games.LDtk.Factories;

public static class BackgroundFactory
{
  public static IEntity CreateBackground(this IServiceProvider serviceProvider)
  {
    var content = serviceProvider.GetRequiredService<IContentManagerContainer>();

    var json = content.Load<LDtkObject>("SampleMap");

    var entity = serviceProvider.AddEntity();

    return entity;
  }
}
