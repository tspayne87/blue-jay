using BlueJay.UI.Components;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.UI
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddUI(this IServiceCollection serviceCollection)
    {
      return serviceCollection
        .AddScoped<UIComponentCollection>();
    }
  }
}
