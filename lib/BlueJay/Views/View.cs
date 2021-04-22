using BlueJay.Events;
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
    private IServiceScope _scope;

    /// <summary>
    /// Getter to get the current service provider based on the scope
    /// </summary>
    protected IServiceProvider ServiceProvider => _scope.ServiceProvider;

    /// <summary>
    /// Initialization method is meant to set a new scope for the DI and have the view configure that provider once it has been created
    /// </summary>
    /// <param name="serviceProvider">The current service provider we are working with</param>
    public void Initialize(IServiceProvider serviceProvider)
    {
      // Lock to only allow the scope to be created once
      if (_scope != null) throw new ArgumentException("Scope has already been created", nameof(_scope));

      _scope = serviceProvider.CreateScope();

      // Add basic listeners for the queue
      var queue = ServiceProvider.GetRequiredService<EventQueue>();
      queue.AddEventListener(ActivatorUtilities.CreateInstance<UpdateEventListener>(ServiceProvider));
      queue.AddEventListener(ActivatorUtilities.CreateInstance<DrawEventListener>(ServiceProvider));
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