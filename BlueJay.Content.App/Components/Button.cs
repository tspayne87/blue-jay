using BlueJay.UI;
using BlueJay.UI.Components;

namespace BlueJay.Content.App.Components
{
  [View(@"
<container style=""NinePatch: Sample_NinePatch; Padding: 13"" hoverStyle=""NinePatch: Sample_Hover_NinePatch"" onClick=""OnClick"">
  <text style=""TextureFont: Default"" onClick=""OnClick"">
    <slot />
  </text>
</container>
  ")]
  public class Button : UIComponent
  {
    public bool OnClick(SelectEvent evt)
    {
      Emit("Click", evt);
      return true;
    }
  }
}
