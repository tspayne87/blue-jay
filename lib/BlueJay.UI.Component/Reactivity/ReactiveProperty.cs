using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Reactive property that will handle when props change on the component
  /// </summary>
  /// <typeparam name="T">The type of property this is</typeparam>
  public class ReactiveProperty<T> : IReactiveProperty
  {
    /// <summary>
    /// The internal value that was set for this property
    /// </summary>
    private T _value;

    /// <summary>
    /// The change event handler we will need to bind to
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// The value for this property
    /// </summary>
    public T Value
    {
      get => _value;
      set {
        if (!_value.Equals(value))
        {
          _value = value;
          NotifyPropertyChanged();
        }
      }
    }

    /// <summary>
    /// The value getter for IValue interface
    /// </summary>
    object IReactiveProperty.Value
    {
      get => _value;
      set
      {
        if (!_value.Equals(value))
        {
          if (value.GetType() == typeof(T))
            _value = (T)value;
          else
            _value = (T)Convert.ChangeType(value, typeof(T));
          NotifyPropertyChanged();
        }
      }
    }

    /// <summary>
    /// Constructor to build out a reactive property starting with a value
    /// </summary>
    /// <param name="value">The value to start with</param>
    public ReactiveProperty(T value)
    {
      _value = value;
      PropertyChanged = default;
    }

    /// <summary>
    /// Notification helper that will call the property change events
    /// </summary>
    /// <param name="property">The property name that has been changed</param>
    private void NotifyPropertyChanged([CallerMemberName] string property = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
  }
}
