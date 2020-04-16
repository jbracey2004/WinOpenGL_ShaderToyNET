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
using System.Text.RegularExpressions;

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
		public int PromptRecallIndex { get; private set; }
		public Place PromptPosition { get; private set; } = new Place();
		public void Write(object obj, string scope = "")
		{
			Write(generalUtils.ExpandedObjectString(obj, generalUtils.TypesExpandExempt, true), scope);
		}
		public void Write(string message, string scope = "")
		{
			IsOutputPosting = true;
			if (IsInputWaiting)
			{
				if (string.IsNullOrEmpty(scope)) scope = $"{DateTime.Now} {DateTime.Now.Ticks}";
				message = $"Log>\0{scope}\0\n{message}\0\n";
				int intInsert = PlaceToPosition(new Place(0,PromptPosition.iLine));
				Text = Text.Insert(intInsert, message);
				PromptPosition = PositionToPlace(Text.LastIndexOf('\0') + 1);
			} else
			{
				AppendText(message);
			}
			GoEnd();
			IsOutputPosting = false;
		}
		public string Prompt(string message)
		{
			Invoke(new Action(() => {
				GoEnd();
				Write($"{message}\0");
				PromptPosition = Range.End;
			}));
			IsInputWaiting = true;
			PromptRecallIndex = 0;
			while (IsInputWaiting)
			{
				Application.DoEvents();
				Thread.Sleep(100);
			}
			
			IsInputWaiting = false;
			ClearUndo();
			return new Range(this, PromptPosition, Range.End).Text.TrimEnd('\r', '\n');
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
				string str = Prompt(ActionArgs.Message);
				Invoke(new Action(() => {AppendText("\b\0\n");}));
				ActionArgs.Message = str;
				ActionArgs.BreakLoop = false;
				Application.DoEvents();
				Invoke(OnPromptReplied, ActionArgs);
				Invoke(new Action(() => { AppendText("\b\0\n"); }));
				if (ActionArgs.BreakLoop)
				{
					IsPromptLoop = false;
					break;
				}
			}
			Application.DoEvents();
		}
		protected static string strParsePattern = $@"(?<PromptText>((.*?)(\s{{0,}}))+)\0" +
												  $@"(?<RecallText>((.*?)(\s{{0,}}))+)\0\s+" +
												  $@"(?<OutputText>((.*?)(\s{{0,}}))+)\0";
		public override void OnTextChanging(ref string text)
		{
			if (IsInputWaiting)
			{
				if (Selection.Start < PromptPosition || Selection.End < PromptPosition) 
				{
					if(!IsOutputPosting)
					{
						text = ""; 
						return;
					}
				}
				if (Selection.Start == PromptPosition || Selection.End == PromptPosition)
				{
					if (text == "\b")
					{
						text = "";
						return;
					}
				}
			}
			base.OnTextChanging(ref text);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (IsInputWaiting)
			{
				if (Selection.Start >= PromptPosition)
				{
					if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
					{
						if(IsInputWaiting) 
						{
							if(Selection.Start == Range.End || Selection.End == Range.End)
							{
								IsInputWaiting = false;
							}
						}
					}
					if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
					{
						string strRecallText = Text.Substring(0, PlaceToPosition(new Place(0, PromptPosition.iLine)));
						MatchCollection aryRecallLines = Regex.Matches(strRecallText, strParsePattern);
						if (aryRecallLines.Count > 0)
						{
							IsOutputPosting = true;
							if (e.KeyCode == Keys.Up) PromptRecallIndex = Math.Min(PromptRecallIndex + 1, aryRecallLines.Count);
							if (e.KeyCode == Keys.Down) PromptRecallIndex = Math.Max(PromptRecallIndex - 1, 1);
							string strSetRecall = aryRecallLines[aryRecallLines.Count - PromptRecallIndex].Groups["RecallText"].Value;
							string strOldText = Text.Substring(0, PlaceToPosition(PromptPosition));
							GoEnd();
							Text = strOldText + strSetRecall;
							GoEnd();
							IsOutputPosting = false;
							return;
						}
					}
				}
			}
			base.OnKeyDown(e);
		}
		public override void Clear()
		{
			var oldIsReadMode = IsInputWaiting;
			IsInputWaiting = false;
			IsOutputPosting = true;
			base.Clear();
			IsOutputPosting = false;
			IsInputWaiting = oldIsReadMode;
			PromptPosition = Place.Empty;
		}
	}
}
