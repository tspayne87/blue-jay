using System;
using BlueJay.Core.Container;

namespace BlueJay.LDtk.Data;

public interface ILDtkLayerInstance
{
  /// <summary>
  /// Gets the enumeration type of the layer instance.
  /// </summary>
  LayerType InstanceType { get; }

  /// <summary>
  /// Creates an IntGrid for the layer instance.
  /// </summary>
  /// <returns>Will return an int grid from the instance</returns>
  IntGrid CreateIntGrid();

  /// <summary>
  /// Creates a texture for the auto layer instance.
  /// </summary>
  /// <param name="serviceProvider">The service provider meant to load all the relevant objects meant to build out the texture</param>
  /// <returns>Will return a newly created texture based on how LDtk was configured</returns>
  /// <exception cref="InvalidOperationException">
  /// Will throw various exceptions based on if the AutoLayerTiles, Instance type is not equal to AutoLayer and
  /// if the reference path could not be generated
  /// </exception>
  /// <remarks>
  /// This meathod will attempt auto-load the tilemap if null configured in LDtk, make sure that the files exist in the
  /// content folder and are properly configured in both the LDtk project and the content pipeline.
  /// </remarks>
  ITexture2DContainer CreateTexture(ITexture2DContainer? tileMap = null);
}
