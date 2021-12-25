using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Keyboard;
using BlueJay.Events.Mouse;
using BlueJay.Events.Touch;
using BlueJay.Systems;
using BlueJay.UI.Addons;
using BlueJay.UI.EventListeners;
using BlueJay.UI.EventListeners.UIUpdate;
using BlueJay.UI.Systems;
using System;

namespace BlueJay.UI
{
  public static class ServiceProviderExtensions
  {
    /// <summary>
    /// Method is meant to add a ui entity to the entity collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <typeparam name="T">The current object we are adding to the entity collection</typeparam>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in DI</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static T AddUIEntity<T>(this IServiceProvider provider, IEntity parent = null, params object[] parameters)
      where T : IEntity
    {
      var item = provider.AddEntity<T>(UIStatic.LayerName, 15, parameters);

      // Add this item as a child to the parent
      if (parent != null)
      {
        var la = parent.GetAddon<LineageAddon>();
        la.Children.Add(item);
        parent.Update(la);
      }

      item.Add(new LineageAddon(parent));
      return item;
    }

    /// <summary>
    /// Method is meant to add all the UI systems in their correct orders
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static IServiceProvider AddUISystems(this IServiceProvider provider)
    {
      // Add Component systems
      provider.AddSystem<UIPositionSystem>();

      // Add event listeners
      provider.AddEventListener<UIGridCalculationUIUpdateEventListener, UIUpdateEvent>();
      provider.AddEventListener<UICalculateWidthUIUpdateEventListener, UIUpdateEvent>();
      provider.AddEventListener<UICalculateHeightUIUpdateEventListener, UIUpdateEvent>();
      provider.AddEventListener<UITextUIUpdateEventListener, UIUpdateEvent>();
      provider.AddEventListener<UIHeightUIUpdateEventListener, UIUpdateEvent>();
      provider.AddEventListener<UITemplateUIUpdateEventListener, UIUpdateEvent>();
      provider.AddEventListener<UICalculateHeightUIUpdateEventListener, UIUpdateEvent>();
      provider.AddEventListener<UIPositionUIUpdateEventListener, UIUpdateEvent>();
      provider.AddEventListener<UIBoundsTriggerUIUpdateEventListener, UIUpdateEvent>();
      
      provider.AddEventListener<ViewportChangeEventListener, ViewportChangeEvent>();

      provider.AddEventListener<UIStyleUpdateEventListener, StyleUpdateEvent>();
      return provider;
    }

    /// <summary>
    /// Method is meant to add in mouse support for UI elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static IServiceProvider AddUIMouseSupport(this IServiceProvider provider)
    {
      // Add the mouse system if it has not been added
      provider.AddSystem<MouseSystem>();

      // Add the event listener
      provider.AddEventListener<UIMouseMoveEventListener, MouseMoveEvent>();
      provider.AddEventListener<UIMouseDownEventListener, MouseDownEvent>();
      provider.AddEventListener<UIMouseUpEventListener, MouseUpEvent>();
      return provider;
    }

    /// <summary>
    /// Method is meant to add in keyboard support for UI elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static IServiceProvider AddKeyboardSupport(this IServiceProvider provider)
    {
      // Add the keyboard system if it has not been added
      provider.AddSystem<KeyboardSystem>();

      // Add the event listener
      provider.AddEventListener<UIKeyboardUpEventListener, KeyboardUpEvent>();
      return provider;
    }

    /// <summary>
    /// Method is meant to add in touch support for UI elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static IServiceProvider AddUITouchSupport(this IServiceProvider provider)
    {
      // Add the mouse system if it has not been added
      provider.AddSystem<TouchSystem>();

      // Add the event listener
      provider.AddEventListener<UITouchDownEventListener, TouchDownEvent>();
      return provider;
    }
  }
}
