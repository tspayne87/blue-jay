using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  internal class ExpressionResult
  {
    public Func<ReactiveScope, object> Callback { get; private set; }
    public List<string> ScopePaths { get; private set; }

    public ExpressionResult(Func<ReactiveScope, object> callback, List<string> scopePaths = null)
    {
      Callback = callback;
      ScopePaths = scopePaths ?? new List<string>();
    }
  }
}
