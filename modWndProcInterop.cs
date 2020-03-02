using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static modCommon.modWndProcInterop.clsTouchInterface;

namespace modCommon
{
	public class modWndProcInterop
	{
		public delegate IntPtr delegateProc(int code, IntPtr wParam, IntPtr lParam);
		[DllImport("user32", SetLastError = true)] private static extern bool RegisterTouchWindow(IntPtr hWnd, int param);
		[DllImport("user32", SetLastError = true)] private static extern bool UnregisterTouchWindow(IntPtr hWnd);
		[DllImport("user32", SetLastError = true)] private static extern bool GetTouchInputInfo(IntPtr hTouchInput, int cInputs, [In, Out] TOUCHINPUT[] pInputs, int cbSize);
		[DllImport("user32", SetLastError = true)] private static extern int CloseTouchInputHandle(IntPtr hTouchInput);
		[DllImport("user32.dll", SetLastError = true)] private static extern bool UnhookWindowsHookEx(IntPtr hookPtr);
		[DllImport("user32.dll", SetLastError = true)] private static extern IntPtr CallNextHookEx(IntPtr hookPtr, int nCode, IntPtr wordParam, IntPtr longParam);
		[DllImport("user32.dll", SetLastError = true)] private static extern IntPtr SetWindowsHookEx(int hookType, delegateProc hookProc, IntPtr hModule, uint threadID);
		[DllImport("kernel32.dll", SetLastError = true)] private static extern uint GetCurrentThreadId();
		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)] private static extern IntPtr GetModuleHandle(string lpModuleName);
		public static int Int32MakeWord(short loWord, short hiWord) => ((int)hiWord << 16) | ((int)loWord & short.MaxValue);
		public static short Int32LoWord(int word) => (short)(word & short.MaxValue);
		public static short Int32HiWord(int word) => (short)((word >> 16));
		public enum WinMessage
		{
			HitTest = 0x0084,
			MouseMove = 0x0200,
			Touch = 0x0240,
			LeftButtonDown = 0x0201,
			LeftButtonUp  = 0x0202,
			RightButtonDown = 0x0204,
			RightButtonUp = 0x0205,
			MiddleButtonDown = 0x0207,
			MiddleButtonUp = 0x0208,
			Activate = 0x0006,
			SetFocus = 0x0007, 
			SetCursor = 0x0020,
			Tablet_Debase = 0x02C0,
			Tablet_GestureSatus = Tablet_Debase + 12
		}
		public enum WinHook
		{
			Min = (-1),
			MsgFilter = (-1),
			JournalRecord = 0x0000,
			JournalPlayback = 0x0001,
			Keyboard = 0x0002,
			GetMessage = 0x0003,
			CallWinProcess = 0x0004,
			WH_CBT = 0x0005,
			SysMsgFilter = 0x0006,
			Mouse = 0x0007,
			Hardware = 0x0008,
			Debug = 0x0009,
			Shell = 0x000A,
			ForegroundIdle = 0x000B,
			CallWinProcessReturn = 0x000C,
			Keyboard_LL = 0x000D,
			Mouse_LL = 0x000E
		}
		public enum WinTabletStatus
		{
			Disable_PressAndHold = 0x0001,
			Disable_PenTapFeedback = 0x0008,
			Disable_PenBarrelFeedback = 0x0010,
			Disable_TouchUIForceOn = 0x0100,
			Disable_TouchUIForceOff = 0x0200,
			Disable_TouchSwitch = 0x8000,
			Disable_Flicks = 0x10000,
			Disable_FlickFallbackKeys = 0x100000,
			Disable_SmoothScrolling = 0x80000,
			Enable_FlicksOnContext = 0x20000, 
			Enable_FlickLearningMode = 0x40000,
			Enable_MultiTouchData = 0x1000000
		}
		public enum WinHitTest
		{
			Left = 10,
			Right = 11,
			BottomRight = 17,
			Bottom = 15,
			BottomLeft = 16,
			Top = 12,
			TopLeft = 13,
			TopRight = 14
		}
		public enum WinTouch
		{
			Move = 0x0001,
			Down = 0x0002,
			Up = 0x0004
		}
		public class clsTouchInterface : IDisposable
		{
			public struct TOUCHINPUT
			{
				public int X;
				public int Y;
				public IntPtr hSource;
				public int dwID;
				public int dwFlags;
				public int dwMask;
				public int dwTime;
				public IntPtr dwExtraInfo;
				public int cxContact;
				public int cyContact;
			}
			public struct MOUSEHOOKSTRUCT
			{
				public Point pt;
				public IntPtr hwnd;
				public uint wHitTestCode;
				public IntPtr dwExtraInfo;
			}
			public struct MOUSELLHOOKSTRUCT
			{
				public Point pt;
				public uint mouseData;
				public uint flags;
				public uint time;
				public IntPtr dwExtraInfo;
			}
			public static int TOUCHINPUT_Size = Marshal.SizeOf(new TOUCHINPUT());
			public struct TouchPoint
			{
				public Size ControlSize { get; set; }
				public Size ContactSize { get; set; }
				public Point Location { get; set; }
				public PointF UV { get => ((!ControlSize.IsEmpty) ? (new PointF((float)Location.X/ControlSize.Width, (float)Location.Y/ControlSize.Height)) : (PointF.Empty)); }
				public Rectangle ContactArea { get => new Rectangle(Location - new Size((int)(ContactSize.Width*0.5), (int)(ContactSize.Height*0.5)), ContactSize); }
				public static TouchPoint Empty => new TouchPoint();
				public override string ToString() => $"Pos {Location.ToString()}; {((ContactSize!=Size.Empty)?($"Size {ContactSize.ToString()}"):(""))}";
			}
			public class clsTouchInput
			{
				public int ID { get; set; }
				public TouchPoint CurrentTouchPoint { get => ((TouchPoints.Count > 0) ? (TouchPoints[TouchPoints.Count - 1]): (TouchPoint.Empty)); }
				public TouchPoint PreviousTouchPoint { get => ((TouchPoints.Count > 1) ? (TouchPoints[TouchPoints.Count - 2]) : (TouchPoint.Empty)); }
				public List<TouchPoint> TouchPoints { get; private set; } = new List<TouchPoint>();
			}
			public class TouchEventArgs : EventArgs
			{
				public TouchPoint Touch => TouchInstance.CurrentTouchPoint;
				public Point Location => Touch.Location;
				public Size ContactSize => Touch.ContactSize;
				public clsTouchInput TouchInstance { get; set; }
				public TouchEventArgs(clsTouchInput Inst)
				{
					TouchInstance = Inst;
				}
			}
			public EventHandler<TouchEventArgs> TouchStart;
			public EventHandler<TouchEventArgs> TouchMove;
			public EventHandler<TouchEventArgs> TouchEnd;
			private IntPtr hookMouse;
			private IntPtr handleModule;
			private uint ptrThreadID;
			private delegateProc dalegateMouseHook;
			public Control Widget { private set; get; }
			public List<clsTouchInput> Touches { private set; get; } = new List<clsTouchInput>();
			public clsTouchInterface(Control widget)
			{
				Widget = widget;
				if (!RegisterTouchWindow(Widget.Handle, 0))
				{
					int intErr = Marshal.GetLastWin32Error();
					Console.WriteLine($"Touch Interface Handle {Widget.Handle.ToString()} Failed To Register. Error {intErr}");
				}
				handleModule = GetModuleHandle(null);
				ptrThreadID = GetCurrentThreadId();
				dalegateMouseHook = HookMessageProc;
				hookMouse = SetWindowsHookEx((int)WinHook.Mouse, dalegateMouseHook, handleModule, ptrThreadID);
				if(hookMouse == IntPtr.Zero)
				{
					int intErr = Marshal.GetLastWin32Error();
					Console.WriteLine($"Mouse Hook Failed To Initialize. Error {intErr}");
				}
			}
			private const uint MOUSEEVENTF_MASK = 0xFFFFFF00;
			private const uint TOUCH_FLAG = 0xFF515700;
			private IntPtr HookMessageProc(int code, IntPtr wParam, IntPtr lParam)
			{
				if(code > 0) 
				{
					var mouseInfo = Marshal.PtrToStructure<MOUSEHOOKSTRUCT>(lParam);
					if(mouseInfo.hwnd == Widget.Handle)
					{
						long EventInfo = mouseInfo.dwExtraInfo.ToInt64();
						long EventFlags = EventInfo & MOUSEEVENTF_MASK;
						Console.WriteLine($"Message Hooked: {code}, W={wParam}, L={lParam}, Info={EventInfo} Flags={EventFlags}");
						if (EventFlags == TOUCH_FLAG) 
						{
							return new IntPtr(1);
						}
					}
				}
				return CallNextHookEx(hookMouse, code, wParam, lParam);
			}
			private bool disposedValue = false;
			protected virtual void Dispose(bool disposing)
			{
				if (!disposedValue)
				{
					if(!UnhookWindowsHookEx(hookMouse))
					{
						int intErr = Marshal.GetLastWin32Error();
						Console.WriteLine($"Hook Process Handle {hookMouse} Failed To Release. Error {intErr}");
					}
					dalegateMouseHook = null;
					if (!UnregisterTouchWindow(Widget.Handle))
					{
						int intErr = Marshal.GetLastWin32Error();
						Console.WriteLine($"Touch Interface Handle {Widget.Handle.ToString()} Failed To UnRegister. Error {intErr}");
					}
					handleModule = IntPtr.Zero;
					ptrThreadID = 0;
					Widget = null;
					disposedValue = true;
				}
			}
			public void Dispose()
			{
				Dispose(true);
			}
		}
		public static bool WinProc_HandleHitRegions(Control sender, ref Message msg, Dictionary<WinHitTest, Rectangle> regions)
		{
			if(msg.Msg == (int)WinMessage.HitTest)
			{
				Point screenPoint = new Point(msg.LParam.ToInt32());
				Point clientPoint = sender.PointToClient(screenPoint);
				foreach(KeyValuePair<WinHitTest, Rectangle> hitBox in regions)
				{
					if (hitBox.Value.Contains(clientPoint))
					{
						msg.Result = (IntPtr)hitBox.Key;
						return true;
					}
				}
			}
			return false;
		}
		public static bool WinProc_HandleTouch(Control sender, ref Message msg, ref clsTouchInterface TouchInterface)
		{
			if(msg.Msg == (int)WinMessage.Tablet_GestureSatus)
			{
				msg.Result = (IntPtr)((int)WinTabletStatus.Disable_PressAndHold |
									  (int)WinTabletStatus.Disable_PenTapFeedback |
									  (int)WinTabletStatus.Disable_PenBarrelFeedback |
									  (int)WinTabletStatus.Disable_Flicks);
				return true;
			}
			if (msg.Msg == (int)WinMessage.Touch)
			{
				int countInputs = Int32LoWord(msg.WParam.ToInt32());
				TOUCHINPUT[] aryInputs = new TOUCHINPUT[countInputs];
				IntPtr hTouch = msg.LParam;
				if (!GetTouchInputInfo(hTouch, countInputs, aryInputs, TOUCHINPUT_Size))
				{
					int intErr = Marshal.GetLastWin32Error();
					Console.WriteLine($"Handle {hTouch} Error {intErr}");
					return false;
				}
				bool bolHandled = true;
				foreach (TOUCHINPUT touchI in aryInputs)
				{
					PointF p = new PointF(touchI.X * 0.01f, touchI.Y * 0.01f);
					Point pt = sender.PointToClient(new Point((int)p.X, (int)p.Y));
					SizeF szf = new SizeF(touchI.cxContact * 0.01f, touchI.cyContact * 0.01f);
					Size sz = new Size((int)szf.Width, (int)szf.Height);
					clsTouchInput instTouch = TouchInterface.Touches.Find(itm => (itm.ID == (int)touchI.dwID));
					if (instTouch == null)
					{
						instTouch = new clsTouchInput();
						instTouch.ID = (int)touchI.dwID;
					}
					TouchPoint touchPt = new TouchPoint() { Location = pt, ContactSize = sz, ControlSize = sender.Size };
					instTouch.TouchPoints.Add(touchPt);
					if (((int)touchI.dwFlags & (int)WinTouch.Down) != 0)
					{
						TouchInterface.Touches.Add(instTouch);
						TouchInterface.TouchStart?.Invoke(sender, new TouchEventArgs(instTouch));
					}
					else if (((int)touchI.dwFlags & (int)WinTouch.Up) != 0)
					{
						TouchInterface.TouchEnd?.Invoke(sender, new TouchEventArgs(instTouch));
						TouchInterface.Touches.Remove(instTouch);
						instTouch.TouchPoints.Clear();
						instTouch = null;
					}
					else if (((int)touchI.dwFlags & (int)WinTouch.Move) != 0)
					{
						TouchInterface.TouchMove?.Invoke(sender, new TouchEventArgs(instTouch));
					}
					else
					{
						bolHandled = false;
					}
				}
				CloseTouchInputHandle(hTouch);
				aryInputs = null;
				return bolHandled;
			}
			return false;
		}
	}
}
