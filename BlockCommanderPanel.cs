using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rhino;

namespace BlockCommander
{
  /// <summary>
  /// BlockCommanderPanel panel class
  /// </summary>
  [System.Runtime.InteropServices.Guid("DCCC69C5-CF71-48E8-8D5E-712AD3E62C18")]
  public partial class BlockCommanderPanel : UserControl
  {
    private readonly BlockCommanderHandlers m_handlers;

    /// <summary>
    /// Public constructor
    /// </summary>
    public BlockCommanderPanel()
    {
      InitializeComponent();

      // Set the panel property on our plug-in
      BlockCommanderPlugIn.Instance.Panel = this;

      // Our tree control is owner drawn
      m_tree.DrawMode = TreeViewDrawMode.OwnerDrawAll;

      SetDefaultImageIndex();

      int selected = m_combo.Items.Add(BlockCommanderTreeView.FilterString(BlockCommanderFilter.All));
      m_combo.Items.Add(BlockCommanderTreeView.FilterString(BlockCommanderFilter.Embedded));
      m_combo.Items.Add(BlockCommanderTreeView.FilterString(BlockCommanderFilter.LinkedAndEmbedded));
      m_combo.Items.Add(BlockCommanderTreeView.FilterString(BlockCommanderFilter.Linked));
      m_combo.SelectedIndex = selected;

      // Create our event handlers
      m_handlers = new BlockCommanderHandlers(this);

      // Create a visible changed event handler
      this.VisibleChanged += new EventHandler(OnVisibleChanged);

      // Create a dispose event handler
      this.Disposed += new EventHandler(OnDisposed);
    }

    /// <summary>
    /// Returns the ID of this panel
    /// </summary>
    public static System.Guid PanelId
    {
      get { return typeof (BlockCommanderPanel).GUID; }
    }

    public BlockCommanderTreeView Tree
    {
      get { return m_tree; }
    }

    /// <summary>
    /// Occurs when the System.Windows.Forms.Control.Visible property value changes.
    /// </summary>
    void OnVisibleChanged(object sender, EventArgs e)
    {
      m_handlers.Enable(this.Visible);
    }

    /// <summary>
    /// Occurs when the component is disposed by a call to the System.ComponentModel.Component.Dispose() method.
    /// </summary>
    void OnDisposed(object sender, EventArgs e)
    {
      // Clear the user control property on our plug-in
      BlockCommanderPlugIn.Instance.Panel = null;
    }

    // !!! ensure default imageindices are invalid !!!
    // ,otherwise treeview assigns first image to all nodes with unspecified (or -1) imageindices.
    // (Setting -1 does not work, nor does specifying empty or invalid ImageKey).
    // Set ImageIndex to >= current ImageList length.
    // Redo after any change in ImageList.Length or re-assignment of ImageList.
    // Caveat: ImageIndex will return ImageList.Images.Count -1;
    private void SetDefaultImageIndex()
    {
      if (null != m_tree.ImageList)
      {
        m_tree.ImageIndex = m_tree.ImageList.Images.Count;
        m_tree.SelectedImageIndex = m_tree.ImageList.Images.Count;
      }
    }

    private void m_btn_block_definition_Click(object sender, EventArgs e)
    {
      RhinoApp.RunScript("! _Block", true);
    }

    private void m_btn_block_insert_Click(object sender, EventArgs e)
    {
      RhinoApp.RunScript("! _Insert", true);
    }

    private void m_btn_block_manager_Click(object sender, EventArgs e)
    {
      RhinoApp.RunScript("! _BlockManager", true);
    }

    private void m_btn_block_editor_Click(object sender, EventArgs e)
    {
      RhinoApp.RunScript("! _BlockEdit", true);
    }

    private void m_btn_block_select_Click(object sender, EventArgs e)
    {
      RhinoApp.RunScript("_SelBlockInstanceNamed", true);
    }

    private void m_combo_SelectedIndexChanged(object sender, EventArgs e)
    {
      m_tree.BlockFilter = (BlockCommanderFilter) m_combo.SelectedIndex;
    }

    private void m_btn_block_settings_DropDownOpening(object sender, EventArgs e)
    {
      m_menu_show_hidden_blocks.Checked = m_tree.ShowHiddenBlocks;
      m_menu_show_reference_blocks.Checked = m_tree.ShowReferenceBlocks;
      m_menu_show_used_blocks.Checked = m_tree.ShowUsedBlocks;
      m_menu_show_unused_blocks.Checked = m_tree.ShowUnusedBlocks;
      m_menu_show_fullpath.Checked = m_tree.ShowFullPath;
      m_menu_show_objects.Checked = m_tree.ShowObjects;
    }

    private void m_menu_show_hidden_blocks_Click(object sender, EventArgs e)
    {
      m_tree.ShowHiddenBlocks = !m_tree.ShowHiddenBlocks;
    }

    private void m_menu_show_reference_blocks_Click(object sender, EventArgs e)
    {
      m_tree.ShowReferenceBlocks = !m_tree.ShowReferenceBlocks;
    }

    private void m_menu_show_used_blocks_Click(object sender, EventArgs e)
    {
      m_tree.ShowUsedBlocks = !m_tree.ShowUsedBlocks;
    }

    private void m_menu_show_unused_blocks_Click(object sender, EventArgs e)
    {
      m_tree.ShowUnusedBlocks = !m_tree.ShowUnusedBlocks;
    }

    private void m_menu_show_objects_Click(object sender, EventArgs e)
    {
      m_tree.ShowObjects = !m_tree.ShowObjects;
    }

    private void m_menu_show_fullpath_Click(object sender, EventArgs e)
    {
      m_tree.ShowFullPath = !m_tree.ShowFullPath;
    }
  }
}
