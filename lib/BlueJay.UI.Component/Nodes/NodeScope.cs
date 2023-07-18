using BlueJay.UI.Component.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// The node scope is where all the scopped variables like provided methods/objects as well as the current
  /// component attached to the set of nodes that this scope is attached too
  /// </summary>
  internal class NodeScope : IDisposable
  {
    /// <summary>
    /// The current parent node scope for this scope
    /// </summary>
    private readonly NodeScope? _parent;

    /// <summary>
    /// The commonent type that we will need to create the UIComponent from
    /// </summary>
    private readonly Type _uiComponentType;

    /// <summary>
    /// The current service provider meant to build out ui components
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// The children that have been attached to this scope
    /// </summary>
    private readonly List<NodeScope> _children;

    /// <summary>
    /// The ui component that has been attached to this node scope
    /// </summary>
    private ParentedDictionary<Guid, UIComponent> _uiComponents;

    /// <summary>
    /// Gets the root component based on the node scope, since the final root scope should never be removed or recreated
    /// </summary>
    public UIComponent? RootComponent => _parent == null ? (_uiComponents.Count == 1 ? _uiComponents.First().Value : null) : _parent.RootComponent;

    /// <summary>
    /// Get a ui component based on the Guid
    /// </summary>
    /// <param name="id">The id we want to grab the component from</param>
    /// <returns>Will return the generated component</returns>
    public UIComponent this[Guid id] => _uiComponents[id];

    /// <summary>
    /// The type for the UIComponent being created
    /// </summary>
    public Type ComponentType => _uiComponentType;

    /// <summary>
    /// The internal service provider to get injectable classes from
    /// </summary>
    public IServiceProvider ServiceProvider => _serviceProvider;

    /// <summary>
    /// Creates a root node for the node scope
    /// </summary>
    /// <param name="serviceProvider">The service provider that will be used to create the UI Component</param>
    /// <param name="uiComponentType">The UI component that needs to be set for this scope</param>
    public NodeScope(IServiceProvider serviceProvider, Type uiComponentType)
      : this(serviceProvider, uiComponentType, null) { }

    /// <summary>
    /// Creates a node scope and attaches a parent to it
    /// </summary>
    /// <param name="serviceProvider">The service provider that will be used to create the UI Component</param>
    /// <param name="uiComponentType">The UI component that needs to be set for this scope</param>
    /// <param name="parent">The parent for this cope</param>
    private NodeScope(IServiceProvider serviceProvider, Type uiComponentType, NodeScope? parent)
    {
      _serviceProvider = serviceProvider;
      _uiComponentType = uiComponentType;
      _parent = parent;
      _children = new List<NodeScope>();
      _uiComponents = new ParentedDictionary<Guid, UIComponent>(parent?._uiComponents);
    }

    /// <summary>
    /// Helper method meant to initialize the ui component from the type to be able to get the underlineing create
    /// UIComponent
    /// </summary>
    /// <exception cref="ArgumentNullException">Will return a null exception if the ui component could not be created</exception>
    public Guid GenerateComponent(Guid? parentScope)
    {
      var scopeKey = Guid.NewGuid();
      var obj = ActivatorUtilities.CreateInstance(_serviceProvider, _uiComponentType) as UIComponent;
      if (obj == null)
        throw new ArgumentNullException(nameof(_uiComponentType));

      if (parentScope != null)
        obj.Parent = _uiComponents[parentScope.Value];
      _uiComponents[scopeKey] = obj;
      return scopeKey;
    }

    /// <summary>
    /// Creates a child scope based on the UI component and attaches this scope as the parent scope
    /// </summary>
    /// <param name="uiComponentType">The UI component that needs to be set for this scope</param>
    /// <returns>Will return the child scope for the parent</returns>
    public NodeScope AddChild(Type uiComponentType)
    {
      var scope = new NodeScope(_serviceProvider, uiComponentType, this);
      _children.Add(scope);
      return scope;
    }

    /// <summary>
    /// Disposible method meant to dispose of this scope and all of its children and remove it from its parent
    /// </summary>
    public void Dispose()
    {
      // Dispose of all the child elements that are attached to this scope
      foreach (var child in _children)
        child.Dispose();

      // Remove this scope from its parent
      if (_parent != null )
        _parent._children.Remove(this);
    }

    /// <summary>
    /// Remove a scope from the node space
    /// </summary>
    /// <param name="scopeKey">The scope key to be removed</param>
    public void RemoveScopeKey(Guid scopeKey)
    {
      if (_uiComponents.ContainsKey(scopeKey))
      {
        _uiComponents[scopeKey].Parent = null;
        _uiComponents.Remove(scopeKey);
      }
    }

    /// <summary>
    /// Checks to see if the underlining components contains the specific key
    /// </summary>
    /// <param name="scopeKey">The scope key we are trying to find</param>
    /// <returns>Will return the generated scope key</returns>
    public bool ContainsKey(Guid scopeKey)
    {
      return _uiComponents.ContainsKey(scopeKey);
    }
  }
}
