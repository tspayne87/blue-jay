using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Addons
{
  public class SpriteSheetAddon : TextureAddon
  {
    private int _frames;
    private int _activeFrame;
    private List<Texture2D> _textures;
    private Texture2D _original;

    public override Texture2D Texture
    {
      get
      {
        return _activeFrame < _textures.Count ? _textures[_activeFrame] : null;
      }
      set
      {
        _original = value;
        if (_textures.Count > 0)
        {
          _textures.Dispose();
          _textures.Clear();
        }

        _textures = _original.SplitTexture(_frames);
        _activeFrame = 0;
      }
    }

    public int ActiveFrame
    {
      get => _activeFrame;
      set
      {
        if (value >= 0 && value < _textures.Count)
        {
          _activeFrame = value;
        }
      }
    }

    public SpriteSheetAddon(string assetName, int frames)
      : base (assetName)
    {
      _frames = frames;
      _textures = new List<Texture2D>();
    }

    public SpriteSheetAddon(Texture2D texture, int frames)
      : base(texture)
    {
      _frames = frames;
      _textures = new List<Texture2D>();
    }
  }
}
