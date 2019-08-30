using modProject;
using System;
using WeifenLuo.WinFormsUI.Docking;
using static modProject.clsProjectObject;
using static WinOpenGL_ShaderToy.ProjectDef;
using static clsHPTimer;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WinOpenGL_ShaderToy
{
	public partial class frmProgram : DockContent
	{
		public frmProgram(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsProgram Program { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsProgram; } }
		private clsHPTimer timerUpdateShaderList;
		private clsHPTimer timerAutoLink;
		private void FrmProgram_Load(object sender, EventArgs e)
		{
			lstLink.AllowDrop = true;
			UpdateShaderList();
			LinkShaders();
			timerUpdateShaderList = new clsHPTimer(this);
			timerUpdateShaderList.Interval = 1000.0;
			timerUpdateShaderList.SleepInterval = 500;
			timerUpdateShaderList.IntervalEnd += new HPIntervalEventHandler(timerUpdateShaderList_EndInterval);
			timerUpdateShaderList.Start();
			timerAutoLink = new clsHPTimer(this);
			timerAutoLink.Interval = 4000.0;
			timerAutoLink.SleepInterval = 500;
			timerAutoLink.IntervalEnd += new HPIntervalEventHandler(timerAutoLink_EndInterval);
			timerAutoLink.Start();			
		}
		private void FrmProgram_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			timerAutoLink.Stop();
			timerAutoLink = null;
			timerUpdateShaderList.Stop();
			timerUpdateShaderList = null;
			panelMain.ProjectObject = null;
		}
		private void timerUpdateShaderList_EndInterval(object sender, HPIntervalEventArgs e)
		{
			UpdateShaderList();
		}
		private void timerAutoLink_EndInterval(object sender, HPIntervalEventArgs e)
		{
			if(chkAutoLink.Checked)
			{
				LinkShaders();
			}
		}
		private void UpdateShaderList()
		{
			if(Program != null)
			{
				Object[] aryTmp = new Object[lstShaders.Items.Count];
				lstShaders.Items.CopyTo(aryTmp, 0);
				foreach(clsProjectObject itm in aryTmp)
				{
					if(!projectMain.ProjectObjects.Contains(itm))
					{
						lstShaders.Items.Remove(itm);
					}
					if(Program.Shaders.Contains((clsShader)itm))
					{
						lstShaders.Items.Remove(itm);
					}
				}
				aryTmp = null;
				lstShaders.Items.AddRange(projectMain.ProjectObjects.FindAll(itm =>
					{
						if (lstShaders.Items.Contains(itm))
						{
							int idx = lstShaders.Items.IndexOf(itm);
							lstShaders.Items[idx] = itm;
							return false;
						}
						if (itm.ProjectObjType != ProjectObjectTypes.Shader) { return false; }
						if (Program.Shaders.Contains((clsShader)itm)) { return false; }
						return true;
					}).ToArray()
				);
				lstLink.Width = (int)lstLink_TotalLength(Program.Shaders.Count);
				lstLink.Invalidate();
			}
		}
		private void LinkShaders()
		{
			if (Program != null)
			{
				lblLinkStatus.ForeColor = Color.Blue;
				lblLinkStatus.Text = "Link Status: Linking...";
				Program.Link();
				UpdateStatus();
			}
		}
		private void ChkAutoLink_CheckedChanged(object sender, EventArgs e)
		{
			btnLink.Enabled = !chkAutoLink.Checked;
		}
		private void BtnLink_Click(object sender, EventArgs e)
		{
			LinkShaders();
		}
		private void UpdateStatus()
		{
			if (Program != null)
			{
				if (Program.LinkInfo.ErrorMessages.Length > 0)
				{
					lblLinkStatus.ForeColor = Color.DarkRed;
					lblLinkStatus.Text = "Link Status: Link Failed.";
				}
				else if (Program.LinkInfo.WarningMessages.Length > 0)
				{
					lblLinkStatus.ForeColor = Color.Blue;
					lblLinkStatus.Text = "Link Status: Linked* See Messages.";
				} else if (Program.LinkInfo.AllMessages.Length > 0)
				{
					lblLinkStatus.ForeColor = Color.Blue;
					lblLinkStatus.Text = "Link Status: Linked* See Messages.";
				}
				else
				{
					lblLinkStatus.ForeColor = Color.Green;
					lblLinkStatus.Text = "Link Status: Linked. Good.";
				}
				chkLinkErrors.Text = $"Errors: {Program.LinkInfo.ErrorMessages.Length}";
				chkLinkWarnings.Text = $"Warnings: {Program.LinkInfo.WarningMessages.Length}";
				dataLinkStatus.DataSource = Array.FindAll(Program.LinkInfo.AllMessages, itm =>
				{
					if (itm.Level == "ERROR" && !chkLinkErrors.Checked) return false;
					if (itm.Level == "WARNING" && !chkLinkWarnings.Checked) return false;
					return true;
				});
			}
		}
		private void ChkLinkErrors_CheckedChanged(object sender, EventArgs e)
		{
			UpdateStatus();
		}
		private void ChkLinkWarnings_CheckedChanged(object sender, EventArgs e)
		{
			UpdateStatus();
		}

		private void LstLink_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(clsShader)))
			{
				e.Effect = DragDropEffects.Move | DragDropEffects.Link;
			}
			if (e.Data.GetDataPresent(typeof(int)))
			{
				e.Effect = DragDropEffects.Move;
			}
		}
		private void LstLink_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (Program != null)
			{
				Point pt = lstLink.PointToClient(new Point(e.X, e.Y));
				int idx = lstLink_ItemIndexFromPoint(pt);
				if(e.Data.GetDataPresent(typeof(clsShader)))
				{
					clsShader obj = e.Data.GetData(typeof(clsShader)) as clsShader;
					if (idx >=0 && idx <= Program.Shaders.Count-1)
					{
						Program.Shaders.Insert(idx, obj);
					} else
					{
						Program.Shaders.Add(obj);
					}
				}
				if (e.Data.GetDataPresent(typeof(int)))
				{
					int srcidx = (int)e.Data.GetData(typeof(int));
					clsShader obj = Program.Shaders[srcidx];
					Program.Shaders.RemoveAt(srcidx);
					if (idx >= 0 && idx <= Program.Shaders.Count - 1)
					{
						Program.Shaders.Insert(idx, obj);
					}
					else
					{
						Program.Shaders.Add(obj);
					}
				}
				UpdateShaderList();
			}
		}
		private int lstLink_MouseOverIdx = -1;
		private float lstLink_GridWidth = 240f;
		private float lstLink_GridHeight = 125f;
		private float lstLink_ItemGap = 100f;
		private float lstLink_TotalLength(int ItemCount) { return Math.Max((lstLink_GridWidth * ItemCount) - 30, 200f) ; }
		private RectangleF lstLink_GridRect(int Idx)
		{
			RectangleF rectRet = new RectangleF();
			rectRet.X = Idx * lstLink_GridWidth;
			rectRet.Y = 0;
			rectRet.Width = lstLink_GridWidth;
			rectRet.Height = lstLink_GridHeight;
			return rectRet;
		}
		private RectangleF lstLink_ItemRect(int Idx)
		{
			RectangleF rectRet = lstLink_GridRect(Idx);
			rectRet.X += 10;
			rectRet.Y += 10;
			rectRet.Height -= 20;
			rectRet.Width -= lstLink_ItemGap;
			return rectRet;
		}
		private int lstLink_ItemIndexFromPoint(PointF point)
		{
			int intRet = (int)(point.X / lstLink_GridWidth);
			RectangleF rect = lstLink_ItemRect(intRet);
			if (point.X >= rect.Right) intRet++;
			return intRet;
		}
		private void LstLink_Paint(object sender, PaintEventArgs e)
		{
			if(Program != null)
			{
				for (int idx = 0; idx < Program.Shaders.Count; idx++)
				{
					RectangleF rectGrid = lstLink_GridRect(idx);
					RectangleF rectItem = lstLink_ItemRect(idx);
					if( idx < Program.Shaders.Count - 1)
					{
						RectangleF rectNextGrid = lstLink_GridRect(idx + 1);
						RectangleF rectNextItem = lstLink_ItemRect(idx + 1);
						RectangleF rectArrow = new RectangleF();
						rectArrow.X = rectItem.Right + 5;
						rectArrow.Height = rectItem.Height * 0.25f;
						rectArrow.Y = 0.5f*(rectItem.Top + rectItem.Bottom) - 0.5f*rectArrow.Height;
						rectArrow.Width = rectNextItem.Left - rectItem.Right - 10;
						e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), rectArrow.X, rectArrow.Y, rectArrow.Width - rectArrow.Height, rectArrow.Height);
						e.Graphics.FillPolygon(new SolidBrush(Color.DarkGray), new PointF[] 
						{
							new PointF(rectArrow.Right - rectArrow.Height*1.5f, rectArrow.Top-rectArrow.Height*0.5f),
							new PointF(rectArrow.Right - rectArrow.Height*1.5f, rectArrow.Bottom+rectArrow.Height*0.5f),
							new PointF(rectArrow.Right, (rectArrow.Top + rectArrow.Bottom)*0.5f)
						});
					}
					//e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray), 2), rectGrid.X, rectGrid.Y, rectGrid.Width, rectGrid.Height);
					e.Graphics.DrawRectangle(new Pen(new SolidBrush(ForeColor), 4), rectItem.X, rectItem.Y, rectItem.Width, rectItem.Height);
					RectangleF rectTextType = rectItem;
					rectTextType.Y += 5;
					rectTextType.Height = 20;
					RectangleF rectTextName = rectTextType;
					rectTextName.Y += rectTextType.Height + 4;
					e.Graphics.DrawString(Regex.Replace(Program.Shaders[idx].Type.ToString(),@"Arb\z",""), new Font(Font.FontFamily,12), new SolidBrush(ForeColor), rectTextType);
					e.Graphics.DrawString(Program.Shaders[idx].Name, new Font(Font.FontFamily, 9), new SolidBrush(ForeColor), rectTextName);
					if (idx == lstLink_MouseOverIdx)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(64, SystemColors.Highlight)), rectItem.X, rectItem.Y, rectItem.Width, rectItem.Height);
					}
				}
			}
		}
		private void LstLink_MouseMove(object sender, MouseEventArgs e)
		{
			lstLink_MouseOverIdx = -1;
			if (Program != null)
			{
				int idx = lstLink_ItemIndexFromPoint(e.Location);
				if (idx >= 0 && idx <= Program.Shaders.Count - 1)
				{
					RectangleF rect = lstLink_ItemRect(idx);
					if(rect.Contains(e.Location))
					{
						lstLink_MouseOverIdx = idx;
					}
				}
			}
			lstLink.Invalidate();
		}
		private void LstLink_MouseLeave(object sender, EventArgs e)
		{
			lstLink_MouseOverIdx = -1;
			lstLink.Invalidate();
		}
		private void LstShaders_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			clsShader obj = lstShaders.SelectedItem as clsShader;
			if(obj != null)
			{
				lstShaders.DoDragDrop(obj, DragDropEffects.Move | DragDropEffects.Link);
			}
		}
		private void MenuShader_Detach_Click(object sender, EventArgs e)
		{
			if (Program != null)
			{
				int idx = (int)menuShader_Detach.Tag;
				if (idx >= 0 && idx <= Program.Shaders.Count - 1)
				{
					Program.Shaders.RemoveAt(idx);
				}
			}
			menuShader_Detach.Tag = null;
		}

		private void LstLink_MouseDown(object sender, MouseEventArgs e)
		{
			if (Program != null)
			{
				if (lstLink_MouseOverIdx >= 0 && lstLink_MouseOverIdx <= Program.Shaders.Count-1)
				{
					if (e.Button == MouseButtons.Left)
					{
						lstLink.DoDragDrop(lstLink_MouseOverIdx, DragDropEffects.Move);
					}
					if (e.Button == MouseButtons.Right)
					{
						menuShader_Detach.Tag = lstLink_MouseOverIdx;
					}
				}
			}
		}
		private void LstShaders_DragEnter(object sender, DragEventArgs e)
		{
			if(Program != null)
			{
				if (e.Data.GetDataPresent(typeof(int)))
				{
					e.Effect = DragDropEffects.Move;
				}
			}
		}
		private void LstShaders_DragDrop(object sender, DragEventArgs e)
		{
			if (Program != null)
			{
				Point pt = lstShaders.PointToClient(new Point(e.X, e.Y));
				int idx = lstShaders.IndexFromPoint(pt);
				if (e.Data.GetDataPresent(typeof(int)))
				{
					int srcidx = (int)e.Data.GetData(typeof(int));
					clsShader obj = Program.Shaders[srcidx];
					Program.Shaders.RemoveAt(srcidx);	
				}
				UpdateShaderList();
			}
		}
	}
}
