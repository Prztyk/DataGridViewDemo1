using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WeldScanApp
{
    public partial class WeldScan : Form
    {
        private PartDTO currentPart;

        public WeldScan()
        {
            InitializeComponent();
            labelProductionLine.Text = Program.ProductionLine;
        }

        private void WeldScan_Load(object sender, EventArgs e)
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

            textBoxReceived.Focus();

            ClearPartInfo();

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

                if(text.StartsWith("O1S") || text.StartsWith("0S"))
                { HandlePartScan(text); }
                else if (text.StartsWith("W"))
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

            currentPart.Result = text;
            PartHelper.UpdateResult(currentPart);
            ClearPartInfo();
            currentPart = null;
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

            WeldDTO weld = new WeldDTO { PartId = currentPart.Id, WeldCode = text };
            WeldHelper.SaveWeld(weld);

            if(currentPart.Welds.Contains(weld.WeldCode)) { return; }
            currentPart.Welds.Add(weld.WeldCode);
            grid.Rows.Insert(0, new object[] { $"Weld Repair {grid.Rows.Count + 1}", $"{weld.WeldCode}" });
            grid.Rows[0].Selected = true;
        }

        private void HandlePartScan(string text)
        {
#if DEBUG
            var r = RandomHelper.Roll(1, 999);
            var newPart = $"{r:000}";
            //text = text.Replace("210", newPart);
#endif

            Log.Information("Part code recognized");
            if(ValidatePartCode(text) == false) { return; }

            currentPart = PartHelper.GetPartByCode(text);
            if (currentPart == null) { currentPart = CreateNewPart(text); }
            PartHelper.SavePart(currentPart);
            DisplayPartInfo(currentPart);
        }

        private void DisplayPartInfo(PartDTO part)
        {
            labelDate.Text = part.Date.ToString("dd-MM-yyyy");
            labelTime.Text = part.Date.ToString("HH:mm");
            labelPartCode.Text = part.PartCode;
            grid.Rows.Clear();
            foreach(var weld in part.Welds)
            {
                grid.Rows.Insert(0, new object[] { $"Weld Repair {grid.Rows.Count + 1}", $"{weld}" });
            }
        }

        private void ClearPartInfo()
        {
            labelDate.Text = string.Empty;
            labelTime.Text = string.Empty;
            labelPartCode.Text = string.Empty;
            grid.Rows.Clear();
            labelComment.Text = "Scan part code";
        }

        private PartDTO CreateNewPart(string text)
        {
            return new PartDTO 
            { 
                Id = 0, 
                PartCode = text, 
                Date = DateTime.Now, 
                Line = Program.ProductionLine
            };
        }

        #region Validators
        private bool ValidateOKCode(string text)
        {
            if (currentPart == null)
            {
                Log.Information($"OK / NOK code scanned [{text}] while no part code is active");
                labelComment.Text = $"Scan part code !";
                return false;
            }

            return true;
        }

        private bool ValidateWeldCode(string weldCode)
        {
            if (currentPart == null)
            {
                Log.Information($"Weld code scanned [{weldCode}] while no part code is active");
                labelComment.Text = $"Scan part code !";
                return false;
            }

            if(currentPart.Welds.Contains(weldCode))
            {
                Log.Information($"Weld code repeated [{weldCode}]");
                labelComment.Text = $"Weld code repeated !";
            }

            return true;
        }

        private bool ValidatePartCode(string text)
        {
            if (currentPart != null)
            {
                Log.Information($"Part code scanned [{text}] while other part code is active [{currentPart.PartCode}]");
                labelComment.Text = $"Scan weld code or OK or NOK !";
                return false;
            }

            return true;
        }

        #endregion Validators
    }
}
