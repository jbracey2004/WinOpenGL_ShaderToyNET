using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinOpenGL_ShaderToy
{
	class clsCollapsePanel
	{
		internal class propsSplitter
		{
			public propsSplitter(clsCollapsePanel main)
			{
				CollapsePanel = main ?? throw new ArgumentNullException("Panel");
			}
			private Splitter objSplitter;
			public Splitter Splitter
			{
				internal set
				{
					objSplitter = value;
					if(objSplitter != null)
					{
						if (!IsCollapsed)
						{
							UnCollapedDist = Splitter.SplitPosition;
						}
					}
				}
				get
				{
					return objSplitter;
				}
			}
			public Panel Panel { internal set; get; }
			public clsCollapsePanel CollapsePanel { internal set; get; }
			public SplitterEventHandler SplitterMove { internal set; get; }
			public MouseEventHandler SplitterMouseEvent { internal set; get; }
			public MouseEventHandler SplitterDoubleClickEvent { internal set; get; }
			private bool bolIsCollapsed;
			public bool IsCollapsed
			{
				set
				{
					if(value)
					{
						Splitter.SplitPosition = 0;
					} else
					{
						Splitter.SplitPosition = UnCollapedDist;
					}
					if (value != bolIsCollapsed)
					{
						bolIsCollapsed = value;
						CollapseStateChangeHandler evnt = CollapsePanel.CollapseStateChanged;
						if(evnt != null)
						{
							Application.DoEvents();
							CollapsePanel.MainPanel.Invoke(evnt, this, new EventArgs());
						}
					}
				}
				get
				{
					return bolIsCollapsed;
				}
			}
			public int UnCollapedDist { internal set; get; }
			internal void OnSplitterMove(object sender, SplitterEventArgs e)
			{
				if (Splitter == null) return;
				if (IsCollapsed)
				{
					if(Splitter.Dock == DockStyle.Left || Splitter.Dock == DockStyle.Right)
					{
						e.SplitX = (Splitter.Dock == DockStyle.Right) ? (CollapsePanel.MainPanel.ClientSize.Width) :  (0);
					}
					if (Splitter.Dock == DockStyle.Top || Splitter.Dock == DockStyle.Bottom)
					{
						e.SplitY = (Splitter.Dock == DockStyle.Bottom) ? (CollapsePanel.MainPanel.ClientSize.Height) : (0); ;
					}
				}
			}
			internal void OnSplitterMouseDown(object sender, MouseEventArgs e)
			{
				if (Splitter == null) return;
				if(IsCollapsed)
				{
					Splitter.SplitPosition = 0;
				} else
				{
					if(UnCollapedDist != Splitter.SplitPosition)
					{
						UnCollapedDist = Splitter.SplitPosition;
						CollapseStateChangeHandler evnt = CollapsePanel.CollapseDistanceChanged;
						if (evnt != null)
						{
							Application.DoEvents();
							CollapsePanel.MainPanel.Invoke(evnt, this, new EventArgs());
						}
					}
				}
			}
			internal void OnSplitterDoubleClick(object sender, MouseEventArgs e)
			{
				if (Splitter == null) return;
				if(IsCollapsed)
				{
					Splitter.SplitPosition = UnCollapedDist;
					IsCollapsed = false;
				} else
				{
					UnCollapedDist = Splitter.SplitPosition;
					IsCollapsed = true;
					Splitter.SplitPosition = 0;
				}
			}
			public override string ToString()
			{
				return $"{((Splitter != null)?($"Splitter: {{{Splitter.Name}}}; "):(""))} {((Panel != null) ? ($"Panel: {{{Panel.Name}}}; ") : (""))}";
			}
		}
		private Panel panelMain;
		public List<propsSplitter> Splitters { private set; get; } = new List<propsSplitter>();
		public delegate void CollapseStateChangeHandler(object sender, EventArgs e);
		public event CollapseStateChangeHandler CollapseStateChanged;
		public event CollapseStateChangeHandler CollapseDistanceChanged;
		public clsCollapsePanel(Panel controlMain)
		{
			MainPanel = controlMain ?? throw new ArgumentNullException("Panel");
		}
		public Panel MainPanel
		{
			set
			{
				panelMain = value ?? throw new ArgumentNullException("Panel");
				foreach(propsSplitter itrSplitter in Splitters)
				{
					itrSplitter.Splitter.SplitterMoved -= itrSplitter.SplitterMove;
					itrSplitter.Splitter.SplitterMoving -= itrSplitter.SplitterMove;
					itrSplitter.Splitter.MouseDown -= itrSplitter.SplitterMouseEvent;
					itrSplitter.Splitter.MouseDoubleClick -= itrSplitter.SplitterDoubleClickEvent;
				}
				Splitters.Clear();
				foreach(Control itrControl in panelMain.Controls)
				{
					Splitter itrSplitter = itrControl as Splitter;
					if(itrSplitter != null)
					{
						if (itrSplitter.Dock == DockStyle.Left || itrSplitter.Dock == DockStyle.Right ||
						itrSplitter.Dock == DockStyle.Top || itrSplitter.Dock == DockStyle.Bottom)
						{
							propsSplitter propsNew = new propsSplitter(this);
							propsNew.Splitter = itrSplitter;
							propsNew.SplitterMove = new SplitterEventHandler(propsNew.OnSplitterMove);
							propsNew.SplitterMouseEvent = new MouseEventHandler(propsNew.OnSplitterMouseDown);
							propsNew.SplitterDoubleClickEvent = new MouseEventHandler(propsNew.OnSplitterDoubleClick);
							propsNew.Splitter.SplitterMoved += propsNew.SplitterMove;
							propsNew.Splitter.SplitterMoving += propsNew.SplitterMove;
							propsNew.Splitter.MouseDown += propsNew.SplitterMouseEvent;
							propsNew.Splitter.MouseDoubleClick += propsNew.SplitterDoubleClickEvent;
							foreach (Control itrChild in panelMain.Controls)
							{
								Panel itrPanel = itrChild as Panel;
								if(itrPanel != null)
								{
									if (itrPanel.Dock == itrSplitter.Dock)
									{
										bool bolValid = false;
										switch (itrSplitter.Dock)
										{
											case DockStyle.Left:
												{
													if (Math.Abs(itrSplitter.Left - itrPanel.Right) <= 2) bolValid = true;
													break;
												}
											case DockStyle.Right:
												{
													if (Math.Abs(itrSplitter.Right - itrPanel.Left) <= 2) bolValid = true;
													break;
												}
											case DockStyle.Top:
												{
													if (Math.Abs(itrSplitter.Top - itrPanel.Bottom) <= 2) bolValid = true;
													break;
												}
											case DockStyle.Bottom:
												{
													if (Math.Abs(itrSplitter.Bottom - itrPanel.Top) <= 2) bolValid = true;
													break;
												}
										}
										if (bolValid)
										{
											propsNew.Panel = itrPanel;
										}
									}
								}
							}
							if (propsNew.Panel != null)
							{
								if (propsNew.Panel.Width == 0 || propsNew.Panel.Height == 0)
								{
									propsNew.IsCollapsed = true;
								}
								else
								{
									propsNew.IsCollapsed = false;
								}
							}
							else
							{
								propsNew.IsCollapsed = true;
							}
							Splitters.Add(propsNew);
						}
					}
				}
			}
			get
			{
				return panelMain;
			}
		}
	}
}
