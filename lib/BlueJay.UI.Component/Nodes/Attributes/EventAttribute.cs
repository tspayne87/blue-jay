namespace BlueJay.UI.Component.Nodes.Attributes
{
  public class EventAttribute : Attribute
  {
    public Func<UIComponent, object?, Dictionary<string, object>?, object> Callback { get; set; }

    public EventAttribute(string name, Func<UIComponent, object?, Dictionary<string, object>?, object> callback)
      : base(name)
    {
      Callback = callback;
    }
  }
}
