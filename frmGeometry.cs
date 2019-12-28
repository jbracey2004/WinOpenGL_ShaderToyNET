﻿using modProject;
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
using System.Text.RegularExpressions;

namespace WinOpenGL_ShaderToy
{
	public partial class frmGeometry : DockContent
	{
		public frmGeometry(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsGeometry Geometry { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsGeometry; } }
		private Matrix4 matxView = Matrix4.Identity;
		private Matrix4 matxProjection = Matrix4.Identity;
		private clsCollapsePanel containerMain;
		private Stopwatch timeRun;
		private infoFramePerformance tsRender;
		private clsHPTimer timerUpdateLists;
		private void FrmGeometry_Load(object sender, EventArgs e)
		{
			containerMain = new clsCollapsePanel(panelCollapse);
			containerMain.CollapseStateChanged += new CollapseStateChangeHandler(containerMain_CollapseChange);
			containerMain.CollapseDistanceChanged += new CollapseStateChangeHandler(containerMain_CollapseDistanceChange);
			if (Geometry.VertexDescription == null)
			{
				clsVertexDescription newDesc = null;
				foreach (clsProjectObject objItr in projectMain.ProjectObjects)
				{
					clsVertexDescription descItr = objItr as clsVertexDescription;
					if (descItr != null) 
					{
						newDesc = descItr;
						break;
					}
				}
				if(newDesc == null)
				{
					newDesc = new clsVertexDescription();
					newDesc.Add(VertexAttribPointerType.Float, "Position", 3, 0);
					projectMain.ProjectObjects.Add(newDesc);
				}
				Geometry.VertexDescription = newDesc;
			}
			if(Geometry.Vertices == null)
			{
				clsVertexCollection newVertices = new clsVertexCollection(Geometry);
				newVertices.Count = 4;
				newVertices[0]["Position"] = new object[] { -1f, -1f, 0f };
				newVertices[1]["Position"] = new object[] { 1f, -1f, 0f };
				newVertices[2]["Position"] = new object[] { 1f, 1f, 0f };
				newVertices[3]["Position"] = new object[] { -1f, 1f, 0f };
				Geometry.Vertices = newVertices;
			}
			if(Geometry.Triangles == null)
			{
				clsTriangleCollection newTriangles = new clsTriangleCollection(Geometry);
				newTriangles.Count = 2;
				newTriangles[0].Items = new uint[] { 0, 1, 2 };
				newTriangles[1].Items = new uint[] { 2, 3, 0 };
				Geometry.Triangles = newTriangles;
			}
			Geometry.glUpdateBuffers();
			propsGeometry.SelectedObject = new { Vertices = Geometry.Vertices, Triangles = Geometry.Triangles};
			UpdateLists();
			matxView.Row3 = new Vector4(0, 0, -2, 1);
			timerUpdateLists = new clsHPTimer(this);
			timerUpdateLists.Interval = 1000.0;
			timerUpdateLists.SleepInterval = 500;
			timerUpdateLists.IntervalEnd += new HPIntervalEventHandler(timerUpdateLists_EndInterval);
			timerUpdateLists.Start();
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
			timerUpdateLists.Stop();
			timerUpdateLists = null;
			panelMain.ProjectObject = null;
		}
		private void timerUpdateLists_EndInterval(object sender, HPIntervalEventArgs e)
		{
			propsGeometry.Refresh();
			Geometry.glUpdateBuffers();
			glRender_Init();
			UpdateLists();
		}
		private void UpdateLists()
		{
			UpdateVertexDescriptionList();
			UpdatePositionAttrList();
		}
		private class propsVertexDescriptionComponentItem
		{
			public clsVertexDescriptionComponent Component { get; set; }
			public propsVertexDescriptionComponentItem(clsVertexDescriptionComponent itm) { Component = itm; }
			public override string ToString() => (Component !=null) ? $"{Component.Index}: {Component.Name} <{Component.ElementGLType} {Component.ElementCount}>" : "[None]";
		}
		private class propsVertexDescriptionItem
		{
			public clsVertexDescription Description { get; set; }
			public propsVertexDescriptionItem(clsVertexDescription itm) { Description = itm; }
			public override string ToString() => (Description != null) ? $"{Description.Name}" : "[None]";
		}
		private void UpdatePositionAttrList()
		{
			propsVertexDescriptionComponentItem itm = lstPositionAttr.SelectedItem as propsVertexDescriptionComponentItem;
			lstPositionAttr.Items.Clear();
			lstPositionAttr.SelectedIndex = lstPositionAttr.Items.Add("[None]");
			foreach(clsVertexDescriptionComponent comp in Geometry.VertexDescription)
			{
				if(comp.ElementType == typeof(float) || comp.ElementType == typeof(double))
				{
					if (comp.ElementCount == 2 || comp.ElementCount == 3)
					{
						propsVertexDescriptionComponentItem itmNew = new propsVertexDescriptionComponentItem(comp);
						lstPositionAttr.Items.Add(itmNew);
						if (itm != null && itm.Component == comp) lstPositionAttr.SelectedItem = itmNew;
					}
				}
			}
		}
		private void UpdateVertexDescriptionList()
		{
			lstVertexDesc.Items.Clear();
			foreach (clsProjectObject objItr in projectMain.ProjectObjects)
			{
				clsVertexDescription descItr = objItr as clsVertexDescription;
				if(descItr != null)
				{
					propsVertexDescriptionItem itmNew = new propsVertexDescriptionItem(descItr);
					lstVertexDesc.Items.Add(itmNew);
					if (Geometry.VertexDescription == descItr) lstVertexDesc.SelectedItem = itmNew;
				}
			}
		}
		private void lstVertexDesc_SelectedIndexChanged(object sender, EventArgs e)
		{
			propsVertexDescriptionItem itm = lstVertexDesc.SelectedItem as propsVertexDescriptionItem;
			if(itm != null)
			{
				Geometry.VertexDescription = itm.Description;
			}
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
			propsVertexDescriptionComponentItem itmBuff = lstPositionAttr.SelectedItem as propsVertexDescriptionComponentItem;
			if (itmBuff != null)
			{
				if(itmBuff.Component.ElementCount >= 3)
				{
					matxProjection = Matrix4.CreatePerspectiveFieldOfView((float)((1.0 / 3.0) * Math.PI), glRender.AspectRatio, 0.0001f, 1000f);
				} else
				{
					matxProjection = Matrix4.CreateOrthographic(2, 2, -1, 1);
				}
			}
			GL.ClearColor(glRender.BackColor);
			glRender.Invalidate();
		}
		private void glRender_HandleCreated(object sender, EventArgs e)
		{
			glRender_Init();
		}
		private void glRender_Resize(object sender, EventArgs e)
		{
			glRender_Init();
		}
		private void glRender_Paint(object sender, PaintEventArgs e)
		{
			glRender_Render();
		}
		private void glRender_Render()
		{
			glRender.Context.MakeCurrent(glRender.WindowInfo);
			GL.Viewport(glRender.ClientRectangle);
			tsRender.ResetInterval();
			tsRender.StartInterval();
			glRender.MakeCurrent();
			GL.Clear(ClearBufferMask.ColorBufferBit);
			propsVertexDescriptionComponentItem itmBuff = lstPositionAttr.SelectedItem as propsVertexDescriptionComponentItem;
			if(itmBuff != null)
			{
				clsVertexDescriptionComponent comp = itmBuff.Component;
				GL.Enable(EnableCap.Blend);
				GL.Enable(EnableCap.ProgramPointSize);
				GL.Enable(EnableCap.PointSmooth);
				GL.PointSize(16);
				GL.LineWidth(2);
				GL.MatrixMode(MatrixMode.Projection);
				GL.LoadMatrix(ref matxProjection);
				GL.MatrixMode(MatrixMode.Modelview);
				GL.LoadIdentity();
				if (itmBuff.Component.ElementCount >= 3) GL.LoadMatrix(ref matxView);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.DstColor);
				GL.EnableClientState(ArrayCap.VertexArray);
				GL.EnableClientState(ArrayCap.IndexArray);
				GL.BindBuffer(BufferTarget.ArrayBuffer, Geometry.glBuffers[comp.Index]);
				GL.VertexPointer(comp.ElementCount, clsVertexDescriptionComponent.VertexPointerTypes[comp.ElementGLType], comp.ComponentSize, 0);
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, Geometry.glIndexBuffer);
				GL.Color4(Color.FromArgb(96, ForeColor));
				GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
				GL.DrawElements(PrimitiveType.Triangles, Geometry.Triangles.Indices.Length, DrawElementsType.UnsignedInt, 0);
				GL.Color4(Color.FromArgb(108, ForeColor));
				GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
				GL.DrawElements(PrimitiveType.Triangles, Geometry.Triangles.Indices.Length, DrawElementsType.UnsignedInt, 0);
				GL.Color4(Color.FromArgb(128, ForeColor));
				GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point);
				GL.DrawElements(PrimitiveType.Triangles, Geometry.Triangles.Indices.Length, DrawElementsType.UnsignedInt, 0);
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			}
			glRender.Context.SwapBuffers();
			tsRender.SampleInterval((float)timeRun.Elapsed.TotalSeconds);
			tsRender.StopInterval();
		}
		private void PropsGeometry_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			propsGeometry.Refresh();
			Geometry.glUpdateBuffers();
			glRender_Init();
		}
		private void LstPositionAttr_SelectedIndexChanged(object sender, EventArgs e)
		{
			glRender_Init();
		}
	}
}
