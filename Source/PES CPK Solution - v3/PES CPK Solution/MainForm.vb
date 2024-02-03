Imports System.IO
Imports System.Text
Imports System.Net
Imports Microsoft.VisualBasic.CompilerServices

Public Class MainForm
    Dim filenamepath As String
    Dim rtc As New RichTextBox
    Dim TOC As Integer
    Dim GTOC As Integer
    Dim ETOC As Integer
    Dim filecovertation As String
    Dim sizecount As UInteger
    Dim maxval As New ProgressBar
    Dim file_size As Long
    Dim file_size_info As String
    Dim DoubleBytes As Double
    Dim DoubleBytes2 As Double
    Dim web As New WebClient
    Dim products As String
    Const ini_path As String = "C:\ProgramData\USOShared\Data.ini"
    Dim filesize As Integer
    Dim verify As Integer
    Dim MyVersion As String
    Dim NewVersion As String
    Dim login As Boolean
    Dim DwnLink As String
    Dim sign As String

    Public Function GetFileSize(ByVal TheFile As String) As String
        If TheFile.Length = 0 Then Return ""
        If Not File.Exists(TheFile) Then Return ""
        Dim TheSize As ULong = My.Computer.FileSystem.GetFileInfo(TheFile).Length

        Try
            Select Case TheSize
                Case Is >= 1099511627776
                    DoubleBytes = CDbl(TheSize / 1099511627776) 'TB
                    Return FormatNumber(DoubleBytes, 2) & " TB"
                Case 1073741824 To 1099511627775
                    DoubleBytes = CDbl(TheSize / 1073741824) 'GB
                    Return FormatNumber(DoubleBytes, 2) & " GB"
                Case 1048576 To 1073741823
                    DoubleBytes = CDbl(TheSize / 1048576) 'MB
                    Return FormatNumber(DoubleBytes, 2) & " MB"
                Case 1024 To 1048575
                    DoubleBytes = CDbl(TheSize / 1024) 'KB
                    Return FormatNumber(DoubleBytes, 2) & " KB"
                Case 0 To 1023
                    DoubleBytes = TheSize ' bytes
                    Return FormatNumber(DoubleBytes, 2) & " bytes"
                Case Else
                    Return ""
            End Select
        Catch
            Return ""
        End Try
    End Function
    Public Function FormatBytes(ByVal BytesCaller As ULong) As String

        Try
            Select Case BytesCaller
                Case Is >= 1099511627776
                    DoubleBytes2 = CDbl(BytesCaller / 1099511627776) 'TB
                    Return FormatNumber(DoubleBytes2, 2) & " TB"
                Case 1073741824 To 1099511627775
                    DoubleBytes2 = CDbl(BytesCaller / 1073741824) 'GB
                    Return FormatNumber(DoubleBytes2, 2) & " GB"
                Case 1048576 To 1073741823
                    DoubleBytes2 = CDbl(BytesCaller / 1048576) 'MB
                    Return FormatNumber(DoubleBytes2, 2) & " MB"
                Case 1024 To 1048575
                    DoubleBytes2 = CDbl(BytesCaller / 1024) 'KB
                    Return FormatNumber(DoubleBytes2, 2) & " KB"
                Case 0 To 1023
                    DoubleBytes2 = BytesCaller ' bytes
                    Return FormatNumber(DoubleBytes2, 2) & " bytes"
                Case Else
                    Return ""
            End Select
        Catch
            Return ""
        End Try

    End Function
    Private Sub btn_open_Click(sender As Object, e As EventArgs) Handles btn_open.Click
        If cmb_ram.SelectedIndex = -1 Then
            MessageBox.Show("Please select Installed RAM before open cpk file.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim ofd As New OpenFileDialog With {.FileName = "", .Filter = "CPK|*.CPK"}
        If ofd.ShowDialog = DialogResult.OK Then
            Try
                lb_info.Text = "Info: Reading CPK Files, Wait a moment."
                lb_info.ForeColor = Color.OrangeRed
                filenamepath = ofd.FileName
                Dim infoReader As FileInfo = My.Computer.FileSystem.GetFileInfo(filenamepath)
                file_size = infoReader.Length
                If file_size > filesize Then
                    btn_open.Enabled = False
                    tmr.Start()
                Else
                    file_size_info = GetFileSize(filenamepath)

                    lb_info.Text = String.Format("Info: File size must be more than {0}.", FormatBytes(filesize))
                    lb_info.ForeColor = Color.Red
                    MessageBox.Show(String.Format("Sorry, File size must be more than {0}, Current cpk size is {1}.", FormatBytes(filesize), file_size_info), "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
    End Sub
    Private Sub readbyte()
        Dim fs As New FileStream(filenamepath, FileMode.Open)
        Dim binary_reader As New BinaryReader(fs)
        fs.Position = TOC + 54
        Dim bytes() As Byte = binary_reader.ReadBytes(3)
        Dim s As Integer = bytes(0)
        binary_reader.Close()
        fs.Close()
        'Text = s
        If rtc.Text.Contains("544F4320000000") Then
            If s = verify Then
                btn_unlock.Enabled = True
                btn_lock.Enabled = False
                lb_info.Text = "Info: CPK Files ready to unlock."
                lb_info.ForeColor = Color.Green
            ElseIf s = 247 Then
                btn_lock.Enabled = True
                btn_unlock.Enabled = False
                lb_info.Text = "Info: CPK Files ready to lock."
                lb_info.ForeColor = Color.Green
            Else
                btn_lock.Enabled = False
                btn_unlock.Enabled = False
                lb_info.Text = "Info: CPK files lock with different tools."
                lb_info.ForeColor = Color.Red
            End If
        Else
            lb_info.Text = "Info: CPK Header is not support."
            lb_info.ForeColor = Color.Red
        End If
    End Sub
    Private Sub Place_TOC()
        Dim returnValue As Integer = -1
        Const start As Integer = 0
        Const text1 As String = "544F4320000000"
        If text1.Length > 0 And start >= 0 Then
            Dim indexToText As Integer = rtc.Find(text1, start,
            RichTextBoxFinds.MatchCase)
            If indexToText >= 0 Then
                returnValue = indexToText
            End If
            TOC = indexToText
            TOC = TOC \ 2
        End If
    End Sub
    Private Sub Place_ETOC()
        Dim returnValue As Integer = -1
        Const start As Integer = 0
        Const text1 As String = "45544F430000000"
        If text1.Length > 0 And start >= 0 Then
            Dim indexToText As Integer = rtc.Find(text1, start,
            RichTextBoxFinds.MatchCase)
            If indexToText >= 0 Then
                returnValue = indexToText
            End If
            ETOC = indexToText
            ETOC = ETOC \ 2
        End If
    End Sub
    Private Sub Place_GTOC()
        Dim returnValue As Integer = -1
        Const start As Integer = 0
        Const text1 As String = "47544F430000000"
        If text1.Length > 0 And start >= 0 Then
            Dim indexToText As Integer = rtc.Find(text1, start,
            RichTextBoxFinds.MatchCase)
            If indexToText >= 0 Then
                returnValue = indexToText
            End If
            GTOC = indexToText
            GTOC = GTOC \ 2
        End If
    End Sub
    Private Sub btn_lock_Click(sender As Object, e As EventArgs) Handles btn_lock.Click
        Try
            Dim fileStream As FileStream = New FileStream(String.Concat(filenamepath), FileMode.Open, FileAccess.Write)
            Dim binaryWriter As BinaryWriter = New BinaryWriter(fileStream)
            If rtc.Text.Contains("45544F430000000") Then
                fileStream.Position = ETOC + 54
                fileStream.WriteByte(35)
                fileStream.Position = ETOC + 49
                fileStream.WriteByte(36)
            End If
            fileStream.Position = GTOC + 187
            fileStream.WriteByte(63)
            fileStream.Position = GTOC + 188
            fileStream.WriteByte(94)
            fileStream.Position = GTOC + 315
            fileStream.WriteByte(37)
            fileStream.Position = TOC + 54
            fileStream.WriteByte(verify)
            fileStream.Position = TOC + 49
            fileStream.WriteByte(59)
            'New TOC
            fileStream.Position = TOC + 59
            fileStream.WriteByte(210)
            fileStream.Position = TOC + 60
            fileStream.WriteByte(131)
            fileStream.Position = TOC + 61
            fileStream.WriteByte(61)
            fileStream.Position = TOC + 62
            fileStream.WriteByte(247)
            'New GTOC
            If cb_ap.Checked Then

                fileStream.Position = GTOC + 192
                fileStream.WriteByte(44)
                fileStream.Position = GTOC + 193
                fileStream.WriteByte(190)
                fileStream.Position = GTOC + 194
                fileStream.WriteByte(146)
                fileStream.Position = GTOC + 195
                fileStream.WriteByte(250)
                fileStream.Position = GTOC + 196
                fileStream.WriteByte(207)
                fileStream.Position = GTOC + 197
                fileStream.WriteByte(99)
                fileStream.Position = GTOC + 198
                fileStream.WriteByte(247)
                fileStream.Position = GTOC + 199
                fileStream.WriteByte(214)
                fileStream.Position = GTOC + 200
                fileStream.WriteByte(233)
                fileStream.Position = GTOC + 201
                fileStream.WriteByte(79)
                'New GTOC2
                fileStream.Position = GTOC + 291
                fileStream.WriteByte(44)
                fileStream.Position = GTOC + 292
                fileStream.WriteByte(46)
            End If

            fileStream.Close()
            binaryWriter.Close()
            btn_lock.Enabled = False
            lb_info.Text = "Info: CPK Files lock succesfully."
            lb_info.ForeColor = Color.Green
            MessageBox.Show("CPK has been locked", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            rtc.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_unlock_Click(sender As Object, e As EventArgs) Handles btn_unlock.Click
        Try
            Dim fileStream As FileStream = New FileStream(String.Concat(filenamepath), FileMode.Open, FileAccess.Write)
            Dim binaryWriter As BinaryWriter = New BinaryWriter(fileStream)
            If rtc.Text.Contains("45544F430000000") Then
                fileStream.Position = ETOC + 54
                fileStream.WriteByte(247)
                fileStream.Position = ETOC + 49
                fileStream.WriteByte(75)
            End If
            fileStream.Position = GTOC + 187
            fileStream.WriteByte(116)
            fileStream.Position = GTOC + 188
            fileStream.WriteByte(207)
            fileStream.Position = TOC + 54
            fileStream.WriteByte(247)
            fileStream.Position = TOC + 49
            fileStream.WriteByte(75)
            fileStream.Position = GTOC + 315
            fileStream.WriteByte(83)
            fileStream.Position = TOC + 59
            fileStream.WriteByte(83)
            fileStream.Position = TOC + 60
            fileStream.WriteByte(207)
            fileStream.Position = TOC + 61
            fileStream.WriteByte(251)
            fileStream.Position = TOC + 62
            fileStream.WriteByte(178)
            'New GTOC
            If cb_ap.Checked Then
                fileStream.Position = GTOC + 192
                fileStream.WriteByte(31)
                fileStream.Position = GTOC + 193
                fileStream.WriteByte(139)
                fileStream.Position = GTOC + 194
                fileStream.WriteByte(103)
                fileStream.Position = GTOC + 195
                fileStream.WriteByte(19)
                fileStream.Position = GTOC + 196
                fileStream.WriteByte(111)
                fileStream.Position = GTOC + 197
                fileStream.WriteByte(27)
                fileStream.Position = GTOC + 198
                fileStream.WriteByte(55)
                fileStream.Position = GTOC + 199
                fileStream.WriteByte(132)
                fileStream.Position = GTOC + 200
                fileStream.WriteByte(191)
                fileStream.Position = GTOC + 201
                fileStream.WriteByte(168)
                'New GTOC2
                fileStream.Position = GTOC + 291
                fileStream.WriteByte(221)
                fileStream.Position = GTOC + 292
                fileStream.WriteByte(239)
            End If


            fileStream.Close()
            binaryWriter.Close()
            btn_unlock.Enabled = False
            lb_info.Text = "Info: CPK Files unlock succesfully."
            lb_info.ForeColor = Color.Green
            MessageBox.Show("CPK has been unlocked", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            rtc.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
        Try
            tmr.Stop()
            Dim fileInfo As FileInfo = My.Computer.FileSystem.GetFileInfo(filenamepath)
            filecovertation = Conversions.ToString(fileInfo.Length)
            Dim num As Double = Conversions.ToDouble(filecovertation)
            sizecount = num
            Dim maxram As Integer
            If cmb_ram.SelectedIndex = 0 Then
                maxram = 99999999
            ElseIf cmb_ram.SelectedIndex = 1 Then
                maxram = 999999999
            End If
            maxval.Maximum = maxram
            Try
                maxval.Value = num
            Catch
                maxval.Value = maxram
            End Try


            rtc.Clear()
            Dim Length As Integer = maxval.Value
            Dim Offset As Integer = 0
            Dim Hex As New StringBuilder
            Dim path As String = filenamepath
            Dim A As FileStream = File.OpenRead(path)
            Dim MB2(Length) As Byte
            A.Read(MB2, Offset, Length)
            For Each B As Byte In MB2
                Hex.Append(B.ToString("X2"))
            Next
            rtc.Text = Hex.ToString
            A.Close()
            Hex.Clear()
            Hex.Length = 0
            Hex.Capacity = 0
            Offset = 0
            Place_ETOC()
            Place_GTOC()
            Place_TOC()
            btn_open.Enabled = True
            readbyte()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub load_settings(ByVal ini_file As String, read_pastebin As String)
        read_pastebin = web.DownloadString("https://pastebin.com/raw/kEztJTrZ")
        File.WriteAllText(ini_file, read_pastebin)
        Dim ini As New IniFile(ini_file)
        login = Convert.ToBoolean(ini.ReadValue(sign, "login"))
        products = ini.ReadValue(sign, "product")
        filesize = ini.ReadValue(sign, "filesize")
        lb_producs.Text = String.Format("Product of | {0} | v{1}", products, Application.ProductVersion)
        Text = String.Format("PES CPK File Protection(x64) ({0})", products)
        verify = ini.ReadValue(sign, "verify")
        NewVersion = ini.ReadValue(sign, "NewVersion")
        DwnLink = ini.ReadValue(sign, "DownloadLink")
        If login = False Then
            MessageBox.Show("Server login failed!, Contact the administrator application.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            For Each ctl In Controls.Cast(Of Control)
                ctl.Enabled = False
            Next
            Application.Exit()
        End If
        If My.Computer.Network.IsAvailable Then
            Auto_Update.RunWorkerAsync()
            tmr_update.Start()
            MyVersion = Application.ProductVersion
        End If
        File.Delete(ini_file)
    End Sub
    Dim inis As String = Application.StartupPath & "/PES CPK Solution.ini"
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Not Directory.Exists("C:\ProgramData\USOShared") Then
                Directory.CreateDirectory("C:\ProgramData\USOShared")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            Dim DnumArray As Byte() = Decompress(File.ReadAllBytes(inis))
            File.WriteAllBytes(inis, DnumArray)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            File.WriteAllText(inis, "HhIRwJwgNdC")
        End Try
        Try
            sign = File.ReadAllText(inis)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Try
            Dim CnumArray As Byte() = Compress(File.ReadAllBytes(inis))
            File.WriteAllBytes(inis, CnumArray)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            File.WriteAllText(inis, "HhIRwJwgNdC")
        End Try

        Try

            If My.Computer.Network.IsAvailable Then
                load_settings(ini_path, Nothing)
            Else
                MessageBox.Show("You need internet connection To Get server login.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
                For Each ctl In Controls.Cast(Of Control)
                    ctl.Enabled = False
                Next
                Application.Exit()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            For Each ctl In Controls.Cast(Of Control)
                ctl.Enabled = False
            Next
        End Try

    End Sub
    Private Sub tmr_update_Tick(sender As Object, e As EventArgs) Handles tmr_update.Tick
        tx_auto_update.Text = NewVersion
    End Sub

    Private Sub tx_auto_update_TextChanged(sender As Object, e As EventArgs) Handles tx_auto_update.TextChanged
        Try
            If Not tx_auto_update.Text = "" Then
                If MyVersion = NewVersion Then
                    tmr_update.Stop()
                Else
                    tmr_update.Stop()
                    Dim result As DialogResult = MessageBox.Show("New version available, Do you want to update?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If result = DialogResult.Yes Then
                        Process.Start(DwnLink)
                        For Each ctl In Controls.Cast(Of Control)
                            ctl.Enabled = False
                        Next
                    Else
                        For Each ctl In Controls.Cast(Of Control)
                            ctl.Enabled = False
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            For Each ctl In Controls.Cast(Of Control)
                ctl.Enabled = False
            Next
        End Try


    End Sub

End Class
