using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Keyboard;
using BlueJay.Events.Mouse;
using BlueJay.UI.Factories;
using System;

namespace BlueJay.UI.Component.Common
{
  /// <summary>
  /// Global container component is meant to be a basic building block in making UI elements
  /// </summary>
  public class Container : UIComponent
  {
    /// <summary>
    /// The service provider to add event listeners onto
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Constructor to inject the service provider from DI
    /// </summary>
    /// <param name="serviceProvider">The service provider from DI</param>
    public Container(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Rendering method to create a container entity to be processed by the UI events and systems
    /// </summary>
    /// <param name="parent">The parent that should be bound to this container</param>
    /// <returns>Will return an entity that represents the container</returns>
    public override IEntity Render(IEntity parent)
    {
      var entity = _serviceProvider.AddContainer(new Style(), parent);
      _serviceProvider.AddEventListener<SelectEvent>(x => Emit("Select", x), entity);
      _serviceProvider.AddEventListener<BlurEvent>(x => Emit("Blur", x), entity);
      _serviceProvider.AddEventListener<FocusEvent>(x => Emit("Focus", x), entity);
      _serviceProvider.AddEventListener<KeyboardUpEvent>(x => Emit("KeyboardUp", x), entity);
      _serviceProvider.AddEventListener<MouseDownEvent>(x => Emit("MouseDown", x), entity);
      _serviceProvider.AddEventListener<MouseMoveEvent>(x => Emit("MouseMove", x), entity);
      _serviceProvider.AddEventListener<MouseUpEvent>(x => Emit("MouseUp", x), entity);
      return entity;
    }
  }
}
