using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace MyCustomControls.InheritedCombo
{
	/// <summary>
	/// Summary description for MultiColumnComboBox.
	/// </summary>
	public delegate void AfterSelectEventHandler();
	public class MultiColumnComboBox : System.Windows.Forms.ComboBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private DataRow selectedRow = null;
		private string displayMember = "";
        private string dataMember = "";
        private string dataValue = "";
		private string displayValue = "";
		private DataTable dataTable = null;
		private DataRow[] dataRows = null;
		private string[] columnsToDisplay = null;
		public event AfterSelectEventHandler AfterSelectEvent;

		public MultiColumnComboBox(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			container.Add(this);
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public MultiColumnComboBox()
		{
			InitializeComponent();
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

        private int LeftPosition()
        {
            Form parentForm = this.FindForm();
            Control container = this.Parent;
            int position = parentForm.Left;
            while (container != parentForm)
            {
                position += container.Left;
                container = container.Parent;
            }
            position += this.Left + 4;
            return position;
        }

        private int TopPosition()
        {
            Form parentForm = this.FindForm();
            Control container = this.Parent;
            int position = parentForm.Top;
            int pos = parentForm.Height;
            while (container != parentForm)
            {
                position += container.Top;
                container = container.Parent;
            }
            position += this.Top + 4;
            return position;
        }


        //protected override 

		protected override void OnDropDown(System.EventArgs e){
			//Form parent = this.FindForm();

            Point locationOnForm = this.Parent.PointToScreen(this.Location);
            locationOnForm.Y += this.Height;
            
			if(this.dataTable != null || this.dataRows!= null){
				MultiColumnComboPopup popup = new MultiColumnComboPopup(this.dataTable,ref this.selectedRow,columnsToDisplay);
				popup.AfterRowSelectEvent+=new AfterRowSelectEventHandler(MultiColumnComboBox_AfterSelectEvent);
				//popup.Location = new Point(LeftPosition(), TopPosition());
                popup.Location = locationOnForm;
				popup.Show();
				if(popup.SelectedRow!=null){
					try{
						this.selectedRow = popup.SelectedRow;
						this.displayValue = popup.SelectedRow[this.displayMember].ToString();
						this.Text = this.displayValue;
					}catch(Exception e2) {
						MessageBox.Show(e2.Message,"Error");	
					}
				}
			}
			//base.OnDropDown(e);
		}

		private void MultiColumnComboBox_AfterSelectEvent(object sender, DataRow drow){
			try{
				if(drow!=null){
                    if (displayMember != null && displayMember != string.Empty)
					    this.Text = drow[displayMember].ToString();
                    if (dataMember != null && dataMember != string.Empty)
                        this.DataValue = drow[dataMember].ToString();

                    if (AfterSelectEvent != null)
                        AfterSelectEvent();
				}
			}catch(Exception exp){
				MessageBox.Show(this,exp.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		public DataRow SelectedRow{
			get{
				return selectedRow;
			}
		}

        public string DisplayValue
        {
            get
            {
                return displayValue;
            }
        }

        public new string DisplayMember
        {
            set
            {
                displayMember = value;
            }
        }

        public string DataMember
        {
            get
            {
                return dataMember;
            }
            set { dataMember = value; }
        }

        public new string DataValue
        {
            set
            {
                dataValue = value;
            }
            get { return dataValue; }
        }

		public DataTable Table{
			set{
				dataTable = value;
				if(dataTable==null)
					return;
				selectedRow=dataTable.NewRow();
			}
		}

		public DataRow[] Rows{
			set{
				dataRows = value;
			}
		}

		public string[] ColumnsToDisplay{
			set{
				columnsToDisplay = value;
			}
		}
	}
}
