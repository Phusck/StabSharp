using System.Windows.Forms;

namespace StabSharp
{
    internal class NoScrollListBox: ListBox
    {
        private const int WM_MOUSEWHEEL = 0x020A;

        protected override void WndProc(ref Message m)
        {
            // Check if the message is a mouse wheel message
            if (m.Msg == WM_MOUSEWHEEL)
            {
                // Eat the message so the ListBox does NOT scroll, but still raise the MouseWheel event
                // so consumers (like InputForm) can use the wheel for custom behavior.
                int wParam = m.WParam.ToInt32();
                int delta = (short)((wParam >> 16) & 0xFFFF);

                int lParam = m.LParam.ToInt32();
                int screenX = (short)(lParam & 0xFFFF);
                int screenY = (short)((lParam >> 16) & 0xFFFF);

                var clientPoint = PointToClient(new System.Drawing.Point(screenX, screenY));
                OnMouseWheel(new MouseEventArgs(MouseButtons.None, 0, clientPoint.X, clientPoint.Y, delta));
                return;
            }
            // For all other messages, call the base method
            base.WndProc(ref m);
        }
    }
}
