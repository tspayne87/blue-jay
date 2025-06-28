using System;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.LDtk.Data;

internal class LDtkLevel : ILDtkLevel
{
  private readonly World _world;
  private readonly Level _level;
  private readonly IServiceProvider _service;

  public LDtkLevel(World world, Level level, IServiceProvider service)
  {
    _world = world ?? throw new ArgumentNullException(nameof(world), "World cannot be null.");
    _level = level ?? throw new ArgumentNullException(nameof(level), "Level cannot be null.");
    _service = service ?? throw new ArgumentNullException(nameof(service), "ServiceProvider cannot be null.");
  }

  /// <inheritdoc />
  public IEnumerable<ILDtkLayerInstance> Layers =>
    _level.LayerInstances.Select(layer => ActivatorUtilities.CreateInstance<LDtkLayerInstance>(_service, _world, _level, layer));

  /// <inheritdoc />
  public ILDtkLayerInstance? GetLayerByIdentifier(string identifier)
  {
    var layer = _level.LayerInstances.FirstOrDefault(x => x.Identifier == identifier);
    if (layer == null)
      return null;
    return ActivatorUtilities.CreateInstance<LDtkLayerInstance>(_service, _world, _level, layer);
  }
}
