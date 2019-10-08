using modProject;
using System;
using System.Linq;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing;
using static clsHPTimer;
using static WinOpenGL_ShaderToy.ProjectDef;

namespace WinOpenGL_ShaderToy
{
	public partial class frmShader : DockContent
	{
		public frmShader(clsProjectObject refObj) {InitializeComponent();  panelMain.ProjectObject = refObj; }
		public clsShader Shader { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsShader; } }
		private readonly static string strPathBase = @"..\..\..\Shaders\RayMarching\";
		private clsHPTimer timerAutoCompile;
		private void FrmShader_Load(object sender, EventArgs e)
		{
			dialogOpenFile.InitialDirectory = System.IO.Path.GetFullPath(strPathBase);
			dialogOpenFile.InitialDirectory = System.IO.Path.GetFullPath(strPathBase);
			lstShaderType.Items.AddRange(Array.FindAll(Enum.GetNames(typeof(ShaderType)), itm => !itm.EndsWith("Arb")));
			lstShaderType.Text = Regex.Replace(Shader.Type.ToString(), @"Arb\z", "");
			Compile();
			timerAutoCompile = new clsHPTimer(this);
			timerAutoCompile.Interval = 1000.0;
			timerAutoCompile.SleepInterval = 500;
			timerAutoCompile.IntervalEnd += new HPIntervalEventHandler(timerAutoCompile_EndInterval);
			timerAutoCompile.Start();
		}
		private void FrmShader_FormClosing(object sender, FormClosingEventArgs e)
		{
			timerAutoCompile.Stop();
			timerAutoCompile = null;
			panelMain.ProjectObject = null;
		}
		private void BtnLoad_Click(object sender, EventArgs e)
		{
			if (dialogOpenFile.ShowDialog() == DialogResult.OK)
			{
				if(Shader != null)
				{
					Shader.LoadSourceFromFile(dialogOpenFile.FileName);
					txtSource.Text = Shader.Source;
				}
			}
		}
		private void BtnSave_Click(object sender, EventArgs e)
		{
			if (dialogSaveFile.ShowDialog() == DialogResult.OK)
			{
				if(Shader != null)
				{
					Shader.Source = txtSource.Text;
					Shader.SaveSourceToFile(dialogOpenFile.FileName);
				}
			}
		}
		private void TxtSource_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
		{
			if(Shader != null)
			{
				Shader.Source = txtSource.Text;
				if(chkAutoCompile.Checked)
				{
					timerAutoCompile.Stop();
					timerAutoCompile.Reset();
					timerAutoCompile.Start();
				}
			}
		}
		private void LstShaderType_TextChanged(object sender, EventArgs e)
		{
			if (Shader != null)
			{
				Shader.Type = (ShaderType)Enum.Parse(typeof(ShaderType), Regex.Replace(lstShaderType.Text, @"Arb\z", ""));
				Shader.Name = clsProjectObject.NextFreeName(Shader.ToFullString(""), Shader.Name, Shader);
				Compile();
			}
		}
		private void ChkAutoCompile_CheckedChanged(object sender, EventArgs e)
		{
			btnCompile.Enabled = !chkAutoCompile.Checked;
		}
		private void BtnCompile_Click(object sender, EventArgs e)
		{
			Compile();
		}
		private void timerAutoCompile_EndInterval(object sender, HPIntervalEventArgs e)
		{
			timerAutoCompile.Stop();
			Compile();
		}
		private void Compile()
		{
			if(Shader != null)
			{
				glContext_Main.MakeCurrent(infoWindow);
				lblCompileStatus.ForeColor = Color.Blue;
				lblCompileStatus.Text = "Compile Status: Compiling...";
				Shader.Compile();
				UpdateStatus();
			}
		}
		private void UpdateStatus()
		{
			if(Shader != null)
			{
				if (Shader.CompileInfo.ErrorMessages.Length > 0)
				{
					lblCompileStatus.ForeColor = Color.DarkRed;
					lblCompileStatus.Text = "Compile Status: Compile Failed.";
				}
				else if (Shader.CompileInfo.WarningMessages.Length > 0)
				{
					lblCompileStatus.ForeColor = Color.Blue;
					lblCompileStatus.Text = "Compile Status: Compiled* See Messages.";
				}
				else if (Shader.CompileInfo.AllMessages.Length > 0)
				{
					lblCompileStatus.ForeColor = Color.Blue;
					lblCompileStatus.Text = "Link Status: Linked* See Messages.";
				}
				else
				{
					lblCompileStatus.ForeColor = Color.Green;
					lblCompileStatus.Text = "Compile Status: Compiled. Good.";
				}
				chkCompileErrors.Text = $"Errors: {Shader.CompileInfo.ErrorMessages.Length}";
				chkCompileWarnings.Text = $"Warnings: {Shader.CompileInfo.WarningMessages.Length}";
				dataCompileStatus.DataSource = Array.FindAll(Shader.CompileInfo.AllMessages, itm => 
				{
					if (itm.Level == "ERROR" && !chkCompileErrors.Checked) return false;
					if (itm.Level == "WARNING" && !chkCompileWarnings.Checked) return false;
					return true;
				});
			}
		}
		private void ChkCompileErrors_CheckedChanged(object sender, EventArgs e)
		{
			UpdateStatus();
		}
		private void ChkCompileWarnings_CheckedChanged(object sender, EventArgs e)
		{
			UpdateStatus();
		}
	}
}
