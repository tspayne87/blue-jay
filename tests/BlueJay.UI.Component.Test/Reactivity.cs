using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BlueJay.UI.Component.Test
{
  public class Reactivity
  {
    [Fact]
    public void Basic()
    {
      var reactive = new Simple();

      var i = 0;
      using var dispose = reactive.Integer.Subscribe(x => ++i);

      Assert.Equal(1, i);
      Assert.Equal(5, reactive.Integer.Value);

      reactive.Integer.Value = 4;
      Assert.Equal(2, i);
      Assert.Equal(4, reactive.Integer.Value);

      reactive.Integer.Value = 4;
      Assert.Equal(2, i);
      Assert.Equal(4, reactive.Integer.Value);

      reactive.Integer.Value = 10;
      Assert.Equal(3, i);
      Assert.Equal(10, reactive.Integer.Value);
    }

    [Fact]
    public void Complex()
    {
      var nested = new Nested();

      var i = 0;
      var j = 0;
      using var dispose = nested.Simple.Subscribe(x => ++i);
      using var intDispose = nested.Simple.Subscribe(x => j = j + (int)x.Data, "Integer");

      Assert.Equal(1, i);
      Assert.Equal(5, j); // We probably should do the same for this
      Assert.Equal(5, nested.Simple.Value.Integer.Value);

      nested.Simple.Value.Integer.Value = 3;
      Assert.Equal(2, i);
      Assert.Equal(8, j);
      Assert.Equal(3, nested.Simple.Value.Integer.Value);

      nested.Simple.Value = new Simple();
      Assert.Equal(4, i);
      Assert.Equal(13, j);
      Assert.Equal(5, nested.Simple.Value.Integer.Value);
    }

    [Fact]
    public void CollectionReactiveItem()
    {
      var collection = new ReactiveCollection<Simple>();

      var i = 0;
      var j = 0;
      collection.Subscribe(x => j++, "[0]");
      collection.Subscribe(x => i += (int)(x.Data ?? 0), "[0].Integer");

      collection.Add(new Simple());
      collection[0].Integer.Value = 5;
      Assert.Equal(5, i);

      collection[0].Integer.Value = 10;
      Assert.Equal(15, i);
      Assert.Equal(2, j);
    }

    [Fact]
    public void AddCollectionItem()
    {
      var collection = new ReactiveCollection<int>();

      var i = 0;
      using var dispose = collection.Subscribe(x => i += (int)x.Data, ReactiveUpdateEvent.EventType.Add);

      collection.Add(1);
      collection.Add(2);
      collection.Add(3);
      collection.Add(4);
      collection.Add(5);
      Assert.Equal(15, i);
    }

    [Fact]
    public void BasicCollection()
    {
      var collection = new ReactiveCollection<int>(1, 2, 3, 4, 5);

      var i = 0;
      var j = 0;
      using var first = collection.Subscribe(x => i = x.Data is List<int> ? ((List<int>)x.Data).Sum() : i + (int)x.Data);
      Assert.Equal(15, i);
      Assert.Equal(0, j);

      collection.Add(6);
      using var second = collection.Subscribe(x => j = ((List<int>)x.Data).Sum());
      Assert.Equal(21, i);
      Assert.Equal(21, j);
    }

    [Fact]
    public void InitSubscriptionCollection()
    {
      var collection = new ReactiveCollection<int>(1, 2, 3, 4, 5);

      var i = 0;
      using var dispose = collection.Subscribe(x => i = (int)x.Data, "[2]");
      Assert.Equal(3, i);

      collection[2] = 5;
      Assert.Equal(5, i);
    }

    private class Simple
    {
      public readonly ReactiveProperty<int> Integer;

      public Simple()
      {
        Integer = new ReactiveProperty<int>(5);
      }
    }

    private class Nested
    {
      public readonly ReactiveProperty<Simple> Simple;

      public Nested()
      {
        Simple = new ReactiveProperty<Simple>(new Simple());
      }
    }
  }
}
