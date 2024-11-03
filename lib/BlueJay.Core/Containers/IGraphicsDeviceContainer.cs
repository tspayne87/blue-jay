using BlueJay.Core.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Core.Containers
{
  /// <summary>
  /// A container for the graphics device.
  /// </summary>
  public interface IGraphicsDeviceContainer
  {
    /// <summary>
    /// Helper method meant to create a containing texture into the system
    /// </summary>
    /// <param name="width">The current width of the texture being created</param>
    /// <param name="height">The height of the textur being created</param>
    /// <returns>Will return the texture container</returns>
    ITexture2DContainer CreateTexture2D(int width, int height);
  }
}
