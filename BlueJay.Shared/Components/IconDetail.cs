using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.Shared.Components;

[View(@"
<Container Style=""TextureFont: Default; GridColumns: 16; ColumnGap: 5"">
  <Container>
    <Texture AssetName=""TestTiles"" :Frame=""Frame"" Rows=""2"" Cols=""4"" />
  </Container>
  <Container Style=""ColumnSpan: 15; TextAlign: Left"">{{GetHeader()}}</Container>
  <Container Style=""ColumnSpan: 16; TextAlign: Left"">
    <Slot />
  </Container>
</Container>
")]
internal class IconDetail : UIComponent
{
  /// <summary>
  /// The current frame that should be used for the icons
  /// </summary>
  [Prop(PropBinding.OneWay)]
  public readonly ReactiveProperty<int> Frame;

  public IconDetail()
  {
    Frame = new ReactiveProperty<int>(0);
  }

  public Text GetHeader()
  {
    switch (Frame.Value)
    {
      case 0: return new Text("Population");
      case 1: return new Text("Crime");
      case 2: return new Text("Pollution");
      case 3: return new Text("Virtue");
      case 4: return new Text("Conservation");
      case 5: return new Text("Population Cap");
      default: return new Text("Unknown Icon");
    }
  }
}