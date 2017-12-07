using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Rhino;
using Rhino.DocObjects;

namespace BlockCommander
{
  public enum BlockCommanderFilter
  {
    All,
    Embedded,
    LinkedAndEmbedded,
    Linked
  }

  public class BlockCommanderTreeView : TreeView
  {
    /// <summary>
    /// Required designer variable
    /// </summary>
    private Container m_components = null;

    private ContextMenuStrip m_embedded_menu;
    private ContextMenuStrip m_linked_menu;

    private ToolStripMenuItem m_embedded_menu_insert;
    private ToolStripMenuItem m_embedded_menu_select;
    private ToolStripMenuItem m_embedded_menu_rename;

    private ToolStripMenuItem m_linked_menu_select;
    private ToolStripMenuItem m_linked_menu_fullpath;
    private ToolStripMenuItem m_linked_menu_open;
    private ToolStripMenuItem m_linked_menu_explore;

    private BlockCommanderFilter m_block_filter;
    private bool m_show_hidden_blocks;
    private bool m_show_reference_blocks;
    private bool m_show_used_blocks;
    private bool m_show_unused_blocks;
    private bool m_show_fullpath;
    private bool m_show_objects;

    private readonly string m_substring = ".3dm : ";

    /// <summary>
    /// Constructor
    /// </summary>
    public BlockCommanderTreeView()
    {
      InitializeComponent();

      m_block_filter = BlockCommanderFilter.All;
      m_show_hidden_blocks = false;
      m_show_reference_blocks = false;
      m_show_used_blocks = true;
      m_show_unused_blocks = false;
      m_show_fullpath = false;
      m_show_objects = true;

      LineColor = SystemColors.GrayText;
      DrawMode = TreeViewDrawMode.OwnerDrawAll;
      Sorted = false; // We do our own sorting...

      m_embedded_menu_insert = new ToolStripMenuItem("Insert");
      m_embedded_menu_insert.Click += new EventHandler(OnInsertMenuItemClick);

      m_embedded_menu_select = new ToolStripMenuItem("Select");
      m_embedded_menu_select.Click += new EventHandler(OnSelectMenuItemClick);

      m_embedded_menu_rename = new ToolStripMenuItem("Rename");
      m_embedded_menu_rename.Click += new EventHandler(OnRenameMenuItemClick);

      m_linked_menu_select = new ToolStripMenuItem("Select");
      m_linked_menu_select.Click += new EventHandler(OnSelectMenuItemClick);

      m_linked_menu_fullpath = new ToolStripMenuItem("Copy Full Path");
      m_linked_menu_fullpath.Click += new EventHandler(OnCopyFullPathClick);

      m_linked_menu_open = new ToolStripMenuItem("Open");
      m_linked_menu_open.Click += new EventHandler(OnOpenMenuItemClick);

      m_linked_menu_explore = new ToolStripMenuItem("Open Containing Folder");
      m_linked_menu_explore.Click += new EventHandler(OnExploreMenuItemClick);

      m_embedded_menu = new ContextMenuStrip();
      m_embedded_menu.Opening += new CancelEventHandler(OnEmbeddedMenuOpening);
      m_embedded_menu.Items.Add(m_embedded_menu_insert);
      m_embedded_menu.Items.Add(m_embedded_menu_select);
      //m_embedded_menu.Items.Add(m_embedded_menu_rename);

      m_linked_menu = new ContextMenuStrip();
      m_linked_menu.Opening += new CancelEventHandler(OnLinkedMenuOpening);
      m_linked_menu.Items.Add(m_linked_menu_select);
      m_linked_menu.Items.Add(new ToolStripSeparator());
      m_linked_menu.Items.Add(m_linked_menu_fullpath);
      m_linked_menu.Items.Add(new ToolStripSeparator());
      m_linked_menu.Items.Add(m_linked_menu_open);
      m_linked_menu.Items.Add(m_linked_menu_explore);
    }

