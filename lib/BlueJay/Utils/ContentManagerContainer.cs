using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Utils
{
  internal class ContentManagerContainer : IContentManagerContainer
  {
    private readonly ContentManager _content;

    public ContentManagerContainer(ContentManager content)
    {
      _content = content;
    }

    /// <inheritdoc />
    public T Load<T>(string assetName)
      => _content.Load<T>(assetName);
  }
}
