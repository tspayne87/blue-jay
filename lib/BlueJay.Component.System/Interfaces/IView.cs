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
  public interface IView
  {
    void Initialize();
    void LoadContent();
    void Draw(int delta);
    void Update(int delta);
  }

  public interface IViewCollection : ICollection<IView>
  {
    IView Current { get; }
    void SetCurrent<T>() where T : IView;
  }
}