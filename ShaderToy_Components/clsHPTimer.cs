using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using static generalUtils;

public class clsHPTimer : IDisposable
{
	public class HPIntervalEventArgs : EventArgs
	{
		public DateTime TimeStamp { set; get; }
		public double TimeDelta { set; get; }
		public HPIntervalEventArgs(DateTime ts, double dt)
		{
			TimeStamp = ts;
			TimeDelta = dt;
		}
	}
	public static double HPTickFrequency = 1000.0/Stopwatch.Frequency;
	public static double HPTickInterval = Stopwatch.Frequency;
	public delegate void HPIntervalEventHandler(object sender, HPIntervalEventArgs e);
	public event HPIntervalEventHandler IntervalEnd;
	private Stopwatch ts;
	public int SleepInterval { set; get; } = 0;
	private double fInterval = 1000.0;
	private double tickInterval = HPTickFrequency;
	public double Interval 
	{ 
		get => fInterval; 
		set 
		{ 
			fInterval = value; 
			tickInterval = (value*0.0009999) * HPTickInterval; 
			ts.Restart(); 
		} 
	}
	private bool bolRunning = false;
	public Control Parent { get; set;}
	private Thread threadLoop;
	public clsHPTimer(Control controlParent)
	{
		Parent = controlParent;
		ts = new Stopwatch();
	}
	public void Start()
	{
		if (bolRunning) return;
		bolRunning = true;
		threadLoop = new Thread(Loop);
		threadLoop.Priority = ThreadPriority.BelowNormal;
		threadLoop.Start();
		ts.Start();
	}
	public void Stop()
	{
		if (!bolRunning) return;
		bolRunning = false;
		threadLoop.Abort();
		threadLoop = null;
		ts.Stop();
	}
	public void Reset()
	{
		ts.Reset();
	}
	private void Loop()
	{
		long ElapsedTicks;
		double fElapsedDelta;
		ts.Start();
		while (bolRunning)
		{
			if (ts.ElapsedTicks >= tickInterval)
			{
				ElapsedTicks = ts.ElapsedTicks;
				fElapsedDelta = ElapsedTicks * HPTickFrequency;
				ts.Restart();
				try
				{
					if (IntervalEnd != null)
					{
						Parent.Invoke(IntervalEnd, this, new HPIntervalEventArgs(DateTime.Now, fElapsedDelta));
					}
				}
				catch
				{
					if (Parent != null)
					{
						if (Parent.IsDisposed || Parent.Disposing)
						{
							bolRunning = false;
						}
					} 
					else
					{
						Console.WriteLine("No Timer Event: Parent Context Disposition Out of Sync");
					}
				}
				Application.DoEvents();
				if (SleepInterval > 0)
				{
					Thread.Sleep(SleepInterval);
				}
			}
			//Application.DoEvents();
		}
	}
	bool bolIsDisposed = false;
	protected virtual void Dispose(bool bolDisposing)
	{
		if (!bolIsDisposed)
		{
			if (bolDisposing)
			{
				Stop();
			}
		}
		bolIsDisposed = true;
	}
	public void Dispose()
	{
		Dispose(true);
	}
}