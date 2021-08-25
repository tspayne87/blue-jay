using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Wrapper to cast with so that we can get the value object
  /// </summary>
  public interface IReactiveProperty : INotifyPropertyChanged
  {
    /// <summary>
    /// The object value we are currently processing
    /// </summary>
    object Value { get; set; }
  }
}
