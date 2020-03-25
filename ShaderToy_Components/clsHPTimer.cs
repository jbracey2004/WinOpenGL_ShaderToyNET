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
	public double Interval { get => fInterval; set { fInterval = value; tickInterval = (value*0.00099) * HPTickInterval; } }
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
		bolRunning = false;
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
		while (bolRunning)
		{
			if (ts.ElapsedTicks >= tickInterval)
			{
				ElapsedTicks = ts.ElapsedTicks;
				fElapsedDelta = ElapsedTicks * HPTickFrequency;
				ts.Restart();
				try
				{
					if (Parent != null)
					{
						if(!(Parent.IsDisposed || Parent.Disposing))
						{
							HPIntervalEventHandler evnt = IntervalEnd;
							if(evnt != null)
							{
								Parent?.Invoke(evnt, this, new HPIntervalEventArgs(DateTime.Now, fElapsedDelta));
							}
						}
					}
				}
				catch
				{
					if(Parent != null)
					{
						if (Parent.IsDisposed || Parent.Disposing)
						{
							bolRunning = false;
						}
					} else
					{
						Console.WriteLine("No Timer Event: Parent Context Disposition Out of Sync");
					}
				}
				if (SleepInterval > 0)
				{
					Thread.Sleep(SleepInterval);
				}
				//Application.DoEvents();
			}
			Application.DoEvents();
		}
		Application.DoEvents();
		threadLoop = null;
	}
	bool bolIsDisposed = false;
	protected virtual void Dispose(bool bolDisposing)
	{
		if (!bolIsDisposed)
		{
			if (bolDisposing)
			{
				Stop();
				if (threadLoop != null)
				{
					threadLoop.Abort();
				}
				Parent = null;
				ts.Stop();
			}
		}
		bolIsDisposed = true;
	}
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}