namespace BlueJay.UI.Addons
{
  /// <summary>
  /// Layout addon to determine where on the screen the children should be rendered
  /// </summary>
  public class LayoutAddon
  {
    /// <summary>
    /// The layout that should be used
    /// </summary>
    public Layout Layout { get; set; }

    /// <summary>
    /// Constructor to build out the layout addon
    /// </summary>
    /// <param name="layout">The default layout this addon should be assigned to</param>
    public LayoutAddon(Layout layout)
    {
      Layout = Layout;
    }
  }
}
