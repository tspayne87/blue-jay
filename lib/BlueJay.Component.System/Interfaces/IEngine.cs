using BlueJay.Component.System.Systems;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Interfaces
{
  public interface IEngine
  {
    void Draw(int delta);
    void Update(int delta);
  }
}
