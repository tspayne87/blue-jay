using BlueJay.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactiveStyle : IReactiveProperty
  {
    private readonly List<IObserver<ReactiveUpdateEvent>> _observers;
    private Style _style;

    public object Value { get => _style; set { _style.Merge((Style)value); Next(_style); } }
    public IReactiveParentProperty ReactiveParent { get; set; }

    public int? Width { get => _style.Width; set { _style.Width = value; Next(_style); } }
    public float? WidthPercentage { get => _style.WidthPercentage; set { _style.WidthPercentage = value; Next(_style); } }

    public int? Height { get => _style.Height; set { _style.Height = value; Next(_style); } }
    public float? HeightPercentage { get => _style.HeightPercentage; set { _style.HeightPercentage = value; Next(_style); } }

    public int? TopOffset { get => _style.TopOffset; set { _style.TopOffset = value; Next(_style); } }
    public int? LeftOffset { get => _style.LeftOffset; set { _style.LeftOffset = value; Next(_style); } }

    public int? Padding { get => _style.Padding; set { _style.Padding = value; Next(_style); } }

    public HorizontalAlign? HorizontalAlign { get => _style.HorizontalAlign; set { _style.HorizontalAlign = value; Next(_style); } }
    public VerticalAlign? VerticalAlign { get => _style.VerticalAlign; set { _style.VerticalAlign = value; Next(_style); } }
    public Position? Position { get => _style.Position; set { _style.Position = value; Next(_style); } }

    public NinePatch NinePatch { get => _style.NinePatch; set { _style.NinePatch = value; Next(_style); } }

    public Color? TextColor { get => _style.TextColor; set { _style.TextColor = value; Next(_style); } }
    public Color? BackgroundColor { get => _style.BackgroundColor; set { _style.BackgroundColor = value; Next(_style); } }
    public TextAlign? TextAlign { get => _style.TextAlign; set { _style.TextAlign = value; Next(_style); } }
    public TextBaseline? TextBaseline { get => _style.TextBaseline; set { _style.TextBaseline = value; Next(_style); } }

    public int GridColumns { get => _style.GridColumns; set { _style.GridColumns = value; Next(_style); } }
    public Point ColumnGap { get => _style.ColumnGap; set { _style.ColumnGap = value; Next(_style); } }
    public int ColumnSpan { get => _style.ColumnSpan; set { _style.ColumnSpan = value; Next(_style); } }
    public int ColumnOffset { get => _style.ColumnOffset; set { _style.ColumnOffset = value; Next(_style); } }

    public string Font { get => _style.Font; set { _style.Font = value; Next(_style); } }
    public string TextureFont { get => _style.TextureFont; set { _style.TextureFont = value; Next(_style); } }
    public int? TextureFontSize { get => _style.TextureFontSize; set { _style.TextureFontSize = value; Next(_style); } }

    public ReactiveStyle()
    {
      _style = new Style();
      _observers = new List<IObserver<ReactiveUpdateEvent>>();
    }

    public void Next(object value, string path = "", ReactiveUpdateEvent.EventType type = ReactiveUpdateEvent.EventType.Update)
    {
      foreach (var observer in _observers.ToArray())
        observer.OnNext(new ReactiveUpdateEvent() { Path = path, Data = value, Type = type });

      if (ReactiveParent != null)
        ReactiveParent.Value.Next(value, string.IsNullOrWhiteSpace(path) ? ReactiveParent.Name : $"{ReactiveParent.Name}.{path}", type);
    }

    public IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, string path = null)
    {
      return Subscribe(new ReactivePropertyObserver(nextAction, path));
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, ReactiveUpdateEvent.EventType type, string path = null)
    {
      return Subscribe(new ReactivePropertyTypeObserver(nextAction, type, path));
    }

    public IDisposable Subscribe(IObserver<ReactiveUpdateEvent> observer)
    {
      if (!_observers.Contains(observer))
      {
        _observers.Add(observer);
        observer.OnNext(new ReactiveUpdateEvent() { Data = _style, Type = ReactiveUpdateEvent.EventType.Update });
      }
      return new ReactivePropertyUnsubscriber(_observers, observer);
    }
  }
}
