using BlueJay.Component.System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Test.Collections
{
  [Collection("KeyHelper Tests")]
  public class FontCollectionTests
  {
    [Fact]
    public void FontCollectionCreation()
    {
      var fontCollection = new FontCollection();

      Assert.NotNull(fontCollection);
      Assert.Empty(fontCollection.SpriteFonts);
      Assert.Empty(fontCollection.TextureFonts);
    }
  }
}
