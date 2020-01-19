using modProject;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static WinOpenGL_ShaderToy.ProjectDef;
using static clsHPTimer;
using static generalUtils;
using System.Collections.Generic;
using static modProject.clsGeometry;

namespace WinOpenGL_ShaderToy
{
	public partial class frmRender : DockContent
	{
		public frmRender(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsRender Render { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsRender; } }
		public GLControl glMain;
		private frmRenderConfigure ConfigureDialog;
		private clsHPTimer timerRender;
		private infoFramePerformance tsRender;
		private infoFramePerformance tsRenderTimer;
		private Stopwatch timeRun;
		private double tsPrevious;
		private void FrmGLMain_Load(object sender, EventArgs e)
		{
			InitDialog();
			InitConfig();
			timeRun.Start();
			timerRender = new clsHPTimer(this);
			timerRender.IntervalEnd += new HPIntervalEventHandler(TimerRender_Tick);
			txtInterval.Text = ((Render!=null)?(Render.RenderInterval):(1000.0/60.0)).ToString("#.########");
			timerRender.Start();
			if(Render != null) { Render.RaiseLoadEvent(); }
		}
		private void FrmGLMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(timerRender != null)
			{
				timerRender.Stop();
				timerRender = null;
				panelMain.ProjectObject = null;
			}
		}
		private void InitDialog()
		{
			ConfigureDialog = frmRenderConfigure.FormWithSubjectObject(Render);
			if(ConfigureDialog == null)
			{
				ConfigureDialog = new frmRenderConfigure();
				ConfigureDialog.RenderSubject = Render;
				ConfigureDialog.HideOnClose = true;
			}
		}
		private void InitConfig()
		{
			timeRun = new Stopwatch();
			tsRender = new infoFramePerformance();
			tsRenderTimer = new infoFramePerformance();
			tsRenderTimer.HistoryDuration = 1.0;
			glMain = new GLControl();
			glMain.Parent = panelMain.Content;
			glMain.Dock = DockStyle.Fill;
			glMain.HandleCreated += glMain_HandleCreated;
			glMain.SizeChanged += glMain_Resized;
			glMain.MakeCurrent();
			UpdateGeometryRouting();
		}
		private void TimerRender_Tick(object sender, HPIntervalEventArgs e)
		{
			double tsCurrent = timeRun.Elapsed.TotalSeconds;
			double tsDelta = tsCurrent - tsPrevious;
			Render.RaiseRenderEvent(tsDelta, tsCurrent);
			tsPrevious = tsCurrent;
			glMain_Render();
			tsRenderTimer.StopInterval();
			tsRenderTimer.SampleInterval();
			btnFPS.Text = string.Format("{0,15:#,##0.00000 FPS}", tsRenderTimer.Median_Rate);
			btnInterval.Text = string.Format("{0,15:#,##0.00000 ms}", tsRenderTimer.Median*1000.0);
			tsRenderTimer.ResetInterval();
			tsRenderTimer.StartInterval();
			lblRenderDuration.Text = string.Format("{0,15:##,##0.00000 ms}", tsRender.Median*1000.0);
			lblRenderFreq.Text = string.Format("{0,15:##,##0.00000 Hz}", tsRender.Median_Rate);
		}
		private void glMain_Resized(object sender, EventArgs e)
		{
			Render.RaiseResizeEvent(glMain.Width, glMain.Height);
		}
		private void glMain_HandleCreated(object sender, EventArgs e)
		{
			glMain.MakeCurrent();
			UpdateGeometryRouting();
		}
		private void glMain_Render()
		{
			glMain.Context.Update(glMain.WindowInfo);
			GL.Viewport(glMain.ClientRectangle);
			glMain.MakeCurrent();
			tsRender.ResetInterval();
			tsRender.StartInterval();
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			UpdateUniformRouting();
			if (Render.Program != null)
			{
				GL.UseProgram(Render.Program.glID);
				if(Render.Geometry != null)
				{
					GL.EnableClientState(ArrayCap.IndexArray);
					GL.BindBuffer(BufferTarget.ElementArrayBuffer, Render.Geometry.glIndexBuffer);
					GL.DrawElements(PrimitiveType.Triangles, Render.Geometry.Triangles.Indices.Length, DrawElementsType.UnsignedInt, 0);
				}
			}
			glMain.Context.SwapBuffers();
			tsRender.StopInterval();
			tsRender.SampleInterval((float)timeRun.Elapsed.TotalSeconds);
		}
		public void UpdateGeometryRouting()
		{
			if (Render.Program != null)
			{
				glMain.MakeCurrent();
				GL.UseProgram(Render.Program.glID);
				if (Render.Geometry != null)
				{
					foreach (KeyValuePair<string, clsVertexDescriptionComponent> itrItm in Render.GeometryShaderLinks)
					{
						string strAttr = itrItm.Key;
						clsVertexDescriptionComponent comp = itrItm.Value as clsVertexDescriptionComponent;
						if (strAttr == null) continue;
						if (comp == null) continue;
						int intUniformLocation = GL.GetAttribLocation(Render.Program.glID, strAttr);
						GL.EnableVertexAttribArray(intUniformLocation);
						GL.BindBuffer(BufferTarget.ArrayBuffer, Render.Geometry.glBuffers[comp.Index]);
						GL.VertexAttribPointer(intUniformLocation, comp.ElementCount, comp.ElementGLType, false, 0, 0);
					}
				}
			}
		}
		public void UpdateUniformRouting()
		{
			if(Render.Program != null)
			{
				glMain.MakeCurrent();
				GL.UseProgram(Render.Program.glID);
				foreach (KeyValuePair<string, clsUniformSet> itm in Render.Uniforms)
				{
					if (itm.Key != null)
					{
						foreach (KeyValuePair<string, string> uni in Render.UniformShaderLinks.FindAll(x => x.Value == itm.Key))
						{
							int glLoc = GL.GetUniformLocation(Render.Program.glID, uni.Key);
							if(glLoc >= 0)
							{
								clsUniformSet.UniformBindDelegate[itm.Value.Type](glLoc, itm.Value.Data.Count, itm.Value.DataInlined);
							}
						}
					}
				}
			}
		}
		private void txtFPS_Change(object sender, EventArgs e)
		{
			double val = 0.0;
			if(double.TryParse(txtFPS.Text, out val))
			{
				timerRender.Interval = (1000.0 / val);
				txtInterval.Text = (1000.0 / val).ToString("#.########");
			}
		}
		private void BtnFPS_DropDownClosed(object sender, EventArgs e)
		{
			double val = 0.0;
			if (!double.TryParse(txtFPS.Text, out val))
			{
				txtFPS.Text = (1000.0/timerRender.Interval).ToString("#.########");
			}
		}
		private void txtInterval_Change(object sender, EventArgs e)
		{
			double val = 0.0;
			if (double.TryParse(txtInterval.Text, out val))
			{
				if (Render != null) Render.RenderInterval = val;
				timerRender.Interval = val;
				txtFPS.Text = (1000.0 / val).ToString("#.########");
			}
		}
		private void BtnInterval_DropDownClosed(object sender, EventArgs e)
		{
			double val = 0.0;
			if (!double.TryParse(txtInterval.Text, out val))
			{
				txtInterval.Text = timerRender.Interval.ToString("#.########");
			}
		}
		private void btnConfigure_Click(object sender, EventArgs e)
		{
			ConfigureDialog.Show(dockMainPanel, this.DockState);
		}
	}
}
