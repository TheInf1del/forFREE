using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace PES_CPK_Solution
{
    public partial class RegForm : DevExpress.XtraEditors.XtraForm
    {
        private void InitializeInstanceFields()
        {
            ini = new IniFile(iniFilepath);
        }
        public RegForm()
        {
            InitializeInstanceFields();
            InitializeComponent();
        }
        private string iniFilepath = MainForm.GetAppDataPath("Microsoft\\Data.ini");
        private IniFile ini;
        private string DataIniName;
        private string DataIniSerial;
        private string read_str;
        private WebClient web = new WebClient();
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                DataIniName = ini.ReadValue(tx_username.Text, "xusername");
                DataIniSerial = ini.ReadValue(tx_username.Text, "xserialnumber");
                if (string.IsNullOrEmpty(tx_username.Text))
                {
                    XtraMessageBox.Show("Insert your username.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(tx_serial.Text))
                {
                    XtraMessageBox.Show("Insert your serial number.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (tx_username.Text != DataIniName)
                {
                    XtraMessageBox.Show("Username isn't correct.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (tx_serial.Text != DataIniSerial)
                {
                    XtraMessageBox.Show("Serial number isn't correct.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\PES CPK Solution", "User Name", DataIniName);
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\PES CPK Solution", "Serial Number", DataIniSerial);
                XtraMessageBox.Show("Registration succesfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (File.Exists(iniFilepath))
                {
                    File.Delete(iniFilepath);
                }
                Environment.Exit(0);
                if (File.Exists($"{Application.StartupPath}\\PES CPK Solution.exe"))
                {
                    Process.Start($"{Application.StartupPath}\\PES CPK Solution.exe");
                }
                return;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void RegForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            Environment.Exit(0);
        }

        private void RegForm_Load(object sender, EventArgs e)
        {
            read_str = web.DownloadString($"{Encryptions.gistUser}{Encryptions.gitHash}");
            string plain = MainClass.DecryptData(read_str, Encryptions.Decrypt("9lCjnKBzny12rZLP65JlkTT0dDIZi7eDkXedgv72+do="));
            string plain2 = MainClass.Decrypt_m2(plain);
            read_str = MainClass.Decrypt_m1(plain2, Encryptions.Decrypt("9lCjnKBzny12rZLP65JlkTT0dDIZi7eDkXedgv72+do="));
            File.WriteAllText(iniFilepath, read_str);
        }
    }
}