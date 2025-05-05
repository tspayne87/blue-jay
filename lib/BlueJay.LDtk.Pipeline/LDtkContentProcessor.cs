using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = System.String;
using TOutput = BlueJay.LDtk.Pipeline.LDtkData;

namespace BlueJay.LDtk.Pipeline
{
  [ContentProcessor(DisplayName = "LDtk Processor - BlueJay")]
  class LDtkContentProcessor : ContentProcessor<TInput, TOutput>
  {
    public override TOutput Process(TInput input, ContentProcessorContext context)
    {
      return new LDtkData() { Json = input };
    }
  }
}
