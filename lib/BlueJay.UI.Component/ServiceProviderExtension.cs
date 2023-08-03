using Antlr4.Runtime;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Language;
using Microsoft.Extensions.DependencyInjection;
using BlueJay.UI.Component.Nodes;
using BlueJay.UI.Component.Elements;
using BlueJay.Utils;
using BlueJay.Events;
using BlueJay.UI.Component.Events;
using BlueJay.UI.Component.Events.EventListeners;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// Service provider extensions meant to create UI elements out of strings and UIComponents
  /// </summary>
  public static class ServiceProviderExtension
  {
    /// <summary>
    /// Creates the shadow dom and attaches the generated component on the UIComponent collection to be
    /// used by other injectable services
    /// </summary>
    /// <typeparam name="T">The component that should be bound to the JayTML structure</typeparam>
    /// <param name="provider">The service provider to be used to generate various objects from</param>
    /// <param name="globalStyle">The global style that should be used when generating the UI for the node structure</param>
    public static void AttachComponent<T>(this IServiceProvider provider, Style? globalStyle = null)
      where T: UIComponent
    {
      var collection = provider.GetRequiredService<UIComponentCollection>();

      var node = provider.ParseJayTML(typeof(T));
      if (node != null)
      {
        node.GenerateUI(globalStyle);
        if (node.RootComponent != null)
        {
          collection.Add(node.RootComponent);
        }
      }
    }

    /// <summary>
    /// Helper method meant to add systems and event listners meant to be used by the ui component system
    /// </summary>
    /// <param name="provider">The service provider to inject systems and event listeners into</param>
    public static void AddUIComponentSystems(this IServiceProvider provider)
    {
      provider.AddEventListener<UpdateNodeWeightEventListener, UpdateNodeWeight>();
    }

    /// <summary>
    /// Helper method meant to parse JayTML string and bind it to the component set
    /// </summary>
    /// <param name="provider">The service provider to be used to generate various objects from</param>
    /// <param name="xml">The xml string the represents the JayTML structure</param>
    /// <param name="componentType">The component that should be bound to the JayTML structure</param>
    /// <returns>Will return a node that can be generate and attach to underlining UI entities</returns>
    public static INode? ParseJayTML(this IServiceProvider provider, string xml, Type componentType)
    {
      return ParseJayTML(provider, xml, componentType, new NodeScope(provider, componentType));
    }

    /// <summary>
    /// Internal method meant to genearate a node structure from the attached JayTML string as an attribute
    /// on the component
    /// </summary>
    /// <param name="provider">The service provider to be used to generate various objects from</param>
    /// <param name="componentType">The component that should be bound to the JayTML structure</param>
    /// <param name="scope">The current node scope that should be used for this element node structure</param>
    /// <returns>Will return a node that can be generate and attach to underlining UI entities</returns>
    internal static INode? ParseJayTML(this IServiceProvider provider, Type componentType, NodeScope? scope = null)
    {
      var view = Attribute.GetCustomAttribute(componentType, typeof(ViewAttribute)) as ViewAttribute;
      return ParseJayTML(provider, view?.XML ?? String.Empty, componentType, scope ?? new NodeScope(provider, componentType));
    }

    /// <summary>
    /// The internal workings to generate a node structure that handles creating and binding to the underlining
    /// UI entities that are created by it
    /// </summary>
    /// <param name="provider">The service provider mean to inject various services into the generators</param>
    /// <param name="xml">The xml string that needs to be parsed by the internal language</param>
    /// <param name="componentType">The component that needs to be used when generating the node structure</param>
    /// <param name="scope">The current node scope we exist in when generating this element</param>
    /// <returns>Will return an internal node structure</returns>
    internal static INode? ParseJayTML(IServiceProvider provider, string xml, Type componentType, NodeScope scope)
    {
      var components = Attribute.GetCustomAttribute(componentType, typeof(ComponentAttribute)) as ComponentAttribute;
      var stream = new AntlrInputStream(xml.Trim());
      ITokenSource lexer = new JayTMLLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new JayTMLParser(tokens);

      var expr = parser.document();

      var visitor = new JayTMLVisitor(provider.GetRequiredService<IContentManagerContainer>(), scope, components?.Components ?? new List<Type>());
      var result = visitor.Visit(expr);

      if (result is UIElement element)
        return element.GenerateShadowTree();
      return null;
    }
  }
}
