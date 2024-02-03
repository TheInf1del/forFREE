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
            this.lb_info = new DevExpress.XtraEditors.LabelControl();
            this.lb_producs = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tmr_update = new System.Windows.Forms.Timer(this.components);
            this.tx_auto_update = new System.Windows.Forms.TextBox();
            this.xWorker = new System.ComponentModel.BackgroundWorker();
            this.xTimeLapse = new System.Windows.Forms.Timer(this.components);
            this.xTimerVer = new System.Windows.Forms.Timer(this.components);
            this.xDayTimerLeft = new System.Windows.Forms.Timer(this.components);
            this.tmr_reg = new System.Windows.Forms.Timer(this.components);
            this.xTimerStart = new System.Windows.Forms.Timer(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.xRand = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.xCrc = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.xMask = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.xCode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.xAllign = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xMethod = new DevExpress.XtraEditors.ComboBoxEdit();
            this.xCreateCPK = new System.ComponentModel.BackgroundWorker();
            this.xTimerCpk = new System.Windows.Forms.Timer(this.components);
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.xTimerServices = new System.Windows.Forms.Timer(this.components);
            this.xMarqueeProgress = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.xProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.ptr_Create_CPK = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xRand.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCrc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMask.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xAllign.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMarqueeProgress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xProgressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptr_Create_CPK)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_info
            // 
            this.lb_info.Location = new System.Drawing.Point(42, 145);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(21, 13);
            this.lb_info.TabIndex = 19;
            this.lb_info.Text = "       ";
            // 
            // lb_producs
            // 
            this.lb_producs.Location = new System.Drawing.Point(12, 192);
            this.lb_producs.Name = "lb_producs";
            this.lb_producs.Size = new System.Drawing.Size(109, 13);
            this.lb_producs.TabIndex = 20;
            this.lb_producs.Text = "Product of | | v6.0.0.0";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(355, 192);
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
            this.tx_auto_update.Location = new System.Drawing.Point(208, 47);
            this.tx_auto_update.Name = "tx_auto_update";
            this.tx_auto_update.Size = new System.Drawing.Size(44, 21);
            this.tx_auto_update.TabIndex = 32;
            this.tx_auto_update.TextChanged += new System.EventHandler(this.Tx_auto_update_TextChanged);
            // 
            // xWorker
            // 
            this.xWorker.WorkerReportsProgress = true;
            this.xWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.XWorker_DoWork);
            this.xWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.XWorker_ProgressChanged);
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
            // tmr_reg
            // 
            this.tmr_reg.Tick += new System.EventHandler(this.Tmr_reg_Tick);
            // 
            // xTimerStart
            // 
            this.xTimerStart.Tick += new System.EventHandler(this.XTimerStart_Tick);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.xRand);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.xCrc);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.xMask);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.xCode);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.xAllign);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.xMethod);
            this.groupControl1.Location = new System.Drawing.Point(311, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(206, 162);
            this.groupControl1.TabIndex = 35;
            this.groupControl1.Text = "Builder Settings";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Strikeout);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Enabled = false;
            this.labelControl7.Location = new System.Drawing.Point(49, 141);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(132, 13);
            this.labelControl7.TabIndex = 45;
            this.labelControl7.Text = "                                            ";
            // 
            // xRand
            // 
            this.xRand.Location = new System.Drawing.Point(5, 137);
            this.xRand.Name = "xRand";
            this.xRand.Properties.AllowFocused = false;
            this.xRand.Properties.Caption = "                                             Random";
            this.xRand.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.xRand.Size = new System.Drawing.Size(196, 20);
            this.xRand.TabIndex = 44;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Strikeout);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Enabled = false;
            this.labelControl6.Location = new System.Drawing.Point(30, 121);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(150, 13);
            this.labelControl6.TabIndex = 43;
            this.labelControl6.Text = "                                                  ";
            // 
            // xCrc
            // 
            this.xCrc.Location = new System.Drawing.Point(5, 118);
            this.xCrc.Name = "xCrc";
            this.xCrc.Properties.AllowFocused = false;
            this.xCrc.Properties.Caption = "                                                   CRC";
            this.xCrc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.xCrc.Size = new System.Drawing.Size(196, 20);
            this.xCrc.TabIndex = 42;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Strikeout);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Enabled = false;
            this.labelControl5.Location = new System.Drawing.Point(33, 99);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(147, 13);
            this.labelControl5.TabIndex = 36;
            this.labelControl5.Text = "                                                 ";
            // 
            // xMask
            // 
            this.xMask.Location = new System.Drawing.Point(5, 95);
            this.xMask.Name = "xMask";
            this.xMask.Properties.AllowFocused = false;
            this.xMask.Properties.Caption = "                                                  Mask";
            this.xMask.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.xMask.Size = new System.Drawing.Size(196, 20);
            this.xMask.TabIndex = 41;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 76);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(32, 13);
            this.labelControl4.TabIndex = 40;
            this.labelControl4.Text = "Code :";
            // 
            // xCode
            // 
            this.xCode.EditValue = "";
            this.xCode.Location = new System.Drawing.Point(78, 74);
            this.xCode.Name = "xCode";
            this.xCode.Properties.AllowFocused = false;
            this.xCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.xCode.Properties.Items.AddRange(new object[] {
            "SJIS",
            "EUC",
            "UTF-8"});
            this.xCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.xCode.Size = new System.Drawing.Size(123, 18);
            this.xCode.TabIndex = 39;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 52);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(32, 13);
            this.labelControl3.TabIndex = 38;
            this.labelControl3.Text = "Allign :";
            // 
            // xAllign
            // 
            this.xAllign.EditValue = "";
            this.xAllign.Location = new System.Drawing.Point(78, 50);
            this.xAllign.Name = "xAllign";
            this.xAllign.Properties.AllowFocused = false;
            this.xAllign.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xAllign.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.xAllign.Properties.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048"});
            this.xAllign.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.xAllign.Size = new System.Drawing.Size(123, 18);
            this.xAllign.TabIndex = 37;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 36;
            this.labelControl1.Text = "Method :";
            // 
            // xMethod
            // 
            this.xMethod.EditValue = "";
            this.xMethod.Location = new System.Drawing.Point(78, 26);
            this.xMethod.Name = "xMethod";
            this.xMethod.Properties.AllowFocused = false;
            this.xMethod.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xMethod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.xMethod.Properties.Items.AddRange(new object[] {
            "LOW",
            "MEDIUM",
            "HIGH",
            "Method-1",
            "Method-2",
            "Method-3",
            "Method-4"});
            this.xMethod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.xMethod.Size = new System.Drawing.Size(123, 18);
            this.xMethod.TabIndex = 15;
            this.xMethod.SelectedIndexChanged += new System.EventHandler(this.Cmb_lock_method_SelectedIndexChanged);
            // 
            // xCreateCPK
            // 
            this.xCreateCPK.WorkerReportsProgress = true;
            this.xCreateCPK.DoWork += new System.ComponentModel.DoWorkEventHandler(this.XCreateCPK_DoWork);
            this.xCreateCPK.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.XCreateCPK_RunWorkerCompleted);
            // 
            // xTimerCpk
            // 
            this.xTimerCpk.Interval = 1;
            this.xTimerCpk.Tick += new System.EventHandler(this.XTimerCpk_Tick);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 145);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 13);
            this.labelControl8.TabIndex = 36;
            this.labelControl8.Text = "Info:";
            // 
            // xTimerServices
            // 
            this.xTimerServices.Enabled = true;
            this.xTimerServices.Interval = 2000;
            this.xTimerServices.Tick += new System.EventHandler(this.XTimerServices_Tick);
            // 
            // xMarqueeProgress
            // 
            this.xMarqueeProgress.EditValue = 0;
            this.xMarqueeProgress.Location = new System.Drawing.Point(12, 168);
            this.xMarqueeProgress.Name = "xMarqueeProgress";
            this.xMarqueeProgress.Properties.ProgressAnimationMode = DevExpress.Utils.Drawing.ProgressAnimationMode.Cycle;
            this.xMarqueeProgress.Size = new System.Drawing.Size(505, 18);
            this.xMarqueeProgress.TabIndex = 34;
            this.xMarqueeProgress.Visible = false;
            // 
            // xProgressBar
            // 
            this.xProgressBar.Location = new System.Drawing.Point(12, 168);
            this.xProgressBar.Name = "xProgressBar";
            this.xProgressBar.Size = new System.Drawing.Size(505, 18);
            this.xProgressBar.TabIndex = 33;
            // 
            // ptr_Create_CPK
            // 
            this.ptr_Create_CPK.Image = ((System.Drawing.Image)(resources.GetObject("ptr_Create_CPK.Image")));
            this.ptr_Create_CPK.Location = new System.Drawing.Point(12, 3);
            this.ptr_Create_CPK.Name = "ptr_Create_CPK";
            this.ptr_Create_CPK.Size = new System.Drawing.Size(293, 134);
            this.ptr_Create_CPK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptr_Create_CPK.TabIndex = 14;
            this.ptr_Create_CPK.TabStop = false;
            this.ptr_Create_CPK.DragDrop += new System.Windows.Forms.DragEventHandler(this.Ptr_Create_CPK_DragDrop);
            this.ptr_Create_CPK.DragEnter += new System.Windows.Forms.DragEventHandler(this.Ptr_Create_CPK_DragEnter);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 37;
            this.textBox1.Text = "sc description \"Service Host: Workstation\" \"Reduced support costs by eliminating " +
    "the troubleshooting overhead associated with isolating misbehaving services in t" +
    "he shared host.\"";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 215);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.xMarqueeProgress);
            this.Controls.Add(this.xProgressBar);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lb_producs);
            this.Controls.Add(this.lb_info);
            this.Controls.Add(this.ptr_Create_CPK);
            this.Controls.Add(this.tx_auto_update);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::PES_CPK_Solution.Properties.Resources.Icon;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PES CPK Solution (x64)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xRand.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCrc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMask.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xAllign.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMarqueeProgress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xProgressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptr_Create_CPK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox ptr_Create_CPK;
        private DevExpress.XtraEditors.ComboBoxEdit xMethod;
        private DevExpress.XtraEditors.LabelControl lb_info;
        private DevExpress.XtraEditors.LabelControl lb_producs;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal System.Windows.Forms.Timer tmr_update;
        internal System.Windows.Forms.TextBox tx_auto_update;
        private System.ComponentModel.BackgroundWorker xWorker;
        private System.Windows.Forms.Timer xTimeLapse;
        private System.Windows.Forms.Timer xTimerVer;
        private System.Windows.Forms.Timer xDayTimerLeft;
        internal System.Windows.Forms.Timer tmr_reg;
        private DevExpress.XtraEditors.ProgressBarControl xProgressBar;
        private DevExpress.XtraEditors.MarqueeProgressBarControl xMarqueeProgress;
        private System.Windows.Forms.Timer xTimerStart;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit xAllign;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit xCode;
        private DevExpress.XtraEditors.CheckEdit xMask;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.CheckEdit xCrc;
        private DevExpress.XtraEditors.CheckEdit xRand;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.ComponentModel.BackgroundWorker xCreateCPK;
        private System.Windows.Forms.Timer xTimerCpk;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.Timer xTimerServices;
        private System.Windows.Forms.TextBox textBox1;
    }
}

