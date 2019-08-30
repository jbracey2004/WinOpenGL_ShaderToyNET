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
namespace WinOpenGL_ShaderToy
{
	public partial class frmGeometry : DockContent
	{
		public frmGeometry(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsGeometry Geometry { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsGeometry; } }
		private clsCollapsePanel containerMain;
		private Stopwatch timeRun;
		private infoFramePerformance tsRender;
		private void FrmGeometry_Load(object sender, EventArgs e)
		{
			containerMain = new clsCollapsePanel(panelCollapse);
			containerMain.CollapseStateChanged += new CollapseStateChangeHandler(containerMain_CollapseChange);
			containerMain.CollapseDistanceChanged += new CollapseStateChangeHandler(containerMain_CollapseDistanceChange);
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
	}
}
