﻿using ShaderToy_Components;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI.Docking;

namespace modProject
{
	public class clsProjectObject : IDisposable
	{
		public enum ProjectObjectTypes
		{
			Null = -2,
			Designer = -1,
			Project = 0,
			VertexDescription = 1,
			Geometry = 2,
			Shader = 3,
			Program = 4,
			IOLinks = 5,
			Render = 6
		}
		public static List<clsProjectObject> All { private set; get; } = new List<clsProjectObject>() { };
		public ProjectObjectTypes ProjectObjType { private set; get; }
		public string Name { set; get; }
		public controlProjectObject ParentControl { set; get; } = null;
		public clsProjectObject(ProjectObjectTypes typ)
		{
			ProjectObjType = typ;
		}
		public void AddToCollection()
		{
			Name = NextFreeName(ToFullString(""), "New");
			All.Add(this);
		}
		public void RemoveFromCollection()
		{
			All.Remove(this);
		}
		public virtual string ToFullString(string name)
		{
			return ProjectObjType.ToString() + "_"+name;
		}
		public override string ToString()
		{
			return ToFullString(Name);
		}
		public static string NextFreeName(string prefix, string str, clsProjectObject obj = null)
		{
			int num = 1;
			string strRet = str;
			while (All.Find(itm => (obj != itm && itm.ToString() == prefix + strRet)) != null)
			{
				MatchCollection matchNum = Regex.Matches(strRet, @"\d{1,}\z");
				if(matchNum.Count >= 1)
				{
					num = int.Parse(matchNum[0].Value);
				} else
				{
					num = 1;
				}
				num++;
				strRet = Regex.Replace(str, @"\d{1,}\z", "") + num.ToString();
			}
			return strRet;
		}
		private bool disposedValue = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if (ParentControl != null)
					{
						ParentControl.ProjectObject = null;
					}
					Name = null;
					ProjectObjType = ProjectObjectTypes.Null;
					RemoveFromCollection();
				}
				disposedValue = true;
			}
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}