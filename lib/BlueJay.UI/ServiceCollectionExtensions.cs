﻿using BlueJay.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI
{
  public static class ServiceCollectionExtensions
  {
    /// <summary>
    /// Helper method is meant to add extra UI elements to DI
    /// </summary>
    /// <param name="serviceCollection">The service collection we are wanting to add some items to</param>
    /// <returns>Will return the service collection given with the added items</returns>
    public static IServiceCollection AddUI(this IServiceCollection serviceCollection)
    {
      return serviceCollection
        .AddSingleton<UIService>();
    }
  }
}
