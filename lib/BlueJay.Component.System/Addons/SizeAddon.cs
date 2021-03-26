using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Addons
{
  public class SizeAddon : Addon<SizeAddon>
  {
    public Size Size { get; set; }

    public SizeAddon(Size size)
    {
      Size = size;
    }
  }
}
