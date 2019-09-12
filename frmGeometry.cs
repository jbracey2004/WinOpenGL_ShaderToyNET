using modProject;
using OpenTK;
using OpenTK.Platform;
using static OpenTK.Platform.Utilities;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static WinOpenGL_ShaderToy.ProjectDef;
using System.Diagnostics;
using static generalUtils;
using static clsHPTimer;
using static WinOpenGL_ShaderToy.clsCollapsePanel;
using static modProject.clsGeometry;

namespace WinOpenGL_ShaderToy
{
	public partial class frmGeometry : DockContent
	{
		public frmGeometry(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsGeometry Geometry { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsGeometry; } }
		private clsCollapsePanel containerMain;
		private Stopwatch timeRun;
		private infoFramePerformance tsRender;
		private bool bolDataGridReady = false;
		private void FrmGeometry_Load(object sender, EventArgs e)
		{
			containerMain = new clsCollapsePanel(panelCollapse);
			containerMain.CollapseStateChanged += new CollapseStateChangeHandler(containerMain_CollapseChange);
			containerMain.CollapseDistanceChanged += new CollapseStateChangeHandler(containerMain_CollapseDistanceChange);
			DataGridViewComboBoxColumn rowCombo = ((DataGridViewComboBoxColumn)datagridVertexDescriptions.Columns["columnElementType"]);
			for (int itr = 0; itr < clsGeometry.clsVertexDescriptionComponent.VertexTypes.Keys.Count; itr++)
			{
				rowCombo.Items.Add(clsGeometry.clsVertexDescriptionComponent.VertexTypes.Keys.ElementAt(itr).ToString());
			}
			for (int itr = 0; itr < Geometry.VertexDescription.Count; itr++)
			{
				int idxrow = datagridVertexDescriptions.Rows.Add(
					Geometry.VertexDescription[itr].Index,
					Geometry.VertexDescription[itr].Name,
					Geometry.VertexDescription[itr].ElementGLType.ToString(),
					Geometry.VertexDescription[itr].ElementCount);
				DataGridViewRow row = datagridVertexDescriptions.Rows[idxrow];
				row.Tag = Geometry.VertexDescription[itr];
			}
			propsGeometry.SelectedObject = new { Vertices = Geometry.Vertices, Triangles = Geometry.Triangles };
			bolDataGridReady = true;
			timeRun = new Stopwatch();
			tsRender = new infoFramePerformance();
			glRender.HandleCreated += new EventHandler(glRender_HandleCreated);
			glRender.ClientSizeChanged += new EventHandler(glRender_Resize);
			glRender.Paint += new PaintEventHandler(glRender_Paint);
			timeRun.Start();
			glRender_Init();
		}
		private void FrmGeometry_FormClosing(object sender, FormClosingEventArgs e)
		{
			tsRender = null;
			panelMain.ProjectObject = null;
		}
		private propsSplitter containerMain_SplitterOpposite(propsSplitter objSplitter, clsCollapsePanel objPanel)
		{
			propsSplitter objOppSplitter = null;
			if (objSplitter.Splitter == splitterLeft)
			{
				objOppSplitter = objPanel.Splitters.Find(itm => (itm.Splitter == splitterRight));
			}
			if (objSplitter.Splitter == splitterRight)
			{
				objOppSplitter = objPanel.Splitters.Find(itm => (itm.Splitter == splitterLeft));
			}
			if (objSplitter.Splitter == splitterTop)
			{
				objOppSplitter = objPanel.Splitters.Find(itm => (itm.Splitter == splitterBottom));
			}
			if (objSplitter.Splitter == splitterBottom)
			{
				objOppSplitter = objPanel.Splitters.Find(itm => (itm.Splitter == splitterTop));
			}
			return objOppSplitter;
		}
		private Control containerMain_SplitterContent(propsSplitter objSplitter)
		{
			Control objContent = null;
			if (objSplitter.Splitter == splitterLeft)
			{
				objContent = groupGeometry;
			}
			if (objSplitter.Splitter == splitterRight)
			{
				objContent = groupGeometry;
			}
			if (objSplitter.Splitter == splitterTop)
			{
				objContent = groupVertexDefinition;
			}
			if (objSplitter.Splitter == splitterBottom)
			{
				objContent = groupVertexDefinition;
			}
			return objContent;
		}
		private void containerMain_CollapseDistanceChange(object sender, EventArgs e)
		{
			propsSplitter objSplitter = sender as propsSplitter;
			if (objSplitter == null) return;
			clsCollapsePanel objPanel = objSplitter.CollapsePanel;
			propsSplitter objOppSplitter = containerMain_SplitterOpposite(objSplitter, objPanel);
			if (objOppSplitter != null)
			{
				objOppSplitter.UnCollapedDist = objSplitter.UnCollapedDist;
			}
		}
		private void containerMain_CollapseChange(object sender, EventArgs e)
		{
			propsSplitter objSplitter = sender as propsSplitter;
			if (objSplitter == null) return;
			clsCollapsePanel objPanel = objSplitter.CollapsePanel;
			if(!objSplitter.IsCollapsed)
			{
				propsSplitter objOppSplitter = containerMain_SplitterOpposite(objSplitter, objPanel);
				Control objContent = containerMain_SplitterContent(objSplitter);
				if(objOppSplitter != null)
				{
					objContent.Parent = objSplitter.Panel;
					objOppSplitter.IsCollapsed = true;
				}
			}
		}
		private void glRender_Init()
		{
			glRender.Context.Update(glRender.WindowInfo);
			GL.Viewport(glRender.ClientRectangle);
			GL.ClearColor(glRender.BackColor);
			glRender.Invalidate();
		}
		private void glRender_HandleCreated(object sender, EventArgs e)
		{
			glRender_Init();
		}
		private void glRender_Resize(object sender, EventArgs e)
		{
			glRender.Context.Update(glRender.WindowInfo);
			GL.Viewport(glRender.ClientRectangle);
			glRender.Invalidate();
		}
		private void glRender_Paint(object sender, PaintEventArgs e)
		{
			glRender_Render();
		}
		private void glRender_Render()
		{
			glRender.Context.MakeCurrent(glRender.WindowInfo);
			tsRender.ResetInterval();
			tsRender.StartInterval();
			glRender.MakeCurrent();
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
			glRender.Context.SwapBuffers();
			tsRender.SampleInterval((float)timeRun.Elapsed.TotalSeconds);
			tsRender.StopInterval();
		}

