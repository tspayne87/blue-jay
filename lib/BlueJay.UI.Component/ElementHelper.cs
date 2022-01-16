using BlueJay.Common.Events.Keyboard;
using BlueJay.Common.Events.Mouse;
using BlueJay.UI.Component.Language;
using BlueJay.UI.Component.Reactivity;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using static BlueJay.Common.Events.Mouse.MouseEvent;

namespace BlueJay.UI.Component
{
  internal static class ElementHelper
  {
    /// <summary>
    /// Internal method is meant to call the callback lambda in the event prop
    /// </summary>
    /// <typeparam name="T">The type of object that is being processed</typeparam>
    /// <param name="evt">The event prop we need to call the callback from</param>
    /// <param name="scope">The current scope of the event</param>
    /// <param name="obj">The object we are parsing</param>
    /// <returns>Will return the result which will determine if propegation needs to continue</returns>
    internal static bool InvokeEvent<T>(ElementEvent evt, ReactiveScope scope, T obj)
    {
      if (!string.IsNullOrWhiteSpace(evt.Modifier) && evt.Modifier != "Global")
      {
        /// If we have a modifier we want to make sure that is the only key that will invoke the callback
        /// Example: @KeyboardUp.Right="OnRightMouseClickCallbafck()"
        var mouseObj = obj as MouseEvent;
        if (mouseObj != null && Enum.TryParse<ButtonType>(evt.Modifier, out var buttonType) && mouseObj.Button != buttonType)
          return true;

        /// If we have a modifier we want to make sure that is the only key that will invoke the callback
        /// Example: @KeyboardUp.Enter="OnEnterCallback()"
        var keyboardObj = obj as KeyboardEvent;
        if (keyboardObj != null && Enum.TryParse<Keys>(evt.Modifier, out var keyType) && keyboardObj.Key != keyType)
          return true;
      }

      scope[PropNames.Event] = obj;
      var result = (bool)evt.Callback(scope);
      scope.Remove(PropNames.Event);
      return result;
    }
  }
}
