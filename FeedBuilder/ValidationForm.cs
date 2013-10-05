using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FeedBuilder
{
    public partial class ValidationForm : Form
    {
        public ValidationForm()
        {
            InitializeComponent();
        }

        public string URL
        {
            set
            {
                string url = value;
                if (!url.StartsWith("http://"))
                    url = "http://" + url;
                mValidationBrowser.Url = new Uri(url);
            }
        }
    }
}
