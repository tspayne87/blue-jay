using BlueJay.UI.Tree;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Interfaces;

public interface IElement
{
  /// <summary>
  /// Gets the child nodes of this node.
  /// </summary>
  public IReadOnlyList<IElement> Children { get; }

  /// <summary>
  /// Gets the root node of this tree.
  /// </summary>
  public IElement? Root { get; internal set; }

  /// <summary>
  /// Gets the parent node of this node.
  /// </summary>
  public IElement? Parent { get; internal set; }

  /// <summary>
  /// Gets or sets the style for this node.
  /// </summary>
  public Style Style { get; set; }

  /// <summary>
  /// Gets or sets the style decorators for this node.
  /// </summary>
  public StyleDecorator Decorators { get; set; }

  /// <summary>
  /// Gets or sets the style for the specified decorator.
  /// </summary>
  /// <param name="decorator">The style decorator.</param>
  /// <returns>The style for the specified decorator.</returns>
  public Style this[StyleDecorator decorator] { get; set; }

  /// <summary>
  /// Calculates the width of the node.
  /// </summary>
  /// <returns>The calculated width of the node.</returns>
  public int CalculateWidth();

  /// <summary>
  /// Calculates the height of the node.
  /// </summary>
  /// <returns>The calculated height of the node.</returns>
  public int CalculateHeight();

  /// <summary>
  /// Calculates the left position of the node.
  /// </summary>
  /// <returns>The calculated left position of the node.</returns>
  public int CalculateLeftPosition();

  /// <summary>
  /// Calculates the top position of the node.
  /// </summary>
  /// <returns>The calculated top position of the node.</returns>
  public int CalculateTopPosition();

  /// <summary>
  /// Gets the computed style for this node.
  /// </summary>
  /// <returns>The computed style for this node.</returns>
  public Style GetComputedStyle();

  /// <summary>
  /// Adds a child node to this node.
  /// </summary>
  /// <param name="child">The child node to add.</param>
  public void AddChild(IElement child);

  /// <summary>
  /// Removes a child node from this node.
  /// </summary>
  /// <param name="child">The child node to remove.</param>
  public void RemoveChild(IElement child);

  /// <summary>
  /// Measures the size of the given text.
  /// </summary>
  /// <param name="text">The text we are wanting to measure.</param>
  /// <returns>Will return the value of the text's size.</returns>
  public Vector2 MeasureString(string text);

  /// <summary>
  /// Fits the given text to the available width, of the element.
  /// </summary>
  /// <param name="text">The text we want to fit.</param>
  /// <returns>Will return the fitted string.</returns>
  public string FitString(string text);

  /// <summary>
  /// Mounts the element to the UI.  This will create all the entities in this
  /// tree element as the root element.
  /// </summary>
  public void MountElement();
}
