namespace WeldScanApp
{
    partial class WeldScan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPartCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.textBoxReceived = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.textBoxBuffer = new System.Windows.Forms.TextBox();
            this.labelProductionLine = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(150, 150);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(500, 300);
            this.grid.TabIndex = 0;
            this.grid.TabStop = false;
            this.grid.Enter += new System.EventHandler(this.grid_Enter);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(147, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Part Number DMCode";
            // 
            // labelPartCode
            // 
            this.labelPartCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPartCode.Location = new System.Drawing.Point(253, 124);
            this.labelPartCode.Name = "labelPartCode";
            this.labelPartCode.Size = new System.Drawing.Size(397, 13);
            this.labelPartCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(147, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(147, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Date";
            // 
            // labelTime
            // 
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(253, 101);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(397, 13);
            this.labelTime.TabIndex = 6;
            // 
            // labelDate
            // 
            this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.Location = new System.Drawing.Point(253, 78);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(397, 13);
            this.labelDate.TabIndex = 7;
            // 
            // textBoxReceived
            // 
            this.textBoxReceived.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReceived.Location = new System.Drawing.Point(150, 456);
            this.textBoxReceived.Multiline = true;
            this.textBoxReceived.Name = "textBoxReceived";
            this.textBoxReceived.Size = new System.Drawing.Size(500, 20);
            this.textBoxReceived.TabIndex = 8;
            this.textBoxReceived.Enter += new System.EventHandler(this.textBoxReceived_Enter);
            this.textBoxReceived.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxReceived_KeyUp);
            this.textBoxReceived.Leave += new System.EventHandler(this.textBoxReceived_Leave);
            // 
            // labelComment
            // 
            this.labelComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComment.ForeColor = System.Drawing.Color.Firebrick;
            this.labelComment.Location = new System.Drawing.Point(150, 479);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(500, 50);
            this.labelComment.TabIndex = 9;
            this.labelComment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxBuffer
            // 
            this.textBoxBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBuffer.Location = new System.Drawing.Point(150, 456);
            this.textBoxBuffer.Multiline = true;
            this.textBoxBuffer.Name = "textBoxBuffer";
            this.textBoxBuffer.Size = new System.Drawing.Size(500, 20);
            this.textBoxBuffer.TabIndex = 10;
            this.textBoxBuffer.Visible = false;
            // 
            // labelProductionLine
            // 
            this.labelProductionLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelProductionLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductionLine.Location = new System.Drawing.Point(12, 9);
            this.labelProductionLine.Name = "labelProductionLine";
            this.labelProductionLine.Size = new System.Drawing.Size(100, 23);
            this.labelProductionLine.TabIndex = 11;
            this.labelProductionLine.Text = "label4";
            this.labelProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WeldScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.labelProductionLine);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.textBoxReceived);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelPartCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.textBoxBuffer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "WeldScan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welds Scanner";
            this.Activated += new System.EventHandler(this.WeldScan_Activated);
            this.Deactivate += new System.EventHandler(this.WeldScan_Deactivate);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPartCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.TextBox textBoxReceived;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.TextBox textBoxBuffer;
        private System.Windows.Forms.Label labelProductionLine;
    }
}

