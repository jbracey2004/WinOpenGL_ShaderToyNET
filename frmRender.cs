using modProject;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static clsHPTimer;
using static generalUtils;

namespace WinOpenGL_ShaderToy
{
	public partial class frmRender : DockContent
	{
		public frmRender(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsRender Render { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsRender; } }
		public GLControl glMain;
		private clsHPTimer timerRender;
		private infoFramePerformance tsRender;
		private infoFramePerformance tsRenderTimer;
		private Stopwatch timeRun;
		private Matrix4 matxView = Matrix4.Identity;
		private void FrmGLMain_Load(object sender, EventArgs e)
		{
			/*InitConfig();
			glInit();
			glInitShaders();
			glConfigureShaders();
			timeRun.Start();
			timerRender = new clsHPTimer(this);
			timerRender.IntervalEnd += new HPIntervalEventHandler(TimerRender_Tick);
			timerRender.Interval = 1000.0 / 60.0;
			timerRender.Start();*/
		}
		private void FrmGLMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(timerRender != null)
			{
				timerRender.Stop();
				timerRender = null;
			}
		}
		private void InitConfig()
		{
			timeRun = new Stopwatch();
			tsRender = new infoFramePerformance();
			tsRenderTimer = new infoFramePerformance();
			tsRenderTimer.HistoryDuration = 1.0;
			glMain.HandleCreated += GlMain_HandleCreated;
		}
		private void GlMain_HandleCreated(object sender, EventArgs e)
		{
			glInitShaders();
			glConfigureShaders();
		}
		private void glInit()
		{
			glMain.Resize += new EventHandler(glMain_Resize);
			GL.Disable(EnableCap.DepthTest);
			GL.ClearColor(glMain.BackColor);
			GL.Viewport(0, 0, glMain.Width, glMain.Height);
		}
		private int shaderVSID = -1;
		private int shaderFSID = -1;
		private int gpuMain = -1;
		private void glInitShaders()
		{
			string strPathBase = @"..\..\..\Shaders\RayMarching\";
			string strPathVS = strPathBase + @"VertexPassThrough.vs";
			string strPathFS = strPathBase + @"Raymarching1.fs";
			shaderVSID = glShaderFromSrc(strPathVS, ShaderType.VertexShader);
			shaderFSID = glShaderFromSrc(strPathFS, ShaderType.FragmentShader);
			gpuMain = glLinkShaders(new int[] { shaderVSID, shaderFSID });
		}
		private int glShaderFromSrc(string pathSrc, ShaderType typ)
		{
			string src = System.IO.File.ReadAllText(pathSrc);
			int retID = GL.CreateShader(typ);
			GL.ShaderSource(retID, src);
			GL.CompileShader(retID);
			string strInfo = GL.GetShaderInfoLog(retID);
			Console.WriteLine("Shader Compilation\n" + ((strInfo == "") ? ("Complete") : (strInfo)));
			return retID;
		}
		private int glLinkShaders(int[] aryShaderIDs)
		{
			int retID = GL.CreateProgram();
			foreach (int shaderID in aryShaderIDs)
			{
				GL.AttachShader(retID, shaderID);
			}
			GL.LinkProgram(retID);
			string strInfo = GL.GetProgramInfoLog(retID);
			Console.WriteLine("Shaders Linking\n" + ((strInfo == "") ? ("Complete") : (strInfo)));
			foreach (int shaderID in aryShaderIDs)
			{
				GL.DetachShader(retID, shaderID);
				GL.DeleteShader(shaderID);
			}
			return retID;
		}
		private int positionAttributeLocation;
		private int uniformLocation_matxView;
		private int uniformLocation_AspectRatio;
		private int uniformLocation_timeRun;
		private void glConfigureShaders()
		{
			positionAttributeLocation = GL.GetAttribLocation(gpuMain, "scrPos");
			uniformLocation_AspectRatio = GL.GetUniformLocation(gpuMain, "asp");
			uniformLocation_timeRun = GL.GetUniformLocation(gpuMain, "timeRun");
			uniformLocation_matxView = GL.GetUniformLocation(gpuMain, "view");
			int[] positionBuffer = { 0 };
			GL.CreateBuffers(1, positionBuffer);
			GL.BindBuffer(BufferTarget.ArrayBuffer, positionBuffer[0]);
			float[] positions = {
				  -1.0f, -1.0f,
				  1.0f, -1.0f,
				  1.0f, 1.0f,
				  1.0f, 1.0f,
				  -1.0f, 1.0f,
				  -1.0f, -1.0f
			};
			GL.BufferData(BufferTarget.ArrayBuffer, positions.Length * sizeof(float), positions, BufferUsageHint.StaticDraw);
			GL.UseProgram(gpuMain);
			GL.EnableVertexAttribArray(positionAttributeLocation);
			GL.BindBuffer(BufferTarget.ArrayBuffer, positionBuffer[0]);
			GL.VertexAttribPointer(positionAttributeLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
			GL.Uniform1(uniformLocation_AspectRatio, glMain.AspectRatio);
			GL.Uniform1(uniformLocation_timeRun, 0.0f);
		}

		private void TimerRender_Tick(object sender, HPIntervalEventArgs e)
		{
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

		private void glMain_Render()
		{
			tsRender.ResetInterval();
			tsRender.StartInterval();
			glMain.MakeCurrent();
			GL.Uniform1(uniformLocation_timeRun, (float)timeRun.Elapsed.TotalSeconds);
			GL.UniformMatrix4(uniformLocation_matxView, false, ref matxView);
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
			glMain.Context.SwapBuffers();
			tsRender.SampleInterval((float)timeRun.Elapsed.TotalSeconds);
			tsRender.StopInterval();
		}
		private void glMain_Resize(object sender, EventArgs e)
		{
			glMain.Context.Update(glMain.WindowInfo);
			GL.Viewport(0, 0, glMain.Width, glMain.Height);
			GL.Uniform1(uniformLocation_AspectRatio, glMain.AspectRatio);
		}
		private void txtFPS_Change(object sender, EventArgs e)
		{
			double val = 0.0;
			if(double.TryParse(txtFPS.Text, out val))
			{
				timerRender.Interval = (1000.0 / val);
			}
		}
		private void BtnFPS_DropDownClosed(object sender, EventArgs e)
		{
			double val = 0.0;
			if (double.TryParse(txtFPS.Text, out val))
			{
				timerRender.Interval = (1000.0 / val);
				txtInterval.Text = (1000.0 / val).ToString("#.########");
			}
			else
			{
				
			}
		}
		private void txtInterval_Change(object sender, EventArgs e)
		{
			double val = 0.0;
			if (double.TryParse(txtInterval.Text, out val))
			{
				timerRender.Interval = val;
			}
		}
		private void BtnInterval_DropDownClosed(object sender, EventArgs e)
		{
			double val = 0.0;
			if (double.TryParse(txtInterval.Text, out val))
			{
				timerRender.Interval = val;
				txtFPS.Text = (1000.0 / val).ToString("#.########");
			}
			else
			{

			}
		}

		private void TxtRenderName_TextChanged(object sender, EventArgs e)
		{
			
		}
	}
}
