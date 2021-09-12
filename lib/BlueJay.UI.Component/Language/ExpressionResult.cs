using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;

namespace BlueJay.UI.Component.Language
{
  /// <summary>
  /// Expression result that is used by the visitor to return data to build out the element node
  /// </summary>
  internal class ExpressionResult
  {
    /// <summary>
    /// The callback that should be used for this expression
    /// </summary>
    public Func<ReactiveScope, object> Callback { get; private set; }

    /// <summary>
    /// The scope paths that should be watched on to refresh the callback data
    /// </summary>
    public List<string> ScopePaths { get; private set; }

    /// <summary>
    /// Constructor is meant to build out the result and assign defaults
    /// </summary>
    /// <param name="callback">The callback that should be used for this expression</param>
    /// <param name="scopePaths">The scope paths that should be watched on to refresh the callback data</param>
    public ExpressionResult(Func<ReactiveScope, object> callback, List<string> scopePaths = null)
    {
      Callback = callback;
      ScopePaths = scopePaths ?? new List<string>();
    }
  }
}
