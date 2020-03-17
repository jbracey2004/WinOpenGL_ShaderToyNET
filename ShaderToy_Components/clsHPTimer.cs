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
	public delegate void HPIntervalEventHandler(object sender, HPIntervalEventArgs e);
	public event HPIntervalEventHandler IntervalEnd;
	private Stopwatch ts;
	public int SleepInterval { set; get; } = 0;
	private double fInterval = 1000.0;
	public double Interval { get => fInterval; set { fElapsed = 0.0; fInterval = value; } }
	private double fElapsed = 0.0;
	private double fRunTimestamp = 0.0;
	private double fLastRunTimestamp = 0.0;
	private bool bolRunning = false;
	public Control Parent { get; set;}
	private Thread threadLoop;
	public clsHPTimer(Control controlParent)
	{
		Parent = controlParent;
		ts = new Stopwatch();
		ts.Start();
	}
	public void Start()
	{
		if (bolRunning) return;
		fRunTimestamp = ts.Elapsed.TotalMilliseconds;
		bolRunning = true;
		threadLoop = new Thread(Loop);
		threadLoop.Priority = ThreadPriority.BelowNormal;
		threadLoop.Start();
	}
	public void Stop()
	{
		bolRunning = false;
	}
	public void Reset()
	{
		fElapsed = 0.0;
	}
	private void Loop()
	{
		while(bolRunning)
		{
			fLastRunTimestamp = fRunTimestamp;
			fRunTimestamp = ts.Elapsed.TotalMilliseconds;
			double fRunDelta = fRunTimestamp - fLastRunTimestamp;
			fElapsed += fRunDelta;
			if (fElapsed >= Interval)
			{
				try
				{
					if (Parent != null)
					{
						if(!(Parent.IsDisposed || Parent.Disposing))
						{
							HPIntervalEventHandler evnt = IntervalEnd;
							if(evnt != null)
							{
								Parent?.Invoke(evnt, this, new HPIntervalEventArgs(DateTime.Now, fElapsed));
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
				fElapsed = (fElapsed % Interval);
			}
			if (SleepInterval > 0)
			{
				Thread.Sleep(SleepInterval);
			}
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