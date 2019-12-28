using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

public class generalUtils
{
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
		string strMin = (objMin != null) ? objMin.ToString() : " ";
		string strMax = (objMax != null) ? objMax.ToString() : " ";
		double.TryParse(strMin, out ret[0]);
		double.TryParse(strMax, out ret[1]);
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
		public double HistoryDuration = 10.0;
		private Stopwatch tsInterval = new Stopwatch();
		[Browsable(false)]
		public List<KeyValuePair<float, float>> Data { get; private set; } = new List<KeyValuePair<float, float>> { };
		[Browsable(false)]
		public List<KeyValuePair<float, float>> Data_Sorted { get; private set; } = new List<KeyValuePair<float, float>> { };
		[Browsable(false)]
		public List<float> Data_TimeStamps { get; private set; } = new List<float> { };
		[Browsable(false)]
		public List<float> Data_Durations { get; private set; } = new List<float> { };
		[Browsable(false)]
		public List<float> Data_Rates { get; private set; } = new List<float> { };
		[TypeConverter(typeof(NumberConverter))]
		public float Accumulated { get; private set; } = 0;
		[TypeConverter(typeof(NumberConverter))]
		public float TimeAccumulated { get { return (Data.Count > 0) ? Data[Data.Count - 1].Key : 0.0f; } }
		[TypeConverter(typeof(NumberConverter))]
		public float TimeSpan { get { return (Data.Count > 0) ? Data[Data.Count - 1].Key - Data[0].Key : 0.0f; } }
		[TypeConverter(typeof(NumberConverter))]
		public float DataAccumulated { get; private set; } = 0;
		[TypeConverter(typeof(NumberConverter))]
		public float Current { get; private set; } = 0;
		[TypeConverter(typeof(NumberConverter))]
		public float Longest { get { return (Data_Sorted.Count > 0) ? Data_Sorted[Data_Sorted.Count - 1].Value : 0.0f; } }
		[TypeConverter(typeof(NumberConverter))]
		public float Shortest { get { return (Data_Sorted.Count > 0) ? Data_Sorted[0].Value : 0.0f; } }
		[TypeConverter(typeof(NumberConverter))]
		public float Average { get { return Accumulated / Count; } }
		[TypeConverter(typeof(NumberConverter))]
		public float DataAvarage { get { return DataAccumulated / Data.Count; } }
		[TypeConverter(typeof(NumberConverter))]
		public float Median
		{
			get
			{
				if (Data_Sorted.Count <= 0) return 0.0f;
				int idxMid = (Data_Sorted.Count - 1) / 2;
				if (Data_Sorted.Count % 2 == 0)
				{
					return (Data_Sorted[idxMid].Value + Data_Sorted[idxMid + 1].Value) / 2;
				}
				else
				{
					return Data_Sorted[idxMid].Value;
				}
			}
		}
		public int Count { get; private set; } = 0;
		public int DataCount { get { return Data.Count; } }
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
		public double DataAverage_Rate { get { return Data.Count / DataAccumulated; } }
		public void StartInterval()
		{
			tsInterval.Start();
		}
		public float IntervalElapsed()
		{
			return (float)tsInterval.Elapsed.TotalSeconds;
		}
		public void StopInterval()
		{
			tsInterval.Stop();
		}
		public void SampleInterval(float ts = float.NaN)
		{
			SampleFrame(IntervalElapsed(), ts);
		}
		public void ResetInterval()
		{
			tsInterval.Reset();
		}
		public void SampleFrame(float value, float ts = float.NaN)
		{
			if (value > 0.0)
			{
				Current = value;
				Accumulated += Current;
				DataAccumulated += Current;
				float xval = (!double.IsNaN(ts)) ? ts : Accumulated;
				KeyValuePair<float, float> itmNew = new KeyValuePair<float, float>(xval, Current);
				double historyCut = xval - HistoryDuration;
				Data.RemoveAll(itm => itm.Key < historyCut);
				Data_Sorted.RemoveAll(itm => { if (itm.Key < historyCut) { DataAccumulated -= itm.Value; return true; } else { return false; } });
				Data_TimeStamps.RemoveAll(itm => itm < historyCut);
				Data_Durations.RemoveRange(0, Data_Durations.Count - Data.Count);
				Data_Rates.RemoveRange(0, Data_Rates.Count - Data.Count);
				int idxNew = FindSortedInsert(Data_Sorted.ToArray(), itmNew, (itmAdding, itm) => Math.Sign(itmAdding.Value - itm.Value));
				Data.Add(itmNew);
				Data_TimeStamps.Add(xval);
				Data_Durations.Add(Current);
				Data_Rates.Add(1.0f / Current);
				Data_Sorted.Insert(idxNew, itmNew);
				Count++;
			}
		}
		public override string ToString()
		{
			return this.Median_Rate.ToString();
		}
		private bool bolIsDisposed = false;
	    protected  virtual void Dispose(bool Disposing)
		{
			if(!bolIsDisposed)
			{
				if(Disposing)
				{
					this.Data.Clear();
					this.Data = null;
					this.Data_Sorted.Clear();
					this.Data_Sorted = null;
					this.Data_Durations.Clear();
					this.Data_Durations = null;
					this.Data_Rates.Clear();
					this.Data_Rates = null;
					this.Data_TimeStamps.Clear();
					this.Data_TimeStamps = null;
				}
			}
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}

	public static int FindSortedInsert<T>(T[] ary, T value, Func<T, T, int> lamdaCompare, int idx = -1, int idxStart = -1, int idxEnd = -1)
	{
		if (idxStart == -1) idxStart = 0;
		if (idxEnd == -1) idxEnd = ary.Length - 1;
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
