﻿//using BlueJay.Core.Container;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using static System.Formats.Asn1.AsnWriter;

//namespace BlueJay.Core
//{
//  /// <summary>
//  /// Set of extension methods built around drawing things on the sprite batch
//  /// </summary>
//  public static class SpriteBatchExtensions
//  {
//    /// <summary>
//    /// Method is meant to draw a string to a place on the screen
//    /// </summary>
//    /// <param name="spriteBatch">The sprite batch to render the string on</param>
//    /// <param name="font">The texture font that is meant to render the text with</param>
//    /// <param name="text">The text that should be printed out</param>
//    /// <param name="position">The position of the text</param>
//    /// <param name="color">The color of the text</param>
//    /// <param name="scale">The scale of the font being used</param>
//    public static void DrawString(this SpriteBatch spriteBatch, TextureFont font, string text, Vector2 position, Color color, float scale = 1.0f)
//    {
//      var pos = position;
//      for (var i = 0; i < text.Length; ++i)
//      {
//        if (text[i] == '\n')
//        {
//          pos = new Vector2(position.X, pos.Y + (font.Height * scale));
//        }
//        else if (text[i] == ' ')
//        {
//          pos += new Vector2((font.Width * scale), 0);
//        }
//        else
//        {
//          var bounds = font.GetBounds(text[i]);
//          spriteBatch.Draw(font.Texture, pos, bounds, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
//          pos += new Vector2(font.Width * scale, 0);
//        }
//      }
//    }

//    /// <summary>
//    /// Method is meant to draw a frame on a sprite sheet
//    /// </summary>
//    /// <param name="spriteBatch">The sprite batch to draw the frame on</param>
//    /// <param name="texture">The sprite sheet that should be drawn from</param>
//    /// <param name="position">The position where the sprite should be drawn</param>
//    /// <param name="rows">The number of rows in the sprite sheet</param>
//    /// <param name="columns">The number of columns in the sprite sheet</param>
//    /// <param name="frame">The current frame we are wanting to render on the sprite sheet</param>
//    /// <param name="color">The color that should be spliced into the sprite being drawn</param>
//    /// <param name="rotation">The rotation of the frame</param>
//    /// <param name="origin">The origin to use for the rotation</param>
//    /// <param name="effects">The effect that should be used on the sprite being drawn</param>
//    /// <param name="layerDepth">A depth of the layer of this sprite</param>
//    public static void DrawFrame(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, int rows, int columns, int frame, Color? color = null, float rotation = 0f, Vector2? origin = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1f)
//    {
//      if (texture != null)
//      {
//        var width = texture.Width / columns;
//        var height = texture.Height / rows;
//        var source = new Rectangle((frame % columns) * width, (frame / columns) * height, width, height);

//        spriteBatch.Draw(texture, new Rectangle(position.ToPoint(), new Point(width, height)), source, color ?? Color.White, rotation, origin ?? Vector2.Zero, effects, layerDepth);
//      }
//    }

//    /// <summary>
//    /// Method is meant to draw a frame on a sprite sheet
//    /// </summary>
//    /// <param name="spriteBatch">The sprite batch to draw the frame on</param>
//    /// <param name="texture">The sprite sheet that should be drawn from</param>
//    /// <param name="destinationRectangle">The desitnation where the frame should be drawn</param>
//    /// <param name="rows">The number of rows in the sprite sheet</param>
//    /// <param name="columns">The number of columns in the sprite sheet</param>
//    /// <param name="frame">The current frame we are wanting to render on the sprite sheet</param>
//    /// <param name="color">The color that should be spliced into the sprite being drawn</param>
//    /// <param name="rotation">The rotation of the frame</param>
//    /// <param name="origin">The origin to use for the rotation</param>
//    /// <param name="effects">The effect that should be used on the sprite being drawn</param>
//    /// <param name="layerDepth">A depth of the layer of this sprite</param>
//    public static void DrawFrame(this SpriteBatch spriteBatch, Texture2D texture, Rectangle destinationRectangle, int rows, int columns, int frame, Color? color = null, float rotation = 0f, Vector2? origin = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1f)
//    {
//      if (texture != null)
//      {
//        var width = texture.Width / columns;
//        var height = texture.Height / rows;
//        var source = new Rectangle((frame % columns) * width, (frame / columns) * height, width, height);

//        spriteBatch.Draw(texture, destinationRectangle, source, color ?? Color.White, rotation, origin ?? Vector2.Zero, effects, layerDepth);
//      }
//    }

//    /// <summary>
//    /// <see cref="ITexture2DContainer" /> draw method for <see cref="SpriteBatch.Draw(Texture2D, Rectangle, Rectangle?, Color, float, Vector2, SpriteEffects, float)" />
//    /// </summary>
//    public static void Draw(this SpriteBatch spriteBatch, ITexture2DContainer container, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
//    {
//      if (container.Current == null)
//        throw new ArgumentNullException("container.Current");
//      spriteBatch.Draw(container.Current, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
//    }

//    /// <summary>
//    /// <see cref="ITexture2DContainer" /> draw method for <see cref="SpriteBatch.Draw(Texture2D, Vector2, Rectangle?, Color, float, Vector2, Vector2, SpriteEffects, float)" />
//    /// </summary>
//    public static void Draw(this SpriteBatch spriteBatch, ITexture2DContainer container, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
//    {
//      if (container.Current == null)
//        throw new ArgumentNullException("container.Current");
//      spriteBatch.Draw(container.Current, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
//    }

