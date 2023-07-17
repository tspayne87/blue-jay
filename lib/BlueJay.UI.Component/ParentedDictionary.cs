using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// Helper dictionary that is meant to use a parent dictionary to get elements from
  /// </summary>
  /// <typeparam name="TKey">The key of the dictionary we are using</typeparam>
  /// <typeparam name="TValue">The value of the dictionary we are using</typeparam>
  internal class ParentedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    where TKey : notnull
  {
    /// <summary>
    /// The internal data of this dictionary object
    /// </summary>
    private readonly Dictionary<TKey, TValue> _data;

    /// <summary>
    /// The parented dictionary for this object
    /// </summary>
    private IDictionary<TKey, TValue>? _parent;

    /// <summary>
    /// Constructor meant to build out a parentable dictionary
    /// </summary>
    /// <param name="parent">The parent of this dictionary</param>
    public ParentedDictionary(IDictionary<TKey, TValue>? parent = null)
    {
      _parent = parent;
      _data = new Dictionary<TKey, TValue>();
    }

    /// <summary>
    /// Getter/Setter method to set the internal data with the key but the lookup will also check
    /// all parents till it cannot find any more parents
    /// </summary>
    /// <param name="key">The key we are looking for</param>
    /// <returns>Will return a value based on the key given</returns>
    public TValue this[TKey key]
    {
      get
      {
        if (!_data.ContainsKey(key) && _parent != null)
          return _parent[key];
        return _data[key];
      }
      set => _data[key] = value;
    }

    /// <inheritdoc />
    public ICollection<TKey> Keys => new Collection<TKey>(this.Select(x => x.Key).ToList());

    /// <inheritdoc />
    public ICollection<TValue> Values => new Collection<TValue>(this.Select(x => x.Value).ToList());

    /// <inheritdoc />
    public int Count
    {
      get
      {
        var enumerator = GetEnumerator();
        var count = 0;
        while (enumerator.MoveNext())
          count++;
        return count;
      }
    }

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public void Add(TKey key, TValue value)
    {
      _data.Add(key, value);
    }

    /// <inheritdoc />
    public void Add(KeyValuePair<TKey, TValue> item)
    {
      _data.Add(item.Key, item.Value);
    }

    /// <inheritdoc />
    public void Clear()
    {
      if (_parent != null)
        _parent.Clear();
      _data.Clear();
    }

    /// <inheritdoc />
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      if (_data.Contains(item))
        return true;
      return _parent?.Contains(item) ?? false;
    }

    /// <inheritdoc />
    public bool ContainsKey(TKey key)
    {
      if (_data.ContainsKey(key))
        return true;
      return _parent?.ContainsKey(key) ?? false;
    }

    /// <inheritdoc />
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      for (var i = arrayIndex; i < array.Length; i++)
        this[array[i].Key] = array[i].Value;
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      var returnedKeys = new List<TKey>();
      foreach (var item in _data)
      {
        yield return item;
        returnedKeys.Add(item.Key);
      }

      if (_parent != null)
      {
        foreach (var item in _parent)
        {
          if (!returnedKeys.Contains(item.Key))
          {
            yield return item;
            returnedKeys.Add(item.Key);
          }
        }
      }
    }

    /// <inheritdoc />
    public bool Remove(TKey key)
    {
      return _data.Remove(key) ? true : (_parent?.Remove(key) ?? false);
    }

    /// <inheritdoc />
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      return Remove(item.Key);
    }

    /// <inheritdoc />
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
      value = default;
      if (ContainsKey(key))
      {
        value = this[key];
        return true;
      }
      return false;
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
