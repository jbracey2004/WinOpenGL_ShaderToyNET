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
	public infoFramePerformance Performence { private set; get; }
	public int SleepInterval { set; get; } = 0;
	public double Interval { set; get; } = 1000.0;
	private double fElapsed = 0.0;
	private double fRunTimestamp = 0.0;
	private double fLastRunTimestamp = 0.0;
	private bool bolRunning = false;
	public Control Parent;
	private Thread threadLoop;
	public clsHPTimer(Control controlParent)
	{
		Parent = controlParent;
		Performence = new infoFramePerformance();
		ts = new Stopwatch();
		ts.Start();
	}
	public void Start()
	{
		if (bolRunning) return;
		fRunTimestamp = ts.Elapsed.TotalMilliseconds;
		bolRunning = true;
		threadLoop = new Thread(Loop);
		threadLoop.Priority = ThreadPriority.Lowest;
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
			Performence.SampleFrame((float)fRunDelta*0.001f, (float)fRunTimestamp*0.001f);
			if (fElapsed >= Interval)
			{
				fElapsed = 0.0;
				try
				{
					if(Parent != null)
					{
						if(!(Parent.IsDisposed || Parent.Disposing))
						{
							HPIntervalEventHandler evnt = IntervalEnd;
							if(evnt != null)
							{
								Application.DoEvents();
								Parent.Invoke(evnt, this, new HPIntervalEventArgs(DateTime.Now));
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
			}
			if(SleepInterval > 0)
			{
				Thread.Sleep(SleepInterval);
			}
			//Application.DoEvents();
		}
		threadLoop = null;
	}
	public void Dispose()
	{
		this.Stop();
		this.Parent = null;
		this.Performence = null;
	}
}