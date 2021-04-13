using BlueJay.Events.Interfaces;
using BlueJay.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlueJay.Views
{
  /// <summary>
  /// Ab
  /// </summary>
  public abstract class View : IView
  {
    /// <summary>
    /// The scope we are currently working with
    /// </summary>
    private readonly IServiceScope _scope;

    /// <summary>
    /// Getter to get the current service provider based on the scope
    /// </summary>
    protected IServiceProvider ServiceProvider => _scope.ServiceProvider;

    /// <summary>
    /// Constructor method is meant to set a new scope for the DI and have the view configure that provider once it has been created
    /// </summary>
    /// <param name="serviceProvider">The current service provider we are working with</param>
    public View(IServiceProvider serviceProvider)
    {
      _scope = serviceProvider.CreateScope();
      ConfigureProvider(ServiceProvider);
    }

    /// <summary>
    /// Abstract method is meant to add a hook once the provider has been created
    /// </summary>
    /// <param name="serviceProvider">The service provider that has been created from the collection</param>
    protected abstract void ConfigureProvider(IServiceProvider serviceProvider);

    /// <summary>
    /// The draw method is meant to draw data to the screen
    /// </summary>
    public virtual void Draw()
    {
      ServiceProvider.GetRequiredService<IEventProcessor>().Draw();
    }


    /// <summary>
    /// The update method is meant to update data to the screen
    /// </summary>
    public virtual void Update()
    {
      ServiceProvider.GetRequiredService<IEventProcessor>().Update();
    }
  }
}