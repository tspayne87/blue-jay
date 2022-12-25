using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Component.Nodes.Attributes;
using BlueJay.UI.Component.Nodes.Elements;
using BlueJay.UI.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Attribute = BlueJay.UI.Component.Nodes.Attributes.Attribute;

namespace BlueJay.UI.Component.Nodes
{
  internal abstract class Node : INode
  {
    private readonly GraphicsDevice _graphics;
    private readonly IEventQueue _eventQueue;

    protected readonly IServiceProvider _provider;

    public readonly List<Attribute> Attributes;
    public readonly List<Node> Children;

    public string Name { get; }
    public UIComponent UIComponent { get; }
    public IEntity? SlotEntity { get; protected set; }

    public Node(string name, UIComponent uiComponent, IServiceProvider provider)
    {
      Name = name;
      UIComponent = uiComponent;
      Attributes = new List<Attribute>();
      Children = new List<Node>();

      _provider = provider;

      _graphics = provider.GetRequiredService<GraphicsDevice>();
      _eventQueue = provider.GetRequiredService<IEventQueue>();
    }

    /// <inheritdoc />
    public void GenerateUI(Style? style = null)
    {
      GenerateUI(style ?? new Style(), null, null);
    }

    protected abstract UIElement? AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope);

    protected void TriggerUIUpdate()
      => _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });

    protected UIElement CreateUIElement(IEntity entity, List<IDisposable>? disposables = null)
      => ActivatorUtilities.CreateInstance<UIElement>(_provider, new object[] { entity, this, disposables ?? new List<IDisposable>() });

    protected UIElement CreateUIElement<T>(IEntity entity, List<IDisposable>? disposables = null)
      where T : UIElement
      => ActivatorUtilities.CreateInstance<T>(_provider, new object[] { entity, this, disposables ?? new List<IDisposable>() });

    internal UIElement? GenerateUI(Style style, UIElement? parent = null, Dictionary<string, object>? scope = null)
    {
      var forAttribute = Attributes.FirstOrDefault(x => x is ForAttribute) as ForAttribute;
      var ifAttribute = Attributes.FirstOrDefault(x => x is IfAttribute) as IfAttribute;
      var refAttribute = Attributes.FirstOrDefault(x => x is RefAttribute) as RefAttribute;
      var eventAttributes = Attributes.Select(x => x as EventAttribute).Where(x => x != null).ToList();

      //if (forAttribute != null)
      //{
      //  var list = forAttribute.Value(UIComponent, null, scope) as IEnumerable;
      //  if (list == null)
      //    throw new ArgumentNullException("Could not create for loop since value is not an IEnumerable");

      //  foreach (var item in list)
      //  {
      //    var newScope = new Dictionary<string, object>(scope ?? new Dictionary<string, object>());
      //    newScope[forAttribute.ScopeName] = item;
      //    BuildAndAddEntity(style, parent, newScope);
      //  }
      //}
      //else
      return BuildAndAddEntity(style, parent, scope);
    }

    private UIElement? BuildAndAddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      var entityStyle = GetStyle(scope);
      if (entityStyle != null)
        entityStyle.Style.Parent = style;

      var entity = BuildEntity(entityStyle?.Style ?? style, parent, scope);
      if (entity != null)
      {
        if (entityStyle != null)
          entity.Disposables.AddRange(entityStyle.Disposables);
        entity.Parent = parent;
        return entity;
      }
      return null;
    }

    private UIElement? BuildEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      var result = AddEntity(style, parent, scope);
      if (result != null && this is ComponentNode componentNode)
      {
        var slot = FindSlot(result);
        if (slot == null)
          throw new ArgumentNullException("Could not find slot in inject children into");
        result = slot;
      }

      foreach (var child in Children)
        child.GenerateUI(style, result, scope);
      return result;
    }

    private UIStyle? GetStyle(Dictionary<string, object>? scope)
    {
      var styleAttribute = Attributes
        .Select(x => x as StyleAttribute)
        .FirstOrDefault(x => x != null);

      if (styleAttribute != null)
      {
        var style = new Style();
        var disposibles = new List<IDisposable>();
        foreach(var item in styleAttribute.Styles)
        {
          var prop = typeof(Style).GetProperty(item.Name);
          if (prop != null)
          {
            var callback = item.Callback;
            prop.SetValue(style, callback(UIComponent, null, scope));

            foreach (var r in item.ReactiveProperties)
            {
              var reactive = r(UIComponent, null, scope);
              if (reactive != null)
                disposibles.Add(reactive.Subscribe(x => prop.SetValue(style, callback(UIComponent, null, scope))));
            }
          }
        }
        return new UIStyle(style, disposibles);
      }
      return null;
    }

    private UISlot? FindSlot(UIElement element)
    {
      foreach (var child in element.Children)
      {
        if (child is UISlot slot)
          return slot;

        var found = FindSlot(child);
        if (found != null)
          return found;
      }
      return null;
    }
  }
}
