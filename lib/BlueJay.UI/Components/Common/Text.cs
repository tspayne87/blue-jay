using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.UI.Addons;
using BlueJay.UI.Factories;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text.RegularExpressions;

namespace BlueJay.UI.Components.Common
{
  /// <summary>
  /// Global text UI component that is meant to render text to the screen
  /// </summary>
  public class Text : UIComponent
  {
    /// <summary>
    /// The expression regex to process the text and convert the expressions into data from the component
    /// </summary>
    private readonly Regex _expressionRegex;

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
      _expressionRegex = new Regex(@"{{([^}]+)}}");
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
      var field = Current.GetType().GetField(Node.InnerText.Replace("{{", string.Empty).Replace("}}", string.Empty));
      if (_expressionRegex.IsMatch(txt))
      {
        entity = _serviceProvider.AddText(_expressionRegex.TranslateText(txt, Current), parent);

        // Calculate all the reactive props used and add a callback if a property changes
        foreach (var prop in _expressionRegex.GetReactiveProps(txt, Current))
        {
          prop.PropertyChanged += (sender, o) =>
          {
            var ta = entity.GetAddon<TextAddon>();
            if (ta != null)
            {
              ta.Text = _expressionRegex.TranslateText(txt, Current);
              _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
            }
          };
        }
      }
      else
      {
        entity = _serviceProvider.AddText(txt, parent);
      }

      // Add select event listener
      _serviceProvider.AddEventListener<SelectEvent>(x => {
        var method = Current?.GetType().GetMethod(Node?.ParentNode?.Attributes?[$"onSelect"]?.InnerText ?? string.Empty);
        if (method != null)
        {
          return (bool)method.Invoke(Current, new object[] { x });
        }
        return true;
      }, entity);
      return entity;
    }
  }
}
