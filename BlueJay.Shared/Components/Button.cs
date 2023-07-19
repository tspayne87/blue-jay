using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.UI.Component.Attributes;

namespace BlueJay.Shared.Components
{
  /// <summary>
  /// Button component
  /// </summary>
  [View(@"
<Container Style=""NinePatch: Sample_NinePatch; Padding: 13; NinePatch::Hover: Sample_Hover_NinePatch"">
  <Slot />
</Container>
  ")]
  public class Button : UIComponent { }
}
