using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using static generalUtils;

public class clsHPTimer : IDisposable
{
	public class HPIntervalEventArgs : EventArgs
	{
		public DateTime TimeStamp;
		public HPIntervalEventArgs(DateTime ts)
		{
			TimeStamp = ts;
		}
	}
	public delegate void HPIntervalEventHandler(object sender, HPIntervalEventArgs e);
	public event HPIntervalEventHandler IntervalEnd;
	private Stopwatch ts;
	public int SleepInterval { set; get; } = 0;
	public double Interval { set; get; } = 1000.0;
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
		threadLoop.Priority = ThreadPriority.Normal;
		threadLoop.Start();
	}
	public void Stop()
	{
		bolRunning = false;
		while (threadLoop != null) { Application.DoEvents(); }
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
			if (fElapsed > Interval)
			{
				fElapsed = 0;
				try
				{
					if (Parent != null)
					{
						if(!(Parent.IsDisposed || Parent.Disposing))
						{
							HPIntervalEventHandler evnt = IntervalEnd;
							if(evnt != null)
							{
								Parent?.BeginInvoke(evnt, this, new HPIntervalEventArgs(DateTime.Now));
							}
							if (SleepInterval > 0)
							{
								Thread.Sleep(SleepInterval);
							}
							Application.DoEvents();
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
				Parent = null;
				ts.Stop();
				Stop();
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