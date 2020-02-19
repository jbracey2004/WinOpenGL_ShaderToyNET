using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace modCommon
{
	public class modWndProcInterop
	{
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
			Activate = 0x006,
			SetFocus = 0x007
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

		}
		public struct TOUCHINPUT
		{
			int X; int Y;
			int hSource;
			int dwID; int dwFlags; int dwMask; int dwTime;
			int dwExtraInfo;
			int cxContact; int cyContact;
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
	}
}
