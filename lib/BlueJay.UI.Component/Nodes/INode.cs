using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component.Nodes
{
  public interface INode
  {
    public void GenerateUI(Style? globalStyle = null);
  }
}
