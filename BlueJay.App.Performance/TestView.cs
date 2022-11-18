using BlueJay.Common.Systems;
using BlueJay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.App.Performance
{
  public class TestView : View
  {
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      serviceProvider.AddSystem<VelocitySystem>();
    }
  }
}
