using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Threading;

namespace WinOpenGL_ShaderToy
{
	public partial class controlConsole : FastColoredTextBox
	{
		public class ConsoleActionArgs : EventArgs
		{
			public string Message { get; set; }
			public bool BreakLoop { get; set; }
		}
		public delegate void ConsoleAction(ref ConsoleActionArgs e);
		public ConsoleAction OnPromptReady { get; set; }
		public ConsoleAction OnPromptReplied { get; set; }
		public controlConsole()
		{
			InitializeComponent();
		}
		public bool IsInputWaiting { get; private set; }
		public bool IsOutputPosting { get; private set; }
		public Place PromtPosition { get; private set; } = new Place();
		public void Write(string message)
		{
			IsInputWaiting = false;
			IsOutputPosting = true;
			try
			{
				AppendText(message);
				GoEnd();
			}
			finally
			{
				IsOutputPosting = false;
				ClearUndo();
			}
		}
		public string Prompt(string message)
		{
			Invoke(new Action(() => {
				GoEnd();
				Write(message);
				PromtPosition = Range.End;
			}));
			IsInputWaiting = true;
			try
			{
				while (IsInputWaiting)
				{
					Application.DoEvents();
					Thread.Sleep(100);
				}
			}
			finally
			{
				IsInputWaiting = false;
				ClearUndo();
			}
			return new Range(this, PromtPosition, Range.End).Text.TrimEnd('\r', '\n');
		}
		private Thread threadPromptLoop;
		public bool IsPromptLoop { get; private set; }
		public void StartPromptLoop(ConsoleAction ActionPromptReady, ConsoleAction ActionPromptReplied)
		{
			if (IsPromptLoop) return;
			OnPromptReady = ActionPromptReady;
			OnPromptReplied = ActionPromptReplied;
			IsPromptLoop = true;
			threadPromptLoop = new Thread(loopPrompt);
			threadPromptLoop.Priority = ThreadPriority.BelowNormal;
			threadPromptLoop.Start();
		}
		public void StopPromptLoop()
		{
			if (!IsPromptLoop) return;
			IsPromptLoop = false;
			IsInputWaiting = false;
			threadPromptLoop.Abort();
			threadPromptLoop = null;
		}
		private void loopPrompt()
		{
			ConsoleActionArgs ActionArgs = new ConsoleActionArgs();
			while(IsPromptLoop)
			{
				ActionArgs.Message = "";
				ActionArgs.BreakLoop = false;
				Invoke(OnPromptReady, ActionArgs);
				Application.DoEvents();
				if(ActionArgs.BreakLoop)
				{
					IsPromptLoop = false;
					break;
				}
				string str  = Prompt(ActionArgs.Message);
				ActionArgs.Message = str;
				ActionArgs.BreakLoop = false;
				Application.DoEvents();
				Invoke(OnPromptReplied, ActionArgs);
				if (ActionArgs.BreakLoop)
				{
					IsPromptLoop = false;
					break;
				}
			}
			Application.DoEvents();
		}
		public override void OnTextChanging(ref string text)
		{
			if (!IsInputWaiting && !IsOutputPosting)
			{
				text = "";
				return;
			}

			if (IsInputWaiting)
			{
				if (Selection.Start < PromtPosition || Selection.End < PromtPosition) GoEnd();
				if (Selection.Start == PromtPosition || Selection.End == PromtPosition)
				{
					if (text == "\b")
					{
						text = "";
						return;
					}
				}
				if (text != null && text.EndsWith("\n"))
				{
					text = text.Substring(0, text.IndexOf('\n') + 1);
					IsInputWaiting = false;
				}
			}
			base.OnTextChanging(ref text);
		}
		public override void Clear()
		{
			var oldIsReadMode = IsInputWaiting;
			IsInputWaiting = false;
			IsOutputPosting = true;
			base.Clear();
			IsOutputPosting = false;
			IsInputWaiting = oldIsReadMode;
			PromtPosition = Place.Empty;
		}
	}
}
