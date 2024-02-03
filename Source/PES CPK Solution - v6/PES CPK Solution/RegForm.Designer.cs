namespace PES_CPK_Solution
{
    partial class RegForm
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
            this.btn_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.LabelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.LabelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tx_serial = new DevExpress.XtraEditors.TextEdit();
            this.tx_username = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_serial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_username.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Ok.Location = new System.Drawing.Point(281, 63);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 9;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // LabelControl9
            // 
            this.LabelControl9.Location = new System.Drawing.Point(16, 40);
            this.LabelControl9.Name = "LabelControl9";
            this.LabelControl9.Size = new System.Drawing.Size(73, 13);
            this.LabelControl9.TabIndex = 6;
            this.LabelControl9.Text = "Serial Number :";
            // 
            // LabelControl1
            // 
            this.LabelControl1.Location = new System.Drawing.Point(16, 15);
            this.LabelControl1.Name = "LabelControl1";
            this.LabelControl1.Size = new System.Drawing.Size(59, 13);
            this.LabelControl1.TabIndex = 5;
            this.LabelControl1.Text = "User Name :";
            // 
            // tx_serial
            // 
            this.tx_serial.Location = new System.Drawing.Point(107, 37);
            this.tx_serial.Name = "tx_serial";
            this.tx_serial.Size = new System.Drawing.Size(249, 20);
            this.tx_serial.TabIndex = 8;
            // 
            // tx_username
            // 
            this.tx_username.Location = new System.Drawing.Point(107, 12);
            this.tx_username.Name = "tx_username";
            this.tx_username.Size = new System.Drawing.Size(249, 20);
            this.tx_username.TabIndex = 7;
            // 
            // RegForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 99);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.tx_serial);
            this.Controls.Add(this.tx_username);
            this.Controls.Add(this.LabelControl9);
            this.Controls.Add(this.LabelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::PES_CPK_Solution.Properties.Resources.Icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activate Product";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegForm_FormClosed);
            this.Load += new System.EventHandler(this.RegForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tx_serial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tx_username.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DevExpress.XtraEditors.SimpleButton btn_Ok;
        internal DevExpress.XtraEditors.TextEdit tx_serial;
        internal DevExpress.XtraEditors.TextEdit tx_username;
        internal DevExpress.XtraEditors.LabelControl LabelControl9;
        internal DevExpress.XtraEditors.LabelControl LabelControl1;
    }
}