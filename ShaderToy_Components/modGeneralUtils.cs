using modProject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Web.UI.Design;

public class generalUtils
{
	public static Random RandomSeed = new Random();
	public static int Rnd(int min, int max) => RandomSeed.Next(min, max);
	public static double Rnd(double min, double max) => min + RandomSeed.NextDouble() * (max - min);
	public static KeyValuePair<string, int>[] SIUnits = new KeyValuePair<string, int>[]
	{
		new KeyValuePair<string, int>( "p", -9 ),
		new KeyValuePair<string, int>( "µ", -6 ),
		new KeyValuePair<string, int>( "m", -3 ),
		new KeyValuePair<string, int>( "", 0 ),
		new KeyValuePair<string, int>( "K", 3 ),
		new KeyValuePair<string, int>( "M", 6 ),
		new KeyValuePair<string, int>( "G", 9 ),
		new KeyValuePair<string, int>( "T", 12 )
	};
	public static void ResizeList<Typ>(ref List<Typ> ary, int newsize, Func<int, Typ> funcLamda)
	{
		int oldsize = ary.Count;
		if (newsize > oldsize)
		{
			for (int itr = 0; itr < newsize - oldsize; itr++) ary.Add(funcLamda(oldsize + itr));
		}
		else if (newsize < oldsize)
		{
			ary.RemoveRange(newsize, oldsize - newsize);
		}
	}
	public static Typ[] ObjectArrayAsType<Typ>(object[] ary)
	{
		Typ[] aryRet = new Typ[ary.Length];
		if (ary.Length <= 0)
		{
			ary.CopyTo(aryRet, 0);
			return aryRet;
		}
		for(int i = 0; i < aryRet.Length; i++)
		{
			aryRet[i] = (Typ)(ConvertToType(ary[i], typeof(Typ)));
		}
		return aryRet;
	}
	public static object ConvertToType(object value, Type typNew)
	{
		Type typOld = value.GetType();
		if (typOld == typNew) return value;
		if (value == null) return Convert.ChangeType(0, typNew);
		double[] rangeNew = RangeOfType(typNew);
		double[] rangeOld = RangeOfType(typOld);
		object NewValue;
		if (rangeNew[0] >= 0 || rangeOld[0] >= 0)
		{
			double valueTOld = RangeTValue(value, rangeOld[0], rangeOld[1]);
			NewValue = RangeLerpValue(valueTOld, rangeNew[0], rangeNew[1]);
		} else
		{
			string strValue = value.ToString();
			double fdValue;
			if(double.TryParse(strValue, out fdValue))
			{
				if (fdValue >= rangeNew[1]) fdValue = rangeNew[1];
				if (fdValue <= rangeNew[0]) fdValue = rangeNew[0];
				NewValue = fdValue;
			} else
			{
				NewValue = 0;
			}
		}
		return Convert.ChangeType(NewValue, typNew);
	}
	public static double[] RangeOfType(Type Typ)
	{
		double[] ret = new double[2] {double.NaN, double.NaN};
		FieldInfo fieldMin = Typ.GetField("MinValue");
		FieldInfo fieldMax = Typ.GetField("MaxValue");
		object objMin = fieldMin?.GetValue(null);
		object objMax = fieldMax?.GetValue(null);
		ret[0] = (double)Convert.ChangeType(objMin, typeof(double));
		ret[1] = (double)Convert.ChangeType(objMax, typeof(double));
		return ret;
	}
	public static double RangeTValue(object value, double min, double max)
	{
		double dValue = double.NaN;
		if (value == null) return double.NaN;
		if (!double.TryParse(value.ToString(), out dValue)) return double.NaN;
		return (double)(dValue - min) / (double)(max - min);
	}
	public static double RangeLerpValue(object t, double min, double max)
	{
		double dValue = double.NaN;
		if (!double.TryParse(t.ToString(), out dValue)) return double.NaN;
		return dValue*(double)(max - min) + min;
	}
	public static Typ[] ObjectToArray<Typ>(object aryObj)
	{
		return (Typ[])aryObj;
	}
	public static Typ ObjectArrayComponent<Typ>(object aryObj, int idx)
	{
		return ((Typ[])aryObj)[idx];
	}
	public static string NumberAsBase(short num, int Base) => Convert.ToString(num, Base).ToString();
	public static string NumberAsBase(int num, int Base) => Convert.ToString(num, Base).ToString();
	public static string NumberAsBase(long num, int Base) => Convert.ToString(num, Base).ToString();
	// @"(?<num>([\+\-]?\d+)?(\.)?(\d+)?)(eE(?<exp>[\+\-]?\d+))?"
	public static string floatToSIUnits(double num, KeyValuePair<string, int>[] arySIunits)
	{
		string strRet = $"{num:0.000000e0}";
		Match matchExp = Regex.Match(strRet, @"[eE](?<exp>[\+\-]?\d+)");
		if (!matchExp.Success) return strRet;
		if (matchExp.Groups["exp"] == null) return strRet;
		if (!int.TryParse(matchExp.Groups["exp"].Value, out int intExp)) return strRet;
		for (int itr = 0; itr < arySIunits.Length - 1; itr++)
		{
			if (intExp >= arySIunits[itr].Value && intExp < arySIunits[itr + 1].Value)
			{
				strRet = $"{(num * Math.Pow(10, -arySIunits[itr].Value)):0.000000} {arySIunits[itr].Key}";
				break;
			}
		}
		return strRet;
	}
	public static bool TryKeyPairParse(object obj, out KeyValuePair<dynamic, dynamic> kvpRet)
	{
		kvpRet = new KeyValuePair<dynamic, dynamic>();
		Type typ = obj.GetType();
		PropertyInfo[] props = typ.GetProperties();
		if (props.Length != 2) return false;
		if (props[0].Name != "Key") return false;
		if (props[1].Name != "Value") return false;
		kvpRet = new KeyValuePair<dynamic, dynamic>(props[0].GetValue(obj), props[1].GetValue(obj));
		return true;
	}
	public static bool IsMemberBrowsable(MemberInfo member)
	{
		foreach (CustomAttributeData attr in member.CustomAttributes)
		{
			if (attr.AttributeType != typeof(BrowsableAttribute)) continue;
			if (attr.ConstructorArguments.Count != 1) continue;
			if (attr.ConstructorArguments[0].ArgumentType != typeof(bool)) continue;
			return (bool)attr.ConstructorArguments[0].Value;
		}
		return true;
	}
	public static bool IsPropertyBrowsable(PropertyInfo prop)
	{
		foreach (CustomAttributeData attr in prop.CustomAttributes)
		{
			if (attr.AttributeType != typeof(BrowsableAttribute)) continue;
			if (attr.ConstructorArguments.Count != 1) continue;
			if (attr.ConstructorArguments[0].ArgumentType != typeof(bool)) continue;
			return (bool)attr.ConstructorArguments[0].Value;
		}
		return true;
	}
	public static List<Type> TypesExpandExempt = new List<Type> { typeof(clsProjectObject) };
	public static string ExpandedObjectString(object obj, List<Type> TypeExempts, bool BroadExempts)
	{
		Stack<Type> stackTypes = new Stack<Type>();
		return ExpandedObjectString(obj, ref stackTypes, TypeExempts, BroadExempts);
	}
	private static string ExpandedObjectString(object obj, ref Stack<Type> stackTypes, List<Type> TypeExempts, bool BroadExempts)
	{
		if (obj == null) return "<NULL>";
		Type typ = obj.GetType();
		if (typ.Name == "RuntimeType" || typ.Name == "Type") return obj.ToString();
		if (typ.Name == "String") return $"\"{obj}\"";
		if (TryKeyPairParse(obj, out KeyValuePair<dynamic, dynamic> kvp)) return $"{{{kvp.Key}: {kvp.Value}}}";
		if (stackTypes.Count > 0)
		{
			if (TypeExempts != null)
			{
				if (BroadExempts)
				{
					if (TypeExempts.Find(itm => itm.IsInstanceOfType(obj)) != null) return obj.ToString();
				}
				else
				{
					if (TypeExempts.Contains(typ)) return obj.ToString();
				}
			}
		}
		if (stackTypes.Contains(typ)) return obj.ToString();
		stackTypes.Push(typ);
		IEnumerable ary = obj as IEnumerable;
		if (ary != null)
		{
			IEnumerator Ienum = ary.GetEnumerator();
			string strDisp = "[";
			bool bol = Ienum.MoveNext();
			while (bol)
			{
				strDisp += ExpandedObjectString(Ienum.Current, ref stackTypes, TypeExempts, BroadExempts);
				bol = Ienum.MoveNext();
				if (bol) strDisp += ", ";
			}
			strDisp += "]";
			stackTypes.Pop();
			return strDisp;
		}
		string strRet = "";
		PropertyInfo[] aryProps = Array.FindAll(typ.GetProperties(), itm => (IsPropertyBrowsable(itm) && 
																			itm.GetIndexParameters().Length == 0 &&
																			(itm.GetMethod==null || !itm.GetMethod.IsStatic) &&
																			(itm.SetMethod==null || !itm.SetMethod.IsStatic)));
		if (aryProps.Length <= 0)
		{
			stackTypes.Pop();
			return obj.ToString();
		}
		strRet += "{";
		for (int itr = 0; itr < aryProps.Length; itr++)
		{
			object objProp = aryProps[itr].GetValue(obj);
			string strProp = ExpandedObjectString(objProp, ref stackTypes, TypeExempts, BroadExempts);
			string strName = aryProps[itr].Name;
			strRet += $"{strName}: {strProp}{((itr < aryProps.Length - 1) ? ($"; ") : (""))}";
		}
		strRet += "}";
		stackTypes.Pop();
		return strRet;
	}
	public static string ArrayToString(IEnumerable ary)
	{
		IEnumerator Ienum = ary.GetEnumerator();
		string strDisp = "[";
		bool bol = Ienum.MoveNext();
		while (bol)
		{
			strDisp += ExpandedObjectString(Ienum.Current, TypesExpandExempt, true);
			bol = Ienum.MoveNext();
			if (bol) strDisp += ", ";
		}
		strDisp += "]";
		return strDisp;
	}
	public class NumberConverter : DoubleConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Format("{0,14:#,##0.000000}", value);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
				return double.Parse(value as string, culture);

