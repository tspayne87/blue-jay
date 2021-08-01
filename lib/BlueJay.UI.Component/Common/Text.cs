using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Keyboard;
using BlueJay.Events.Mouse;
using BlueJay.UI.Addons;
using BlueJay.UI.Factories;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text.RegularExpressions;

namespace BlueJay.UI.Component.Common
{
  /// <summary>
  /// Global text UI component that is meant to render text to the screen
  /// </summary>
  public class Text : UIComponent
  {
    /// <summary>
    /// The event queue we need to fire updates if a reactive element makes an update and we want to process the UI again
    /// </summary>
    private readonly EventQueue _eventQueue;

    /// <summary>
    /// The graphics element so we can send the proper update methods
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The service provider to add event listeners onto
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Constructor to inject the service provider from DI
    /// </summary>
    /// <param name="eventQueue">The current event queue</param>
    /// <param name="graphics">The graphics device that represents the screen</param>
    /// <param name="serviceProvider">The service provider from DI</param>
    public Text(EventQueue eventQueue, GraphicsDevice graphics, IServiceProvider serviceProvider)
    {
      _eventQueue = eventQueue;
      _graphics = graphics;
      _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Custom renderer is meant to build out the text entity and bind events to reactive properties to update
    /// the UI on change of those properties and to also pass up select events to the parent component
    /// </summary>
    /// <param name="parent">The current parent component we need to add to the created entity</param>
    /// <returns>The generated text entity ready for rendering</returns>
    public override IEntity Render(IEntity parent)
    {
      IEntity entity;
      var txt = Node.InnerText;
      if (ServiceProviderExtension.ExpressionRegex.IsMatch(txt))
      {
        entity = _serviceProvider.AddText(ServiceProviderExtension.ExpressionRegex.TranslateText(txt, Current).Trim(), parent);

        // Calculate all the reactive props used and add a callback if a property changes
        foreach (var prop in ServiceProviderExtension.ExpressionRegex.GetReactiveProps(txt, Current))
        {
          prop.PropertyChanged += (sender, o) =>
          {
            var ta = entity.GetAddon<TextAddon>();
            ta.Text = ServiceProviderExtension.ExpressionRegex.TranslateText(txt, Current);
            entity.Update(ta);
            _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
          };
        }
      }
      else
      {
        entity = _serviceProvider.AddText(txt, parent);
      }

      // Add Event Listeners that should send event up to parent since text cannot handle events
      _serviceProvider.AddEventListener(CallParentEmitCallback<SelectEvent>("onSelect"), entity);
      _serviceProvider.AddEventListener(CallParentEmitCallback<BlurEvent>("onBlur"), entity);
      _serviceProvider.AddEventListener(CallParentEmitCallback<FocusEvent>("onFocus"), entity);
      _serviceProvider.AddEventListener(CallParentEmitCallback<KeyboardUpEvent>("onKeyboardUp"), entity);
      _serviceProvider.AddEventListener(CallParentEmitCallback<MouseDownEvent>("MouseDown"), entity);
      _serviceProvider.AddEventListener(CallParentEmitCallback<MouseMoveEvent>("MouseMove"), entity);
      _serviceProvider.AddEventListener(CallParentEmitCallback<MouseUpEvent>("MouseUp"), entity);

      return entity;
    }

    public Func<T, bool> CallParentEmitCallback<T>(string evt)
    {
      return x =>
      {
        var method = Current?.GetType().GetMethod(Node?.ParentNode?.Attributes?[evt]?.InnerText ?? string.Empty);
        if (method != null)
        {
          return (bool)method.Invoke(Current, new object[] { x });
        }
        return true;
      };
    }
  }
}
