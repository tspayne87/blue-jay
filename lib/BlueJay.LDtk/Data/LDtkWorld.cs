using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.LDtk.Data;

internal class LDtkWorld : ILDtkWorld
{
  /// <summary>
  /// The current ldtk world data used by the game.
  /// </summary>
  private readonly World _world;

  /// <summary>
  /// The service provider used to create instances of LDtkLevel.
  /// </summary>
  private readonly IServiceProvider _service;

  /// <summary>
  /// Initializes a new instance of the <see cref="LDtkWorld"/> class.
  /// </summary>
  /// <param name="ldtkObject">The LDtkObject representing the world.</param>
  /// <param name="service">The service provider used to create instances of LDtkLevel.</param>
  /// <exception cref="ArgumentNullException">Thrown when ldtkObject or service is null.</exception>
  public LDtkWorld(World ldtkObject, IServiceProvider service)
  {
    _world = ldtkObject ?? throw new ArgumentNullException(nameof(ldtkObject), "World cannot be null.");
    _service = service ?? throw new ArgumentNullException(nameof(service), "ServiceProvider cannot be null.");
  }

  /// <inheritdoc />
  public IEnumerable<ILDtkLevel> Levels =>
    _world.Levels.Select(level => ActivatorUtilities.CreateInstance<LDtkLevel>(_service, _world, level));

  /// <inheritdoc />
  public ILDtkLevel? GetLevelByIdentifier(string identifier)
  {
    var level = _world.Levels.FirstOrDefault(x => x.Identifier == identifier);
    if (level == null)
      return null;
    return ActivatorUtilities.CreateInstance<LDtkLevel>(_service, _world, level);
  }
}
