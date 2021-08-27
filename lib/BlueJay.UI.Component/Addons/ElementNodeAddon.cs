using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Addons
{
  public struct ElementNodeAddon : IAddon
  {
    public ElementNode Node { get; set; }

    public ElementNodeAddon(ElementNode node)
    {
      Node = node;
    }
  }
}
