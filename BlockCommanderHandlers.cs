using System;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace BlockCommander
{
  internal enum BlockCommanderEvent
  {
    NoEvent,
    HandlersEnabled,
    NewDocument,
    CloseDocument,
    EndOpenDocument,
    InstanceDefinitionAdded,
    InstanceDefinitionModified,
    InstanceDefinitionDeleted,
    AddRhinoObject,
    DeleteRhinoObject,
    UndoRedo
  }


  class BlockCommanderHandlers
  {
    #region Private Members
    private readonly EventHandler<DocumentEventArgs> m_new_document_hander;
    private readonly EventHandler<DocumentEventArgs> m_close_document_handler;
    private readonly EventHandler<DocumentOpenEventArgs> m_end_open_document_handler;
    private readonly EventHandler<InstanceDefinitionTableEventArgs> m_idef_table_handler;
    private readonly EventHandler<RhinoObjectEventArgs> m_add_object_handler;
    private readonly EventHandler<RhinoObjectEventArgs> m_delete_object_handler;
    private readonly EventHandler<UndoRedoEventArgs> m_undo_redo_handler;
    private readonly EventHandler m_idle_handler;
    private bool m_enabled;
    private readonly object m_locker;
    private BlockCommanderEvent m_event;
    #endregion

    public BlockCommanderHandlers(BlockCommanderPanel panel)
    {
      Panel = panel;
      m_new_document_hander = new EventHandler<DocumentEventArgs>(OnNewDocument);
      m_close_document_handler = new EventHandler<DocumentEventArgs>(OnCloseDocument);
      m_end_open_document_handler = new EventHandler<DocumentOpenEventArgs>(OnEndOpenDocument);
      m_idef_table_handler = new EventHandler<InstanceDefinitionTableEventArgs>(OnInstanceDefinitionTableEvent);
      m_add_object_handler = new EventHandler<RhinoObjectEventArgs>(OnAddObject);
      m_delete_object_handler = new EventHandler<RhinoObjectEventArgs>(OnDeleteObject);
      m_undo_redo_handler = new EventHandler<UndoRedoEventArgs>(OnUndoRedo);
      m_idle_handler = new EventHandler(OnIdle);
      m_enabled = false;
      m_locker = new object();
      m_event = BlockCommanderEvent.NoEvent;
    }

    /// <summary>
    /// The tabbed dockbar panel
    /// </summary>
    public BlockCommanderPanel Panel
    {
      get;
      private set;
    }

    /// <summary>
    /// Enables or disables the event handlers
    /// </summary>
    public void Enable(bool bEnable)
    {
      if (bEnable != m_enabled)
      {
        if (bEnable)
        {
          RhinoDoc.NewDocument += m_new_document_hander;
          RhinoDoc.CloseDocument += m_close_document_handler;
          RhinoDoc.EndOpenDocument += m_end_open_document_handler;
          RhinoDoc.InstanceDefinitionTableEvent += m_idef_table_handler;
          RhinoDoc.AddRhinoObject += m_add_object_handler;
          RhinoDoc.DeleteRhinoObject += m_delete_object_handler;
          Command.UndoRedo += m_undo_redo_handler;
          RhinoApp.Idle += m_idle_handler;
          m_event = BlockCommanderEvent.HandlersEnabled;
        }
        else
        {
          RhinoDoc.NewDocument -= m_new_document_hander;
          RhinoDoc.CloseDocument -= m_close_document_handler;
          RhinoDoc.EndOpenDocument -= m_end_open_document_handler;
          RhinoDoc.InstanceDefinitionTableEvent -= m_idef_table_handler;
          RhinoDoc.AddRhinoObject -= m_add_object_handler;
          RhinoDoc.DeleteRhinoObject -= m_delete_object_handler;
          Command.UndoRedo -= m_undo_redo_handler;
          RhinoApp.Idle -= m_idle_handler;
          m_event = BlockCommanderEvent.NoEvent;
        }
      }
      m_enabled = bEnable;
    }

    /// <summary>
    /// Returns whether or not the event handlers are enabled
    /// </summary>
    public bool IsEnabled()
    {
      return m_enabled;
    }

    /// <summary>
    /// OnNewDocument event handler
    /// </summary>
    void OnNewDocument(object sender, DocumentEventArgs e)
    {
      m_event = BlockCommanderEvent.NewDocument;
    }

    /// <summary>
    /// OnCloseDocument event handler
    /// </summary>
    void OnCloseDocument(object sender, DocumentEventArgs e)
    {
      //m_event = BlockCommanderEvent.CloseDocument;
      Panel.Tree.Clear();
    }

    /// <summary>
    /// OnEndOpenDocument event handler
    /// </summary>
    void OnEndOpenDocument(object sender, DocumentOpenEventArgs e)
    {
      m_event = BlockCommanderEvent.EndOpenDocument;
    }

    /// <summary>
    /// OnInstanceDefinitionTableEvent event handler
    /// </summary>
    void OnInstanceDefinitionTableEvent(object sender, InstanceDefinitionTableEventArgs e)
    {
      if (e.EventType == InstanceDefinitionTableEventType.Added)
        m_event = BlockCommanderEvent.InstanceDefinitionAdded;
      else if (e.EventType == InstanceDefinitionTableEventType.Modified)
        m_event = BlockCommanderEvent.InstanceDefinitionModified;
      else if (e.EventType == InstanceDefinitionTableEventType.Deleted)
        m_event = BlockCommanderEvent.InstanceDefinitionDeleted;
    }

    /// <summary>
    /// OnAddObject event handler
    /// </summary>
    void OnAddObject(object sender, RhinoObjectEventArgs e)
    {
      if (null != e.TheObject && e.TheObject.ObjectType == ObjectType.InstanceReference)
        m_event = BlockCommanderEvent.AddRhinoObject;
    }

    /// <summary>
    /// OnDeleteObject event handler
    /// </summary>
    void OnDeleteObject(object sender, RhinoObjectEventArgs e)
    {
      if (null != e.TheObject && e.TheObject.ObjectType == ObjectType.InstanceReference)
        m_event = BlockCommanderEvent.DeleteRhinoObject;
    }

    /// <summary>
    /// OnUndoRedo event handler
    /// </summary>
    private void OnUndoRedo(object sender, UndoRedoEventArgs e)
    {
      if (e.IsBeginRedo || e.IsBeginUndo)
        m_event = BlockCommanderEvent.UndoRedo;
    }

    /// <summary>
    /// OnIdle event handler
    /// </summary>
    void OnIdle(object sender, EventArgs e)
    {
      lock (m_locker)
      {
        if (m_event != BlockCommanderEvent.NoEvent)
        {
          Panel.Tree.Load();
          m_event = BlockCommanderEvent.NoEvent;
        }
      }
    }

  }
}
