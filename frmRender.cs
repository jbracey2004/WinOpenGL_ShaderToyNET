using modProject;
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
using static modCommon.modWndProcInterop.clsTouchInterface;

namespace WinOpenGL_ShaderToy
{
	public partial class frmRender : DockContent
	{
		public frmRender(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsRender Render { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsRender; } }
		public controlRender glRender;
		private frmRenderConfigure ConfigureDialog;
		private clsHPTimer timerRender;
		private infoFramePerformance tsRender;
		private infoFramePerformance tsRenderTimer;
		private Stopwatch timeRun;
		public double CurrentTimeStamp { get; private set; }
		public double PreviousTimeStamp { get; private set; }
		public double DeltaTimeStamp { get => CurrentTimeStamp - PreviousTimeStamp; }
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
			timerRender?.Stop();
			timerRender = null;
			panelMain.ProjectObject = null;
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
			tsRenderTimer.HistoryDuration = 10.0;
			glRender = new controlRender();
			glRender.Parent = panelMain.Content;
			glRender.Dock = DockStyle.Fill;
			glRender.HandleCreated += glMain_HandleCreated;
			glRender.SizeChanged += glMain_Resized;
			glRender.PointerStart += glMain_PointerStart;
			glRender.PointerMove += glMain_PointerMove;
			glRender.PointerEnd += glMain_PointerEnd;
			glRender.MakeCurrent();
			UpdateGeometryRouting();
		}
		private void glMain_PointerStart(object sender, TouchEventArgs e)
		{
			Render?.RaisePointerStartEvent(e.Touch);
		}
		private void glMain_PointerMove(object sender, TouchEventArgs e)
		{
			Render?.RaisePointerMoveEvent(e.Touch);
		}
		private void glMain_PointerEnd(object sender, TouchEventArgs e)
		{
			Render?.RaisePointerEndEvent(e.Touch);
		}
		private void TimerRender_Tick(object sender, HPIntervalEventArgs e)
		{
			PreviousTimeStamp = CurrentTimeStamp;
			CurrentTimeStamp = timeRun.Elapsed.TotalSeconds;
			double tsDelta = CurrentTimeStamp - PreviousTimeStamp;
			Render?.RaiseRenderEvent(tsDelta, CurrentTimeStamp);
			glMain_Render();
			tsRenderTimer.StopInterval();
			tsRenderTimer.SampleInterval();
			btnFPS.Text = string.Format("{0,15:#,##0.00000 FPS}", tsRenderTimer.Median_Rate);
			btnInterval.Text = string.Format("{0,15:#,##0.00000 ms}", tsRenderTimer.Median * 1000.0);
			tsRenderTimer.ResetInterval();
			tsRenderTimer.StartInterval();
			lblRenderDuration.Text = string.Format("{0,15:##,##0.00000 ms}", tsRender.Median * 1000.0);
			lblRenderFreq.Text = string.Format("{0,15:##,##0.00000 Hz}", tsRender.Median_Rate);
		}
		private void glMain_Resized(object sender, EventArgs e)
		{
			Render?.RaiseResizeEvent(glRender.Width, glRender.Height);
		}
		private void glMain_HandleCreated(object sender, EventArgs e)
		{
			glRender.MakeCurrent();
			UpdateGeometryRouting();
		}
		private void glMain_Render()
		{
			glRender.Context.Update(glRender.WindowInfo);
			GL.Viewport(glRender.ClientRectangle);
			glRender.MakeCurrent();
			tsRender.ResetInterval();
			tsRender.StartInterval();
			GL.ClearColor(glRender.BackColor);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			UpdateUniformRouting();
			if (Render.Program != null)
			{
				GL.UseProgram(Render.Program.glID);
				if (Render.Geometry != null)
				{
					GL.EnableClientState(ArrayCap.IndexArray);
					GL.BindBuffer(BufferTarget.ElementArrayBuffer, Render.Geometry.glIndexBuffer);
					GL.DrawElements(PrimitiveType.Triangles, Render.Geometry.Triangles.Indices.Length, DrawElementsType.UnsignedInt, 0);
				}
			}
			glRender.Context.SwapBuffers();
			tsRender.StopInterval();
			tsRender.SampleInterval(timeRun.Elapsed.TotalSeconds);
		}
		public void UpdateGeometryRouting()
		{
			if (Render.Program != null)
			{
				glRender.MakeCurrent();
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
				glRender.MakeCurrent();
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
