using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Core
{
  public static class MathHelper
  {
    public static float Clamp(float val, float min, float max) => val < min ? min : (val > max ? max : val);
  }
}
