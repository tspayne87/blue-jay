using System;
using Xunit;

namespace BlueJay.Core.Test
{
  public class MathHelperTests
  {
    [Fact]
    public void Clamp()
    {
      Assert.Equal(5f, MathHelper.Clamp(10f, 0f, 5f));
      Assert.Equal(5f, MathHelper.Clamp(5f, 0f, 10f));
      Assert.Equal(5f, MathHelper.Clamp(-10f, 5f, 10f));

      Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.Clamp(float.NaN, 0f, 5f));
      Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.Clamp(0f, float.NaN, 5f));
      Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.Clamp(0f, 0f, float.NaN));
      Assert.Throws<ArgumentException>(() => MathHelper.Clamp(0f, 10f, 0f));
    }
  }
}
