namespace BlueJay.UI.Component.Nodes.Elements
{
  internal class UIStyle
  {
    public Style Style { get; private set; }
    public List<IDisposable> Disposables { get; private set; }

    public UIStyle(Style style, List<IDisposable> disposables)
    {
      Style = style;
      Disposables = disposables;
    }
  }
}
