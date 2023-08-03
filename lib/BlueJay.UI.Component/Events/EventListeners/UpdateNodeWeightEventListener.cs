using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Nodes;

namespace BlueJay.UI.Component.Events.EventListeners
{
  /// <summary>
  /// Listener meant to watch for update ui entity weights and update them accordingly
  /// </summary>
  internal class UpdateNodeWeightEventListener : EventListener<UpdateNodeWeight>
  {
    /// <inheritdoc />
    public override void Process(IEvent<UpdateNodeWeight> evt)
    {
      var weight = 0;
      var rootEntity = GetRoot(evt.Data.Node).UIEntities?.FirstOrDefault();
      if (rootEntity == null)
        return;

      var entities = Flatten(rootEntity).ToList();
      foreach (var entity in entities)
        entity.Weight = weight++;

      foreach (var entity in entities)
      {
        var la = entity.GetAddon<LineageAddon>();
        la.Children = la.Children.OrderBy(x => x.Weight).ToList();
        entity.Update(la);
      }
    }

    public IEnumerable<IEntity> GetEntities(Node node)
    {
      if (node.UIEntities != null)
        foreach (var uiEntity in node.UIEntities)
          if (uiEntity.Entity != null)
            yield return uiEntity.Entity;

      foreach (var child in node.Children)
        foreach(var entity in GetEntities(child))
          yield return entity;
    }

    /// <summary>
    /// Helper method meant to flatten the the found entities into a single list
    /// </summary>
    /// <param name="node">The node we need to extract entities from</param>
    /// <returns>Will return a flattened list of entities based on the shadow tree</returns>
    private IEnumerable<IEntity> Flatten(UIEntity uiEntity)
    {
      var foundKeys = new HashSet<long>();

      if (uiEntity.Entity != null)
      {
        foundKeys.Add(uiEntity.Entity.Id);
        yield return uiEntity.Entity;
      }

      foreach (var child in uiEntity.Children)
      {
        foreach (var entity in Flatten(child))
        {
          if (!foundKeys.Contains(entity.Id))
          {
            foundKeys.Add(entity.Id);
            yield return entity;
          }
        }
      }
    }

    private Node GetRoot(Node entity)
    {
      if (entity.Parent == null)
        return entity;
      return GetRoot(entity.Parent);
    }
  }
}
