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
			MouseMove = 0x0200
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
