using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.UI;

namespace BlockCommander
{
  [System.Runtime.InteropServices.Guid("a0a21556-6b78-439e-b0d6-06d3a4b6e7b4")]
  public class BlockCommanderCommand : Command
  {
    public BlockCommanderCommand()
    {
      // Rhino only creates one instance of each command class defined in a
      // plug-in, so it is safe to store a refence in a static property.
      Instance = this;
    }

    ///<summary>The only instance of this command.</summary>
    public static BlockCommanderCommand Instance
    {
      get;
      private set;
    }

    ///<returns>The command name as it appears on the Rhino command line.</returns>
    public override string EnglishName
    {
      get { return "BlockCommander"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      Guid panel_id = BlockCommanderPanel.PanelId;

      if (mode == RunMode.Interactive)
      {
        Panels.OpenPanel(panel_id);
      }
      else
      {
        bool visible = Rhino.UI.Panels.IsPanelVisible(panel_id);
        string prompt = string.Format("{0} is {1}. New value", EnglishName, visible ? "visible" : "hidden");

        GetOption go = new GetOption();
        go.SetCommandPrompt(prompt);
        int hide_index = go.AddOption("Hide");
        int show_index = go.AddOption("Show");
        int toggle_index = go.AddOption("Toggle");
        go.Get();
        if (go.CommandResult() != Rhino.Commands.Result.Success)
          return go.CommandResult();

        CommandLineOption option = go.Option();
        if (null == option)
          return Rhino.Commands.Result.Failure;

        int index = option.Index;
        if (index == hide_index)
        {
          if (visible)
            Panels.ClosePanel(panel_id);
        }
        else if (index == show_index)
        {
          if (!visible)
            Panels.OpenPanel(panel_id);
        }
        else if (index == toggle_index)
        {
          if (visible)
            Panels.ClosePanel(panel_id);
          else
            Panels.OpenPanel(panel_id);
        }
      }

      return Result.Success;
    }
  }
}
