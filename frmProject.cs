using modProject;
using System;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static generalUtils;
using static WinOpenGL_ShaderToy.ProjectDef;
using static clsHPTimer;
using static modProject.clsGeometry;
using System.Collections.Generic;

namespace WinOpenGL_ShaderToy
{
	public partial class frmProject : DockContent
	{
		public clsProject Project { private set; get; }
		public frmProject(clsProject refProject)
		{
			InitializeComponent();
			Project = refProject;
		}
		clsHPTimer timerUpdate;
		private void FrmProjects_Load(object sender, EventArgs e)
		{
			ToolStripMenuItem itm = menuNew_Shader;
			foreach(string str in Enum.GetNames(typeof(ShaderType)))
			{
				if(!str.EndsWith("Arb"))
				{
					ToolStripMenuItem menuitm = new ToolStripMenuItem(str);
					menuitm.Click += new EventHandler((itmS, evnt) => { AddNewShader((ShaderType)Enum.Parse(typeof(ShaderType), str)); });
					itm.DropDownItems.Add(menuitm);
				}
			}
			timerUpdate = new clsHPTimer(this);
			timerUpdate.Interval = 1000.0;
			timerUpdate.SleepInterval = 250;
			timerUpdate.Performence.HistoryDuration = timerUpdate.Interval / 1000.0;
			timerUpdate.IntervalEnd += new HPIntervalEventHandler(timerUpdate_Tick);
			timerUpdate.Start();
		}
		private void FrmProject_FormClosing(object sender, FormClosingEventArgs e)
		{
			timerUpdate.Stop();
			timerUpdate = null;
		}

		private void timerUpdate_Tick(object sender, HPIntervalEventArgs e)
		{
			UpdateProjectTree();
			UpdateAllNodes(treeMain);
		}
		private void UpdateProjectTree()
		{
			List<clsProjectObject> aryObjsCurrent = new List<clsProjectObject>();
			List<clsProjectObject> aryObjsToAdd = new List<clsProjectObject>();
			List<TreeNode> aryNodesToRemove = new List<TreeNode>();
			foreach (TreeNode N in treeMain.Nodes["nodeProject"].Nodes)
			{
				clsProjectObject obj = N.Tag as clsProjectObject;
				if (obj != null)
				{
					if(Project.ProjectObjects.Contains(obj))
					{
						aryObjsCurrent.Add(obj);
					} else
					{
						aryNodesToRemove.Add(N);
					}
				}
			}
			foreach (clsProjectObject obj in Project.ProjectObjects)
			{
				if (!aryObjsCurrent.Contains(obj)) aryObjsToAdd.Add(obj);
			}
			foreach (TreeNode N in aryNodesToRemove)
			{
				N.Remove();
			}
			foreach(clsProjectObject obj in aryObjsToAdd)
			{
				TreeNode newNode = new TreeNode();
				newNode.ContextMenuStrip = menuProjectObject;
				newNode.Text = obj.ToString();
				newNode.Tag = obj;
				treeMain.Nodes["nodeProject"].Nodes.Add(newNode);
			}
		}
		private void UpdateAllNodes(TreeView tree)
		{
			foreach (TreeNode nodeI in tree.Nodes)
			{
				UpdateAllNodes(nodeI);
			}
		}
		private void UpdateAllNodes(TreeNode node)
		{
			clsProjectObject obj = node.Tag as clsProjectObject;
			if(obj != null)
			{
				if(!node.IsEditing && node != nodeHover)
				{
					node.Name = obj.ToString();
					node.Text = obj.ToString();
				}
			}
			foreach (TreeNode nodeI in node.Nodes)
			{
				UpdateAllNodes(nodeI);
			}
		}

		private void TreeMain_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			clsProjectObject obj = (clsProjectObject)e.Node.Tag;
		}
		private void TreeMain_AfterSelect(object sender, TreeViewEventArgs e)
		{
			clsProjectObject obj = (clsProjectObject)e.Node.Tag;
		}

		private void MenuNew_Geometry_Click(object sender, EventArgs e)
		{
			AddNewProjectObject(new clsGeometry());
		}
		private void AddNewShader(ShaderType typ)
		{
			AddNewProjectObject(new clsShader(typ));
		}
		private void MenuNew_Program_Click(object sender, EventArgs e)
		{
			AddNewProjectObject(new clsProgram());
		}
		private void MenuNew_Render_Click(object sender, EventArgs e)
		{
			AddNewProjectObject(new clsRender());
		}
		private void menuNew_VertexDescription_Click(object sender, EventArgs e)
		{
			AddNewProjectObject(new clsVertexDescription());
		}
		private TreeNode nodeMouseDown = null;
		private void TreeMain_MouseDown(object sender, MouseEventArgs e)
		{
			nodeMouseDown = treeMain.GetNodeAt(e.Location);
		}
		private void MenuProjectObject_Delete_Click(object sender, EventArgs e)
		{
			if(nodeMouseDown != null)
			{
				clsProjectObject obj = nodeMouseDown.Tag as clsProjectObject;
				if (obj != null)
				{
					if(obj.ParentControl != null)
					{
						if(obj.ParentControl.ParentForm != null)
						{
							obj.ParentControl.ParentForm.Close();
						} else
						{
							obj.ParentControl.Dispose();
						}
					}
					projectMain.ProjectObjects.Remove(obj);
					obj.Dispose();
				}
				nodeMouseDown.Remove();
			}
		}
		public void AddNewProjectObject<T>(T obj) where T : clsProjectObject
		{
			projectMain.ProjectObjects.Add(obj);
			TreeNode newNode = new TreeNode();
			newNode.ContextMenuStrip = menuProjectObject;
			newNode.Text = obj.ToString();
			newNode.Tag = obj;
			treeMain.Nodes["nodeProject"].Nodes.Add(newNode);
			DockContent newForm = NewFormFromObject(obj);
			newForm.Show(dockMainPanel, DockState.Document);
		}
		private void TreeMain_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TreeNode nodeClicked = treeMain.GetNodeAt(e.Location);
			clsProjectObject objNode = nodeClicked.Tag as clsProjectObject;
			if(objNode != null)
			{
				if(objNode.ParentControl == null || objNode.ParentControl.IsDisposed)
				{
					DockContent frmNew = NewFormFromObject(objNode);
					frmNew.Show(dockMainPanel, DockState.Document);
				} else
				{
					objNode.ParentControl.ParentControl.Focus();
				}
			}
		}
		private void TreeMain_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			clsProjectObject obj = e.Node.Tag as clsProjectObject;
			if (obj != null)
			{
				e.Node.Text = obj.Name;
			} else
			{
				e.CancelEdit = true;
			}
		}
		private void TreeMain_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			clsProjectObject obj = e.Node.Tag as clsProjectObject;
			obj.Name = e.Label;
			e.Node.Text = obj.ToString();
			treeMain.SelectedNode = null;
		}
		private TreeNode nodeHover = null;
		private void TreeMain_MouseMove(object sender, MouseEventArgs e)
		{
			TreeNode node = treeMain.GetNodeAt(e.Location);
			if (node != nodeHover)
			{
				if (nodeHover != null)
				{
					if (!nodeHover.IsEditing)
					{
						clsProjectObject obj = nodeHover.Tag as clsProjectObject;
						if (obj != null)
						{
							nodeHover.Text = obj.ToString();
						}
					}
				}
				if (node != null)
				{
					if (!node.IsEditing)
					{
						clsProjectObject obj = node.Tag as clsProjectObject;
						if (obj != null)
						{
							node.Text = obj.Name;
						}
					}
				}
				nodeHover = node;
			}
		}
	}
}
