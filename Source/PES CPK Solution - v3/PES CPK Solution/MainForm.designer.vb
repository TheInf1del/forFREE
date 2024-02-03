<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.btn_unlock = New System.Windows.Forms.Button()
        Me.btn_lock = New System.Windows.Forms.Button()
        Me.btn_open = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lb_info = New System.Windows.Forms.Label()
        Me.tmr = New System.Windows.Forms.Timer(Me.components)
        Me.cmb_ram = New System.Windows.Forms.ComboBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.lb_producs = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.VisualStyler1 = New SkinSoft.VisualStyler.VisualStyler(Me.components)
        Me.tmr_update = New System.Windows.Forms.Timer(Me.components)
        Me.Auto_Update = New System.ComponentModel.BackgroundWorker()
        Me.tx_auto_update = New System.Windows.Forms.TextBox()
        Me.cb_ap = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VisualStyler1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_unlock
        '
        Me.btn_unlock.Enabled = False
        Me.btn_unlock.Location = New System.Drawing.Point(216, 133)
        Me.btn_unlock.Name = "btn_unlock"
        Me.btn_unlock.Size = New System.Drawing.Size(92, 31)
        Me.btn_unlock.TabIndex = 7
        Me.btn_unlock.Text = "Unlock CPK"
        Me.btn_unlock.UseVisualStyleBackColor = True
        '
        'btn_lock
        '
        Me.btn_lock.Enabled = False
        Me.btn_lock.Location = New System.Drawing.Point(114, 133)
        Me.btn_lock.Name = "btn_lock"
        Me.btn_lock.Size = New System.Drawing.Size(92, 31)
        Me.btn_lock.TabIndex = 6
        Me.btn_lock.Text = "Lock CPK"
        Me.btn_lock.UseVisualStyleBackColor = True
        '
        'btn_open
        '
        Me.btn_open.Location = New System.Drawing.Point(12, 133)
        Me.btn_open.Name = "btn_open"
        Me.btn_open.Size = New System.Drawing.Size(92, 31)
        Me.btn_open.TabIndex = 5
        Me.btn_open.Text = "Open CPK"
        Me.btn_open.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(246, 170)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Copyright © 2019 MjTs-140914™"
        '
        'lb_info
        '
        Me.lb_info.AutoSize = True
        Me.lb_info.Location = New System.Drawing.Point(12, 116)
        Me.lb_info.Name = "lb_info"
        Me.lb_info.Size = New System.Drawing.Size(28, 13)
        Me.lb_info.TabIndex = 9
        Me.lb_info.Text = "Info:"
        '
        'tmr
        '
        '
        'cmb_ram
        '
        Me.cmb_ram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_ram.FormattingEnabled = True
        Me.cmb_ram.Items.AddRange(New Object() {"4/8GB RAM", "16/32GB RAM"})
        Me.cmb_ram.Location = New System.Drawing.Point(314, 12)
        Me.cmb_ram.Name = "cmb_ram"
        Me.cmb_ram.Size = New System.Drawing.Size(126, 21)
        Me.cmb_ram.TabIndex = 10
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Location = New System.Drawing.Point(314, 59)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(126, 105)
        Me.TextBox2.TabIndex = 11
        Me.TextBox2.Text = "Please notice:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This tools need RAM Usage Minimum 4GB of RAM." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please select In" &
    "stalled RAM from this computer."
        '
        'lb_producs
        '
        Me.lb_producs.AutoSize = True
        Me.lb_producs.Location = New System.Drawing.Point(9, 170)
        Me.lb_producs.Name = "lb_producs"
        Me.lb_producs.Size = New System.Drawing.Size(90, 13)
        Me.lb_producs.TabIndex = 12
        Me.lb_producs.Text = "Product of | | v3.0"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(15, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(293, 101)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'VisualStyler1
        '
        Me.VisualStyler1.HostForm = Me
        Me.VisualStyler1.License = CType(resources.GetObject("VisualStyler1.License"), SkinSoft.VisualStyler.Licensing.VisualStylerLicense)
        Me.VisualStyler1.ShowFocusCues = False
        Me.VisualStyler1.LoadVisualStyle(Nothing, "OSX (Leopard).vssf")
        '
        'tmr_update
        '
        Me.tmr_update.Interval = 3000
        '
        'tx_auto_update
        '
        Me.tx_auto_update.Location = New System.Drawing.Point(344, 77)
        Me.tx_auto_update.Name = "tx_auto_update"
        Me.tx_auto_update.Size = New System.Drawing.Size(21, 20)
        Me.tx_auto_update.TabIndex = 31
        '
        'cb_ap
        '
        Me.cb_ap.AutoSize = True
        Me.cb_ap.Location = New System.Drawing.Point(314, 39)
        Me.cb_ap.Name = "cb_ap"
        Me.cb_ap.Size = New System.Drawing.Size(126, 17)
        Me.cb_ap.TabIndex = 32
        Me.cb_ap.Text = "Advanced Protection"
        Me.ToolTip1.SetToolTip(Me.cb_ap, "If game crash, dont checklist this.")
        Me.cb_ap.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 190)
        Me.Controls.Add(Me.cb_ap)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.tx_auto_update)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lb_producs)
        Me.Controls.Add(Me.cmb_ram)
        Me.Controls.Add(Me.lb_info)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_unlock)
        Me.Controls.Add(Me.btn_lock)
        Me.Controls.Add(Me.btn_open)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PES CPK File Protection(x64) ()"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VisualStyler1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_unlock As Button
    Friend WithEvents btn_lock As Button
    Friend WithEvents btn_open As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lb_info As Label
    Friend WithEvents tmr As Timer
    Friend WithEvents cmb_ram As ComboBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents lb_producs As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents VisualStyler1 As SkinSoft.VisualStyler.VisualStyler
    Friend WithEvents tmr_update As Timer
    Friend WithEvents Auto_Update As System.ComponentModel.BackgroundWorker
    Friend WithEvents tx_auto_update As TextBox
    Friend WithEvents cb_ap As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
