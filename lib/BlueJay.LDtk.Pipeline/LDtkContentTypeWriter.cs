using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TInput = BlueJay.LDtk.Pipeline.LDtkData;

namespace BlueJay.LDtk.Pipeline
{
  [ContentTypeWriter]
  public class LDtkContentTypeWriter : ContentTypeWriter<TInput>
  {
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
      return "BlueJay.LDtk.LDtkContentTypeReader, BlueJay.LDtk";
    }

    protected override void Write(ContentWriter output, TInput value)
    {
      output.Write(value.Json);
    }
  }
}
