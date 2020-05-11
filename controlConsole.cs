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
using System.Collections;
using System.Diagnostics;

namespace WinOpenGL_ShaderToy
{
	public partial class controlConsole : UserControl
	{
		public class ConsoleEntry
		{
			public FastColoredTextBox Console;
			public int Occurances { get; set; } = 0;
			public bool Written { get; set; } = false;
			public bool Append { get; set; } = false;
			public string PromptText { get; set; }
			public string RecallText { get; set; }
			public string OutputText { get; set; }
			public string Text
			{
				get => $"{PromptText} {RecallText}\r\n{OutputText}\r\n";
			}
			public string DisplayableText => Text.Replace('\0', ' ');
			public override string ToString()
			{
				return DisplayableText;
			}
		}
		public class ConsoleEntryGroup
		{
			public FastColoredTextBox Console;
			public string LastWrittenText { get; set; } = "";
			public int LastWrittenLocation(bool Append)
			{
				int intRet = -1;
				string strTxt = @Console.Text;
				string strL = @LastWrittenText;
				if (!string.IsNullOrEmpty(LastWrittenText))
				{
					intRet = (Append) ? (strTxt.LastIndexOf(strL)) : (strTxt.IndexOf(strL));
				}
				if (intRet == -1) intRet = (Append) ? (strTxt.Length) : (0);
				return intRet;
			}
			public int LastWrittenLength() { return ((!string.IsNullOrEmpty(LastWrittenText)) ? (@LastWrittenText.Length) : (0)); }
			public string ScopeText { get; set; }
			public string HeaderText
			{
				get
				{
					if (Entries.Count <= 0) return "";
					ConsoleEntry tmp = Entries[Entries.Count - 1];
					return $"{tmp.PromptText} {tmp.RecallText} : {tmp.OutputText}";
				}
			}
			public ConsoleEntry CurrentEntry { get => (Entries.Count > 0) ? (Entries[Entries.Count - 1]) : (null); }
			public List<ConsoleEntry> Entries { get; private set; } = new List<ConsoleEntry>();
			public string Text => Entries.Aggregate("", (str, itm) => str += string.Join("", ArrayList.Repeat(itm.Text, itm.Occurances).ToArray()));
			public string DisplayableText { get => Text.Replace('\0', ' '); }
			public int EntryCount { get => Entries.Aggregate(0, (total, itm) => total += itm.Occurances); }
			public override string ToString()
			{
				if (Entries.Count <= 0) return "";
				ConsoleEntry tmp = Entries[Entries.Count - 1];
				return $"{tmp.PromptText} {tmp.RecallText}{((EntryCount > 1)?($" x{EntryCount}"):(""))}\r\n{tmp.OutputText}";
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
		public Place PromptPosition { get; private set; }
		public int PromptRecallIndex { get; private set; }
		public List<ConsoleEntryGroup> EntryGroups { get; private set; } = new List<ConsoleEntryGroup>();
		private Thread threadWriteGroupEntries;
		private bool bolMainLoop;
		private bool bolInit;
		public controlConsole()
		{
			InitializeComponent();
		}
		private void controlConsole_Load(object sender, EventArgs e)
		{
			bolMainLoop = true;
			threadWriteGroupEntries = new Thread(loopWriteGroupEntries);
			threadWriteGroupEntries.Priority = ThreadPriority.BelowNormal;
			threadWriteGroupEntries.Start();
			bolInit = true;
		}
		private void loopWriteGroupEntries()
		{
			int idxGroup = 0;
			//int idxEntry = 0;
			while(bolMainLoop)
			{
				if(idxGroup < EntryGroups.Count)
				{
					ConsoleEntryGroup group = EntryGroups[idxGroup];
					if(group != null)
					{
						ConsoleEntry entry = group.Entries[group.Entries.Count - 1];
						if (entry != null && !entry.Written)
						{
							try
							{
								Invoke(new Action(() =>
								{
									int loc = group.LastWrittenLocation(entry.Append);
									int locend = loc + group.LastWrittenLength();
									Place placeInsert = Output.PositionToPlace(loc);
									Place placeEnd = Output.PositionToPlace(locend);
									Range rangeInsert = new Range(Output, placeInsert, placeEnd);
									string str = $"{group}\r\n";
									Output.BeginUpdate();
									Output.InsertTextAndRestoreSelection(rangeInsert, str, null);
									Output.EndUpdate();
									entry.Written = true;
									group.LastWrittenText = str;
									if (entry.Append) Output.DoSelectionVisible();
								}));
							}
							catch(Exception err)
							{
								Console.WriteLine(err);
							}
							Application.DoEvents();
							Thread.Sleep(50);
						}
					}
					//idxEntry = (idxEntry + 1) % Math.Max(group.Entries.Count, 1);
					//idxEntry = group.Entries.Count-1;
				}
				idxGroup = (idxGroup + 1) % Math.Max(EntryGroups.Count, 1);
				Application.DoEvents();
			}
		}
		public void Write(object obj, string Label)
		{
			Write(generalUtils.ExpandedObjectString(obj, generalUtils.TypesExpandExempt, true), "Log>", Label, false);
		}
		public void Write(string message, string Label)
		{
			Write(message, "Log>", Label, false);
		}
		[Browsable(false)]
		public void Write(object obj, string prompt, string scope, bool OutputAppend)
		{
			Write(generalUtils.ExpandedObjectString(obj, generalUtils.TypesExpandExempt, true), prompt, scope, OutputAppend);
		}
		[Browsable(false)]
		public void Write(string message, string prompt, string scope, bool OutputAppend)
		{
			ConsoleEntryGroup group = null;
			ConsoleEntry entry = null;
			if (!string.IsNullOrEmpty(prompt) && !string.IsNullOrEmpty(scope) && !string.IsNullOrEmpty(message))
			{
				prompt = prompt.Trim('\0', '\r', '\n');
				scope = scope.Trim('\0', '\r', '\n');
				message = message.Trim('\0', '\r', '\n');
				group = EntryGroups.Find(itm => itm.ScopeText == scope);
				if (group == null)
				{
					group = new ConsoleEntryGroup()
					{
						Console = Output,
						ScopeText = scope
					};
					EntryGroups.Add(group);
				}
				entry = group.Entries.Find(itm => itm.OutputText == message);
				if(entry == null)
				{
					entry = new ConsoleEntry()
					{
						Console = Output,
						Append = OutputAppend,
						PromptText = prompt,
						RecallText = scope,
						OutputText = message
					};
					group.Entries.Add(entry);
				} else
				{
					entry.Written = false;
				}
				entry.Occurances++;
			}
		}
		public string Prompt(string message, string RetainText)
		{
			Invoke(new Action(() => {
				if(!string.IsNullOrEmpty(RetainText)) Input.Clear();
				Input.AppendText($"{message} ");
				PromptPosition = Input.Range.End;
				Input.AppendText($"{RetainText}");
				Input.GoEnd();
			}));
			IsInputWaiting = true;
			PromptRecallIndex = 0;
			while (IsInputWaiting)
			{
				Application.DoEvents();
				Thread.Sleep(250);
			}
			string strRet = new Range(Input, PromptPosition, Input.Range.End).Text.TrimEnd('\r', '\n');
			Input.ClearUndo();
			Input.Clear();
			return strRet;
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
				string str = Prompt(strPrompt, Input.Text);
				if(!string.IsNullOrEmpty(str))
				{
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
			}
			Application.DoEvents();
		}
		private void Output_KeyPressing(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}
		private static TextStyle styleBracketMatch = new TextStyle(new SolidBrush(Color.FromArgb(0,0,0)), 
																   new SolidBrush(Color.FromArgb(255,255,0)),
																   FontStyle.Regular);
		private Range BracketRange = null;
		private void Output_SelectionChanged(object sender, EventArgs e)
		{
			if (BracketRange != null) BracketRange.ClearStyle(styleBracketMatch);
			string str = @Output.Text;
			Range range = Output.Selection;
			int st = Output.PlaceToPosition(range.Start);
			int ed = Output.PlaceToPosition(range.End);
			int intOpen; int intClose;
			int bracketCount = 1;
			while (bracketCount > 0)
			{
				intOpen = str.Substring(0, st).LastIndexOf('{');
				intClose = str.Substring(0, st).LastIndexOf('}');
				if (intOpen > intClose) { st = intOpen; bracketCount--; }
				else if (intClose > intOpen) { st = intClose; bracketCount++; }
				else bracketCount = -1;
			}
			if (bracketCount != 0) return;
			bracketCount = 1;
			while (bracketCount > 0)
			{
				intOpen = str.IndexOf('{', ed);
				intClose = str.IndexOf('}', ed);
				if (intOpen < intClose && intOpen >= 0) { ed = intOpen + 1; bracketCount++; }
				else if (intClose < intOpen && intClose >= 0) { ed = intClose + 1; bracketCount--; }
				else bracketCount = -1;
			}
			if (bracketCount != 0) return;
			BracketRange = new Range(Output, Output.PositionToPlace(st), Output.PositionToPlace(ed));
			BracketRange.SetStyle(styleBracketMatch);
		}
		private bool bolSelectionLock = false;
		private void Input_SelectionChanged(object sender, EventArgs e)
		{
			if (bolSelectionLock) return;
			if (IsInputWaiting)
			{
				Place st = Input.Selection.Start;
				Place ed = Input.Selection.End;
				if (st < PromptPosition) st = PromptPosition;
				if (ed < PromptPosition) ed = PromptPosition;
				bolSelectionLock = true;
				Input.Selection = new Range(Input, st, ed);
				bolSelectionLock = false;
			}
			ResizeInputWindow();
		}
		private void Input_SizeChanged(object sender, EventArgs e)
		{
			ResizeInputWindow();
		}
		private void ResizeInputWindow()
		{
			if (bolInit)
			{
				int intLineCount = Input.LineInfos.Aggregate(0, (count, itm) => count += itm.WordWrapStringsCount);
				Invoke(new Action(() => { Input.Height = intLineCount * (Input.CharHeight + 2); }));
			}
		}
		private void Input_TextChanging(object sender, TextChangingEventArgs e)
		{
			if(IsInputWaiting)
			{
				Range cur = Input.Selection;
				if (cur.Start == PromptPosition && cur.End == PromptPosition)
				{
					if(e.InsertingText == "\b")
					{
						e.InsertingText = null;
						e.Cancel = true;
					}
				}
				if (cur.Start < PromptPosition && cur.End < PromptPosition)
				{
					e.InsertingText = "";
				}
			}
		}
		private void Input_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
			{
				if (IsInputWaiting)
				{
					if (!string.IsNullOrWhiteSpace(new Range(Input, PromptPosition, Input.Range.End).Text.Replace('\0', ' ').Trim()))
					{
						IsInputWaiting = false;
						e.Handled = true;
					}
				}
			}
			if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
			{
				if (Input.Selection.Start >= PromptPosition && Input.Selection.End >= PromptPosition)
				{
					if (IsInputWaiting && EntryGroups.Count > 0)
					{
						int dir = ((e.KeyCode == Keys.Up)?1:0) - ((e.KeyCode == Keys.Down)?1:0);
						string strSetRecall = "";
						while (dir != 0)
						{
							PromptRecallIndex += dir;
							if (PromptRecallIndex < 1) { dir = 0; PromptRecallIndex = 1; continue; }
							if (PromptRecallIndex > EntryGroups.Count) { dir = 0; PromptRecallIndex = EntryGroups.Count; continue; }
							int idx = EntryGroups.Count - PromptRecallIndex;
							if (!EntryGroups[idx].CurrentEntry.Append) { continue; }
							strSetRecall = EntryGroups[idx].ScopeText; dir = 0;
						}
						if(!string.IsNullOrEmpty(strSetRecall))
						{
							Input.BeginUpdate();
							Input.InsertTextAndRestoreSelection(new Range(Input, PromptPosition, Input.Range.End), strSetRecall, null);
							Input.EndUpdate();
							Input.GoEnd();
						}
						e.Handled = true;
					}
				}
			}
			if (e.Control && e.KeyCode == Keys.Delete)
			{
				Clear();
				e.Handled = true;
			}
		}
		public void Clear()
		{
			IsInputWaiting = false;
			Input.Clear();
			Output.Clear();
			EntryGroups.Clear();
			GC.Collect();
		}
		public new void Dispose()
		{
			bolMainLoop = false;
			IsInputWaiting = false;
			IsPromptLoop = false;
			if (threadWriteGroupEntries != null)
			{
				if (threadWriteGroupEntries.IsAlive) threadWriteGroupEntries.Abort();
			}
			if (threadPromptLoop != null)
			{
				while (threadPromptLoop.IsAlive) { }
			}
			EntryGroups.Clear();
			GC.Collect();
			base.Dispose();
		}
	}
}
