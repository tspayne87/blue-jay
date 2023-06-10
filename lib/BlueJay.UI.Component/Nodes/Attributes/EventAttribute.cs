namespace BlueJay.UI.Component.Nodes.Attributes
{
  public class EventAttribute : Attribute
  {
    public string Modifier { get; set; }
    public Func<UIComponent, object?, Dictionary<string, object>?, object> Callback { get; set; }

    public bool IsGlobal => Modifier == "Global";

    public EventAttribute(string name, string modifier, Func<UIComponent, object?, Dictionary<string, object>?, object> callback)
      : base(name)
    {
      Callback = callback;
      Modifier = modifier;
    }
  }
}