			return base.ConvertFrom(context, culture, value);
		}
	}
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class infoFramePerformance : IDisposable
	{
		public struct propTimeStampData
		{
			public propTimeStampData(double ts, double dt)
			{
				TimeStamp = ts;
				Duration = dt;
			}
			public double TimeStamp { get; set; }
			public double Duration { get; set; }
			public double Rate { get => 1.0 / Duration; 
								 set { Duration = 1.0 / value; } }
			public override string ToString()
			{
				return $"({TimeStamp.ToString()}, {Duration.ToString()})";
			}
		}
		public double HistoryDuration { get; set; } = 1.0;
		private Stopwatch tsInterval = new Stopwatch();
		[Browsable(false)]
		private List<propTimeStampData> aryData = new List<propTimeStampData> { };
		[Browsable(false)]
		private List<propTimeStampData> aryData_Sorted = new List<propTimeStampData> { };
		public propTimeStampData[] Data => aryData.ToArray();
		[TypeConverter(typeof(NumberConverter))]
		public double Accumulated { get; private set; } = 0;
		[TypeConverter(typeof(NumberConverter))]
		public double TimeAccumulated { get { return (aryData.Count > 0) ? aryData[aryData.Count - 1].TimeStamp : 0.0f; } }
		[TypeConverter(typeof(NumberConverter))]
		public double TimeSpan { get { return (aryData.Count > 0) ? aryData[aryData.Count - 1].TimeStamp - aryData[0].TimeStamp : 0.0f; } }
		[TypeConverter(typeof(NumberConverter))]
		public double DataAccumulated { get; private set; } = 0;
		[TypeConverter(typeof(NumberConverter))]
		public double Current { get; private set; } = 0;
		[TypeConverter(typeof(NumberConverter))]
		public double Longest { get { return (aryData_Sorted.Count > 0) ? aryData_Sorted[aryData_Sorted.Count - 1].Duration : 0.0f; } }
		[TypeConverter(typeof(NumberConverter))]
		public double Shortest { get { return (aryData_Sorted.Count > 0) ? aryData_Sorted[0].Duration : 0.0f; } }
		[TypeConverter(typeof(NumberConverter))]
		public double Average { get { return Accumulated / Count; } }
		[TypeConverter(typeof(NumberConverter))]
		public double DataAvarage { get { return DataAccumulated / aryData.Count; } }
		[TypeConverter(typeof(NumberConverter))]
		public double Median
		{
			get
			{
				if (aryData_Sorted.Count <= 0) return 0.0f;
				int idxMid = (aryData_Sorted.Count - 1) / 2;
				if (aryData_Sorted.Count % 2 == 0)
				{
					return (aryData_Sorted[idxMid].Duration + aryData_Sorted[idxMid + 1].Duration) / 2;
				}
				else
				{
					return aryData_Sorted[idxMid].Duration;
				}
			}
		}
		public int Count { get; private set; } = 0;
		public int DataCount { get { return aryData.Count; } }
		[TypeConverter(typeof(NumberConverter))]
		public double Current_Rate { get { return 1.0 / Current; } }
		[TypeConverter(typeof(NumberConverter))]
		public double Longest_Rate { get { return 1.0 / Longest; } }
		[TypeConverter(typeof(NumberConverter))]
		public double Shortest_Rate { get { return 1.0 / Shortest; } }
		[TypeConverter(typeof(NumberConverter))]
		public double Average_Rate { get { return Count / Accumulated; } }
		[TypeConverter(typeof(NumberConverter))]
		public double Median_Rate { get { return 1.0 / Median; } }
		[TypeConverter(typeof(NumberConverter))]
		public double DataAverage_Rate { get { return aryData.Count / DataAccumulated; } }
		public void StartInterval()
		{
			tsInterval.Start();
		}
		public double IntervalElapsed()
		{
			return (double)tsInterval.Elapsed.TotalSeconds;
		}
		public void StopInterval()
		{
			tsInterval.Stop();
		}
		public void SampleInterval(double ts = double.NaN)
		{
			SampleFrame(IntervalElapsed(), ts);
		}
		public void ResetInterval()
		{
			tsInterval.Reset();
		}
		private bool bolUpdateLock = false;
		public void SampleFrame(double value, double ts = double.NaN)
		{
			while (bolUpdateLock) { }
			bolUpdateLock = true;
			Current = value;
			Accumulated += value;
			DataAccumulated += value;
			double xval = (!double.IsNaN(ts)) ? ts : Accumulated;
			propTimeStampData itmNew = new propTimeStampData(xval, value);
			double historyCut = xval - HistoryDuration;
			_ = aryData.RemoveAll(itm => (itm.TimeStamp < historyCut));
			_ = aryData_Sorted.RemoveAll(itm => { if (itm.TimeStamp < historyCut) { DataAccumulated -= itm.Duration; return true; } else { return false; } });
			int idxNew = FindSortedInsert(aryData_Sorted, itmNew, (itmAdding, itm) => Math.Sign(itmAdding.Duration - itm.Duration));
			aryData.Add(itmNew);
			aryData_Sorted.Insert(idxNew, itmNew);
			Count++;
			bolUpdateLock = false;
		}
		public void ClearSamples()
		{
			while (bolUpdateLock) { }
			bolUpdateLock = true;
			aryData.Clear();
			aryData_Sorted.Clear();
			DataAccumulated = 0;
			bolUpdateLock = false;
		}
		public void Reset()
		{
			while (bolUpdateLock) { }
			bolUpdateLock = true;
			aryData.Clear();
			aryData_Sorted.Clear();
			DataAccumulated = 0;
			Accumulated = 0;
			Count = 0;
			bolUpdateLock = false;
		}
		public override string ToString()
		{
			return this.Median_Rate.ToString();
		}
		private bool bolIsDisposed = false;
	    protected virtual void Dispose(bool Disposing)
		{
			if (!bolIsDisposed)
			{
				if(Disposing)
				{
					aryData.Clear();
					aryData = null;
					aryData_Sorted.Clear();
					aryData_Sorted = null;
				}
			}
		}
		public void Dispose()
		{
			Dispose(true);
			//GC.SuppressFinalize(this);
		}
	}

	public static int FindSortedInsert<T>(List<T> ary, T value, Func<T, T, int> lamdaCompare, int idx = -1, int idxStart = -1, int idxEnd = -1)
	{
		if (idxStart == -1) idxStart = 0;
		if (idxEnd == -1) idxEnd = ary.Count - 1;
		if (idxEnd <= 0) return 0;
		if (idx == -1) idx = idxEnd;
		int c = lamdaCompare(value, ary[idx]);
		int jmpPlus = Math.Max(1, (idxEnd - idx) / 2);
		int jmpMinus = Math.Max(1, (idx - idxStart) / 2);
		int idxMid = (idxEnd + idxStart) / 2;
		if (c > 0)
		{
			if (idx == idxEnd) return idx + 1;
			if (lamdaCompare(value, ary[idx + 1]) <= 0) return idx + 1;
			return FindSortedInsert(ary, value, lamdaCompare, idx + jmpPlus, idx, idxEnd);
		}
		else if (c < 0)
		{
			if (idx == idxStart) return idx;
			if (lamdaCompare(value, ary[idx - 1]) >= 0) return idx;
			return FindSortedInsert(ary, value, lamdaCompare, idx - jmpMinus, idxStart, idx);
		}
		else
		{
			return idx;
		}
	}
}