    /// <summary>
    /// Clean up any resources being used
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (null != m_components)
          m_components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      m_components = new Container();
    }
    #endregion

    /// <summary>
    /// Gets or sets show hidden blocks flag
    /// </summary>
    public bool ShowHiddenBlocks
    {
      get { return m_show_hidden_blocks; }
      set
      {
        m_show_hidden_blocks = value;
        Load();
      }
    }

    /// <summary>
    /// Gets or sets show reference blocks flag
    /// </summary>
    public bool ShowReferenceBlocks
    {
      get { return m_show_reference_blocks; }
      set
      {
        m_show_reference_blocks = value;
        Load();
      }
    }

    /// <summary>
    /// Gets or sets show used blocks flag
    /// </summary>
    public bool ShowUsedBlocks
    {
      get { return m_show_used_blocks; }
      set
      {
        m_show_used_blocks = value;
        Load();
      }
    }

    /// <summary>
    /// Gets or sets show unused blocks flag
    /// </summary>
    public bool ShowUnusedBlocks
    {
      get { return m_show_unused_blocks; }
      set
      {
        m_show_unused_blocks = value;
        Load();
      }
    }

    /// <summary>
    /// Gets or sets show objects flag
    /// </summary>
    public bool ShowFullPath
    {
      get { return m_show_fullpath; }
      set
      {
        m_show_fullpath = value;
        Load();
      }
    }

    /// <summary>
    /// Gets or sets show objects flag
    /// </summary>
    public bool ShowObjects
    {
      get { return m_show_objects; }
      set
      {
        m_show_objects = value;
        Load();
      }
    }

    /// <summary>
    /// Gets or sets block filter
    /// </summary>
    public BlockCommanderFilter BlockFilter
    {
      get { return m_block_filter; }
      set
      {
        m_block_filter = value;
        Load();
      }
    }

    /// <summary>
    /// Returns a block filter string (for UI)
    /// </summary>
    public static string FilterString(BlockCommanderFilter filter)
    {
      string str = null;
      switch (filter)
      {
        case BlockCommanderFilter.All:
          str = "All Blocks";
          break;
        case BlockCommanderFilter.Embedded:
          str = "Embedded Blocks";
          break;
        case BlockCommanderFilter.LinkedAndEmbedded:
          str = "Embedded-Linked Blocks";
          break;
        case BlockCommanderFilter.Linked:
          str = "Linked Blocks";
          break;
      }
      return str;
    }

    /// <summary>
    /// Clear the tree
    /// </summary>
    public void Clear()
    {
      Nodes.Clear();
    }

    /// <summary>
    /// Load the tree
    /// </summary>
    public void Load()
    {
      BeginUpdate();

      Nodes.Clear();

      var doc = RhinoDoc.ActiveDoc;
      if (null != doc)
      {
        var instance_definitions = doc.InstanceDefinitions.GetList(true);

        // Sort
        Array.Sort(instance_definitions, (idef0, idef1) => string.Compare(idef0.Name, idef1.Name, System.StringComparison.OrdinalIgnoreCase));

        // Cull tenuous blocks
        //instance_definitions = instance_definitions.Where(x => !CullInstanceDefinition(x)).ToArray();

        foreach (InstanceDefinition idef in instance_definitions)
        {
          if (!idef.IsDeleted)
            LoadHelper(idef, null);
        }
      }

      EndUpdate();
    }


