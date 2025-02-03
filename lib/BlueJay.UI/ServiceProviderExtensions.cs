using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Common.Events.Keyboard;
using BlueJay.Common.Events.Mouse;
using BlueJay.Common.Events.Touch;
using BlueJay.Common.Systems;
using BlueJay.UI.Addons;
using BlueJay.UI.Events.EventListeners;
using BlueJay.UI.Events.EventListeners.UIUpdate;
using BlueJay.UI.Systems;
using BlueJay.Common.Events;
using BlueJay.UI.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace BlueJay.UI
{
  /// <summary>
  /// Service provider extensions to add in extra functionallity to the provider to add in entities and systems
  /// </summary>
  public static class ServiceProviderExtensions
  {
    /// <summary>
    /// Method is meant to add a ui entity to the entity collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static IEntity AddUIEntity(this IServiceProvider provider, IEntity? parent = null)
    {
      var entity = provider.AddEntity(UIStatic.LayerName, 15);

      // Add this item as a child to the parent
      if (parent != null)
      {
        var la = parent.GetAddon<LineageAddon>();
        la.Children.Add(entity);
        parent.Update(la);
      }

      entity.Add(new LineageAddon(parent));
      return entity;
    }

    /// <summary>
    /// Method is meant to add a ui entity to the entity collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="entity">The current created entity we need to add</param>
    /// <returns></returns>
    public static IEntity AddUIEntity(this IServiceProvider provider, IEntity entity, IEntity? parent = null)
    {
      provider.AddEntity(entity, UIStatic.LayerName, 15);

      // Add this item as a child to the parent
      if (parent != null)
      {
        var la = parent.GetAddon<LineageAddon>();
        la.Children.Add(entity);
        parent.Update(la);
      }

      entity.Add(new LineageAddon(parent));
      return entity;
    }

    /// <summary>
    /// Method is meant to add all the UI systems in their correct orders
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static IServiceProvider AddUISystems(this IServiceProvider provider)
    {
      // Add Component systems
      provider.AddSystem<ViewportSystem>();
      provider.AddSystem<UIPositionSystem>();
      provider.AddSystem<FrameUpdateSystem>();

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
    public static IServiceProvider AddUIKeyboardSupport(this IServiceProvider provider)
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

    /// <summary>
    /// Method is meant to add in the UI Rendering systems
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="addDebugRendering">If we want to include the debug rendering systems</param>
    /// <returns>Will return the service provider for chaining</returns>
    public static IServiceProvider AddUIRenderSystems(this IServiceProvider provider, bool addDebugRendering = false)
    {
      provider.AddSystem<UIRenderingSystem>();

      if (addDebugRendering)
        provider.AddSystem<DebugBoundingBoxSystem>();
      return provider;
    }

    public static string GetUIDebugStructureString(this IServiceProvider provider)
    {
      var query = provider.GetRequiredService<IQuery<LineageAddon>>().WhereLayer(UIStatic.LayerName);
      var sb = new StringBuilder();

      foreach (var item in query)
      {
        var la = item.GetAddon<LineageAddon>();
        if (la.Parent == null)
          sb.AppendLine(item.PrintUIStructure());
      }
      return sb.ToString().Trim();
    }

    public static IEntity? GetFirstUIRootEntity(this IServiceProvider provider)
    {
      var query = provider.GetRequiredService<IQuery<LineageAddon>>()
        .WhereLayer(UIStatic.LayerName);

      foreach (var item in query)
      {
        var la = item.GetAddon<LineageAddon>();
        if (la.Parent == null)
          return item;
      }
      return null;
    }

    private static string PrintUIStructure(this IEntity entity, int tab = 2, int indentAmount = 2, char tabChar = '-')
    {
      var sb = new StringBuilder();
      var la = entity.GetAddon<LineageAddon>();
      var ta = entity.GetAddon<TextAddon>();

      sb.Append(tabChar.Explode(tab));
      sb.Append(' ');
      sb.AppendLine(entity.Contains<TextAddon>() ? $"Text: {ta.Text}" : "Container");

      foreach (var child in la.Children)
        sb.AppendLine(child.PrintUIStructure(tab + indentAmount, indentAmount, tabChar));
      return sb.ToString().Trim();
    }

    private static string Explode(this char ch, int amount)
    {
      var sb = new StringBuilder();
      for (var i = 0; i < amount; i++)
        sb.Append(ch);
      return sb.ToString();
    }
  }
}
