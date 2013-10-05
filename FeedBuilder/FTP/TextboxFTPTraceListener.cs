using System;
using System.Diagnostics;
using System.Net.FtpClient;
using System.Windows.Forms;
using System.Text;

namespace FeedBuilder.FTP
{
    public class TextboxFTPTraceListener : TraceListener
    {
        private TextBox mTextBoxTraceInfo;

        delegate void SetTextCallback(string text);

        private TextboxFTPTraceListener()
        { }

        public TextboxFTPTraceListener(System.Windows.Forms.TextBox tb) : this()
        {
            mTextBoxTraceInfo = tb;
        }

        public override void Write(string message)
        {
            if (mTextBoxTraceInfo.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Write);
                Control parent = FindParentForm(mTextBoxTraceInfo);
                (parent as Form).Invoke(d, new object[] { message });
            }
            else
            {
                if (!mTextBoxTraceInfo.IsDisposed)
                    mTextBoxTraceInfo.AppendText(message);
            }
        }

        public Control FindParentForm(Control ctrl)
        {
            if (ctrl as Form != null)
                return ctrl;
            else
                return FindParentForm(ctrl.Parent);
        }

        public override void WriteLine(string message)
        {
            mTextBoxTraceInfo.AppendText(message + System.Environment.NewLine);
        }
    }
}
