using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BlueJay.Core.Test
{
  public class RandomTests
  {
    [Fact]
    public void NextFloat()
    {
      var random = new Random(10004);

      Assert.Equal(5.34543228f, random.NextFloat(5f, 10f));
      Assert.Equal(-3.98801255f, random.NextFloat(-7f, 10f));
      Assert.Equal(-7.780603f, random.NextFloat(-10f, -5f));
    }
  }
}
