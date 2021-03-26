using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Enums;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Interfaces
{
  public interface IEntity
  {
    long Id { get; set; }
    bool Active { get; set; }
    EntityType Type { get; }

    void LoadContent();
    void Add(IAddon addon);
    void Add<T>(params object[] parameters) where T : IAddon;
    void Remove(IAddon addon);
    IEnumerable<IAddon> GetAddons(long key);
    TAddon GetAddon<TAddon>() where TAddon : IAddon;
    bool MatchKey(long key);
  }
}
