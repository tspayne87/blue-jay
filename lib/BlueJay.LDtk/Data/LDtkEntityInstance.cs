using System;
using BlueJay.LDtk.Fields;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.LDtk.Data;

internal class LDtkEntityInstance : ILDtkEntityInstance
{
  private readonly LDtkObject _object;
  private readonly EntityInstance _instance;
  private readonly IServiceProvider _service;

  public LDtkEntityInstance(LDtkObject ldtkObject, EntityInstance instance, IServiceProvider service)
  {
    _object = ldtkObject ?? throw new ArgumentNullException(nameof(ldtkObject), "LDtkObject cannot be null.");
    _instance = instance ?? throw new ArgumentNullException(nameof(instance), "EntityInstance cannot be null.");
    _service = service ?? throw new ArgumentNullException(nameof(service), "ServiceProvider cannot be null.");
  }

  /// <summary>
  /// Retrieves the field data from the entity instance and loads various field types based on how they are defined in the LDtk project.
  /// </summary>
  /// <param name="ldtk">The ldtk project that will be loaded</param>
  /// <param name="service">The injected service provider</param>
  /// <returns>Will return a list of fields found for the entity instance</returns>
  public IEnumerable<Field> Fields =>
    _instance.FieldInstances.Select(field => ActivatorUtilities.CreateInstance<ILDtkFieldInstance>(_service, _object, field).AsField());
}
