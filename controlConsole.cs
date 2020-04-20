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
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace WinOpenGL_ShaderToy
{
	public partial class controlConsole : FastColoredTextBox
	{
		public class ConsoleEntry
		{
			public string PromptText { get; set; }
			public string RecallText { get; set; }
			public string OutputText { get; set; }
			public string Text
			{
				get => $"{PromptText}\0{RecallText}\0\n{OutputText}\0\n";
				set
				{
					Match match = Regex.Match(value, strParsePattern);
					if (!match.Success) return;
					if (match.Groups["PromptText"] != null) PromptText = match.Groups["PromptText"].Value;
					if (match.Groups["RecallText"] != null) PromptText = match.Groups["RecallText"].Value;
					if (match.Groups["OutputText"] != null) PromptText = match.Groups["OutputText"].Value;
				}
			}
			public string DisplayableText => Text.Replace('\0', ' ');
			public override string ToString()
			{
				return DisplayableText;
			}
		}
		public class ConsoleEntryGroup
		{
			public string ScopeText { get; set; }
			public string HeaderText
			{
				get
				{
					if(Entries.Count <= 0) return "";
					ConsoleEntry tmp = Entries[Entries.Count - 1];
					return $"{tmp.PromptText} {tmp.RecallText} : {tmp.OutputText}";
				}
			}
			public List<ConsoleEntry> Entries { get; private set; } = new List<ConsoleEntry>();
			public string Text => Entries.Aggregate("", (str, itm) => str += itm.Text);
			public string DisplayableText => Text.Replace('\0', ' ');
			public override string ToString()
			{
				return $"{HeaderText} x{Entries.Count}";
			}
		}
		public class ConsoleActionArgs : EventArgs
		{
			public string Message { get; set; }
			public bool BreakLoop { get; set; }
		}
		public delegate void ConsoleAction(ref ConsoleActionArgs e);
		public ConsoleAction OnPromptReady { get; set; }
		public ConsoleAction OnPromptReplied { get; set; }
		public bool IsInputWaiting { get; private set; }
		public bool IsOutputPosting { get; private set; }
		public int PromptRecallIndex { get; private set; }
		public Place PromptPosition { get; private set; } = new Place();
		public List<ConsoleEntryGroup> EntryGroups { get; private set; } = new List<ConsoleEntryGroup>();
		public controlConsole()
		{
			InitializeComponent();
		}
		public void Write(object obj, string prompt, string scope)
		{
			Write(generalUtils.ExpandedObjectString(obj, generalUtils.TypesExpandExempt, true), prompt, scope);
		}
		public void Write(string message, string prompt, string scope)
		{
			IsOutputPosting = true;
			ConsoleEntryGroup group = null;
			if (!string.IsNullOrEmpty(prompt) && !string.IsNullOrEmpty(scope) && !string.IsNullOrEmpty(message))
			{
				group = EntryGroups.Find(itm => itm.ScopeText == scope);
				if (group == null)
				{
					group = new ConsoleEntryGroup()
					{
						ScopeText = scope
					};
					EntryGroups.Add(group);
				}
				group.Entries.Add(new ConsoleEntry()
				{
					PromptText = prompt.Trim('\0', '\n'),
					RecallText = scope.Trim('\0', '\n'),
					OutputText = message.Trim('\0', '\n')
				});
			}
			if (IsInputWaiting)
			{
				message = $"{prompt}{scope}\0\n{message}\0\n";
				/*bool bolIsAtPrompt = (Selection.Start >= PromptPosition && Selection.End >= PromptPosition);
				int[] posAfterPrompt = new int[2];
				if (bolIsAtPrompt) 
				{
					posAfterPrompt[0] = PlaceToPosition(Selection.Start) - PlaceToPosition(PromptPosition);
					posAfterPrompt[1] = PlaceToPosition(Selection.End) - PlaceToPosition(PromptPosition);
				}
				MatchCollection collLines = Regex.Matches(Text.Substring(0, PlaceToPosition(new Place(0, PromptPosition.iLine))), strParsePattern);
				Match[] aryLines = new Match[collLines.Count];
				collLines.CopyTo(aryLines, 0); collLines = null;
				Match LineMatch = aryLines.LastOrDefault(itm =>
				{
					if (!itm.Groups["RecallText"].Success) return false;
					if (!itm.Groups["OutputText"].Success) return false;
					if (itm.Groups["RecallText"].Value.Trim() == scope.Trim()) return true;
					if (itm.Groups["OutputText"].Value.Trim() == message.Trim()) return true;
					return false;
				});
				int intInsert = PlaceToPosition(new Place(0,PromptPosition.iLine));
				if(LineMatch != null)
				{
					intInsert = LineMatch.Index + LineMatch.Length + 2;
				}
				Place placeInsert = PositionToPlace(intInsert);
				Range rangeInsert = new Range(this, placeInsert, placeInsert);
				BeginUpdate();
				InsertTextAndRestoreSelection(rangeInsert, message, null);
				EndUpdate();
				int intIndexPromptPosition = Text.LastIndexOf('\0') + 1;
				PromptPosition = PositionToPlace(intIndexPromptPosition);
				if (bolIsAtPrompt)
				{
					Selection = new Range(this, PositionToPlace(posAfterPrompt[0] + intIndexPromptPosition), 
												PositionToPlace(posAfterPrompt[1] + intIndexPromptPosition));
					DoSelectionVisible();
				}*/
			} else
			{
				AppendText(message);
			}
			IsOutputPosting = false;
		}
		public string Prompt(string message)
		{
			Invoke(new Action(() => {
				GoEnd();
				Write($"{message}\0","", "");
				GoEnd();
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
				string strPrompt = ActionArgs.Message;
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
		protected static string strParsePattern = @"(?<PromptText>((.*?)(\s{0,}))+)\0" +
												  @"(?<RecallText>((.*?)(\s{0,}))+)\0\s+" +
												  @"(?<OutputText>((.*?)(\s{0,}))+)\0";
		public override void OnTextChanging(ref string text)
		{
			if (IsInputWaiting)
			{
				if (Selection.Start < PromptPosition || Selection.End < PromptPosition) 
				{
					if(!IsOutputPosting)
					{
						text = "";
						GoEnd();
						return;
					}
				}
				if (Selection.Start == PromptPosition || Selection.End == PromptPosition)
				{
					if (text == "\b")
					{
						text = "";
						GoEnd();
						return;
					}
				}
			}
			base.OnTextChanging(ref text);
		}
		public override void OnTextChangedDelayed(Range changedRange)
		{
			base.OnTextChangedDelayed(changedRange);
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
						if (EntryGroups.Count > 0)
						{
							IsOutputPosting = true;
							if (e.KeyCode == Keys.Up) PromptRecallIndex = Math.Min(PromptRecallIndex + 1, EntryGroups.Count);
							if (e.KeyCode == Keys.Down) PromptRecallIndex = Math.Max(PromptRecallIndex - 1, 1);
							string strSetRecall = EntryGroups[EntryGroups.Count - PromptRecallIndex].ScopeText;
							string strOldText = Text.Substring(0, PlaceToPosition(PromptPosition));
							GoEnd();
							BeginUpdate();
							Text = strOldText + strSetRecall;
							EndUpdate();
							GoEnd();
							IsOutputPosting = false;
							return;
						}
					}
				}
			}
			if(e.Control && e.KeyCode == Keys.Delete)
			{
				Clear();
			}
			base.OnKeyDown(e);
		}
		public override void Clear()
		{
			if (!IsInputWaiting) { base.Clear(); return; }
			int intLinePrompt = PlaceToPosition(new Place(0, PromptPosition.iLine));
			IsOutputPosting = true;
			BeginUpdate();
			Text = Text.Substring(intLinePrompt, Text.Length - intLinePrompt);
			EndUpdate();
			IsOutputPosting = false;
			int intIndexPromptPosition = Text.LastIndexOf('\0') + 1;
			PromptPosition = PositionToPlace(intIndexPromptPosition);
			GoEnd();
		}
		public new void Dispose()
		{
			IsInputWaiting = false;
			IsPromptLoop = false;
			if(threadPromptLoop != null)
			{
				while (threadPromptLoop.IsAlive) { }
			}
			base.Dispose();
		}
	}
}
