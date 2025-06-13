using System;

namespace BlueJay.LDtk.Fields;

public class FilePathField : Field
{
  public string Path { get; }

  public FilePathField(string identifier, string path)
    : base(identifier)
  {
    Path = path ?? throw new ArgumentNullException(nameof(path), "Path cannot be null");
  }
}
