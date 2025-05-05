using System;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

using TOutput = BlueJay.LDtk.LDtkObject;

namespace BlueJay.LDtk;

public class LDtkContentTypeReader : ContentTypeReader<TOutput>
{
  protected override TOutput Read(ContentReader input, TOutput existingInstance)
  {
    string json = input.ReadString();
    var result = JsonSerializer.Deserialize<TOutput>(json);
    return result ?? default!;
  }
}
