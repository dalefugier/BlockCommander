namespace BlockCommander
{
  partial class BlockCommanderPanel
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockCommanderPanel));
      this.m_combo = new System.Windows.Forms.ComboBox();
      this.m_toolstrip = new System.Windows.Forms.ToolStrip();
      this.m_btn_block_definition = new System.Windows.Forms.ToolStripButton();
      this.m_btn_block_insert = new System.Windows.Forms.ToolStripButton();
      this.m_btn_block_manager = new System.Windows.Forms.ToolStripButton();
      this.m_btn_block_editor = new System.Windows.Forms.ToolStripButton();
      this.m_btn_block_select = new System.Windows.Forms.ToolStripButton();
      this.m_btn_block_settings = new System.Windows.Forms.ToolStripDropDownButton();
      this.m_menu_show_hidden_blocks = new System.Windows.Forms.ToolStripMenuItem();
      this.m_menu_show_reference_blocks = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.m_menu_show_used_blocks = new System.Windows.Forms.ToolStripMenuItem();
      this.m_menu_show_unused_blocks = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.m_menu_show_fullpath = new System.Windows.Forms.ToolStripMenuItem();
      this.m_menu_show_objects = new System.Windows.Forms.ToolStripMenuItem();
      this.m_image_list = new System.Windows.Forms.ImageList(this.components);
      this.m_tree = new BlockCommander.BlockCommanderTreeView();
      this.m_toolstrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_combo
      // 
      this.m_combo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.m_combo.FormattingEnabled = true;
      this.m_combo.Location = new System.Drawing.Point(0, 28);
      this.m_combo.Name = "m_combo";
      this.m_combo.Size = new System.Drawing.Size(215, 21);
      this.m_combo.TabIndex = 0;
      this.m_combo.SelectedIndexChanged += new System.EventHandler(this.m_combo_SelectedIndexChanged);
      // 
      // m_toolstrip
      // 
      this.m_toolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.m_toolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_btn_block_definition,
            this.m_btn_block_insert,
            this.m_btn_block_manager,
            this.m_btn_block_editor,
            this.m_btn_block_select,
            this.m_btn_block_settings});
      this.m_toolstrip.Location = new System.Drawing.Point(0, 0);
      this.m_toolstrip.Name = "m_toolstrip";
      this.m_toolstrip.Size = new System.Drawing.Size(215, 25);
      this.m_toolstrip.TabIndex = 2;
      // 
      // m_btn_block_definition
      // 
      this.m_btn_block_definition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_btn_block_definition.Image = global::BlockCommander.Properties.Resources.BlockDefinition24;
      this.m_btn_block_definition.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_btn_block_definition.Name = "m_btn_block_definition";
      this.m_btn_block_definition.Size = new System.Drawing.Size(23, 22);
      this.m_btn_block_definition.Text = "Block Definition";
      this.m_btn_block_definition.Click += new System.EventHandler(this.m_btn_block_definition_Click);
      // 
      // m_btn_block_insert
      // 
      this.m_btn_block_insert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_btn_block_insert.Image = global::BlockCommander.Properties.Resources.BlockInsert24;
      this.m_btn_block_insert.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_btn_block_insert.Name = "m_btn_block_insert";
      this.m_btn_block_insert.Size = new System.Drawing.Size(23, 22);
      this.m_btn_block_insert.Text = "Insert Block";
      this.m_btn_block_insert.Click += new System.EventHandler(this.m_btn_block_insert_Click);
      // 
      // m_btn_block_manager
      // 
      this.m_btn_block_manager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_btn_block_manager.Image = global::BlockCommander.Properties.Resources.BlockManager24;
      this.m_btn_block_manager.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_btn_block_manager.Name = "m_btn_block_manager";
      this.m_btn_block_manager.Size = new System.Drawing.Size(23, 22);
      this.m_btn_block_manager.Text = "Block Manager";
      this.m_btn_block_manager.Click += new System.EventHandler(this.m_btn_block_manager_Click);
      // 
      // m_btn_block_editor
      // 
      this.m_btn_block_editor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_btn_block_editor.Image = global::BlockCommander.Properties.Resources.BlockEditor24;
      this.m_btn_block_editor.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_btn_block_editor.Name = "m_btn_block_editor";
      this.m_btn_block_editor.Size = new System.Drawing.Size(23, 22);
      this.m_btn_block_editor.Text = "Block Editor";
      this.m_btn_block_editor.Click += new System.EventHandler(this.m_btn_block_editor_Click);
      // 
      // m_btn_block_select
      // 
      this.m_btn_block_select.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_btn_block_select.Image = global::BlockCommander.Properties.Resources.BlockSelect24;
      this.m_btn_block_select.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_btn_block_select.Name = "m_btn_block_select";
      this.m_btn_block_select.Size = new System.Drawing.Size(23, 22);
      this.m_btn_block_select.Text = "Select Block";
      this.m_btn_block_select.Click += new System.EventHandler(this.m_btn_block_select_Click);
      // 
      // m_btn_block_settings
      // 
      this.m_btn_block_settings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_btn_block_settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menu_show_hidden_blocks,
            this.m_menu_show_reference_blocks,
            this.toolStripSeparator1,
            this.m_menu_show_used_blocks,
            this.m_menu_show_unused_blocks,
            this.toolStripSeparator2,
            this.m_menu_show_fullpath,
            this.m_menu_show_objects});
      this.m_btn_block_settings.Image = global::BlockCommander.Properties.Resources.BlockSettings24;
      this.m_btn_block_settings.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_btn_block_settings.Name = "m_btn_block_settings";
      this.m_btn_block_settings.Size = new System.Drawing.Size(29, 22);
      this.m_btn_block_settings.Text = "Settings";
      this.m_btn_block_settings.DropDownOpening += new System.EventHandler(this.m_btn_block_settings_DropDownOpening);
      // 
      // m_menu_show_hidden_blocks
      // 
      this.m_menu_show_hidden_blocks.Name = "m_menu_show_hidden_blocks";
      this.m_menu_show_hidden_blocks.Size = new System.Drawing.Size(195, 22);
      this.m_menu_show_hidden_blocks.Text = "Show Hidden Blocks";
      this.m_menu_show_hidden_blocks.Click += new System.EventHandler(this.m_menu_show_hidden_blocks_Click);
      // 
      // m_menu_show_reference_blocks
      // 
      this.m_menu_show_reference_blocks.Name = "m_menu_show_reference_blocks";
      this.m_menu_show_reference_blocks.Size = new System.Drawing.Size(195, 22);
      this.m_menu_show_reference_blocks.Text = "Show Reference Blocks";
      this.m_menu_show_reference_blocks.Click += new System.EventHandler(this.m_menu_show_reference_blocks_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
      // 
      // m_menu_show_used_blocks
      // 
      this.m_menu_show_used_blocks.Name = "m_menu_show_used_blocks";
      this.m_menu_show_used_blocks.Size = new System.Drawing.Size(195, 22);
      this.m_menu_show_used_blocks.Text = "Show Used Blocks";
      this.m_menu_show_used_blocks.Click += new System.EventHandler(this.m_menu_show_used_blocks_Click);
      // 
      // m_menu_show_unused_blocks
      // 
      this.m_menu_show_unused_blocks.Name = "m_menu_show_unused_blocks";
      this.m_menu_show_unused_blocks.Size = new System.Drawing.Size(195, 22);
      this.m_menu_show_unused_blocks.Text = "Show Unused Blocks";
      this.m_menu_show_unused_blocks.Click += new System.EventHandler(this.m_menu_show_unused_blocks_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
      // 
      // m_menu_show_fullpath
      // 
      this.m_menu_show_fullpath.Name = "m_menu_show_fullpath";
      this.m_menu_show_fullpath.Size = new System.Drawing.Size(195, 22);
      this.m_menu_show_fullpath.Text = "Show Full Block Name";
      this.m_menu_show_fullpath.Click += new System.EventHandler(this.m_menu_show_fullpath_Click);
      // 
      // m_menu_show_objects
      // 
      this.m_menu_show_objects.Name = "m_menu_show_objects";
      this.m_menu_show_objects.Size = new System.Drawing.Size(195, 22);
      this.m_menu_show_objects.Text = "Show Objects";
      this.m_menu_show_objects.Click += new System.EventHandler(this.m_menu_show_objects_Click);
      // 
      // m_image_list
      // 
      this.m_image_list.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_image_list.ImageStream")));
      this.m_image_list.TransparentColor = System.Drawing.Color.Transparent;
      this.m_image_list.Images.SetKeyName(0, "BlockEmbedded16.png");
      this.m_image_list.Images.SetKeyName(1, "BlockLinked16.png");
      // 
      // m_tree
      // 
      this.m_tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_tree.BlockFilter = BlockCommander.BlockCommanderFilter.All;
      this.m_tree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
      this.m_tree.ImageIndex = 0;
      this.m_tree.ImageList = this.m_image_list;
      this.m_tree.LabelEdit = true;
      this.m_tree.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
      this.m_tree.Location = new System.Drawing.Point(0, 50);
      this.m_tree.Name = "m_tree";
      this.m_tree.SelectedImageIndex = 0;
      this.m_tree.ShowFullPath = false;
      this.m_tree.ShowHiddenBlocks = false;
      this.m_tree.ShowObjects = true;
      this.m_tree.ShowReferenceBlocks = false;
      this.m_tree.ShowUnusedBlocks = false;
      this.m_tree.ShowUsedBlocks = true;
      this.m_tree.Size = new System.Drawing.Size(212, 194);
      this.m_tree.TabIndex = 1;
      // 
      // BlockCommanderPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_toolstrip);
      this.Controls.Add(this.m_tree);
      this.Controls.Add(this.m_combo);
      this.Name = "BlockCommanderPanel";
      this.Size = new System.Drawing.Size(215, 244);
      this.m_toolstrip.ResumeLayout(false);
      this.m_toolstrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox m_combo;
    private BlockCommanderTreeView m_tree;
    private System.Windows.Forms.ToolStrip m_toolstrip;
    private System.Windows.Forms.ToolStripButton m_btn_block_definition;
    private System.Windows.Forms.ToolStripButton m_btn_block_insert;
    private System.Windows.Forms.ToolStripButton m_btn_block_manager;
    private System.Windows.Forms.ToolStripButton m_btn_block_editor;
    private System.Windows.Forms.ToolStripButton m_btn_block_select;
    private System.Windows.Forms.ImageList m_image_list;
    private System.Windows.Forms.ToolStripDropDownButton m_btn_block_settings;
    private System.Windows.Forms.ToolStripMenuItem m_menu_show_hidden_blocks;
    private System.Windows.Forms.ToolStripMenuItem m_menu_show_reference_blocks;
    private System.Windows.Forms.ToolStripMenuItem m_menu_show_objects;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem m_menu_show_used_blocks;
    private System.Windows.Forms.ToolStripMenuItem m_menu_show_unused_blocks;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem m_menu_show_fullpath;
  }
}
