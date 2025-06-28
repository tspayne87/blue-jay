using System;
using BlueJay.LDtk.Data;

namespace BlueJay.LDtk.Fields;

public class EntityRefField : Field
{
  public ILDtkWorld? World { get; }
  public ILDtkLevel? Level { get; }
  public ILDtkLayerInstance? Layer { get; }
  public ILDtkEntityInstance? Entity { get; }

  public EntityRefField(string identifier, ILDtkWorld? world, ILDtkLevel? level, ILDtkLayerInstance? layer, ILDtkEntityInstance? entity)
    : base(identifier)
  {
    World = world;
    Level = level;
    Layer = layer;
    Entity = entity;
  }
}
