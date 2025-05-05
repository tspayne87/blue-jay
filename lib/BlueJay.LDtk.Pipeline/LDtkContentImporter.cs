using System;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Content.Pipeline;

using TImport = System.String;

namespace BlueJay.LDtk.Pipeline
{
  [ContentImporter(".ldtk", DisplayName = "LDtk Importer - BlueJay", DefaultProcessor = nameof(LDtkContentProcessor))]
  public class LDtkContentImporter : ContentImporter<TImport>
  {
    public override TImport Import(string filename, ContentImporterContext context)
    {
      Console.WriteLine($"Importing LDtk file: {filename}");
      string json = File.ReadAllText(filename);
      ThrowIfInvalidJson(json);
      return json;
    }

    private void ThrowIfInvalidJson(string json)
    {
      //  Ensure there's actually data in the file.
      if (string.IsNullOrEmpty(json))
      {
        throw new InvalidContentException("The JSON file is empty");
      }

      //  Attempt to parse the data as a JsonDocument. If it fails, return false.
      try
      {
        _ = JsonConvert.DeserializeObject(json);
      }
      catch (Exception ex)
      {
        throw new InvalidContentException("This does not appear to be valid JSON. See inner exception for details", ex);
      }
    }
  }
}
