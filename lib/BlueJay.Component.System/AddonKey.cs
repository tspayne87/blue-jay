using System.Diagnostics.CodeAnalysis;

namespace BlueJay.Component.System
{
  public struct AddonKey : IEquatable<AddonKey>
  {
    /// <summary>
    /// The computed last bit that will be used for the internal incremented method
    /// </summary>
    private static ulong _lastBit = 1 << 62;

    /// <summary>
    /// The key that represents this addon
    /// </summary>
    private readonly ulong[] _key;

    /// <summary>
    /// Privated constructor to only allow entities keys to be created within this class
    /// </summary>
    /// <param name="key">The current key that is being stored by this addon</param>
    private AddonKey(params ulong[] key)
    {
      _key = key;
    }

    /// <summary>
    /// Overloaded operator to handle the and operator on the addon key
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>Will return the result of an and operation between the two entities</returns>
    public static AddonKey operator&(AddonKey left, AddonKey right)
    {
      var newKey = new ulong[Math.Max(left._key.Length, right._key.Length)];
      for (var i = 0; i < newKey.Length; ++i)
        newKey[i] = (left._key.Length > i ? left._key[i] : 0) & (right._key.Length > i ? right._key[i] : 0);
      return new AddonKey(newKey);
    }

    /// <summary>
    /// Overloaded operator to handle the or operation on the addon key
    /// </summary>
    /// <param name="left">The left side of the overloaded operator</param>
    /// <param name="right">The right side of the overloaded operator</param>
    /// <returns>Will return the result of the or operation on the two addon keys</returns>
    public static AddonKey operator|(AddonKey left, AddonKey right)
    {
      var newKey = new ulong[Math.Max(left._key.Length, right._key.Length)];
      for (var i = 0; i < newKey.Length; ++i)
        newKey[i] = (left._key.Length > i ? left._key[i] : 0) | (right._key.Length > i ? right._key[i] : 0);
      return new AddonKey(newKey);
    }

    /// <summary>
    /// Overloaded operator to handle if the two keys are equal or not
    /// </summary>
    /// <param name="left">The left side of the overloaded operator</param>
    /// <param name="right">The right side of the overloaded operator</param>
    /// <returns>Results in true if the left and right are the same key</returns>
    public static bool operator ==(AddonKey left, AddonKey right)
    {
      /// Special case since none could have multiple lengths and we want to make sure it is found
      if (left.IsNone() && right.IsNone()) return true;
      if (left._key.Length != right._key.Length) return false;
      for (var i = 0; i < left._key.Length; ++i)
        if (left._key[i] != right._key[i]) return false;
      return true;
    }

    /// <summary>
    /// Helper method meant to determine if the addon key is none
    /// </summary>
    /// <returns>Helper method to check if the addon keys are none</returns>
    public bool IsNone()
    {
      for (var i = 0; i < _key.Length; ++i)
        if (_key[i] != 0)
          return false;
      return true;
    }

    /// <summary>
    /// Overloaded operator to handle the not equality between two addon keys
    /// </summary>
    /// <param name="left">The left side of the overloaded operator</param>
    /// <param name="right">The right side of the overloaded operator</param>
    /// <returns>Result of if the two addon keys are not equal</returns>
    public static bool operator !=(AddonKey left, AddonKey right) => !(left == right);

    /// <inheritdoc />
    public bool Equals(AddonKey other) => this == other;

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
      if (obj is AddonKey e)
        return Equals(e);
      return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      int result = 0;
      for (var i = 0; i< _key.Length; ++i)
        result = HashCode.Combine(_key[i], result);
      return result;
    }

    /// <summary>
    /// Helper internal method meant to increment a key by shifting the bit down the ulong array
    /// </summary>
    /// <returns>Will return a new addon key that has the incremented key</returns>
    internal AddonKey IncrementKey()
    {
      var newKey = new ulong[_key.Length];
      Array.Copy(_key, newKey, _key.Length);
      if (newKey[newKey.Length - 1] == _lastBit)
      {
        newKey[newKey.Length - 1] = 0;
        Array.Resize(ref newKey, _key.Length + 1);
        newKey[newKey.Length - 1] = 1;
        return new AddonKey(newKey);
      }

      newKey[newKey.Length - 1] <<= 1;
      return new AddonKey(newKey);
    }

    /// <summary>
    /// Implicit cast from the ulong into an adoc key
    /// </summary>
    /// <param name="val">The current value to set as the adhoc key</param>
    public static implicit operator AddonKey(ulong val) => new AddonKey(new ulong[] { val });

    /// <summary>
    /// Implicit cast from uint into an addoc key
    /// </summary>
    /// <param name="val">The current value to set as the adhoc key</param>
    public static implicit operator AddonKey(uint val) => new AddonKey(new ulong[] { val });

    /// <summary>
    /// Key that represents one for the key
    /// </summary>
    public static AddonKey One => new AddonKey(1);

    /// <summary>
    /// The key that represents none for the addon key
    /// </summary>
    public static AddonKey None => new AddonKey(0);
  }
}