    /// <summary>
    /// Load helper (recursive function)
    /// </summary>
    protected void LoadHelper(InstanceDefinition idef, TreeNode node)
    {
      if (null != idef)
      {
        TreeNode root = null;
        if (null == node)
        {
          bool add = ShowInstanceDefinition(idef);
          if (!add)
            return;

          string text = InstanceDefinitionNodeText(idef);
          int index = InstanceDefinitionImageIndex(idef);
          root = Nodes.Add(idef.Name, text, index, index);
          root.Tag = idef.Id;
          root.ContextMenuStrip = (idef.UpdateType == InstanceDefinitionUpdateType.Static)
            ? m_embedded_menu
            : m_linked_menu;
        }
        else
        {
          root = node;
        }

        RhinoObject[] rhino_objects = idef.GetObjects();
        if (null != rhino_objects)
        {
          // 1.) Add nested instance definitions
          Dictionary<string, int> idef_dictionary = new Dictionary<string, int>();
          for (int i = 0; i < rhino_objects.Length; i++)
          {
            RhinoObject rhino_object = rhino_objects[i];
            if (null != rhino_object && !rhino_object.IsDeleted)
            {
              InstanceObject instance_object = rhino_object as InstanceObject;
              if (null != instance_object)
              {
                InstanceDefinition instance_definition = instance_object.InstanceDefinition;
                if (null != instance_definition && !instance_definition.IsDeleted)
                {
                  int value = 0;
                  if (!idef_dictionary.TryGetValue(instance_definition.Name, out value))
                    idef_dictionary.Add(instance_definition.Name, 1);
                  else
                    idef_dictionary[instance_definition.Name] = value + 1;
                  rhino_objects[i] = null;
                }
              }
            }
          }

          if (idef_dictionary.Count > 0)
          {
            List<string> list = idef_dictionary.Keys.ToList();
            list.Sort();

            foreach (var key in list)
            {
              InstanceDefinition instance_definition = RhinoDoc.ActiveDoc.InstanceDefinitions.Find(key);
              if (null != instance_definition)
              {
                string str = key;

                if (!m_show_fullpath)
                {
                  int length = m_substring.Length;
                  int last = key.LastIndexOf(m_substring);
                  if (last > 0)
                  {
                    str = key.Substring(last + length);
                    str = str.Trim();
                  }
                }

                string text = string.Format("{0} ({1})", str, idef_dictionary[key]);
                int index = InstanceDefinitionImageIndex(instance_definition);
                TreeNode item = root.Nodes.Add(key, text, index, index);
                item.Tag = instance_definition.Id;
                item.ContextMenuStrip = (instance_definition.UpdateType == InstanceDefinitionUpdateType.Static)
                  ? m_embedded_menu
                  : m_linked_menu;
                LoadHelper(instance_definition, item);
              }
            }
          }


          // 2.) Add objects (if enabled)
          if (m_show_objects)
          {
            Dictionary<string, int> obj_dictionary = new Dictionary<string, int>();
            for (int i = 0; i < rhino_objects.Length; i++)
            {
              RhinoObject rhino_object = rhino_objects[i];
              if (null != rhino_object && !rhino_object.IsDeleted)
              {
                string key = rhino_object.ShortDescription(false);
                int value = 0;
                if (!obj_dictionary.TryGetValue(key, out value))
                  obj_dictionary.Add(key, 1);
                else
                  obj_dictionary[key] = value + 1;
              }
            }

            List<string> list = obj_dictionary.Keys.ToList();
            list.Sort();

            foreach (var key in list)
            {
              string text = string.Format("{0} ({1})", key, obj_dictionary[key]);
              TreeNode item = root.Nodes.Add(key, text, -1, -1);
              item.Tag = Guid.Empty;
            }
          }
        }
      }
    }

