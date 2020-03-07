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

namespace WinOpenGL_ShaderToy
{
	public partial class frmRenderConfigure : DockContent
	{
		public static List<frmRenderConfigure> AllForms = new List<frmRenderConfigure>();
		public static frmRenderConfigure FormWithSubjectForm(frmRender subject) => AllForms.Find(itm => itm.RenderSubjectForm == subject);
		public static frmRenderConfigure FormWithSubjectObject(clsRender subject) => AllForms.Find(itm => itm.RenderSubject == subject);
		private clsHPTimer timerUpdateLists;
		public frmRenderConfigure()
		{
			InitializeComponent();
			AllForms.Add(this);
		}
		private void frmRenderConfigure_Load(object sender, EventArgs e)
		{
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
			timerUpdateLists.SleepInterval = 500;
			timerUpdateLists.IntervalEnd += new HPIntervalEventHandler(timerUpdateLists_EndInterval);
			timerUpdateLists.Start();
		}
		private void frmSRenderConfigure_FormClosing(object sender, FormClosingEventArgs e)
		{
			timerUpdateLists.Stop();
			timerUpdateLists.Dispose();
			timerUpdateLists = null;
			AllForms.Remove(this);
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
				if (lstGeometry.Items.Contains(renderSubject.Geometry))
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
				if (lstProgram.Items.Contains(renderSubject.Program))
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
			foreach(KeyValuePair<string, clsUniformSet> itm in RenderSubject.Uniforms)
			{
				datagridUniformsValues.Rows.Add(new object[] { itm.Key as string, itm.Value });
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
			RenderSubjectForm.UpdateGeometryRouting();
		}
		private void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bolUpdateLock) return;
			clsProgram itm = lstProgram.SelectedItem as clsProgram;
			RenderSubject.Program = itm;
			if(RenderSubject.Program != null) RenderSubject.Program.Link();
			UpdateAttributeList();
			UpdateUniformsList();
			RenderSubjectForm.UpdateGeometryRouting();
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
			RenderSubjectForm.UpdateGeometryRouting();
		}
		private void datagridGeometryRouting_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			RenderSubject.GeometryShaderLinks.RemoveAt(e.Row.Index);
			RenderSubjectForm.UpdateGeometryRouting();
		}
		private void datagridGeometryRouting_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (bolUpdateLock) return;
			if(e.RowIndex < 0) return;
			DataGridViewRow row = datagridGeometryRouting.Rows[e.RowIndex];
			DataGridViewColumn column = datagridGeometryRouting.Columns[e.ColumnIndex];
			DataGridViewCell cell = row.Cells[column.Index];
			DataGridViewComboBoxEditingControl lst = cell.Tag as DataGridViewComboBoxEditingControl;
			if (lst == null) return;
			KeyValuePair<string, clsVertexDescriptionComponent> oldValue = RenderSubject.GeometryShaderLinks[row.Index];
			if(column.Name == "columnProgramAttr")
			{
				string obj = lst.SelectedItem as string;
				RenderSubject.GeometryShaderLinks[row.Index] = new KeyValuePair<string, clsVertexDescriptionComponent>(obj, oldValue.Value);
			}
			if (column.Name == "columnVertDesc")
			{
				clsVertexDescriptionComponent obj = lst.SelectedItem as clsVertexDescriptionComponent;
				RenderSubject.GeometryShaderLinks[row.Index] = new KeyValuePair<string, clsVertexDescriptionComponent>(oldValue.Key, obj);
			}
			cell.Tag = null;
			RenderSubjectForm.UpdateGeometryRouting();
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
			RenderSubject.Uniforms.Add(new KeyValuePair<string, clsUniformSet> (null, objValue));
			UpdateUniformVariableList();
		}
		private void datagridUniformsValues_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			if (e.Row.Index < 0) return;
			RenderSubject.Uniforms.RemoveAt(e.Row.Index);
			UpdateUniformVariableList();
		}
		private void datagridUniformsValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (bolUpdateLock) return;
			if (e.RowIndex < 0) return;
			if (e.ColumnIndex < 0) return;
			if (e.RowIndex >= RenderSubject.Uniforms.Count) return;
			DataGridViewColumn column = datagridUniformsValues.Columns[e.ColumnIndex];
			DataGridViewCell cell = datagridUniformsValues[e.ColumnIndex, e.RowIndex];
			KeyValuePair<string, clsUniformSet> oldValue = RenderSubject.Uniforms[e.RowIndex];
			if(column.Name == "columnVariableName")
			{
				RenderSubject.Uniforms[e.RowIndex] = new KeyValuePair<string, clsUniformSet>(cell.Value as string, oldValue.Value);
				UpdateUniformVariableList();
			}
			if(column.Name == "columnVariableValue")
			{
				clsUniformDataCell cellUniformData = cell as clsUniformDataCell;
				if(cellUniformData != null)
				{
					clsUniformSet valueNew = new clsUniformSet(cellUniformData.Value.ToString());
					valueNew.Type = cellUniformData.DataUniformType;
					valueNew.Data = cellUniformData.DataObject;
					RenderSubject.Uniforms[e.RowIndex] = new KeyValuePair<string, clsUniformSet>(oldValue.Key, valueNew);
				}
			}
			cell.Tag = null;
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
			RenderSubjectForm.UpdateGeometryRouting();
		}
		private void datagridUniformsRouting_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			RenderSubject.UniformShaderLinks.RemoveAt(e.Row.Index);
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
		}
		private void datagridUniformsRouting_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			DataGridViewCell cell = datagridGeometryRouting.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
			if (cell == null) return;
			controlEventScript control = e.Control as controlEventScript;
			if (control == null) return;
			clsEventScript scriptNew = new clsEventScript();
			scriptNew.Subject = RenderSubject;
			cell.Tag = scriptNew;
			control.ScriptContext = scriptNew.ScriptContext;
		}
		public void UpdateDataGrid()
		{
			if (!Visible) return;
			Invoke(new Action(() => 
			{
				bolUpdateLock = true;
				for (int idxRow = 0; idxRow < Math.Min(datagridUniformsValues.Rows.Count, RenderSubject.Uniforms.Count); idxRow++)
				{
					DataGridViewTextBoxCell cellName = datagridUniformsValues["columnVariableName", idxRow] as DataGridViewTextBoxCell;
					if (cellName == null) continue;
					string strName = cellName.Value as string;
					if (strName == null) continue;
					if (strName != RenderSubject.Uniforms[idxRow].Key) continue;
					clsUniformDataCell cellValue = datagridUniformsValues["columnVariableValue", idxRow] as clsUniformDataCell;
					if (cellValue == null) continue;
					clsUniformSet objData = RenderSubject.Uniforms[idxRow].Value;
					if (objData == null) continue;
					cellValue.Value = objData;
					datagridUniformsValues.InvalidateCell(cellValue);
				}
				bolUpdateLock = false;
			}));
		}
	}
}
