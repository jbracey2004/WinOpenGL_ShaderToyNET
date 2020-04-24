﻿using System;
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
	public partial class controlConsole : UserControl
	{
		protected static string strParsePattern = @"(?<PromptText>((.*?)(\s{0,}))+)\0" +
												  @"(?<RecallText>((.*?)(\s{0,}))+)\0\s+" +
												  @"(?<OutputText>((.*?)(\s{0,}))+)\0";
		public class ConsoleEntry
		{
			public FastColoredTextBox Console;
			public bool Written = false;
			public bool Append = false;
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
			public FastColoredTextBox Console;
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
				if (Entries.Count <= 0) return "";
				ConsoleEntry tmp = Entries[Entries.Count - 1];
				return $"{tmp.PromptText} {tmp.RecallText}{((Entries.Count > 1)?($" x{Entries.Count}"):(""))}\n{tmp.OutputText}";
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
							try
							{
								Invoke(new Action(() =>
								{
									int loc = group.LastWrittenLocation;
									if (loc == -1) loc = (entry.Append)?(Output.TextLength):(0);
									int len = group.LastWrittenLength;
									Place placeInsert = Output.PositionToPlace(loc);
									Place placeEnd = Output.PositionToPlace(loc + len + 1);
									Range rangeInsert = new Range(Output, placeInsert, placeEnd);
									string str = $"{group}\r\n";
									Output.BeginUpdate();
									Output.InsertTextAndRestoreSelection(rangeInsert, str, null);
									Output.EndUpdate();
									entry.Written = true;
									group.LastWrittenText = str;
								}));
							}
							catch (Exception err)
							{
								Console.WriteLine(err);
							}
						}
					}
					idxGroup = (idxGroup + 1) % Math.Max(EntryGroups.Count, 1);
				}
				idxGroup = idxGroup % Math.Max(EntryGroups.Count, 1);
				Application.DoEvents();
				Thread.Sleep(50);
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
				entry = new ConsoleEntry()
				{
					Console = Output,
					Append = OutputAppend,
					PromptText = prompt.Trim('\0', '\r', '\n'),
					RecallText = scope.Trim('\0', '\r', '\n'),
					OutputText = message.Trim('\0', '\r', '\n')
				};
				group.Entries.Add(entry);
			}
		}
		public string Prompt(string message)
		{
			Invoke(new Action(() => {
				Input.AppendText($"{message}\0");
				Input.GoEnd();
			}));
			IsInputWaiting = true;
			PromptRecallIndex = 0;
			PromptPosition = Input.Range.End;
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
				string str = Prompt(strPrompt);
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
		private void Input_SelectionChanged(object sender, EventArgs e)
		{
			if (IsInputWaiting)
			{
				Range cur = Input.Selection;
				if (cur.Start < PromptPosition) cur.Start = PromptPosition;
				if (cur.End < PromptPosition) cur.End = PromptPosition;
			}
			if(bolInit)
			{
				int intLineCount = Input.LineInfos.Aggregate(0, (count, itm) => count += itm.WordWrapStringsCount);
				Invoke(new Action(() => { Input.Height = intLineCount * (Input.CharHeight + 1); }));
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
						if (e.KeyCode == Keys.Up) PromptRecallIndex = Math.Min(PromptRecallIndex + 1, EntryGroups.Count);
						if (e.KeyCode == Keys.Down) PromptRecallIndex = Math.Max(PromptRecallIndex - 1, 1);
						string strSetRecall = EntryGroups[EntryGroups.Count - PromptRecallIndex].ScopeText;
						Input.BeginUpdate();
						Input.InsertTextAndRestoreSelection(new Range(Input, PromptPosition, Input.Range.End), strSetRecall, null);
						Input.EndUpdate();
						Input.GoEnd();
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