    /// <summary>
    /// ShowInstanceDefinition
    /// </summary>
    protected bool ShowInstanceDefinition(InstanceDefinition idef)
    {
      if (null == idef || idef.IsDeleted)
        return false;

      bool add = false;
      switch (m_block_filter)
      {
        case BlockCommanderFilter.All:
          add = true;
          break;
        case BlockCommanderFilter.Embedded:
          if (idef.UpdateType == InstanceDefinitionUpdateType.Static)
            add = true;
          break;
        case BlockCommanderFilter.LinkedAndEmbedded:
          if (idef.UpdateType == InstanceDefinitionUpdateType.LinkedAndEmbedded)
            add = true;
          break;
        case BlockCommanderFilter.Linked:
          if (idef.UpdateType == InstanceDefinitionUpdateType.Linked)
            add = true;
          break;
      }

      if (add)
      {
        // Show hidden blocks?
        bool hidden = (idef.Name[0] == '*');
        if (!m_show_hidden_blocks && hidden)
          add = false;
      }

      if (add)
      {
        // Show reference blocks?
        if (!m_show_reference_blocks && idef.IsReference)
          add = false;
      }

      // If not showing used or unused blocks, show nothing
      if (add)
      {
        if (!m_show_used_blocks && !m_show_unused_blocks)
        {
          add = false;
        }
        else if (m_show_used_blocks && m_show_unused_blocks)
        {
          if (!idef.InUse(0) && (idef.InUse(1) || idef.InUse(2)))
            add = false;
        }
        else if (m_show_used_blocks && !idef.InUse(0))
        {
          // If only showing used blocks, if the block is not
          // used at top level, don't show it.
          if (!idef.InUse(0))
            add = false;
        }
        else if (m_show_unused_blocks)
        {
          // If only showing unused blocks, if the block is used
          // in any way, don't show it.
          if (idef.InUse(0) || idef.InUse(1) || idef.InUse(2))
            add = false;
        }

      }

      return add;
    }

    /// <summary>
    /// CullInstanceDefinition (unused right now...)
    /// </summary>
    protected bool CullInstanceDefinition(InstanceDefinition idef)
    {
      if (null == idef)
        return true;

      if (idef.IsDeleted)
        return true;

      if (idef.IsTenuous)
      {
        // CRhinoInstanceDefinition::IsReference() returns true if
        // CRhinoInstanceDefinition::IsInWorksessionReferenceModel() or
        // CRhinoInstanceDefinition::IsInLinkedBlockReferenceModel()
        // return true
        return !idef.IsReference;
      }

      return false;
    }

    /// <summary>
    /// InstanceDefinitonImageIndex
    /// </summary>
    protected int InstanceDefinitionImageIndex(InstanceDefinition idef)
    {
      int index = -1;
      if (null != idef)
      {
        if (idef.UpdateType == InstanceDefinitionUpdateType.Static)
          index = 0;
        else
          index = 1;
      }
      return index;
    }

    /// <summary>
    /// InstanceDefinitionNodeText
    /// </summary>
    protected string InstanceDefinitionNodeText(InstanceDefinition idef)
    {
      string text = null;
      if (null != idef)
      {
        InstanceObject[] irefs = idef.GetReferences(0);
        text = string.Format("{0} ({1})", idef.Name, (null == irefs) ? 0 : irefs.Length);
      }
      return text;
    }


    #region Context Menu Handlers

    /// <summary>
    /// Embedded context menu opening
    /// </summary>
    private void OnEmbeddedMenuOpening(object sender, CancelEventArgs e)
    {
      m_embedded_menu_select.Enabled = false;

      if (null != this.SelectedNode)
      {
        Guid idef_id = (Guid)this.SelectedNode.Tag;
        InstanceDefinition idef = RhinoDoc.ActiveDoc.InstanceDefinitions.Find(idef_id, true);
        if (null != idef)
        {
          m_embedded_menu_select.Enabled = idef.InUse(0);
        }
      }
    }

    /// <summary>
    /// Linked context menu opening
    /// </summary>
    void OnLinkedMenuOpening(object sender, CancelEventArgs e)
    {
      m_linked_menu_select.Enabled = false;
      m_linked_menu_fullpath.Enabled = false;
      m_linked_menu_explore.Enabled = false;
      m_linked_menu_open.Enabled = false;

      if (null != this.SelectedNode)
      {
        Guid idef_id = (Guid)this.SelectedNode.Tag;
        InstanceDefinition idef = RhinoDoc.ActiveDoc.InstanceDefinitions.Find(idef_id, true);
        if (null != idef)
        {
          m_linked_menu_select.Enabled = idef.InUse(0);
          m_linked_menu_fullpath.Enabled = !string.IsNullOrEmpty(idef.SourceArchive);
          bool exists = File.Exists(idef.SourceArchive);
          m_linked_menu_explore.Enabled = exists;
          m_linked_menu_open.Enabled = exists;
        }
      }
    }

