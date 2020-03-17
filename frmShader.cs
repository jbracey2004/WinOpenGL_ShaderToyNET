﻿using modProject;
using System;
using System.Linq;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing;
using static clsHPTimer;
using static WinOpenGL_ShaderToy.ProjectDef;
using FastColoredTextBoxNS;
using System.Collections.Generic;
using System.ComponentModel;

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
			timerAutoCompile = new clsHPTimer(this);
			timerAutoCompile.Interval = 1000.0;
			timerAutoCompile.SleepInterval = 500;
			timerAutoCompile.IntervalEnd += timerAutoCompile_EndInterval;
			timerAutoCompile.Start();
			txtSource.Text = Shader.Source;
			Compile();
		}
		private void FrmShader_FormClosing(object sender, FormClosingEventArgs e)
		{
			timerAutoCompile.Stop();
			timerAutoCompile.Dispose();
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
					Shader.SaveSourceToFile(dialogSaveFile.FileName);
				}
			}
		}
		private void TxtSource_TextChanged(object sender, TextChangedEventArgs e)
		{
			if(Shader != null)
			{
				Shader.Source = txtSource.Text.Trim();
				UpdateSourceHighlighting(sender, e);
				if(chkAutoCompile.Checked)
				{
					timerAutoCompile.Stop();
					timerAutoCompile.Reset();
					timerAutoCompile.Start();
				}
			}
		}
		public static Style styleAccessModifiers { set; get; } = new TextStyle(new SolidBrush(Color.FromArgb(0x00, 0x80, 0xFF)), null, FontStyle.Regular);
		public static Style styleGLTypes { set; get; } = new TextStyle(new SolidBrush(Color.FromArgb(0x40, 0x80, 0x80)), null, FontStyle.Regular);
		public static Style styleCodeFlow { set; get; } = new TextStyle(new SolidBrush(Color.FromArgb(0xC4, 0x62, 0x00)), null, FontStyle.Regular);
		public static Style styleGLInputOutput { set; get; } = new TextStyle(new SolidBrush(Color.FromArgb(0x80, 0x80, 0xFF)), null, FontStyle.Regular);
		public static Style styleGLKeywords { set; get; } = new TextStyle(new SolidBrush(Color.FromArgb(0x00, 0x00, 0x00)), null, FontStyle.Regular);
		public static Style styleBuiltInFunctions { set; get; } = new TextStyle(new SolidBrush(Color.FromArgb(0xFF, 0x80, 0x40)), null, FontStyle.Regular);
		private void UpdateSourceHighlighting(object sender, TextChangedEventArgs e)
		{
			e.ChangedRange.ClearStyle(styleAccessModifiers, styleGLTypes, styleCodeFlow, styleGLInputOutput, styleGLKeywords, styleBuiltInFunctions);
			e.ChangedRange.SetStyle(styleCodeFlow,
				@"\s*#\b(define|undef|if|ifdef|ifndef|else|elif|endif|error|pragma|extension |version |line)\b");
			e.ChangedRange.SetStyle(styleAccessModifiers,
				@"\b(attribute|const|uniform|varying|buffer|shared|coherent|volatile|restrict|readonly|writeonly|struct|layout|centroid|flat|smooth|noperspective|patch|sample|break|continue|do|for|while|switch|case|default|if|else|subroutine|in|out|inout|true|false|invariant|discard|return|lowp|mediump|highp|precision)\b");
			e.ChangedRange.SetStyle(styleGLTypes,
				@"\b(sampler1D|sampler2D|sampler3D|samplerCube|sampler1DShadow|sampler2DShadow|samplerCubeShadow|sampler1DArray|sampler2DArray|sampler1DArrayShadow|sampler2DArrayShadow|isampler1D|isampler2D|isampler3D|isamplerCube|isampler1DArray|isampler2DArray|usampler1D|usampler2D|usampler3D|usamplerCube|usampler1DArray|usampler2DArray|sampler2DRect|sampler2DRectShadow|isampler2DRect|usampler2DRect|samplerBuffer|isamplerBuffer|usamplerBuffer|sampler2DMS|isampler2DMS|usampler2DMS|sampler2DMSArray|isampler2DMSArray|usampler2DMSArray|samplerCubeArray|samplerCubeArrayShadow|isamplerCubeArray|usamplerCubeArray|image1D|iimage1D|uimage1D|image2D|iimage2D|uimage2D|image3D|iimage3D|uimage3D|image2DRect|iimage2DRect|uimage2DRect|imageCube|iimageCube|uimageCube|imageBuffer|iimageBuffer|uimageBuffer|image1DArray|iimage1DArray|uimage1DArray|image2DArray|iimage2DArray|uimage2DArray|imageCubeArray|iimageCubeArray|uimageCubeArray|image2DMS|iimage2DMS|uimage2DMS|image2DMSArray|iimage2DMSArray|uimage2DMSArray|atomic_uint|mat2|mat3|mat4|dmat2|dmat3|dmat4|mat2x2|mat2x3|mat2x4|dmat2x2|dmat2x3|dmat2x4|mat3x2|mat3x3|mat3x4|dmat3x2|dmat3x3|dmat3x4|mat4x2|mat4x3|mat4x4|dmat4x2|dmat4x3|dmat4x4|vec2|vec3|vec4|ivec2|ivec3|ivec4|bvec2|bvec3|bvec4|dvec2|dvec3|dvec4|float|double|int|void|bool|uint|uvec2|uvec3|uvec4)\b");
			e.ChangedRange.SetStyle(styleGLInputOutput,
				@"\b(gl_NumWorkGroups|gl_WorkGroupSize|gl_WorkGroupID|gl_LocalInvocationID|gl_GlobalInvocationID|gl_LocalInvocationIndex|gl_VertexID|gl_InstanceID|gl_PerVertex|gl_Position|gl_PointSize|gl_ClipDistance|gl_in|gl_PrimitiveIDIn|gl_InvocationID|gl_Layer|gl_ViewportIndex|gl_PatchVerticesIn|gl_InvocationID|gl_out|gl_TessLevelOuter|gl_TessLevelInner|gl_PatchVerticesIn|gl_PrimitiveID|gl_TessCoord|gl_FragCoord|gl_FrontFacing|gl_ClipDistance|gl_PointCoord|gl_PrimitiveID|gl_SampleID|gl_SamplePosition|gl_SampleMaskIn|gl_Layer|gl_ViewportIndex|gl_FragDepth|gl_SampleMask)\b");
			e.ChangedRange.SetStyle(styleGLKeywords,
				@"\b(gl_MaxComputeWorkGroupCount|gl_MaxComputeWorkGroupSize|gl_MaxComputeUniformComponents|gl_MaxComputeTextureImageUnits|gl_MaxComputeImageUniforms|gl_MaxComputeAtomicCounters|gl_MaxComputeAtomicCounterBuffers|gl_MaxVertexAttribs|gl_MaxVertexUniformComponents|gl_MaxVaryingComponents|gl_MaxVertexOutputComponents|gl_MaxGeometryInputComponents|gl_MaxGeometryOutputComponents|gl_MaxFragmentInputComponents|gl_MaxVertexTextureImageUnits|gl_MaxCombinedTextureImageUnits|gl_MaxTextureImageUnits|gl_MaxImageUnits|gl_MaxCombinedImageUnitsAndFragmentOutputs|gl_MaxImageSamples|gl_MaxVertexImageUniforms|gl_MaxTessControlImageUniforms|gl_MaxTessEvaluationImageUniforms|gl_MaxGeometryImageUniforms|gl_MaxFragmentImageUniforms|gl_MaxCombinedImageUniforms|gl_MaxFragmentUniformComponents|gl_MaxDrawBuffers|gl_MaxClipDistances|gl_MaxGeometryTextureImageUnits|gl_MaxGeometryOutputVertices|gl_MaxGeometryTotalOutputComponents|gl_MaxGeometryUniformComponents|gl_MaxGeometryVaryingComponents|gl_MaxTessControlInputComponents|gl_MaxTessControlOutputComponents|gl_MaxTessControlTextureImageUnits|gl_MaxTessControlUniformComponents|gl_MaxTessControlTotalOutputComponents|gl_MaxTessEvaluationInputComponents|gl_MaxTessEvaluationOutputComponents|gl_MaxTessEvaluationTextureImageUnits|gl_MaxTessEvaluationUniformComponents|gl_MaxTessPatchComponents|gl_MaxPatchVertices|gl_MaxTessGenLevel|gl_MaxViewports|gl_MaxVertexUniformVectors|gl_MaxFragmentUniformVectors|gl_MaxVaryingVectors|gl_MaxVertexAtomicCounters|gl_MaxTessControlAtomicCounters|gl_MaxTessEvaluationAtomicCounters|gl_MaxGeometryAtomicCounters|gl_MaxFragmentAtomicCounters|gl_MaxCombinedAtomicCounters|gl_MaxAtomicCounterBindings|gl_MaxVertexAtomicCounterBuffers|gl_MaxTessControlAtomicCounterBuffers|gl_MaxTessEvaluationAtomicCounterBuffers|gl_MaxGeometryAtomicCounterBuffers|gl_MaxFragmentAtomicCounterBuffers|gl_MaxCombinedAtomicCounterBuffers|gl_MaxAtomicCounterBufferSize|gl_MinProgramTexelOffset|gl_MaxProgramTexelOffset)\b");
			e.ChangedRange.SetStyle(styleBuiltInFunctions,
				@"\b(radians|degrees|sin|cos|tan|asin|acos|atan|sinh|cosh|tanh|asinh|acosh|atanh|pow|exp|log|exp2|log2|sqrt|inversqrt|abs|sign|floor|trunc|round|roundEven|ceil|fract|mod|modf|min|max|clamp|mix|step|smoothstep|isnan|isinf|floatBitsToInt|floatBitsToUInt|intBitsToFloat|uintBitsToFloat|fma|frexp|ldexp|packUnorm2x16|packSnorm2x16|packUnorm4x8|packSnorm4x8|unpackUnorm2x16|unpackSnorm2x16|unpackUnorm4x8|unpackSnorm4x8|packDouble2x32|unpackDouble2x32|packHalf2x16|unpackHalf2x16|length|distance|dot|cross|normalize|faceforward|reflect|refract|matrixCompMult|outerProduct|transpose|determinant|inverse|lessThan|lessThanEqual|greaterThan|greaterThanEqual|equal|notEqual|any|all|not|uaddCarry|usubBorrow|umulExtended|imulExtended|bitfieldExtract|bitfieldInsert|bitfieldReverse|findLSB|bitCount|findMSB|textureSize|textureQueryLod|textureQueryLevels|texture|textureProj|textureLod|textureOffset|texelFetch|texelFetchOffset|textureProjOffset|textureLodOffset|textureProjLod|textureProjLodOffset|textureGrad|textureGradOffset|textureProjGrad|textureProjGradOffset|textureGather|textureGatherOffset|textureGatherOffsets|texture1D|texture1DProj|texture1DLod|texture1DProjLod|texture2D|texture2DProj|texture2DLod|texture2DProjLod|texture3D|texture3DProj|texture3DLod|texture3DProjLod|textureCube|textureCubeLod|shadow1D|shadow2D|shadow1DProj|shadow2DProj|shadow1DLod|shadow2DLod|shadow1DProjLod|shadow2DProjLod|atomicCounterIncrement|atomicCounterDecrement|atomicCounter|atomicAdd|atomicMin|atomicMax|atomicAnd|atomicOr|atomicXor|atomicExchange|atomicCompSwap|imageSize|imageLoad|imageStore|imageAtomicAdd|imageAtomicMin|imageAtomicMax|imageAtomicAnd|imageAtomicOr|imageAtomicXor|imageAtomicExchange|imageAtomicCompSwap|dFdx|dFdy|fwidth|interpolateAtCentroid|interpolateAtSample|interpolateAtOffset|noise1|noise2|noise3|noise4|EmitStreamVertex|EndStreamPrimitive|EmitVertex|EndPrimitive|barrier|memoryBarrier|memoryBarrierAtomicCounter|memoryBarrierBuffer|memoryBarrierShared|memoryBarrierImage|groupMemoryBarrier)\b");
		}
		private void lstShaderType_SelectedIndexChanged(object sender, EventArgs e)
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
					lblCompileStatus.ForeColor = Color.Red;
					lblCompileStatus.Text = "Compile Status: Compile Failed.";
				}
				else if (Shader.CompileInfo.WarningMessages.Length > 0)
				{
					lblCompileStatus.ForeColor = Color.RoyalBlue;
					lblCompileStatus.Text = "Compile Status: Compiled* See Messages.";
				}
				else if (Shader.CompileInfo.AllMessages.Length > 0)
				{
					lblCompileStatus.ForeColor = Color.RoyalBlue;
					lblCompileStatus.Text = "Link Status: Linked* See Messages.";
				}
				else
				{
					lblCompileStatus.ForeColor = Color.Lime;
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
				dataCompileStatus.Columns["Message"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
