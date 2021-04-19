using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System
{
  internal class Layer : ILayer
  {
    public EntityCollection Entities { get; private set; }

    public string Id { get; private set; }

    public int Weight { get; private set; }

    public Layer(string id, int weight)
    {
      Id = id;
      Weight = weight;
      Entities = new EntityCollection();
    }
  }
}