//    /// <summary>
//    /// <see cref="ITexture2DContainer" /> draw method for <see cref="SpriteBatch.Draw(Texture2D, Rectangle, Color)" />
//    /// </summary>
//    public static void Draw(this SpriteBatch spriteBatch, ITexture2DContainer container, Rectangle destinationRectangle, Color color)
//    {
//      if (container.Current == null)
//        throw new ArgumentNullException("container.Current");
//      spriteBatch.Draw(container.Current, destinationRectangle, color);
//    }

//    /// <summary>
//    /// <see cref="ITexture2DContainer "/> draw method for <see cref="SpriteBatch.Draw(Texture2D, Vector2, Rectangle?, Color, float, Vector2, float, SpriteEffects, float)"/>
//    /// </summary>
//    public static void Draw(this SpriteBatch spriteBatch, ITexture2DContainer container, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
//    {
//      if (container.Current == null)
//        throw new ArgumentNullException("container.Current");
//      spriteBatch.Draw(container.Current, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
//    }

//    /// <summary>
//    /// Method is meant to draw a rectangle to the screen
//    /// </summary>
//    /// <param name="spriteBatch">The sprite batch to draw the ninepatch to</param>
//    /// <param name="ninePatch">The nine patch that should be used when processing</param>
//    /// <param name="width">The width of the rectangle that should be drawn</param>
//    /// <param name="height">The height of the rectangle that should be drawn</param>
//    /// <param name="position">The position of the rectangle</param>
//    /// <param name="color">The color of the rectangle</param>
//    public static void DrawNinePatch(this SpriteBatch spriteBatch, NinePatch ninePatch, int width, int height, Vector2 position, Color color)
//    {
//      var innerHeight = height - (ninePatch.Break.Y * 2);
//      var innerWidth = width - (ninePatch.Break.X * 2);

//      var heightLength = (int)Math.Ceiling(innerHeight / (float)ninePatch.Break.Y);
//      var widthLength = (int)Math.Ceiling(innerWidth / (float)ninePatch.Break.X);

//      var heightOffset = innerHeight % ninePatch.Break.Y;
//      var widthOffset = innerWidth % ninePatch.Break.X;

//      // Draw middle of rectangle
//      for (var y = 0; y < heightLength; ++y)
//      {
//        var yPos = ninePatch.Break.Y + (y * ninePatch.Break.Y);

//        // Fill in middle
//        for (var x = 0; x < widthLength; ++x)
//        {
//          var dest = new Rectangle(position.ToPoint() + new Point(ninePatch.Break.X + (x * ninePatch.Break.X), yPos), ninePatch.Break);
//          var source = ninePatch.Middle;
//          if (widthOffset > 0 && x == widthLength - 1)
//          {
//            dest.Width = widthOffset;
//            source.Width = widthOffset;
//          }

//          if (heightOffset > 0 && y == heightLength - 1)
//          {
//            dest.Height = heightOffset;
//            source.Height = heightOffset;
//          }

//          spriteBatch.Draw(ninePatch.Texture, dest, source, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
//        }

//        // Draw left and right lines
//        var leftDest = new Rectangle(position.ToPoint() + new Point(0, yPos), ninePatch.Break);
//        var leftSource = ninePatch.MiddleLeft;
//        if (heightOffset > 0 && y == heightLength - 1)
//        {
//          leftDest.Height = heightOffset;
//          leftSource.Height = heightOffset;
//        }
//        spriteBatch.Draw(ninePatch.Texture, leftDest, leftSource, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

//        var rightDest = new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, yPos), ninePatch.Break);
//        var rightSource = ninePatch.MiddleRight;
//        if (heightOffset > 0 && y == heightLength - 1)
//        {
//          rightDest.Height = heightOffset;
//          rightSource.Height = heightOffset;
//        }
//        spriteBatch.Draw(ninePatch.Texture, rightDest, rightSource, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
//      }

//      // Draw top and bottom lines
//      for (var i = 0; i < widthLength; ++i)
//      {
//        var xPos = ninePatch.Break.X + (i * ninePatch.Break.X);

//        var topDest = new Rectangle(position.ToPoint() + new Point(xPos, 0), ninePatch.Break);
//        var topSource = ninePatch.Top;
//        if (widthOffset > 0 && i == widthLength - 1)
//        {
//          topDest.Width = widthOffset;
//          topSource.Width = widthOffset;
//        }
//        spriteBatch.Draw(ninePatch.Texture, topDest, topSource, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

//        var bottomDest = new Rectangle(position.ToPoint() + new Point(xPos, height - ninePatch.Break.Y), ninePatch.Break);
//        var bottomSource = ninePatch.Bottom;
//        if (widthOffset > 0 && i == widthLength - 1)
//        {
//          bottomDest.Width = widthOffset;
//          bottomSource.Width = widthOffset;
//        }
//        spriteBatch.Draw(ninePatch.Texture, bottomDest, bottomSource, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
//      }

//      // Draw four corners
//      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint(), ninePatch.Break), ninePatch.TopLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
//      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, 0), ninePatch.Break), ninePatch.TopRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
//      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(0, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 1f);
//      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
//    }
//  }
//}
