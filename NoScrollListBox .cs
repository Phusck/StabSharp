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
                base.WndProc(ref m);
                // Do nothing to effectively ignore the mouse wheel scrolling.
                return;
            }
            // For all other messages, call the base method
            base.WndProc(ref m);
        }
    }
}
