using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace BlueJay.UI.Component.Nodes
{
  internal class ParentDictionary : IDictionary<string, object?>
  {
    private readonly ParentDictionary? Parent;
    private readonly Dictionary<string, object> Data;

    public object? this[string key]
    {
      get
      {
        if (!Data.ContainsKey(key) || Data[key] == null)
        {
          if (Parent == null)
            throw new ArgumentOutOfRangeException(nameof(key));
          return Parent[key];
        }
        return Data[key];
      }
      set => Data[key] = value;
    }

    private IEnumerable<string> _availableKeys => Data.Keys.Concat(Parent?.Keys ?? new List<string>()).Distinct();

    public ICollection<string> Keys => _availableKeys.ToList();

    public ICollection<object> Values => _availableKeys.Select(x => this[x]).ToList();

    public int Count => _availableKeys.Count();

    public bool IsReadOnly => false;

    public ParentDictionary(ParentDictionary? parent = null)
    {
      Parent = parent;
      Data = new Dictionary<string, object>();
    }

    public void Add(string key, object value)
    {
      Data.Add(key, value);
    }

    public void Add(KeyValuePair<string, object> item)
    {
      Data.Add(item.Key, item.Value);
    }

    public void Clear()
    {
      Clear(false);
    }

    public void Clear(bool includeParent)
    {
      Data.Clear();
      if (includeParent && Parent != null)
        Parent.Clear(includeParent);
    }

    public bool Contains(KeyValuePair<string, object> item)
    {
      return _availableKeys.Any(x => x == item.Key);
    }

    public bool ContainsKey(string key)
    {
      return Keys.Contains(key);
    }

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      for (var i = arrayIndex; i < array.Length; i++)
        Add(array[i]);
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      var enumerable = _availableKeys.Select(x => new KeyValuePair<string, object>(x, this[x]));
      return enumerable.GetEnumerator();
    }

    public bool Remove(string key)
    {
      return Data.Remove(key);
    }

    public bool Remove(KeyValuePair<string, object> item)
    {
      return Data.Remove(item.Key);
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
    {
      value = null;

      if (_availableKeys.Any(x => x == key))
      {
        value = this[key];
        return true;
      }
      return false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