		private void DatagridVertexDescriptions_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (!bolDataGridReady) return;
			bolDataGridReady = false;
			clsVertexDescriptionComponent compNew = Geometry.VertexDescription.Add(VertexAttribPointerType.Byte, "", 1, (object)0);
			DataGridViewRow rowNew = datagridVertexDescriptions.Rows[e.RowIndex-1];
			rowNew.Tag = compNew;
			rowNew.Cells["columnIndex"].Value = compNew.Index.ToString();
			rowNew.Cells["columnElementType"].Value = compNew.ElementGLType.ToString();
			rowNew.Cells["columnElementCount"].Value = compNew.ElementCount.ToString();
			propsGeometry.Refresh();
			Geometry.glUpdateBuffers();
			bolDataGridReady = true;
		}
		private void DatagridVertexDescriptions_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			List<clsVertexDescriptionComponent> aryToDelete = new List<clsVertexDescriptionComponent>();
			foreach(clsVertexDescriptionComponent vrt in Geometry.VertexDescription)
			{
				bool bolFound = false;
				for (int itr = 0; itr < datagridVertexDescriptions.Rows.Count; itr++)
				{
					DataGridViewRow row = datagridVertexDescriptions.Rows[itr];
					int vrtIdx = -1;
					string strIdx = (string)(row.Cells["columnIndex"].Value ?? " ");
					if (strIdx != null) int.TryParse(strIdx, out vrtIdx);
					if (vrt.Index == vrtIdx) { bolFound = true; break; }
				}
				if (!bolFound) aryToDelete.Add(vrt);
			}
			for (int itr = 0; itr < aryToDelete.Count; itr++) Geometry.VertexDescription.Remove(aryToDelete[itr]);
			propsGeometry.Refresh();
			Geometry.glUpdateBuffers();
		}
		private void DatagridVertexDescriptions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (!bolDataGridReady) return;
			DataGridViewRow rowData = datagridVertexDescriptions.Rows[e.RowIndex];
			clsVertexDescriptionComponent vrt = rowData.Tag as clsVertexDescriptionComponent;
			if (vrt == null) return;
			vrt.Index = int.Parse(rowData.Cells["columnIndex"].Value.ToString());
			vrt.Name = (string)rowData.Cells["columnComponentName"].Value;
			vrt.ElementGLType = (VertexAttribPointerType)Enum.Parse(typeof(VertexAttribPointerType), rowData.Cells["columnElementType"].Value.ToString());
			vrt.ElementCount = int.Parse(rowData.Cells["columnElementCount"].Value.ToString());
			for (int itrRow = 0; itrRow < datagridVertexDescriptions.Rows.Count; itrRow++)
			{
				DataGridViewRow rowItr = datagridVertexDescriptions.Rows[itrRow];
				clsVertexDescriptionComponent vrtItr = rowItr.Tag as clsVertexDescriptionComponent;
				if (vrtItr != null) rowItr.Cells["columnIndex"].Value = vrtItr.Index.ToString();
			}
			propsGeometry.Refresh();
			Geometry.glUpdateBuffers();
		}
		private void DatagridVertexDescriptions_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			Console.WriteLine($"Vertex Description: DataError {{{e.Exception}}} - Row{e.RowIndex} Column{e.ColumnIndex}");
		}

		private void PropsGeometry_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			propsGeometry.Refresh();
			Geometry.glUpdateBuffers();
		}
	}
}
