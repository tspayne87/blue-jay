namespace BlueJay.Events
{
  /// <summary>
  /// Internal class meant to wrap multiple disposable values into a single disposable
  /// </summary>
  internal class ZipDisposable : IDisposable
  {
    /// <summary>
    /// The list of disposables we need to dispose of
    /// </summary>
    private readonly IDisposable[] _disposables;

    /// <summary>
    /// Constructor meant to build out the list of disposables
    /// </summary>
    /// <param name="disposables">The list of disposables we need to dispose of</param>
    public ZipDisposable(params IDisposable[] disposables)
    {
      _disposables = disposables;
    }

    /// <inheritdoc />
    public void Dispose()
    {
      foreach (var item in _disposables)
        item.Dispose();
    }
  }
}
