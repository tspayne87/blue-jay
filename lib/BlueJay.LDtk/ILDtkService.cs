namespace BlueJay.LDtk;

public interface ILDtkService
{
  /// <summary>
  /// Loads a file from the LDtk project.
  /// </summary>
  /// <param name="assetName">The asset name for the ldtk file</param>
  void LoadFile(string assetName);
}
