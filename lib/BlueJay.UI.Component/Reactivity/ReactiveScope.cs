using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactiveScope
  {
    private readonly Dictionary<string, object> _scope;

    public object this[string key]
    {
      get
      {
        var keys = key.Split('.');
        if (key.Length == 0 || !_scope.ContainsKey(keys[0])) return null;

        var obj = _scope[keys[0]];
        for (var i = 1; i < keys.Length; ++i)
        {
          var member = obj.GetType().GetMember(keys[i])?[0];
          if (member == null) return null;

          obj = member is FieldInfo ? ((FieldInfo)member).GetValue(obj) : ((PropertyInfo)member).GetValue(obj);
        }
        return obj;
      }
      set => _scope[key] = value;
    }

    public ReactiveScope()
      : this(new Dictionary<string, object>()) { }

    public ReactiveScope(Dictionary<string, object> scope)
    {
      _scope = scope;
    }

    public bool ContainsKey(string key)
    {
      return _scope.ContainsKey(key);
    }

    public ReactiveScope NewScope()
    {
      return new ReactiveScope(new Dictionary<string, object>(_scope));
    }

    public IEnumerable<IDisposable> Subscribe(Action<object> nextAction, List<string> paths)
    {
      foreach(var path in paths)
      {
        var obj = this[path];
        if (obj != null && obj is IReactiveProperty)
        {
          yield return ((IReactiveProperty)obj).Subscribe(nextAction);
        }
      }
    }
  }
}
