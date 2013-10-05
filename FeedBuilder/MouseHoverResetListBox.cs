using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FeedBuilder
{
    class MouseHoverResetListBox : ListBox
    {

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ResetMouseEventArgs();
        }
    }
}
