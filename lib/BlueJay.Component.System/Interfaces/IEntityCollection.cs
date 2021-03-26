using System.Collections.Generic;

namespace BlueJay.Component.System.Interfaces
{
  public interface IEntityCollection : ICollection<IEntity>
  {
    IEnumerable<IEntity> GetByKey(long key, bool includeInActive = false);
    void UpdateAddonTree(IEntity item);
  }
}
