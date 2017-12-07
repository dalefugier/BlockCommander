using Rhino.PlugIns;
using Rhino.UI;

namespace BlockCommander
{
  ///<summary>
  /// BlockCommanderPlugIn plug-in class
  ///</summary>
  public class BlockCommanderPlugIn : PlugIn
  {
    /// <summary>
    /// Public constructor
    /// </summary>
    public BlockCommanderPlugIn()
    {
      Instance = this;
    }

    /// <summary>
    /// Gets the only instance of the BlockCommanderPlugIn plug-in.
    /// </summary>
    public static BlockCommanderPlugIn Instance
    {
      get;
      private set;
    }

    /// <summary>
    /// Gets the only instance of the BlockCommanderPanel user control.
    /// </summary>
    public BlockCommanderPanel Panel
    {
      get;
      set;
    }

    /// <summary>
    /// Is called when the plug-in is being loaded.
    /// </summary>
    protected override LoadReturnCode OnLoad(ref string errorMessage)
    {
      System.Type panel_type = typeof(BlockCommanderPanel);
      Panels.RegisterPanel(this, panel_type, "Blocks", Properties.Resources.BlockCommander);

      return LoadReturnCode.Success;
    }
  }
}