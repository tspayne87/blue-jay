using System;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.UI.Addons;

/// <summary>
/// Represents a text addon.
/// </summary>
/// <param name="text">The current text that needs to be displayed.</param>
public struct TextAddon(string text) : IAddon
{
  /// <summary>
  /// The current text that needs to be displayed.
  /// </summary>
  public string Text = text;
}
