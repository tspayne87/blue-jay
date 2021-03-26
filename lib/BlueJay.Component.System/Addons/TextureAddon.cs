using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Addons
{
  public class TextureAddon : Addon<TextureAddon>
  {
    private string _assetName;
    public virtual Texture2D Texture { get; set; }

    public TextureAddon(string assetName)
    {
      _assetName = assetName;
    }

    public TextureAddon(Texture2D texture)
    {
      Texture = texture;
    }

    public override void LoadContent(ContentManager manager)
    {
      if (Texture == null)
      {
        Texture = manager.Load<Texture2D>(_assetName);
      }
    }
  }
}
