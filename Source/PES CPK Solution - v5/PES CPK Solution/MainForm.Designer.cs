namespace PES_CPK_Solution
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem1 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmb_lock_method = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.btn_open = new DevExpress.XtraEditors.SimpleButton();
            this.btn_lock = new DevExpress.XtraEditors.SimpleButton();
            this.lb_info = new DevExpress.XtraEditors.LabelControl();
            this.lb_producs = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tmr_update = new System.Windows.Forms.Timer(this.components);
            this.tx_auto_update = new System.Windows.Forms.TextBox();
            this.xWorker = new System.ComponentModel.BackgroundWorker();
            this.xTimeLapse = new System.Windows.Forms.Timer(this.components);
            this.xTimerVer = new System.Windows.Forms.Timer(this.components);
            this.xDayTimerLeft = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_lock_method.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(12, 3);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(293, 114);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 14;
            this.PictureBox1.TabStop = false;
            // 
            // cmb_lock_method
            // 
            this.cmb_lock_method.EditValue = "";
            this.cmb_lock_method.Location = new System.Drawing.Point(311, 1);
            this.cmb_lock_method.Name = "cmb_lock_method";
            this.cmb_lock_method.Properties.AllowFocused = false;
            this.cmb_lock_method.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cmb_lock_method.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_lock_method.Properties.Items.AddRange(new object[] {
            "LOW",
            "MEDIUM",
            "HIGH",
            "Method-1",
            "Method-2",
            "Method-3"});
            this.cmb_lock_method.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_lock_method.Size = new System.Drawing.Size(142, 18);
            toolTipTitleItem1.Text = "Locck Mode";
            toolTipItem1.Text = "Select lock mode";
            toolTipItem2.Text = "Low : Low  Protections\r\nMedium : Standard Protections\r\nHight : Advanced Protectio" +
    "ns";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            superToolTip1.Items.Add(toolTipSeparatorItem1);
            superToolTip1.Items.Add(toolTipItem2);
            this.cmb_lock_method.SuperTip = superToolTip1;
            this.cmb_lock_method.TabIndex = 15;
            this.cmb_lock_method.SelectedIndexChanged += new System.EventHandler(this.Cmb_lock_method_SelectedIndexChanged);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = "Please notice:\r\nSelect protection method\r\n \r\nYou can try method low, medium, high" +
    ", method-1, method-2, method-3.\r\n\r\nIf you problem with protection high please se" +
    "lect another method.";
            this.comboBoxEdit1.Enabled = false;
            this.comboBoxEdit1.Location = new System.Drawing.Point(311, 25);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.AllowFocused = false;
            this.comboBoxEdit1.Properties.ReadOnly = true;
            this.comboBoxEdit1.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.comboBoxEdit1.Size = new System.Drawing.Size(142, 140);
            this.comboBoxEdit1.TabIndex = 16;
            // 
            // btn_open
            // 
            this.btn_open.AllowFocus = false;
            this.btn_open.Location = new System.Drawing.Point(12, 142);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(94, 23);
            this.btn_open.TabIndex = 17;
            this.btn_open.Text = "Open CPK";
            this.btn_open.Click += new System.EventHandler(this.Btn_open_Click);
            // 
            // btn_lock
            // 
            this.btn_lock.AllowFocus = false;
            this.btn_lock.Enabled = false;
            this.btn_lock.Location = new System.Drawing.Point(112, 142);
            this.btn_lock.Name = "btn_lock";
            this.btn_lock.Size = new System.Drawing.Size(193, 23);
            this.btn_lock.TabIndex = 18;
            this.btn_lock.Text = "Protect CPK File!";
            this.btn_lock.Click += new System.EventHandler(this.Btn_lock_Click);
            // 
            // lb_info
            // 
            this.lb_info.Location = new System.Drawing.Point(12, 123);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(24, 13);
            this.lb_info.TabIndex = 19;
            this.lb_info.Text = "Info:";
            // 
            // lb_producs
            // 
            this.lb_producs.Location = new System.Drawing.Point(12, 172);
            this.lb_producs.Name = "lb_producs";
            this.lb_producs.Size = new System.Drawing.Size(109, 13);
            this.lb_producs.TabIndex = 20;
            this.lb_producs.Text = "Product of | | v5.0.0.0";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(291, 172);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(162, 13);
            this.labelControl2.TabIndex = 21;
            this.labelControl2.Text = "Copyright © 2021 MjTs-140914™";
            // 
            // tmr_update
            // 
            this.tmr_update.Interval = 3000;
            this.tmr_update.Tick += new System.EventHandler(this.Tmr_update_Tick);
            // 
            // tx_auto_update
            // 
            this.tx_auto_update.Location = new System.Drawing.Point(360, 120);
            this.tx_auto_update.Name = "tx_auto_update";
            this.tx_auto_update.Size = new System.Drawing.Size(44, 21);
            this.tx_auto_update.TabIndex = 32;
            this.tx_auto_update.TextChanged += new System.EventHandler(this.Tx_auto_update_TextChanged);
            // 
            // xWorker
            // 
            this.xWorker.WorkerReportsProgress = true;
            this.xWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.XWorker_DoWork);
            this.xWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.XWorker_RunWorkerCompleted);
            // 
            // xTimeLapse
            // 
            this.xTimeLapse.Tick += new System.EventHandler(this.XTimeLapse_Tick);
            // 
            // xTimerVer
            // 
            this.xTimerVer.Interval = 10000;
            this.xTimerVer.Tick += new System.EventHandler(this.XTimerVer_Tick);
            // 
            // xDayTimerLeft
            // 
            this.xDayTimerLeft.Interval = 10000;
            this.xDayTimerLeft.Tick += new System.EventHandler(this.XDayTimerLeft_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 196);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lb_producs);
            this.Controls.Add(this.lb_info);
            this.Controls.Add(this.btn_lock);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.cmb_lock_method);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.tx_auto_update);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::PES_CPK_Solution.Properties.Resources.Icon;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PES CPK Solution (x64)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_lock_method.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox PictureBox1;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_lock_method;
        private DevExpress.XtraEditors.MemoEdit comboBoxEdit1;
        private DevExpress.XtraEditors.SimpleButton btn_open;
        private DevExpress.XtraEditors.SimpleButton btn_lock;
        private DevExpress.XtraEditors.LabelControl lb_info;
        private DevExpress.XtraEditors.LabelControl lb_producs;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal System.Windows.Forms.Timer tmr_update;
        internal System.Windows.Forms.TextBox tx_auto_update;
        private System.ComponentModel.BackgroundWorker xWorker;
        private System.Windows.Forms.Timer xTimeLapse;
        private System.Windows.Forms.Timer xTimerVer;
        private System.Windows.Forms.Timer xDayTimerLeft;
    }
}

