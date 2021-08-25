using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  internal class ExpressionResult
  {
    public Func<Dictionary<string, object>, object> Callback { get; private set; }
    public List<IReactiveProperty> ReactiveProps { get; private set; }

    public ExpressionResult(Func<Dictionary<string, object>, object> callback, List<IReactiveProperty> reactiveProps)
    {
      Callback = callback;
      ReactiveProps = reactiveProps;
    }
  }
}
