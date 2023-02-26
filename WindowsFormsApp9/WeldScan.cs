using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeldScanApp
{
    public partial class WeldScan : Form
    {
        public WeldScan()
        {
            InitializeComponent();
            labelProductionLine.Text = Program.ProductionLine;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grid.Columns.Add("Column1", "Column1Header");
            grid.Columns.Add("Column2", "Column2Header");

            grid.Columns["Column1"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.Columns["Column1"].FillWeight = 40;
            grid.Columns["Column2"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.Columns["column2"].FillWeight = 60;

            grid.RowHeadersVisible = false;
            grid.ColumnHeadersVisible = false;
            grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;

            grid.Rows.Clear();

            textBoxReceived.Focus();

#if DEBUG
            labelComment.BorderStyle = BorderStyle.FixedSingle;
#endif
        }

        #region Form Events

        private void WeldScan_Activated(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.Control;
            textBoxReceived.Focus();
        }

        private void WeldScan_Deactivate(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Firebrick;
        }

        #endregion Form Events

        #region Grid Events

        private void grid_Enter(object sender, EventArgs e)
        {
            textBoxReceived.Focus();
        }

        #endregion Grid Events

        #region Text Box Received Events
        private void textBoxReceived_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //Log.Information($"KeyCode: {e.KeyCode} KeyValue: {e.KeyValue}");
                if (textBoxReceived.Text.EndsWith("\r\n"))
                {
                    Log.Information("New line detected");
                    HandleScan(textBoxReceived.Text);
                }
                else if (e.KeyCode.ToString() == "Return")
                {
                    Log.Information("Enter key detected");
                    HandleScan(textBoxReceived.Text);
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Error at {nameof(textBoxReceived_KeyUp)}");
            }
        }

        private void textBoxReceived_Leave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Firebrick;
        }

        private void textBoxReceived_Enter(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.Control;
        }

        #endregion Text Box Received Events

        private void HandleScan(string text)
        {
            try
            {
                Log.Information($"Received text = [{text}]");
                labelComment.Text = string.Empty;
                text = RemoveSpecialChars(text);

                if(text.StartsWith("O1S"))
                { HandlePartScan(text); }
                else if (text.StartsWith("W-"))
                { HandleWeldScan(text); }
                else if (text == "OK" || text == "NOK")
                { HandleOKScan(text); }
                else
                { HandleUnknownScan(text); }
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Error at {nameof(HandleScan)}");
                throw;
            }
            finally
            {
                textBoxReceived.Text = string.Empty;
            }
        }

        private string RemoveSpecialChars(string text)
        {
            text = text.Replace("\r\n", string.Empty);
            return text;
        }

        private void HandleOKScan(string text)
        {
            Log.Information("OK / NOK code recognized");
            if (ValidateOKCode(text) == false) { return; }

            labelDate.Text = string.Empty;
            labelTime.Text = string.Empty;
            labelPartCode.Text = string.Empty;
            grid.Rows.Clear();
        }

        private void HandleUnknownScan(string text)
        {
            Log.Warning($"Unknown code [{text}]");
            labelComment.Text = "Unknown code !";
        }

        private void HandleWeldScan(string text)
        {
            Log.Information("Weld code recognized");
            if (ValidateWeldCode(text) == false) { return; }
            
            grid.Rows.Insert(0, new object[] { $"Weld Repair {grid.Rows.Count + 1}", $"{textBoxBuffer.Text}" });
            grid.Rows[0].Selected = true;
        }

        private void HandlePartScan(string text)
        {
#if DEBUG
            var r = RandomHelper.Roll(1, 999);
            var newPart = $"{r:000}";
            text = text.Replace("160", newPart);
#endif

            Log.Information("Part code recognized");
            if(ValidatePartCode(text) == false) { return; }

            labelDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            labelTime.Text = DateTime.Now.ToString("hh:mm");
            labelPartCode.Text = text;
            grid.Rows.Clear();
        }

        #region Validators
        private bool ValidateOKCode(string text)
        {
            if (labelPartCode.Text.Length == 0)
            {
                Log.Information($"OK / NOK code scanned [{text}] while no part code is active");
                labelComment.Text = $"Scan part code !";
                return false;
            }

            return true;
        }

        private bool ValidateWeldCode(string text)
        {
            if (labelPartCode.Text.Length == 0)
            {
                Log.Information($"Weld code scanned [{text}] while no part code is active");
                labelComment.Text = $"Scan part code !";
                return false;
            }

            return true;
        }

        private bool ValidatePartCode(string text)
        {
            if (labelPartCode.Text.Length > 0)
            {
                Log.Information($"Part code scanned [{text}] while other part code is active [{labelPartCode.Text}]");
                labelComment.Text = $"Scan weld code or OK or NOK !";
                return false;
            }

            return true;
        }

        #endregion Validators
    }
}