    private void OnInsertMenuItemClick(object sender, EventArgs e)
    {
      if (null != this.SelectedNode)
      {
        Guid idef_id = (Guid)this.SelectedNode.Tag;
        InstanceDefinition idef = RhinoDoc.ActiveDoc.InstanceDefinitions.Find(idef_id, true);
        if (null != idef)
        {
          string cmd = string.Format("! _-Insert _File=_No \"{0}\" _Block", idef.Name);
          RhinoApp.RunScript(cmd, true);
        }
      }

    }

    /// <summary>
    /// Select context menu handler
    /// </summary>
    void OnSelectMenuItemClick(object sender, EventArgs e)
    {
      if (null != this.SelectedNode)
      {
        Guid idef_id = (Guid)this.SelectedNode.Tag;
        InstanceDefinition idef = RhinoDoc.ActiveDoc.InstanceDefinitions.Find(idef_id, true);
        if (null != idef)
        {
          InstanceObject[] irefs = idef.GetReferences(0);
          if (null != irefs && irefs.Length > 0)
          {
            int selected = 0;
            foreach (InstanceObject iref in irefs)
            {
              if (null != iref && iref.IsSelectable())
              {
                if (iref.Select(true) > 0)
                  selected++;
              }
            }

            if (selected > 0)
              RhinoDoc.ActiveDoc.Views.Redraw();

            if (1 == selected)
              RhinoApp.WriteLine("1 block instance added to selection.");
            else
              RhinoApp.WriteLine("{0} block instances added to selection.", selected);
          }
        }
      }
    }

    /// <summary>
    /// Rename context menu handler
    /// </summary>
    void OnRenameMenuItemClick(object sender, EventArgs e)
    {
      if (null != this.SelectedNode)
        this.SelectedNode.BeginEdit();
    }

    protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
    {
      //if (e.Label.Length > 0)
      //{
      //  RhinoDoc doc = RhinoDoc.ActiveDoc;
      //  if (null != doc)
      //  {
      //    InstanceDefinition idef = doc.InstanceDefinitions.Find(e.Label);
      //    if (null == idef)
      //    {
      //      Guid id = (Guid)e.Node.Tag;
      //      if (id != Guid.Empty)
      //      {
      //        idef = RhinoDoc.ActiveDoc.InstanceDefinitions.Find(id, true);
      //        if (null != idef)
      //        {
      //          e.Node.EndEdit(false);
      //          uint undo_record = doc.BeginUndoRecord("Rename block instance");
      //          doc.InstanceDefinitions.Modify(idef, e.Label, idef.Description, true);
      //          if (undo_record > 0)
      //            doc.EndUndoRecord(undo_record);
      //          return;
      //        }
      //      }
      //    }
      //  }
      //}
      //e.CancelEdit = true;
    }

    /// <summary>
    /// Full path context menu handler
    /// </summary>
    void OnCopyFullPathClick(object sender, EventArgs e)
    {
      if (null != this.SelectedNode)
      {
        Guid idef_id = (Guid)this.SelectedNode.Tag;
        InstanceDefinition idef = RhinoDoc.ActiveDoc.InstanceDefinitions.Find(idef_id, true);
        if (null != idef)
        {
          if (!string.IsNullOrEmpty(idef.SourceArchive))
            Clipboard.SetText(idef.SourceArchive);
        }
      }
    }

