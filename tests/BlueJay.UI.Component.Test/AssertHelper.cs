using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component.Test
{
  public static class AssertHelper
  {
    public static void UIEqual(params string[] items)
    {
      Assert.Equal(string.Join(Environment.NewLine, items.Take(items.Length - 1)), items.Last());
    }
  }
}
