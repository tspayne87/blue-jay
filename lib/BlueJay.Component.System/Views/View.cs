using BlueJay.Component.System.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlueJay.Component.System.Views
{
  public abstract class View : IView
  {
    private readonly IServiceScope _scope;

    protected IServiceProvider ServiceProvider => _scope.ServiceProvider;

    public View(IServiceProvider serviceProvider)
    {
      _scope = serviceProvider.CreateScope();
    }

    public virtual void Initialize() { }
    public virtual void LoadContent() { }

    public virtual void Draw(int delta)
    {
      ServiceProvider.GetService<IEngine>()
        .Draw(delta);
    }

    public virtual void Update(int delta)
    {
      ServiceProvider.GetService<IEngine>()
        .Update(delta);
    }
  }
}