    /// <summary>
    /// Open context menu handler
    /// </summary>
    private void OnOpenMenuItemClick(object sender, EventArgs e)
    {
      if (null != this.SelectedNode)
      {
        Guid idef_id = (Guid)this.SelectedNode.Tag;
        InstanceDefinition idef = RhinoDoc.ActiveDoc.InstanceDefinitions.Find(idef_id, true);
        if (null != idef)
        {
          if (File.Exists(idef.SourceArchive))
          {
            string path = Rhino.ApplicationSettings.FileSettings.ExecutableFolder;
            if (Environment.Is64BitProcess)
              path += @"\Rhino.exe";
            else
              path += @"\Rhino4.exe";

            if (File.Exists(path))
            {
              ProcessStartInfo info = new ProcessStartInfo();
              info.FileName = path;
              info.Arguments = string.Format("\"{0}\"", idef.SourceArchive);
              Process.Start(info);
            }
          }
        }
      }
    }

    /// <summary>
    /// Explore context menu handler
    /// </summary>
    void OnExploreMenuItemClick(object sender, EventArgs e)
    {
      if (null != this.SelectedNode)
      {
        Guid idef_id = (Guid)this.SelectedNode.Tag;
        InstanceDefinition idef = RhinoDoc.ActiveDoc.InstanceDefinitions.Find(idef_id, true);
        if (null != idef_id)
        {
          if (File.Exists(idef.SourceArchive))
          {
            string args = string.Format("/e, /select, \"{0}\"", idef.SourceArchive);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "Explorer";
            info.Arguments = args;
            Process.Start(info);
          }
        }
      }
    }

    #endregion

    /// <summary>
    /// OnDrawNode override
    /// </summary>
    protected override void OnDrawNode(DrawTreeNodeEventArgs e)
    {
      // Space between ImageList and Label
      const int spacing = 3;

      e.DrawDefault = true;
      base.OnDrawNode(e);

      if (ShowLines && null != ImageList && -1 == e.Node.ImageIndex)
      {
        int width = ImageList.ImageSize.Width;
        int height = ImageList.ImageSize.Height;

        int x_pos = e.Node.Bounds.Left - spacing - width / 2;
        int y_pos = (e.Node.Bounds.Top + e.Node.Bounds.Bottom) / 2;

        Rectangle rect = new Rectangle(x_pos, y_pos, 0, 0);
        rect.Inflate(width / 2, height / 2);

        using (Pen pen = new Pen(base.LineColor, 1))
        {
          pen.DashStyle = DashStyle.Dot;

          // Account for uneven indent for both lines
          pen.DashOffset = Indent % 2;

          // Horizontal tree line across width of image.
          int y_horz = y_pos + ((ItemHeight - rect.Height) / 2) % 2;
          e.Graphics.DrawLine(pen, (ShowRootLines || e.Node.Level > 0) ? rect.Left : x_pos - (int)pen.DashOffset, y_horz, rect.Right, y_horz);

          if (!CheckBoxes && e.Node.IsExpanded)
          {
            // Vertical treeline, from node's image center to e.Node.Bounds.Bottom
            int y_vert = y_horz + (int)pen.DashOffset;
            e.Graphics.DrawLine(pen, x_pos, y_vert, x_pos, e.Node.Bounds.Bottom);
          }
        }
      }
    }

    /// <summary>
    /// OnAfterCollapse override
    /// </summary>
    protected override void OnAfterCollapse(TreeViewEventArgs e)
    {
      base.OnAfterCollapse(e);
      if (!CheckBoxes && null != ImageList && -1 == e.Node.ImageIndex)
      {
        // DrawNode event not raised: redraw node with collapsed treeline
        base.Invalidate(e.Node.Bounds);
      }
    }

    /// <summary>
    /// OnNodeMouseClick
    /// </summary>
    protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
    {
      base.OnNodeMouseClick(e);
      if (e.Button == MouseButtons.Right)
      {
        // Helps with context menu click
        this.SelectedNode = e.Node;
      }
    }
  }
}
