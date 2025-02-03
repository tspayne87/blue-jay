namespace BlueJay.Component.System.Interfaces;

public interface IQuery : IEnumerable<IEntity>
{
  /// <summary>
  /// Will filter the query to only include entities that have on a specific layer
  /// </summary>
  /// <param name="layers">The current layers we want to include when finding entities</param>
  /// <returns>Will return a new query based on the layers given</returns>
  public IQuery WhereLayer(params string[] layers);

  /// <summary>
  /// Will filter the query to exclude entities that have on a specific layer
  /// </summary>
  /// <param name="layers">The layers we want to exclude from the query</param>
  /// <returns>Will return a query that excludes certain layers</returns>
  public IQuery ExcludeLayer(params string[] layers);
}

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1> : IQuery
  where A1 : struct, IAddon
{ }

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1, A2> : IQuery
  where A1 : struct, IAddon
  where A2 : struct, IAddon
{ }

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1, A2, A3> : IQuery
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
{ }

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1, A2, A3, A4> : IQuery
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
{ }

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1, A2, A3, A4, A5> : IQuery
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
{ }

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1, A2, A3, A4, A5, A6> : IQuery
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
  where A6 : struct, IAddon
{ }

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1, A2, A3, A4, A5, A6, A7> : IQuery
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
  where A6 : struct, IAddon
  where A7 : struct, IAddon
{ }

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1, A2, A3, A4, A5, A6, A7, A8> : IQuery
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
  where A6 : struct, IAddon
  where A7 : struct, IAddon
  where A8 : struct, IAddon
{ }

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1, A2, A3, A4, A5, A6, A7, A8, A9> : IQuery
  where A1 : struct, IAddon
  where A2 : struct, IAddon
  where A3 : struct, IAddon
  where A4 : struct, IAddon
  where A5 : struct, IAddon
  where A6 : struct, IAddon
  where A7 : struct, IAddon
  where A8 : struct, IAddon
  where A9 : struct, IAddon
{ }

/// <summary>
/// The query interface that will be used to query entities
/// </summary>
public interface IQuery<A1, A2, A3, A4, A5, A6, A7, A8, A9, A10> : IQuery
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
{ }