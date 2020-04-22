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
using Microsoft.CodeAnalysis.Operations;

namespace WinOpenGL_ShaderToy
{
	public partial class controlConsole : FastColoredTextBox
	{
		public class ConsoleEntry
		{
			public controlConsole Console;
			public bool Written = false;
			public string PromptText { get; set; }
			public string RecallText { get; set; }
			public string OutputText { get; set; }
			public string Text
			{
				get => $"{PromptText}\0{RecallText}\0\r\n{OutputText}\0\r\n";
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
			public controlConsole Console;
			public string LastWrittenText = "";
			public int LastWrittenLocation => ((!string.IsNullOrEmpty(LastWrittenText)) ? (Console.Text.Replace('\0', ' ').LastIndexOf(LastWrittenText)) : (-1));
			public int LastWrittenLength => ((!string.IsNullOrEmpty(LastWrittenText)) ? (LastWrittenText.Length) : (0));
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
		private Thread threadWriteGroupEntries;
		private bool bolMainLoop;
		private bool bolWriteLock = false;
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
		}
		private void loopWriteGroupEntries()
		{
			int idxGroup = 0;
			while(bolMainLoop)
			{
				if(idxGroup < EntryGroups.Count)
				{
					ConsoleEntryGroup group = EntryGroups[idxGroup];
					if(group.Entries.Count > 0)
					{
						ConsoleEntry entry = group.Entries[group.Entries.Count - 1];
						if (entry != null && !entry.Written)
						{
							while (bolWriteLock) { Application.DoEvents(); }
							bolWriteLock = true;
							try
							{
								Invoke(new Action(() =>
								{
									int intIndexPromptPosition;
									bool bolIsAtPrompt = (Selection.Start >= PromptPosition && Selection.End >= PromptPosition);
									int[] posAfterPrompt = new int[2];
									if (bolIsAtPrompt)
									{
										posAfterPrompt[0] = PlaceToPosition(Selection.Start) - PlaceToPosition(PromptPosition);
										posAfterPrompt[1] = PlaceToPosition(Selection.End) - PlaceToPosition(PromptPosition);
									}
									int loc = group.LastWrittenLocation;
									if (loc == -1) loc = PlaceToPosition(new Place(0, PromptPosition.iLine));
									int len = group.LastWrittenLength;
									Place placeInsert = PositionToPlace(loc);
									Place placeEnd = PositionToPlace(loc + len);
									Range rangeInsert = new Range(this, placeInsert, placeEnd);
									string str = $"{group}\r\n";
									IsOutputPosting = true;
									BeginUpdate();
									InsertTextAndRestoreSelection(rangeInsert, str, null);
									EndUpdate();
									IsOutputPosting = false;
									entry.Written = true;
									group.LastWrittenText = str;
									if (bolIsAtPrompt)
									{
										intIndexPromptPosition = Text.LastIndexOf('\0') + 1;
										PromptPosition = PositionToPlace(intIndexPromptPosition);
										Selection = new Range(this, PositionToPlace(posAfterPrompt[0] + intIndexPromptPosition),
																	PositionToPlace(posAfterPrompt[1] + intIndexPromptPosition));
										DoSelectionVisible();
									}
									//Application.DoEvents();
								}));
							}
							catch (Exception err)
							{
								Console.WriteLine(err);
							}
							bolWriteLock = false;
						}
					}
					idxGroup = (idxGroup + 1) % Math.Max(EntryGroups.Count, 1);
				}
				idxGroup = idxGroup % Math.Max(EntryGroups.Count, 1);
				Application.DoEvents();
				Thread.Sleep(20);
			}
		}
		public void Write(object obj, string Label)
		{
			Write(generalUtils.ExpandedObjectString(obj, generalUtils.TypesExpandExempt, true), "Log>", Label, true);
		}
		public void Write(string message, string Label)
		{
			Write(message, "Log>", Label, true);
		}
		[Browsable(false)]
		public void Write(object obj, string prompt, string scope, bool IsOutputAsync)
		{
			Write(generalUtils.ExpandedObjectString(obj, generalUtils.TypesExpandExempt, true), prompt, scope, IsOutputAsync);
		}
		[Browsable(false)]
		public void Write(string message, string prompt, string scope, bool IsOutputAsync)
		{
			ConsoleEntryGroup group = null;
			ConsoleEntry entry = null;
			if (!string.IsNullOrEmpty(prompt) && !string.IsNullOrEmpty(scope) && !string.IsNullOrEmpty(message))
			{
				group = EntryGroups.Find(itm => itm.ScopeText == scope);
				if (group == null)
				{
					group = new ConsoleEntryGroup()
					{
						Console = this,
						ScopeText = scope
					};
					EntryGroups.Add(group);
				}
				entry = new ConsoleEntry()
				{
					Console = this,
					Written = !IsOutputAsync,
					PromptText = prompt.Trim('\0', '\r', '\n'),
					RecallText = scope.Trim('\0', '\r', '\n'),
					OutputText = message.Trim('\0', '\r', '\n')
				};
				group.Entries.Add(entry);
			}
			if (!IsOutputAsync)
			{
				while (bolWriteLock) { Application.DoEvents(); }
				bolWriteLock = true;
				IsOutputPosting = true;
				int loc = PlaceToPosition(Range.End);
				AppendText(message);
				IsOutputPosting = false;
				bolWriteLock = false;
			}
		}
		public string Prompt(string message)
		{
			while (bolWriteLock) { Application.DoEvents(); }
			bolWriteLock = true;
			Invoke(new Action(() => {
				GoEnd();
				AppendText($"{message}\0");
				GoEnd();
				PromptPosition = Range.End;
			}));
			bolWriteLock = false;
			IsInputWaiting = true;
			PromptRecallIndex = 0;
			while (IsInputWaiting)
			{
				Application.DoEvents();
				Thread.Sleep(250);
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
				string str = Prompt(strPrompt);
				if(!string.IsNullOrEmpty(str))
				{
					while(bolWriteLock) { Application.DoEvents(); }
					bolWriteLock = true;
					Invoke(new Action(() => {AppendText("\b\0\n");}));
					bolWriteLock = false;
					ActionArgs.Message = str;
					ActionArgs.BreakLoop = false;
					Application.DoEvents();
					Invoke(OnPromptReplied, ActionArgs);
					while(bolWriteLock) { Application.DoEvents(); }
					bolWriteLock = true;
					Invoke(new Action(() => { AppendText("\b\0\n"); }));
					bolWriteLock = false;
					if (ActionArgs.BreakLoop)
					{
						IsPromptLoop = false;
						break;
					}
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
						while(bolWriteLock) { Application.DoEvents(); }
						bolWriteLock = true;
						text = "";
						GoEnd();
						bolWriteLock = false;
						return;
					}
				}
				if (Selection.Start == PromptPosition || Selection.End == PromptPosition)
				{
					if (text == "\b")
					{
						while (bolWriteLock) { Application.DoEvents(); }
						bolWriteLock = true;
						text = "";
						GoEnd();
						bolWriteLock = false;
						return;
					}
				}
			}
			base.OnTextChanging(ref text);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
			{
				if(IsInputWaiting) 
				{
					if(Selection.Start >= PromptPosition && Selection.End >= PromptPosition)
					{
						if(Selection.Start == Range.End || Selection.End == Range.End)
						{
							if(!string.IsNullOrWhiteSpace(new Range(this, PromptPosition, Selection.End).Text.Replace('\0', ' ').Trim()))
							{
								IsInputWaiting = false;
							}
						}
					}
				}
			}
			if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
			{
				if(Selection.Start >= PromptPosition && Selection.End >= PromptPosition)
				{
					if (IsInputWaiting && EntryGroups.Count > 0)
					{
						if (e.KeyCode == Keys.Up) PromptRecallIndex = Math.Min(PromptRecallIndex + 1, EntryGroups.Count);
						if (e.KeyCode == Keys.Down) PromptRecallIndex = Math.Max(PromptRecallIndex - 1, 1);
						string strSetRecall = EntryGroups[EntryGroups.Count - PromptRecallIndex].ScopeText;
						string strOldText = Text.Substring(0, PlaceToPosition(PromptPosition));
						while (bolWriteLock) { Application.DoEvents(); }
						bolWriteLock = true;
						IsOutputPosting = true;
						GoEnd();
						BeginUpdate();
						Text = strOldText + strSetRecall;
						EndUpdate();
						GoEnd();
						IsOutputPosting = false;
						bolWriteLock = false;
						return;
					}
				}
			}
			if(e.Control && e.KeyCode == Keys.Delete)
			{
				Clear();
				return;
			}
			base.OnKeyDown(e);
		}
		public override void Clear()
		{
			while (bolWriteLock) { Application.DoEvents(); }
			bolWriteLock = true;
			IsOutputPosting = true;
			base.Clear();
			IsOutputPosting = false;
			EntryGroups.Clear();
			PromptPosition = Place.Empty;
			IsInputWaiting = false;
			bolWriteLock = false;
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
			base.Dispose();
		}
	}
}
