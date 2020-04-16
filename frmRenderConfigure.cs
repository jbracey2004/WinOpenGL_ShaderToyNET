using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;
using modProject;
using static WinOpenGL_ShaderToy.ProjectDef;
using static WinOpenGL_ShaderToy.controlUniformData;
using static modProject.clsUniformSet;
using static clsHPTimer;
using static modProject.clsGeometry;
using modUniformDataView;
using modEventScriptView;
using static WinOpenGL_ShaderToy.controlEventScript;
using static modProject.clsEventScript;
using static WinOpenGL_ShaderToy.controlConsole;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;
using static generalUtils;
using System.Linq;

namespace WinOpenGL_ShaderToy
{
	public partial class frmRenderConfigure : DockContent
	{
		public static List<frmRenderConfigure> AllForms = new List<frmRenderConfigure>();
		public static frmRenderConfigure FormWithSubjectForm(frmRender subject) => AllForms.Find(itm => itm.RenderSubjectForm == subject);
		public static frmRenderConfigure FormWithSubjectObject(clsRender subject) => AllForms.Find(itm => itm.RenderSubject == subject);
		private clsHPTimer timerUpdateLists;
		private clsHPTimer timerUpdateUniformDataGrid;
		public frmRenderConfigure()
		{
			InitializeComponent();
			AllForms.Add(this);
		}
		private void frmRenderConfigure_Load(object sender, EventArgs e)
		{
			InitDocking();
			InitConsole();
			DataGridViewColumn column = new clsUniformDataColumn();
			column.Name = "columnVariableValue";
			column.HeaderText = "Variable Value";
			datagridUniformsValues.Columns.Add(column);
			column = new clsEventScriptColumn();
			column.Name = "columnEvent";
			column.HeaderText = "Event Scripts";
			datagridEvents.Columns.Add(column);
			UpdateLists();
			UpdateTables();
			timerUpdateLists = new clsHPTimer(this);
			timerUpdateLists.Interval = 1000.0;
			timerUpdateLists.SleepInterval = 1000;
			timerUpdateLists.IntervalEnd += new HPIntervalEventHandler(timerUpdateLists_EndInterval);
			timerUpdateLists.Start();
			timerUpdateUniformDataGrid = new clsHPTimer(this);
			timerUpdateUniformDataGrid.Interval = 50;
			timerUpdateUniformDataGrid.SleepInterval = 50;
			timerUpdateUniformDataGrid.IntervalEnd += new HPIntervalEventHandler(timerUpdateUniformDataGrid_EndInterval);
			timerUpdateUniformDataGrid.Start();
			ProjectDef.AllForms.Add(this);
		}
		private void frmRenderConfigure_FormClosing(object sender, FormClosingEventArgs e)
		{
			UnloadDocking();
			UnloadConsole();
			DockPanel = null;
			timerUpdateUniformDataGrid.Stop();
			timerUpdateUniformDataGrid.Dispose();
			timerUpdateUniformDataGrid = null;
			timerUpdateLists.Stop();
			timerUpdateLists.Dispose();
			timerUpdateLists = null;
			AllForms.Remove(this);
			ProjectDef.AllForms.Remove(this);
		}
		public controlConsole Console { get; private set; }
		private clsEventScriptContext consoleScriptContext;
		private clsAutoComplete consoleAutoComplete;
		private AutoCompleteCollection consoleItemsCollection;
		private void InitConsole()
		{
			consoleScriptContext = new clsEventScriptContext();
			consoleScriptContext.RenderSubject = RenderSubject;
			Console = new controlConsole();
			consoleAutoComplete = new clsAutoComplete(Console);
			consoleAutoComplete.SearchPattern = @"[\w\d\.\[\(\]\)\""]";
			consoleAutoComplete.AutoSize = true;
			consoleAutoComplete.MinFragmentLength = 1;
			consoleItemsCollection = new AutoCompleteCollection(consoleScriptContext, Console, consoleAutoComplete);
			consoleAutoComplete.Items.SetAutocompleteItems(consoleItemsCollection);
			Console.Parent = panelEventsConsole;
			Console.Dock = DockStyle.Fill;
			Console.StartPromptLoop(ConsolePromptReady, ConsolePromptReplied);
		}
		private void UnloadConsole()
		{
			Console.StopPromptLoop();
			Console.Dispose();
			Console = null;
			consoleScriptContext.RenderSubject = null;
			consoleScriptContext = null;
		}
		private void ConsolePromptReady(ref ConsoleActionArgs e)
		{
			e.Message = ">";
		}
		private void ConsolePromptReplied(ref ConsoleActionArgs e)
		{
			string strCode = e.Message;
			Task<object> Eval = new Task<object>(() => 
			{
				try
				{
					return MainScript.ContinueWith(strCode, GenericScriptOptions).RunAsync(consoleScriptContext).Result;
				}
				catch(Exception err)
				{
					return err;
				}
			});
			Eval.Start();
			while(!Eval.IsCompleted && !Eval.IsCanceled && !Eval.IsFaulted) { Application.DoEvents(); }
			string strDisp = "\n";
			ScriptState state = Eval.Result as ScriptState;
			if (state != null)
			{
				object Ret = state.ReturnValue;
				if (Ret != null)
				{
					strDisp = ExpandedObjectString(Ret, TypesExpandExempt, true) + '\n';
				}
				else
				{
					strDisp = Eval.Status.ToString() + '\n';
				}
			}
			else
			{
				string strErr = "";
				Exception errInner = Eval.Result as Exception;
				while (errInner != null)
				{
					strErr += errInner.Message + "; ";
					errInner = errInner.InnerException;
				}
				System.Console.WriteLine(strErr);
				strDisp = strErr + '\n';
			}
			Invoke(new Action(() => { Console.Write(strDisp); }));
		}
		private DockPanel panelDockMain;
		private Dictionary<string, DockContent> aryDockContent = new Dictionary<string, DockContent>();
		private void InitDocking()
		{
			DockAreas dockingAllowed = DockAreas.Document |
									   DockAreas.DockLeft |
									   DockAreas.DockRight |
									   DockAreas.DockTop |
									   DockAreas.DockBottom |
									   DockAreas.Float;
			panelDockMain = new DockPanel();
			panelDockMain.Theme = dockMainPanel.Theme;
			panelDockMain.Theme.Measures.DockPadding = 0;
			panelDockMain.BorderStyle = BorderStyle.None;
			panelDockMain.Padding = new Padding(0);
			panelDockMain.Margin = new Padding(0);
			panelDockMain.ShowDocumentIcon = false;
			panelDockMain.Parent = this;
			panelDockMain.Dock = DockStyle.Fill;
			panelDockMain.DocumentStyle = DocumentStyle.DockingWindow;
			panelDockMain.BringToFront();
			aryDockContent.Add("GeomRoute", AddDockContent(panelDockMain, panelGeometryRouting, dockingAllowed, "Geometry Routing"));
			aryDockContent.Add("UniDat", AddDockContent(panelDockMain.ActivePane, DockAlignment.Bottom, 0.8, panelUniformsValues, dockingAllowed, "Uniform Data"));
			aryDockContent.Add("UniRoute", AddDockContent(panelDockMain.ActivePane, DockAlignment.Bottom, 0.6, panelUniformsRouting, dockingAllowed, "Uniform Routing"));
			aryDockContent.Add("Events", AddDockContent(panelDockMain.ActivePane, DockAlignment.Bottom, 0.5, panelEvents, dockingAllowed, "Events"));
			aryDockContent.Add("Console", AddDockContent(panelDockMain.ActivePane, DockAlignment.Bottom, 0.5, panelEventsConsole, dockingAllowed, "Console"));
			aryDockContent["Console"].DockState = DockState.DockBottomAutoHide;
		}
		private static DockContent AddDockContent(DockPanel panel, Control control, DockAreas areas, string caption)
		{
			DockContent contentNew = NewDockContent(caption, areas);
			DockControlToContentPanel(control, contentNew);
			contentNew.Show(panel);
			return contentNew;
		}
		private static DockContent AddDockContent(DockPane panelPrevious, DockAlignment aliggnment, double ratio, Control control, DockAreas areas, string caption)
		{
			DockContent contentNew = NewDockContent(caption, areas);
			DockControlToContentPanel(control, contentNew);
			contentNew.Show(panelPrevious, aliggnment, ratio);
			return contentNew;
		}
		private static void DockControlToContentPanel(Control control, DockContent content)
		{
			control.Padding = new Padding(0);
			control.Margin = new Padding(0);
			control.Parent = content;
			control.Dock = DockStyle.Fill;
		}
		private static DockContent NewDockContent(string caption, DockAreas areas)
		{
			DockContent contentRet = new DockContent();
			contentRet.Text = caption;
			contentRet.FormBorderStyle = FormBorderStyle.None;
			contentRet.ControlBox = false;
			contentRet.CloseButton = false;
			contentRet.CloseButtonVisible = false;
			contentRet.ShowInTaskbar = false;
			contentRet.DockAreas = areas;
			return contentRet;
		}
		private void UnloadDocking()
		{
			foreach(var content in aryDockContent)
			{
				if(content.Value != null)
				{
					content.Value.Close();
					content.Value.Dispose();
				}
			}
			aryDockContent.Clear();
			panelDockMain.Dispose();
		}
		private clsRender renderSubject = null;
		public clsRender RenderSubject
		{
			get => renderSubject;
			set
			{
				renderSubject = value;
				UpdateRenderSubject();
			}
		}
		public frmRender RenderSubjectForm
		{
			get => (renderSubject != null && renderSubject.ParentControl != null) ? (renderSubject.ParentControl.ParentForm as frmRender) : null;
			set => RenderSubject = value.Render;
		}
		private void UpdateRenderSubject()
		{
			if(renderSubject != null)
			{
				Text = $"{renderSubject.ToString()} Configuration";
			} else
			{
				Text = "Render Configuration";
			}
			UpdateLists();
			UpdateTables();
		}
		private void timerUpdateLists_EndInterval(object sender, HPIntervalEventArgs e)
		{
			UpdateLists();
		}
		private void UpdateLists()
		{
			UpdateGeometryList();
			UpdateProgramList();
			UpdateAttributeList();
			UpdateUniformsList();
		}
		private bool bolUpdateLock  = false;
		private void UpdateGeometryList()
		{
			clsGeometry geomItm = lstGeometry.SelectedItem as clsGeometry;
			bolUpdateLock = true;
			lstGeometry.BeginUpdate();
			lstGeometry.Items.Clear();
			lstGeometry.SelectedIndex = lstGeometry.Items.Add("[None]");
			foreach(clsProjectObject objItr in projectMain.ProjectObjects)
			{
				clsGeometry geom = objItr as clsGeometry;
				if(geom != null)
				{
					lstGeometry.Items.Add(geom);
					if (geom == geomItm) lstGeometry.SelectedItem = geomItm;
				}
			}
			lstGeometry.EndUpdate();
			geomItm = lstGeometry.SelectedItem as clsGeometry;
			if (geomItm != renderSubject.Geometry)
			{
				if (renderSubject.Geometry != null && lstGeometry.Items.Contains(renderSubject.Geometry))
					lstGeometry.SelectedItem = renderSubject.Geometry;
				else
					lstGeometry.SelectedIndex = 0;
				UpdateVertexDescriptionList();
			}
			bolUpdateLock = false;
		}
		private void UpdateProgramList()
		{
			clsProgram progItm = lstProgram.SelectedItem as clsProgram;
			bolUpdateLock = true;
			lstProgram.BeginUpdate();
			lstProgram.Items.Clear();
			lstProgram.SelectedIndex = lstProgram.Items.Add("[None]");
			foreach (clsProjectObject objItr in projectMain.ProjectObjects)
			{
				clsProgram prog = objItr as clsProgram;
				if (prog != null)
				{
					lstProgram.Items.Add(prog);
					if (prog == progItm) lstProgram.SelectedItem = progItm;
				}
			}
			lstProgram.EndUpdate();
			progItm = lstProgram.SelectedItem as clsProgram;
			if (progItm != renderSubject.Program)
			{
				if (renderSubject.Program != null && lstProgram.Items.Contains(renderSubject.Program))
					lstProgram.SelectedItem = renderSubject.Program;
				else
					lstProgram.SelectedIndex = 0;
				UpdateAttributeList();
				UpdateUniformsList();
			}
			bolUpdateLock = false;
		}
		private void UpdateTables()
		{
			UpdateUniformsData();
			UpdateGeometryRouting();
			UpdateUniformRouting();
			UpdateEventScripts();
		}
		private void UpdateUniformsData()
		{
			if (RenderSubject == null) return;
			if (RenderSubject.Uniforms == null) return;
			datagridUniformsValues.Rows.Clear();
			for(int itr = 0; itr < RenderSubject.Uniforms.Count; itr++)
			{
				KeyValuePair<string, clsUniformSet> itm = RenderSubject.Uniforms[itr];
				int idxRow = datagridUniformsValues.Rows.Add(new object[] { itm.Key as string, itm.Value });
				datagridUniformsValues.Rows[idxRow].Tag = itr;
			}
			UpdateUniformVariableList();
		}
		private void UpdateGeometryRouting()
		{
			if (RenderSubject == null) return;
			if (RenderSubject.Geometry == null) return;
			datagridGeometryRouting.Rows.Clear();
			foreach (KeyValuePair<string, clsVertexDescriptionComponent> itm in RenderSubject.GeometryShaderLinks)
			{
				datagridGeometryRouting.Rows.Add(new object[] { itm.Key as string, itm.Value as clsVertexDescriptionComponent });
			}
		}
		private void UpdateUniformRouting()
		{
			if (RenderSubject == null) return;
			if (RenderSubject.Program == null) return;
			datagridUniformsRouting.Rows.Clear();
			foreach(KeyValuePair<string, string> itm in RenderSubject.UniformShaderLinks)
			{
				datagridUniformsRouting.Rows.Add(new object[] { itm.Key as string, itm.Value as string } );
			}
		}
		private void UpdateEventScripts()
		{
			if (RenderSubject == null) return;
			if (datagridEvents.ColumnCount <= 0) return;
			datagridEvents.Rows.Clear();
			foreach (clsEventScript itm in RenderSubject.EventScripts)
			{
				datagridEvents.Rows.Add(itm.ToString());
			}
		}
		private void lstGeometry_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bolUpdateLock) return;
			clsGeometry itm = lstGeometry.SelectedItem as clsGeometry;
			RenderSubject.Geometry = itm;
			UpdateVertexDescriptionList();
			RenderSubjectForm?.UpdateGeometryRouting();
		}
		private void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bolUpdateLock) return;
			clsProgram itm = lstProgram.SelectedItem as clsProgram;
			RenderSubject.Program = itm;
			if(RenderSubject.Program != null) RenderSubject.Program.Link();
			UpdateAttributeList();
			UpdateUniformsList();
			RenderSubjectForm?.UpdateGeometryRouting();
		}
		private void UpdateVertexDescriptionList()
		{
			DataGridViewComboBoxColumn column = datagridGeometryRouting.Columns["columnVertDesc"] as DataGridViewComboBoxColumn;
			column.Items.Clear();
			if (RenderSubject.Geometry == null) return;
			if (RenderSubject.Geometry.VertexDescription == null) return;
			foreach (clsVertexDescriptionComponent comp in RenderSubject.Geometry.VertexDescription) { column.Items.Add(comp); }
		}
		private void UpdateAttributeList()
		{
			DataGridViewComboBoxColumn column = datagridGeometryRouting.Columns["columnProgramAttr"] as DataGridViewComboBoxColumn;
			column.Items.Clear();
			if (RenderSubject.Program == null) return;
			column.Items.AddRange(RenderSubject.Program.Inputs);
		}
		private void UpdateUniformsList()
		{
			DataGridViewComboBoxColumn column = datagridUniformsRouting.Columns["columnProgramUniform"] as DataGridViewComboBoxColumn;
			column.Items.Clear();
			if (RenderSubject.Program == null) return;
			column.Items.AddRange(RenderSubject.Program.Uniforms);
		}

		private void datagridGeometryRouting_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			DataGridViewCell cell = datagridGeometryRouting.CurrentCell;
			DataGridViewColumn column = cell.OwningColumn;
			if(column.Name == "columnVertDesc")
			{
				if (RenderSubject.Geometry == null) return;
				if (RenderSubject.Geometry.VertexDescription == null) return;
				DataGridViewComboBoxEditingControl lst = e.Control as DataGridViewComboBoxEditingControl;
				cell.Tag = lst;
			}
			if(column.Name == "columnProgramAttr")
			{
				if (RenderSubject.Program == null) return;
				DataGridViewComboBoxEditingControl lst = e.Control as DataGridViewComboBoxEditingControl;
				cell.Tag = lst;
			}
		}
		private void datagridGeometryRouting_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			clsVertexDescriptionComponent descComp = e.Row.Cells["columnVertDesc"].Value as clsVertexDescriptionComponent;
			string strShaderVar = e.Row.Cells["columnProgramAttr"].Value as string;
			RenderSubject.GeometryShaderLinks.Add(new KeyValuePair<string, clsVertexDescriptionComponent>(strShaderVar, descComp));
			RenderSubjectForm?.UpdateGeometryRouting();
		}
		private void datagridGeometryRouting_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			RenderSubject.GeometryShaderLinks.RemoveAt(e.Row.Index);
			RenderSubjectForm?.UpdateGeometryRouting();
		}
		private void datagridGeometryRouting_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			if (e.RowIndex >= RenderSubject.GeometryShaderLinks.Count) return;
			KeyValuePair<string, clsVertexDescriptionComponent> kvp = RenderSubject.GeometryShaderLinks[e.RowIndex];
			DataGridViewCell cell = datagridGeometryRouting.CurrentCell;
			DataGridViewColumn column = cell.OwningColumn;
			if (column.Name == "columnVertDesc")
			{
				clsVertexDescriptionComponent comp = RenderSubject.Geometry.VertexDescription.FirstOrDefault(itm => itm.ToString() == cell.Value.ToString());
				RenderSubject.GeometryShaderLinks[e.RowIndex] = new KeyValuePair<string, clsVertexDescriptionComponent>(kvp.Key, comp);
			}
			if (column.Name == "columnProgramAttr")
			{
				RenderSubject.GeometryShaderLinks[e.RowIndex] = new KeyValuePair<string, clsVertexDescriptionComponent>(cell.Value as string, kvp.Value);
			}
			cell.Tag = null;
			RenderSubjectForm?.UpdateGeometryRouting();
		}
		private void datagridGeometryRouting_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			DataGridViewCell cell = datagridGeometryRouting.Rows[e.RowIndex].Cells[e.ColumnIndex];
			e.Cancel = true;
		}

		private void datagridUniformsValues_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			clsUniformSet objValue = new clsUniformSet("<Float> 0");
			datagridUniformsValues.CurrentRow.Cells["columnVariableValue"].Value = objValue.ToString();
			RenderSubject.Uniforms.Add(new KeyValuePair<string, clsUniformSet>(null, objValue));
			datagridUniformsValues.CurrentRow.Tag = RenderSubject.Uniforms.Count - 1;
			RenderSubject.LinkShaderUniforms();
			UpdateUniformVariableList();
		}
		private void datagridUniformsValues_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			if (e.Row.Index < 0) return;
			if(!int.TryParse(e.Row.Tag?.ToString(), out int intUni)) return;
			if (intUni < 0 || intUni >= RenderSubject.Uniforms.Count) return;
			RenderSubject.Uniforms.RemoveAt(intUni);
			RenderSubject.LinkShaderUniforms();
			UpdateUniformVariableList();
		}
		private void datagridUniformsValues_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (bolUpdateLock) return;
			if (e.RowIndex < 0) return;
			if (e.ColumnIndex < 0) return;
			DataGridViewRow row = datagridUniformsValues.Rows[e.RowIndex];
			DataGridViewColumn column = datagridUniformsValues.Columns[e.ColumnIndex];
			DataGridViewCell cell = datagridUniformsValues[e.ColumnIndex, e.RowIndex];
			if(!int.TryParse(row.Tag?.ToString(), out int intUni)) return;
			if (intUni < 0 || intUni >= RenderSubject.Uniforms.Count) return;
			KeyValuePair<string, clsUniformSet> oldValue = RenderSubject.Uniforms[intUni];
			if(column.Name == "columnVariableName")
			{
				RenderSubject.Uniforms[intUni] = new KeyValuePair<string, clsUniformSet>(cell.Value as string, oldValue.Value);
				RenderSubject?.LinkShaderUniforms();
				UpdateUniformVariableList();
			}
			if(column.Name == "columnVariableValue")
			{
				clsUniformDataCell cellUniformData = cell as clsUniformDataCell;
				if(cellUniformData != null)
				{
					clsUniformSet valueNew = new clsUniformSet(cellUniformData.Value.ToString());
					valueNew.Type = cellUniformData.DataUniformType;
					valueNew.SetData(cellUniformData.DataObject.ToArray());
					RenderSubject.Uniforms[intUni] = new KeyValuePair<string, clsUniformSet>(oldValue.Key, valueNew);
				}
			}
		}
		private void datagridUniformsValues_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Back)
			{
				foreach (var cellSelected in datagridUniformsValues.SelectedCells)
				{
					clsUniformDataCell cell = cellSelected as clsUniformDataCell;
					if (cell == null) continue;
					if (cell.OwningColumn.Name == "columnVariableValue")
					{
						if(!int.TryParse(cell.OwningRow.Tag?.ToString(), out int intUni)) continue;
						if (intUni < 0 || intUni >= RenderSubject.Uniforms.Count) continue;
						clsUniformSet data = RenderSubject.Uniforms[intUni].Value;
						object[] dataReset = UniformType_InitialValues[data.Type];
						for (int itr = 0; itr < data.ElementCount; itr++)
						{
							for (int itrElem = 0; itrElem < Math.Min(dataReset.Length, data.ComponentPerElement); itrElem++)
							{
								Type typ = data.GetData(itr)[itrElem].GetType();
								data.SetData(itr, itrElem, Convert.ChangeType(dataReset[itrElem], typ));
							}
						}
						cell.Value = data;
					}
				}
			}
		}
		private void UpdateUniformVariableList()
		{
			DataGridViewComboBoxColumn lst = datagridUniformsRouting.Columns["columnVarName"] as DataGridViewComboBoxColumn;
			lst.Items.Clear();
			foreach (KeyValuePair<string, clsUniformSet> itm in RenderSubject.Uniforms)
			{
				if (itm.Key != null)
				{
					lst.Items.Add(itm.Key);
				}
			}
		}
		private void datagridUniformsRouting_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			DataGridViewCell cell = datagridUniformsRouting.CurrentCell;
			DataGridViewColumn column = cell.OwningColumn;
			if (column.Name == "columnProgramUniform")
			{
				if (RenderSubject.Program == null) return;
				DataGridViewComboBoxEditingControl lst = e.Control as DataGridViewComboBoxEditingControl;
				cell.Tag = lst;
			}
			if (column.Name == "columnVarName")
			{
				if (RenderSubject.Program == null) return;
				DataGridViewComboBoxEditingControl lst = e.Control as DataGridViewComboBoxEditingControl;
				cell.Tag = lst;
			}
		}
		private void datagridUniformsRouting_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			string strUniform = e.Row.Cells["columnProgramUniform"].Value as string;
			string strVarName = e.Row.Cells["columnVarName"].Value as string;
			RenderSubject.UniformShaderLinks.Add(new KeyValuePair<string, string>(strUniform, strVarName));
			RenderSubject.LinkShaderUniforms();
			RenderSubjectForm?.UpdateGeometryRouting();
		}
		private void datagridUniformsRouting_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			RenderSubject.UniformShaderLinks.RemoveAt(e.Row.Index);
			RenderSubject.LinkShaderUniforms();
		}
		private void datagridUniformsRouting_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			DataGridViewRow row = datagridUniformsRouting.Rows[e.RowIndex];
			DataGridViewColumn column = datagridUniformsRouting.Columns[e.ColumnIndex];
			DataGridViewCell cell = row.Cells[column.Index];
			KeyValuePair<string, string> oldValue = RenderSubject.UniformShaderLinks[row.Index];
			if (column.Name == "columnProgramUniform")
			{
				DataGridViewComboBoxEditingControl lst = cell.Tag as DataGridViewComboBoxEditingControl;
				if (lst == null) return;
				string obj = lst.SelectedItem as string;
				RenderSubject.UniformShaderLinks[row.Index] = new KeyValuePair<string, string>(obj, oldValue.Value);
			}
			if (column.Name == "columnVarName")
			{
				DataGridViewComboBoxEditingControl txt = cell.Tag as DataGridViewComboBoxEditingControl;
				if (txt == null) return;
				string obj = txt.Text as string;
				RenderSubject.UniformShaderLinks[row.Index] = new KeyValuePair<string, string>(oldValue.Key, obj);
			}
			cell.Tag = null;
			RenderSubjectForm.UpdateGeometryRouting();
			RenderSubject.LinkShaderUniforms();
		}
		private void datagridUniformsRouting_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.Cancel = true;
		}
		private void datagridEvents_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			clsEventScriptCell cell = datagridEvents.CurrentCell as clsEventScriptCell;
			clsEventScriptEditor cellEdit = datagridEvents.EditingControl as clsEventScriptEditor;
			clsEventScript scriptNew = cell.Tag as clsEventScript;
			if (cellEdit.Source == "")
			{
				datagridEvents.Rows.RemoveAt(cell.RowIndex);
				scriptNew.Dispose();
				return;
			}
			clsEventScript.EventScript_FromString(cellEdit.Text, ref scriptNew);
			scriptNew.Compile();
			RenderSubject.EventScripts.Add(scriptNew);
		}
		private void datagridEvents_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			clsEventScript scriptTmp = RenderSubject.EventScripts[e.Row.Index];
			scriptTmp.Dispose();
			RenderSubject.EventScripts.RemoveAt(e.Row.Index);
		}
		private void datagridEvents_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= RenderSubject.EventScripts.Count) return;
			clsEventScriptCell cell = datagridEvents[e.ColumnIndex, e.RowIndex] as clsEventScriptCell;
			clsEventScript scriptTmp = RenderSubject.EventScripts[e.RowIndex];
			clsEventScript.EventScript_FromString(cell.Value as string, ref scriptTmp);
			scriptTmp.Compile();
		}
		private void datagridEvents_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			clsEventScriptCell cell = datagridEvents.CurrentCell as clsEventScriptCell;
			clsEventScriptEditor cellEdit = e.Control as clsEventScriptEditor;
			if (cell == null) return;
			if (cellEdit == null) return;
			clsEventScript scriptNew = new clsEventScript();
			scriptNew.Subject = RenderSubject;
			cellEdit.ScriptContext = scriptNew.ScriptContext;
			cell.Tag = scriptNew;
		}
		private bool bolUpdateDataGridReady = false;
		private void timerUpdateUniformDataGrid_EndInterval(object sender, HPIntervalEventArgs e)
		{
			if (!Visible) return;
			if (!bolUpdateDataGridReady) return;
			Invoke(new Action(() =>
			{
				bolUpdateLock = true;
				for (int idxRow = 0; idxRow < datagridUniformsValues.Rows.Count; idxRow++)
				{
					DataGridViewTextBoxCell cellName = datagridUniformsValues["columnVariableName", idxRow] as DataGridViewTextBoxCell;
					if (cellName == null) continue;
					string strName = cellName.Value as string;
					if (strName == null) continue;
					int idxUni = RenderSubject.Uniforms.FindIndex(itm => itm.Key == strName);
					if (idxUni < 0) continue;
					clsUniformDataCell cellValue = datagridUniformsValues["columnVariableValue", idxRow] as clsUniformDataCell;
					if (cellValue == null) continue;
					clsUniformSet objData = RenderSubject.Uniforms[idxUni].Value;
					if (objData == null) continue;
					cellValue.Value = objData;
					datagridUniformsValues.InvalidateCell(cellValue);
				}
				bolUpdateLock = false;
			}));
			bolUpdateDataGridReady = false;
		}
		public void UpdateDataGrid()
		{
			bolUpdateDataGridReady = true;
		}
	}
}
