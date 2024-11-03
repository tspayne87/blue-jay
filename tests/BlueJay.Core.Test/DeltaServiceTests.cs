using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlueJay.Core.Test
{
  public class DeltaServiceTests
  {
    [Fact]
    public void Create()
    {
      var service = new DeltaService() { Delta = 1000, DeltaSeconds = 1 };

      Assert.Equal(1000, service.Delta);
      Assert.Equal(1, service.DeltaSeconds);
    }
  }
}
