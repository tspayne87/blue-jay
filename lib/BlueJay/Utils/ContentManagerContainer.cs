using BlueJay.Core;
using BlueJay.Core.Container;
using BlueJay.Core.Containers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
    {
      if (typeof(T) == typeof(ITexture2DContainer))
        return (T)_content.Load<Texture2D>(assetName).AsContainer();
      if (typeof(T) == typeof(ISpriteFontContainer))
        return (T)_content.Load<SpriteFont>(assetName).AsContainer();
      return _content.Load<T>(assetName);
    }
  }
}
