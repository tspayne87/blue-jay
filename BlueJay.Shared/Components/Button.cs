using BlueJay.UI;
using BlueJay.UI.Component;

namespace BlueJay.Shared.Components
{
  /// <summary>
  /// Button component
  /// </summary>
  [View(@"
<container style=""NinePatch: Sample_NinePatch; Padding: 13"" hoverStyle=""NinePatch: Sample_Hover_NinePatch"" onSelect=""OnSelect"">
  <slot />
</container>
  ")]
  public class Button : UIComponent
  {
    /// <summary>
    /// Callback method is meant to trigger an emit call to the caller of this component
    /// </summary>
    /// <param name="evt">The event that was sent from the triggered event</param>
    /// <returns>Will return true representing we should keep propegating this event</returns>
    public bool OnSelect(SelectEvent evt)
    {
      Emit("Select", evt);
      return true;
    }
  }
}
