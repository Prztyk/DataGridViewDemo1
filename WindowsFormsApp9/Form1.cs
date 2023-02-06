using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grid.Columns.Add("Column1", "Column1Header");
            grid.Columns.Add("Column2", "Column2Header");

            grid.Columns["Column1"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.Columns["Column1"].FillWeight = 30;
            grid.Columns["Column2"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.Columns["column2"].FillWeight = 70;

            grid.RowHeadersVisible = false;
            grid.ColumnHeadersVisible = false;
            grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;

            grid.Rows.Clear();
        }


        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            grid.Rows.Insert(0, new object[] { $"Weld Repair {grid.Rows.Count + 1}", "W65" });
        }
    }
}
