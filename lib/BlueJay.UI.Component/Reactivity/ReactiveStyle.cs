using BlueJay.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Implementation of the <see cref="IReactiveProperty"/> for a style
  /// </summary>
  public class ReactiveStyle : IReactiveProperty
  {
    /// <summary>
    /// The observers that are watching changes on this style
    /// </summary>
    private readonly List<IObserver<ReactiveEvent>> _observers;

    /// <summary>
    /// The current style for this reactive style
    /// </summary>
    private readonly Style _style;

    /// <summary>
    /// The style value
    /// </summary>
    public object Value { get => _style; set { _style.Merge((Style)value); Next(_style); } }

    /// <inheritdoc />
    public IReactiveParentProperty ReactiveParent { get; set; }

    /// <summary>
    /// Width of the element
    /// </summary>
    public int? Width { get => _style.Width; set { _style.Width = value; Next(_style); } }

    /// <summary>
    /// The width percentage for the element
    /// </summary>
    public float? WidthPercentage { get => _style.WidthPercentage; set { _style.WidthPercentage = value; Next(_style); } }

    /// <summary>
    /// The height percentage for the element
    /// </summary>
    public int? Height { get => _style.Height; set { _style.Height = value; Next(_style); } }

    /// <summary>
    /// The height percentage for the element
    /// </summary>
    public float? HeightPercentage { get => _style.HeightPercentage; set { _style.HeightPercentage = value; Next(_style); } }

    /// <summary>
    /// The top offset for the element
    /// </summary>
    public int? TopOffset { get => _style.TopOffset; set { _style.TopOffset = value; Next(_style); } }

    /// <summary>
    /// The left offset for the element
    /// </summary>
    public int? LeftOffset { get => _style.LeftOffset; set { _style.LeftOffset = value; Next(_style); } }

    /// <summary>
    /// The padding for the element
    /// </summary>
    public int? Padding { get => _style.Padding; set { _style.Padding = value; Next(_style); } }

    /// <summary>
    /// The horizontal align for the element
    /// </summary>
    public HorizontalAlign? HorizontalAlign { get => _style.HorizontalAlign; set { _style.HorizontalAlign = value; Next(_style); } }

    /// <summary>
    /// The vertical align for the element
    /// </summary>
    public VerticalAlign? VerticalAlign { get => _style.VerticalAlign; set { _style.VerticalAlign = value; Next(_style); } }

    /// <summary>
    /// The postion for the element
    /// </summary>
    public Position? Position { get => _style.Position; set { _style.Position = value; Next(_style); } }

    /// <summary>
    /// The nine patch background image for the element
    /// </summary>
    public NinePatch NinePatch { get => _style.NinePatch; set { _style.NinePatch = value; Next(_style); } }

    /// <summary>
    /// The text color for the element
    /// </summary>
    public Color? TextColor { get => _style.TextColor; set { _style.TextColor = value; Next(_style); } }

    /// <summary>
    /// The background color for the element
    /// </summary>
    public Color? BackgroundColor { get => _style.BackgroundColor; set { _style.BackgroundColor = value; Next(_style); } }

    /// <summary>
    /// The text align for the element
    /// </summary>
    public TextAlign? TextAlign { get => _style.TextAlign; set { _style.TextAlign = value; Next(_style); } }

    /// <summary>
    /// The text baseline for the element
    /// </summary>
    public TextBaseline? TextBaseline { get => _style.TextBaseline; set { _style.TextBaseline = value; Next(_style); } }

    /// <summary>
    /// The grid columns for the element
    /// </summary>
    public int GridColumns { get => _style.GridColumns; set { _style.GridColumns = value; Next(_style); } }

    /// <summary>
    /// The column gap for the element
    /// </summary>
    public Point ColumnGap { get => _style.ColumnGap; set { _style.ColumnGap = value; Next(_style); } }

    /// <summary>
    /// The column span for the element
    /// </summary>
    public int ColumnSpan { get => _style.ColumnSpan; set { _style.ColumnSpan = value; Next(_style); } }

    /// <summary>
    /// The column offset for the element
    /// </summary>
    public int ColumnOffset { get => _style.ColumnOffset; set { _style.ColumnOffset = value; Next(_style); } }

    /// <summary>
    /// The font offset for the element
    /// </summary>
    public string Font { get => _style.Font; set { _style.Font = value; Next(_style); } }

    /// <summary>
    /// The texture for for the element
    /// </summary>
    public string TextureFont { get => _style.TextureFont; set { _style.TextureFont = value; Next(_style); } }

    /// <summary>
    /// The texture font size for the element
    /// </summary>
    public int? TextureFontSize { get => _style.TextureFontSize; set { _style.TextureFontSize = value; Next(_style); } }

    /// <summary>
    /// How the height should be templated out
    /// </summary>
    public HeightTemplate? HeightTemplate { get => _style.HeightTemplate; set { _style.HeightTemplate = value; Next(_style); } }

    /// <summary>
    /// Constructor to build out defaults for the readonly items
    /// </summary>
    public ReactiveStyle()
    {
      _style = new Style();
      _observers = new List<IObserver<ReactiveEvent>>();
    }

    /// <inheritdoc />
    public void Next(object value, string path = "", ReactiveEvent.EventType type = ReactiveEvent.EventType.Update)
    {
      foreach (var observer in _observers.ToArray())
        observer.OnNext(new ReactiveEvent() { Path = path, Data = value, Type = type });

      if (ReactiveParent != null)
        ReactiveParent.Value.Next(value, string.IsNullOrWhiteSpace(path) ? ReactiveParent.Name : $"{ReactiveParent.Name}.{path}", type);
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveEvent> nextAction, string path = null)
    {
      return Subscribe(new ReactivePropertyObserver(nextAction, path));
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveEvent> nextAction, ReactiveEvent.EventType type, string path = null)
    {
      return Subscribe(new ReactivePropertyTypeObserver(nextAction, type, path));
    }

    /// <inheritdoc />
    public IDisposable Subscribe(IObserver<ReactiveEvent> observer)
    {
      if (!_observers.Contains(observer))
      {
        _observers.Add(observer);
        observer.OnNext(new ReactiveEvent() { Data = _style, Type = ReactiveEvent.EventType.Update });
      }
      return new ReactivePropertyUnsubscriber(_observers, observer);
    }
  }
}
