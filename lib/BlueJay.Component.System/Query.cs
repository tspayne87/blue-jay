using System.Collections;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System;

internal class Query : IEnumerable<IEntity>, IQuery
{
  /// <summary>
  /// The internal collection of entities
  /// </summary>
  private readonly ILayerCollection _layers;

  /// <summary>
  /// The key we are looking for in the entities
  /// </summary>
  private readonly AddonKey _key;

  /// <summary>
  /// The layers we are filtering on
  /// </summary>
  private readonly List<string>? _filterOnLayers;

  /// <summary>
  /// The layers we are excluding
  /// </summary>
  private readonly List<string>? _layersToExclude;

  /// <summary>
  /// Constructor to build out the query
  /// </summary>
  /// <param name="layers">The internal collection of entities</param>
  /// <param name="key">The key we are looking for in the entities</param>
  /// <param name="filterOnLayers">The layers we are filtering on</param>
  /// <param name="layersToExclude">The layers we are excluding</param>
  public Query(ILayerCollection layers, AddonKey key, List<string>? filterOnLayers = null, List<string>? layersToExclude = null)
  {
    _layers = layers;
    _key = key;
    _filterOnLayers = filterOnLayers;
    _layersToExclude = layersToExclude;
  }

  /// <inheritdoc />
  public IEnumerator<IEntity> GetEnumerator()
  {
    return new QueryEnumerator(_layers, _key, _filterOnLayers, _layersToExclude);
  }

  /// <inheritdoc />
  public IQuery WhereLayer(params string[] layers)
  {
    var filterOnLayers = layers.ToList().Concat(_filterOnLayers ?? new List<string>()).ToList();
    return new Query(_layers, _key, filterOnLayers, _layersToExclude);
  }

  /// <inheritdoc />
  public IQuery ExcludeLayer(params string[] layers)
  {
    var layersToExclude = layers.ToList().Concat(_layersToExclude ?? new List<string>()).ToList();
    return new Query(_layers, _key, _filterOnLayers, layersToExclude);
  }

  /// <inheritdoc />
  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}

internal class AllQuery : Query, IQuery
{
  public AllQuery(ILayerCollection layers)
    : base(layers, 0) { }
}

internal class Query<A1> : Query, IQuery<A1>
  where A1 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1>()) { }
}

internal class Query<A1, A2> : Query, IQuery<A1, A2>
  where A1 : struct, IAddon
  where A2 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1, A2>()) { }
}

internal class Query<A1, A2, A3> : Query, IQuery<A1, A2, A3>
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1, A2, A3>()) { }
}

internal class Query<A1, A2, A3, A4> : Query, IQuery<A1, A2, A3, A4>
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1, A2, A3, A4>()) { }
}

internal class Query<A1, A2, A3, A4, A5> : Query, IQuery<A1, A2, A3, A4, A5>
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1, A2, A3, A4, A5>()) { }
}

internal class Query<A1, A2, A3, A4, A5, A6> : Query, IQuery<A1, A2, A3, A4, A5, A6>
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
  where A6 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1, A2, A3, A4, A5, A6>()) { }
}

internal class Query<A1, A2, A3, A4, A5, A6, A7> : Query, IQuery<A1, A2, A3, A4, A5, A6, A7>
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
  where A6 : struct, IAddon
  where A7 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1, A2, A3, A4, A5, A6, A7>()) { }
}

internal class Query<A1, A2, A3, A4, A5, A6, A7, A8> : Query, IQuery<A1, A2, A3, A4, A5, A6, A7, A8>
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
  where A6 : struct, IAddon
  where A7 : struct, IAddon
  where A8 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1, A2, A3, A4, A5, A6, A7, A8>()) { }
}

internal class Query<A1, A2, A3, A4, A5, A6, A7, A8, A9> : Query, IQuery<A1, A2, A3, A4, A5, A6, A7, A8, A9>
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
  where A6 : struct, IAddon
  where A7 : struct, IAddon
  where A8 : struct, IAddon
  where A9 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1, A2, A3, A4, A5, A6, A7, A8, A9>()) { }
}

internal class Query<A1, A2, A3, A4, A5, A6, A7, A8, A9, A10> : Query, IQuery<A1, A2, A3, A4, A5, A6, A7, A8, A9, A10>
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
  where A6 : struct, IAddon
  where A7 : struct, IAddon
  where A8 : struct, IAddon
  where A9 : struct, IAddon
  where A10 : struct, IAddon
{
  public Query(ILayerCollection layers)
    : base(layers, KeyHelper.Create<A1, A2, A3, A4, A5, A6, A7, A8, A9, A10>()) { }
}