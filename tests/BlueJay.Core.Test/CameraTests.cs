using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlueJay.Core.Test
{
  public class CameraTests
  {
    [Fact]
    public void ToWorld()
    {
      var position = new Vector2(10, 10);
      var camera = new Camera();
      Assert.Equal(position, camera.ToWorld(position));

      camera.Position = new Vector2(-10, -10);
      Assert.Equal(Vector2.Zero, camera.ToWorld(position));
    }

    [Fact]
    public void ToScreen()
    {
      var position = Vector2.Zero;
      var camera = new Camera();
      Assert.Equal(Vector2.Zero, camera.ToScreen(position));

      camera.Position = new Vector2(-10, -10);
      Assert.Equal(new Vector2(10, 10), camera.ToScreen(position));
    }

    [Fact]
    public void Zoom()
    {
      var position = Vector2.One;
      var camera = new Camera();
      Assert.Equal(Vector2.One, position);

      camera.Zoom = 0.5f;
      Assert.Equal(new Vector2(2, 2), camera.ToWorld(position));
      Assert.Equal(0.5f, camera.Zoom);

      camera.Zoom = 2f;
      Assert.Equal(new Vector2(0.5f), camera.ToWorld(position));
    }

    [Fact]
    public void GetViewMatrix()
    {
      var parallaxFactor = new Vector2(10, 0);
      var camera = new Camera();

      Assert.Equal(Matrix.Identity, camera.GetViewMatrix(parallaxFactor));

      camera.Position = new Vector2(-20, 0);
      var matrix1 = new Matrix(
        new Vector4(1, 0, 0, 0),
        new Vector4(0, 1, 0, 0),
        new Vector4(0, 0, 1, 0),
        new Vector4(200, 0, 0, 1)
      );
      Assert.Equal(matrix1, camera.GetViewMatrix(parallaxFactor));

      camera.Zoom = 0.5f;
      var matrix2 = new Matrix(
        new Vector4(0.5f, 0, 0, 0),
        new Vector4(0, 0.5f, 0, 0),
        new Vector4(0, 0, 0.5f, 0),
        new Vector4(100, 0, 0, 1)
      );
      Assert.Equal(matrix2, camera.GetViewMatrix(parallaxFactor));
    }
  }
}
