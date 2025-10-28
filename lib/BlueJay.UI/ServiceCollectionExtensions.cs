using System;
using BlueJay.UI.Interfaces;
using BlueJay.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.UI;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddBlueJayUI(this IServiceCollection collection)
  {
    return collection
      .AddSingleton<IStyleService, StyleService>();
  }
}
