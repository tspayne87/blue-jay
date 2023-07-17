namespace BlueJay.UI.Component
{
  /// <summary>
  /// Extension methods meant for strings to do manipulation to string entities
  /// </summary>
  internal static class StringExtensions
  {
    /// <summary>
    /// Extension method is meant to slice some characters into a string
    /// </summary>
    /// <param name="str">The string we want to manipulate</param>
    /// <param name="index">The index in the string we want to manipulate</param>
    /// <param name="length">The length of the string we want to carve out</param>
    /// <param name="insert">The string we want to insert at this location</param>
    /// <returns>The spliced string</returns>
    public static Text Splice(this Text str, int index, int length = 0, string insert = "")
    {
      return str.Substring(0, index) + insert + str.Substring(index + length);
    }

    /// <summary>
    /// Extension method is meant to slice some characters into a string
    /// </summary>
    /// <param name="str">The string we want to manipulate</param>
    /// <param name="index">The index in the string we want to manipulate</param>
    /// <param name="length">The length of the string we want to carve out</param>
    /// <param name="inserts">The list of char inserts we need to make when adding into this string</param>
    /// <returns>The spliced string</returns>
    public static Text Splice(this Text str, int index, int length = 0, params char[] inserts)
    {
      return str.Splice(index, length, new string(inserts));
    }
  }
}
