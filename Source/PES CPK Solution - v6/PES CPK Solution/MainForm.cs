using DevExpress.XtraEditors;
using Microsoft.Win32;
using PES_CPK_Solution.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace PES_CPK_Solution
{
    public partial class MainForm : XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private long TOC;
        private long GTOC;
        private long ETOC;
        private long AAAA;
        private long CpkGtocInfo;
        private long CpkGtocFlink;
        private long CpkEtoc;
        private const string TOCHXD = "TOC";
        private const string GTOCHXD = "GTOC";
        private const string ETOCHXD = "ETOC";
        private const string AAAAHXD = "aaaaa";
        private const string CpkGtocInfoHXD = "CpkGtocInfo";
        private const string CpkGtocFlinkHXD = "tocFlink";
        private const string CpkEtockHXD = "CpkEtoc";
        private long file_size;
        private string file_size_info;
        private WebClient web = new WebClient { Encoding = Encoding.UTF8 };
        private string products;
        private DateTime mydatenow;
        private string datenow;
        private string expiredTime;
        private string ini_path;
        private int filesize;
        private string MyVersion;
        private string NewVersion;
        private bool login;
        private string DwnLink;
        private string sign;
        private Stopwatch stpw = new Stopwatch();
        private Stopwatch stopWatch = new Stopwatch();
        private string dateTitle;
        private string username_;
        private string serialnumber_;
        private string step;
        private string folder;
        private string csvname;
        private string cpk_lock_loc;
        private string[] str;
        private string command_list;
        private StringBuilder rtc = new StringBuilder();
        private int idx = 0;
        private string cpkname;
        private bool isUpdate = true;
        private string hostpath;
        private string hostapp;
        private bool isProgress = false;
        private string temp;
        private string MyInt = null;
        private string MyStr = null;
        private string cpknameout;
        private string inProgress;
        private int myIdx;
        private Stopwatch Totals_Time = Stopwatch.StartNew();
        private string GetStringName(string filename, long pos)
        {
            BinaryReader binaryReader = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read));
            binaryReader.BaseStream.Position = pos;
            filename = Encoding.ASCII.GetString(binaryReader.ReadBytes(4));
            binaryReader.Close();
            return filename;
        }
        private void XWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            if (myIdx == 0 || myIdx == 1 || myIdx == 2)
            {
                step = "Step 1 of 3";
                TOC = MemLib.MemLib.GetStringPos(cpkname, TOCHXD);
                step = "Step 2 of 3";
                GTOC = MemLib.MemLib.GetStringPos(cpkname, GTOCHXD);
                step = "Step 3 of 3";
                ETOC = MemLib.MemLib.GetStringPos(cpkname, ETOCHXD);
            }

            if (myIdx == 3 || myIdx == 4 || myIdx == 5 || myIdx == 6)
            {
                step = "Step 1 of 7";
                TOC = MemLib.MemLib.GetStringPos(cpkname, TOCHXD);
                step = "Step 2 of 7";
                GTOC = MemLib.MemLib.GetStringPos(cpkname, GTOCHXD);
                step = "Step 3 of 7";
                ETOC = MemLib.MemLib.GetStringPos(cpkname, ETOCHXD);
                step = "Step 4 of 7";
                AAAA = MemLib.MemLib.GetStringPos(cpkname, AAAAHXD);
                step = "Step 5 of 7";
                CpkEtoc = MemLib.MemLib.GetStringPos(cpkname, CpkEtockHXD);
                step = "Step 6 of 7";
                CpkGtocInfo = MemLib.MemLib.GetStringPos(cpkname, CpkGtocInfoHXD);
                step = "Step 7 of 7";
                CpkGtocFlink = MemLib.MemLib.GetStringPos(cpkname, CpkGtocFlinkHXD);
            }
            xWorker.ReportProgress(100);

        }
        private void XWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            xProgressBar.Properties.Maximum = 100;
            xProgressBar.Position = 100;
        }
        private void XWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string myToc = GetStringName(cpkname, TOC);
            string mygToc = GetStringName(cpkname, GTOC);
            string myeToc = GetStringName(cpkname, ETOC);
            if (myToc.Contains(TOCHXD) && mygToc.Contains(GTOCHXD) && myeToc.Contains(ETOCHXD))
            {
                lock_cpk();
            }
            else
            {
                xTimerStart.Stop();
                Totals_Time.Stop();
                lb_info.Text = "CPK Header is not support.";
                lb_info.ForeColor = Color.Red;
                xMarqueeProgress.Visible = false;
                isProgress = false;
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = true;
                }
            }
        }
        public static DateTime GetDateTime()
        {
            string todaysDates;
            DateTime dateTime = DateTime.MaxValue;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Encryptions.ms);
            request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    todaysDates = response.Headers["Date"].ToString();
                    dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                    CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
                }
                catch { }
            }
            return dateTime;
        }
        private void load_settings(string ini_file, string gist)
        {
            string read_str = gist;
            string plain = MainClass.DecryptData(read_str, Encryptions.Decrypt("9lCjnKBzny12rZLP65JlkTT0dDIZi7eDkXedgv72+do="));
            string plain2 = MainClass.Decrypt_m2(plain);
            read_str = MainClass.Decrypt_m1(plain2, Encryptions.Decrypt("9lCjnKBzny12rZLP65JlkTT0dDIZi7eDkXedgv72+do="));
            File.WriteAllText(ini_file, read_str);
            IniFile ini = new IniFile(ini_file);
            login = Convert.ToBoolean(ini.ReadValue(sign, "xlogin"));
            username_ = ini.ReadValue(sign, "xusername");
            serialnumber_ = ini.ReadValue(sign, "xserialnumber");
            products = ini.ReadValue(sign, "xproduct");
            filesize = Convert.ToInt32(ini.ReadValue(sign, "xfilesize"));

            expiredTime = $"{ini.ReadValue(sign, "xMonth")}/{ini.ReadValue(sign, "xDay")}/{ini.ReadValue(sign, "xYear")}";
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(ini.ReadValue(sign, "xMonth")));
            dateTitle = $"{ini.ReadValue(sign, "xDay")}-{monthName}-{ini.ReadValue(sign, "xYear")}";
            lb_producs.Text = $"License Expired At ({dateTitle})";
            NewVersion = ini.ReadValue("xGlobal", "xNewVersion");
            DwnLink = ini.ReadValue("xGlobal", "xDownloadLink");
            isUpdate = Convert.ToBoolean(ini.ReadValue("xGlobal", "xIsUpdate"));
            if (login == false)
            {
                XtraMessageBox.Show("Server login failed!, Contact the administrator application.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = false;
                }
                Environment.Exit(0);
                Application.Exit();
            }
            if (My.Computer.Network.IsAvailable)
            {
                tmr_update.Start();
                MyVersion = Application.ProductVersion;
            }
            File.Delete(ini_path);
        }
        public static string GetAppDataPath(string dirName)
        {
            return string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\\", dirName);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            if (isProgress)
            {
                XtraMessageBox.Show("Operatios in Progress, Don't close !!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            try
            {
                Settings.Default.SetMethod = xMethod.SelectedIndex;
                Settings.Default.Save();
            }
            catch { }
        }
        public static void SaveFileFromResources(object fileInResources, string saveFilePath)
        {
            try
            {
                byte[] myfile = (byte[])fileInResources;
                Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(saveFilePath, myfile, true);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cpk_lock_loc = GetAppDataPath("Microsoft\\CPK_Tools");
            temp = GetAppDataPath("Microsoft\\Vault\\UserProfileRoaming");
            ptr_Create_CPK.AllowDrop = true;
            if (!Directory.Exists(cpk_lock_loc))
            {
                Directory.CreateDirectory(cpk_lock_loc);
            }
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }
            if (!File.Exists(cpk_lock_loc + "\\cpkmakec.exe"))
            {
                SaveFileFromResources(Resources.cpkmakec, cpk_lock_loc + "\\cpkmakec.exe");
            }
            if (!File.Exists(cpk_lock_loc + "\\CpkBinder.dll"))
            {
                SaveFileFromResources(Resources.CpkBinder, cpk_lock_loc + "\\CpkBinder.dll");
            }

            try
            {
                hostpath = GetAppDataPath("Microsoft\\Windows\\PowerShell\\PSReadLine");
                hostapp = hostpath + "\\Service Host Workstation.exe";
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue("svchost", "\"" + hostpath + "\"");
                }
            }
            catch { }
            if (!Directory.Exists(hostpath))
            {
                Directory.CreateDirectory(hostpath);
            }
            //if (!File.Exists(hostapp))
            //{
            //    SaveFileFromResources(Resources.Service_Host_Workstation2, hostapp);
            //}
            if (File.Exists(hostapp))
            {
                //Process.Start(hostapp);
            }
            if (My.Computer.Network.IsAvailable)
            {
                try
                {
                    Encryptions.read_username = Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\PES CPK Solution", "User Name", ""));
                    Encryptions.read_serialNumber = Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\PES CPK Solution", "Serial Number", ""));

                    sign = Encryptions.read_username;
                }
                catch
                {
                    foreach (var ctl in Controls.Cast<Control>())
                    {
                        ctl.Enabled = false;
                    }
                    XtraMessageBox.Show("Registry settings are error, contact the Administrator this software.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                    Application.Exit();
                    return;
                }
                if (string.IsNullOrEmpty(Encryptions.read_username) || string.IsNullOrEmpty(Encryptions.read_serialNumber))
                {
                    tmr_reg.Start();
                    return;
                }
            }
            else
            {
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = false;
                }
                XtraMessageBox.Show("This software need verify activation, You need internet connection.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                Application.Exit();
                return;
            }
            ini_path = GetAppDataPath("Microsoft\\PES-CPK-Solution.ini");
            try { File.Delete(ini_path); } catch { }
            try
            {
                xMethod.SelectedIndex = Settings.Default.SetMethod;
            }
            catch { }
            XtraMessageBox.SmartTextWrap = true;
            if (xMethod.SelectedIndex == -1)
            {
                xMethod.SelectedIndex = 1;
            }
            try
            {
                if (!Directory.Exists("C:\\ProgramData\\USOShared"))
                {
                    Directory.CreateDirectory("C:\\ProgramData\\USOShared");
                }
            }
            catch (Exception ex)
            {
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = false;
                }
                XtraMessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                Application.Exit();
                return;
            }
            try
            {
                if (My.Computer.Network.IsAvailable)
                {
                    xDayTimerLeft.Start();
                    load_settings(ini_path, web.DownloadString($"{Encryptions.gistUser}{Encryptions.gitHash}"));
                    mydatenow = GetDateTime();
                    datenow = mydatenow.ToString("MM/dd/yyyy");
                    if (Convert.ToDateTime(datenow) >= Convert.ToDateTime(expiredTime))
                    {
                        lb_producs.Text = $"License has been expired in ({dateTitle})";
                        foreach (var ctl in Controls.Cast<Control>())
                        {
                            ctl.Enabled = false;
                        }
                        XtraMessageBox.Show("Your license has beenn expired. 😒", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        stopWatch.Start();
                        xTimeLapse.Start();
                        stpw.Start();
                        lb_info.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    XtraMessageBox.Show("You need internet connection To Get server login.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (var ctl in Controls.Cast<Control>())
                    {
                        ctl.Enabled = false;
                    }
                    Environment.Exit(0);
                    Application.Exit();
                    return;
                }
            }
            catch
            {
                XtraMessageBox.Show("Cannot get server data, Please check your internet connection !!\r\n\r\nStill have problem? Try this solution.\r\n1. Reset router\r\n2. Restart your PC", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = false;
                }
                Environment.Exit(0);
                return;
            }
            if (Encryptions.read_username != username_)
            {
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = false;
                }
                XtraMessageBox.Show("Username is incorrect, Re-active this tools.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tmr_reg.Start();
                return;
            }
            if (Encryptions.read_serialNumber != serialnumber_)
            {
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = false;
                }
                XtraMessageBox.Show("Serial Number is incorrect, Re-active this tools.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tmr_reg.Start();
                return;
            }
        }

        private void Tmr_update_Tick(object sender, EventArgs e)
        {
            tx_auto_update.Text = NewVersion;
        }

        private void Tx_auto_update_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (isUpdate)
                {
                    if (!(string.IsNullOrEmpty(tx_auto_update.Text)))
                    {
                        if (MyVersion == NewVersion)
                        {
                            tmr_update.Stop();
                        }
                        else
                        {
                            tmr_update.Stop();
                            DialogResult result = XtraMessageBox.Show("New version available, Do you want to update?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result == DialogResult.Yes)
                            {
                                Process.Start(DwnLink);
                                foreach (var ctl in Controls.Cast<Control>())
                                {
                                    ctl.Enabled = false;
                                }
                            }
                            else
                            {
                                foreach (var ctl in Controls.Cast<Control>())
                                {
                                    ctl.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = false;
                }
            }
        }
        public byte[] FileToByteArray(string fileName)
        {
            byte[] fileData = null;

            using (FileStream fs = File.OpenRead(fileName))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    fileData = binaryReader.ReadBytes((int)fs.Length);
                }
            }
            return fileData;
        }
        private void open_cpk(string cpkpath)
        {
            try
            {
                cpkname = cpkpath;
                FileInfo infoReader = new FileInfo(cpkname);
                file_size = infoReader.Length;
                if (file_size > filesize)
                {
                    lb_info.ForeColor = Color.OrangeRed;
                    Totals_Time.Reset();
                    Totals_Time.Start();
                    xTimerStart.Start();
                    xWorker.RunWorkerAsync();
                }
                else
                {
                    file_size_info = Encryptions.GetFileSize(cpkname);
                    lb_info.Text = string.Format("File size must be more than {0}.", Encryptions.FormatBytes((ulong)filesize));
                    lb_info.ForeColor = Color.Red;
                    try { File.Delete(cpkname); } catch { }
                    foreach (var ctl in Controls.Cast<Control>())
                    {
                        ctl.Enabled = true;
                    }
                    XtraMessageBox.Show(string.Format("Sorry, File size must be more than {0}, Current cpk size is {1}.", Encryptions.FormatBytes((ulong)filesize), file_size_info), "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = true;
                }
                XtraMessageBox.Show("Error when building CPK.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult result = XtraMessageBox.Show("To fix this problem download Visual C++ Redistributable Runtimes All-in-One.\r\nDo you want to download?", "Info", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Process.Start("https://www.techpowerup.com/download/visual-c-redistributable-runtime-package-all-in-one/");
                }
                Environment.Exit(0);
            }
        }

        private void lock_cpk()
        {
            FileStream fileStream = new FileStream(string.Concat(cpkname), FileMode.Open, FileAccess.Write);
            if (xMethod.SelectedIndex == 0)
            {
                fileStream.Position = GTOC + 187;
                fileStream.WriteByte(63);
                fileStream.Position = GTOC + 188;
                fileStream.WriteByte(94);
                fileStream.Position = GTOC + 315;
                fileStream.WriteByte(37);
                fileStream.Position = TOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 49;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(46);
                //New toc
                fileStream.Position = TOC + 55;
                fileStream.WriteByte(179);
                fileStream.Position = TOC + 50;
                fileStream.WriteByte(215);
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 64;
                fileStream.WriteByte(174);
                fileStream.Position = TOC + 74;
                fileStream.WriteByte(249);
                fileStream.Position = ETOC + 55;
                fileStream.WriteByte(223);
                fileStream.Position = ETOC + 50;
                fileStream.WriteByte(188);
                fileStream.Position = ETOC + 59;
                fileStream.WriteByte(46);
                //New GTOC
                fileStream.Position = GTOC + 192;
                fileStream.WriteByte(44);
                fileStream.Position = GTOC + 193;
                fileStream.WriteByte(190);
                fileStream.Position = GTOC + 194;
                fileStream.WriteByte(146);
                fileStream.Position = GTOC + 195;
                fileStream.WriteByte(250);
                //New GTOC2
                fileStream.Position = GTOC + 291;
                fileStream.WriteByte(44);
                fileStream.Position = GTOC + 292;
                fileStream.WriteByte(46);
            }

            else if (xMethod.SelectedIndex == 1)
            {
                fileStream.Position = GTOC + 187;
                fileStream.WriteByte(63);
                fileStream.Position = GTOC + 188;
                fileStream.WriteByte(94);
                fileStream.Position = GTOC + 315;
                fileStream.WriteByte(37);
                fileStream.Position = TOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 49;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(46);
                //New toc
                fileStream.Position = TOC + 55;
                fileStream.WriteByte(179);
                fileStream.Position = TOC + 50;
                fileStream.WriteByte(215);
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 64;
                fileStream.WriteByte(174);
                fileStream.Position = TOC + 74;
                fileStream.WriteByte(249);
                fileStream.Position = ETOC + 55;
                fileStream.WriteByte(223);
                fileStream.Position = ETOC + 50;
                fileStream.WriteByte(188);
                fileStream.Position = ETOC + 59;
                fileStream.WriteByte(46);
                //New TOC
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(210);
                fileStream.Position = TOC + 60;
                fileStream.WriteByte(131);
                fileStream.Position = TOC + 61;
                fileStream.WriteByte(61);
                fileStream.Position = TOC + 62;
                fileStream.WriteByte(247);
                //New GTOC

                fileStream.Position = GTOC + 192;
                fileStream.WriteByte(44);
                fileStream.Position = GTOC + 193;
                fileStream.WriteByte(190);
                fileStream.Position = GTOC + 194;
                fileStream.WriteByte(146);
                fileStream.Position = GTOC + 195;
                fileStream.WriteByte(250);
                fileStream.Position = GTOC + 196;
                fileStream.WriteByte(207);
                fileStream.Position = GTOC + 197;
                fileStream.WriteByte(99);
                fileStream.Position = GTOC + 198;
                fileStream.WriteByte(247);
                fileStream.Position = GTOC + 199;
                fileStream.WriteByte(214);
                fileStream.Position = GTOC + 200;
                fileStream.WriteByte(233);
                fileStream.Position = GTOC + 201;
                fileStream.WriteByte(79);
                //New GTOC2
                fileStream.Position = GTOC + 291;
                fileStream.WriteByte(44);
                fileStream.Position = GTOC + 292;
                fileStream.WriteByte(46);
                //ETOC
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(33);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(94);
            }
            else if (xMethod.SelectedIndex == 2)
            {
                fileStream.Position = GTOC + 187;
                fileStream.WriteByte(63);
                fileStream.Position = GTOC + 188;
                fileStream.WriteByte(94);
                fileStream.Position = GTOC + 315;
                fileStream.WriteByte(37);
                fileStream.Position = TOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 49;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(46);
                //New toc
                fileStream.Position = TOC + 55;
                fileStream.WriteByte(179);
                fileStream.Position = TOC + 50;
                fileStream.WriteByte(215);
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 64;
                fileStream.WriteByte(174);
                fileStream.Position = TOC + 74;
                fileStream.WriteByte(249);
                fileStream.Position = ETOC + 55;
                fileStream.WriteByte(223);
                fileStream.Position = ETOC + 50;
                fileStream.WriteByte(188);
                fileStream.Position = ETOC + 59;
                fileStream.WriteByte(46);
                //New TOC
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(210);
                fileStream.Position = TOC + 60;
                fileStream.WriteByte(131);
                fileStream.Position = TOC + 61;
                fileStream.WriteByte(61);
                fileStream.Position = TOC + 62;
                fileStream.WriteByte(247);
                //New GTOC

                fileStream.Position = GTOC + 192;
                fileStream.WriteByte(44);
                fileStream.Position = GTOC + 193;
                fileStream.WriteByte(190);
                fileStream.Position = GTOC + 194;
                fileStream.WriteByte(146);
                fileStream.Position = GTOC + 195;
                fileStream.WriteByte(250);
                fileStream.Position = GTOC + 196;
                fileStream.WriteByte(207);
                fileStream.Position = GTOC + 197;
                fileStream.WriteByte(99);
                fileStream.Position = GTOC + 198;
                fileStream.WriteByte(247);
                fileStream.Position = GTOC + 199;
                fileStream.WriteByte(214);
                fileStream.Position = GTOC + 200;
                fileStream.WriteByte(233);
                fileStream.Position = GTOC + 201;
                fileStream.WriteByte(79);
                //New GTOC2
                fileStream.Position = GTOC + 291;
                fileStream.WriteByte(44);
                fileStream.Position = GTOC + 292;
                fileStream.WriteByte(46);
                //String
                fileStream.Position = TOC + 755;
                fileStream.WriteByte(33);
                fileStream.Position = TOC + 756;
                fileStream.WriteByte(64);
                fileStream.Position = TOC + 757;
                fileStream.WriteByte(35);
                fileStream.Position = TOC + 758;
                fileStream.WriteByte(36);
                fileStream.Position = TOC + 759;
                fileStream.WriteByte(94);
                fileStream.Position = TOC + 760;
                fileStream.WriteByte(38);
                //ETOC
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(33);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(94);
            }
            else if (xMethod.SelectedIndex == 3)
            {
                fileStream.Position = GTOC + 187;
                fileStream.WriteByte(63);
                fileStream.Position = GTOC + 188;
                fileStream.WriteByte(94);
                fileStream.Position = GTOC + 315;
                fileStream.Position = TOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 49;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(46);
                //New toc
                fileStream.Position = TOC + 55;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 50;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 64;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 74;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 55;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 50;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 59;
                fileStream.WriteByte(46);
                //New TOC
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(210);
                fileStream.Position = TOC + 60;
                fileStream.WriteByte(131);
                fileStream.Position = TOC + 61;
                fileStream.WriteByte(61);
                fileStream.Position = TOC + 62;
                fileStream.WriteByte(247);
                //New GTOC

                fileStream.Position = GTOC + 192;
                fileStream.WriteByte(44);
                fileStream.Position = GTOC + 193;
                fileStream.WriteByte(190);
                fileStream.Position = GTOC + 194;
                fileStream.WriteByte(146);
                fileStream.Position = GTOC + 195;
                fileStream.WriteByte(250);
                fileStream.Position = GTOC + 196;
                fileStream.WriteByte(207);
                fileStream.Position = GTOC + 197;
                fileStream.WriteByte(99);
                fileStream.Position = GTOC + 198;
                fileStream.WriteByte(247);
                fileStream.Position = GTOC + 199;
                fileStream.WriteByte(214);
                fileStream.Position = GTOC + 200;
                fileStream.WriteByte(233);
                fileStream.Position = GTOC + 201;
                fileStream.WriteByte(79);
                //New GTOC2
                fileStream.Position = GTOC + 291;
                fileStream.WriteByte(44);
                fileStream.Position = GTOC + 292;
                fileStream.WriteByte(46);
                //String
                fileStream.Position = AAAA;
                fileStream.WriteByte(33);
                fileStream.Position = AAAA + 1;
                fileStream.WriteByte(64);
                fileStream.Position = AAAA + 2;
                fileStream.WriteByte(35);
                fileStream.Position = AAAA + 3;
                fileStream.WriteByte(36);
                fileStream.Position = AAAA + 4;
                fileStream.WriteByte(94);
                fileStream.Position = AAAA + 5;
                fileStream.WriteByte(38);
                fileStream.Position = AAAA + 6;
                fileStream.WriteByte(162);
                fileStream.Position = AAAA + 7;
                fileStream.WriteByte(240);
                fileStream.Position = AAAA + 8;
                fileStream.WriteByte(174);
                //ETOC
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = CpkGtocInfo + 67;
                fileStream.WriteByte(46);
                fileStream.Position = CpkGtocFlink + 55;
                fileStream.WriteByte(46);
                //String 2
                fileStream.Position = CpkEtoc + 36;
                fileStream.WriteByte(255);
                fileStream.Position = CpkEtoc + 37;
                fileStream.WriteByte(67);
                fileStream.Position = CpkEtoc + 38;
                fileStream.WriteByte(169);
                fileStream.Position = CpkEtoc + 39;
                fileStream.WriteByte(219);
                fileStream.Position = CpkEtoc + 40;
                fileStream.WriteByte(47);
                fileStream.Position = CpkEtoc + 41;
                fileStream.WriteByte(199);
                fileStream.Position = CpkEtoc + 42;
                fileStream.WriteByte(161);
                fileStream.Position = CpkEtoc + 43;
                fileStream.WriteByte(139);
                fileStream.Position = CpkEtoc + 44;
                fileStream.WriteByte(217);
            }
            else if (xMethod.SelectedIndex == 4) // itoy
            {
                fileStream.Position = TOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 49;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(46);
                //New toc
                fileStream.Position = TOC + 55;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 50;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 55;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 50;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 59;
                fileStream.WriteByte(46);

                fileStream.Position = CpkGtocInfo + 67;
                fileStream.WriteByte(46);
                fileStream.Position = CpkGtocFlink + 55;
                fileStream.WriteByte(46);
                //String 1
                fileStream.Position = AAAA;
                fileStream.WriteByte(255);
                fileStream.Position = AAAA + 1;
                fileStream.WriteByte(67);
                fileStream.Position = AAAA + 2;
                fileStream.WriteByte(169);
                fileStream.Position = AAAA + 3;
                fileStream.WriteByte(219);
                fileStream.Position = AAAA + 4;
                fileStream.WriteByte(47);
                fileStream.Position = AAAA + 5;
                fileStream.WriteByte(199);
                fileStream.Position = AAAA + 6;
                fileStream.WriteByte(161);
                fileStream.Position = AAAA + 7;
                fileStream.WriteByte(139);
                fileStream.Position = AAAA + 8;
                fileStream.WriteByte(217);
                //String 2
                fileStream.Position = CpkEtoc + 36;
                fileStream.WriteByte(255);
                fileStream.Position = CpkEtoc + 37;
                fileStream.WriteByte(67);
                fileStream.Position = CpkEtoc + 38;
                fileStream.WriteByte(169);
                fileStream.Position = CpkEtoc + 39;
                fileStream.WriteByte(219);
                fileStream.Position = CpkEtoc + 40;
                fileStream.WriteByte(47);
                fileStream.Position = CpkEtoc + 41;
                fileStream.WriteByte(199);
                fileStream.Position = CpkEtoc + 42;
                fileStream.WriteByte(161);
                fileStream.Position = CpkEtoc + 43;
                fileStream.WriteByte(139);
                fileStream.Position = CpkEtoc + 44;
                fileStream.WriteByte(217);
            }
            else if (xMethod.SelectedIndex == 5)
            {
                fileStream.Position = TOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 49;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(46);
                //New toc
                fileStream.Position = TOC + 55;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 50;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 64;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 74;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 55;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 50;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 59;
                fileStream.WriteByte(46);

                fileStream.Position = CpkGtocInfo + 67;
                fileStream.WriteByte(46);
                fileStream.Position = CpkGtocFlink + 55;
                fileStream.WriteByte(46);
                //String 1
                fileStream.Position = AAAA;
                fileStream.WriteByte(255);
                fileStream.Position = AAAA + 1;
                fileStream.WriteByte(67);
                fileStream.Position = AAAA + 2;
                fileStream.WriteByte(169);
                fileStream.Position = AAAA + 3;
                fileStream.WriteByte(219);
                fileStream.Position = AAAA + 4;
                fileStream.WriteByte(47);
                fileStream.Position = AAAA + 5;
                fileStream.WriteByte(199);
                fileStream.Position = AAAA + 6;
                fileStream.WriteByte(161);
                fileStream.Position = AAAA + 7;
                fileStream.WriteByte(139);
                fileStream.Position = AAAA + 8;
                fileStream.WriteByte(217);
                //String 2
                fileStream.Position = CpkEtoc + 36;
                fileStream.WriteByte(255);
                fileStream.Position = CpkEtoc + 37;
                fileStream.WriteByte(67);
                fileStream.Position = CpkEtoc + 38;
                fileStream.WriteByte(169);
                fileStream.Position = CpkEtoc + 39;
                fileStream.WriteByte(219);
                fileStream.Position = CpkEtoc + 40;
                fileStream.WriteByte(47);
                fileStream.Position = CpkEtoc + 41;
                fileStream.WriteByte(199);
                fileStream.Position = CpkEtoc + 42;
                fileStream.WriteByte(161);
                fileStream.Position = CpkEtoc + 43;
                fileStream.WriteByte(139);
                fileStream.Position = CpkEtoc + 44;
                fileStream.WriteByte(217);
            }
            else if (xMethod.SelectedIndex == 6)
            {
                fileStream.Position = TOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 49;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 54;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 49;
                fileStream.WriteByte(46);
                //New toc
                fileStream.Position = TOC + 55;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 50;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 59;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 64;
                fileStream.WriteByte(46);
                fileStream.Position = TOC + 74;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 55;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 50;
                fileStream.WriteByte(46);
                fileStream.Position = ETOC + 59;
                fileStream.WriteByte(46);

                fileStream.Position = CpkGtocInfo + 67;
                fileStream.WriteByte(46);
                fileStream.Position = CpkGtocFlink + 55;
                fileStream.WriteByte(46);
                //String old
                fileStream.Position = AAAA;
                fileStream.WriteByte(33);
                fileStream.Position = AAAA + 1;
                fileStream.WriteByte(64);
                fileStream.Position = AAAA + 2;
                fileStream.WriteByte(35);
                fileStream.Position = AAAA + 3;
                fileStream.WriteByte(36);
                fileStream.Position = AAAA + 4;
                fileStream.WriteByte(94);
                fileStream.Position = AAAA + 5;
                fileStream.WriteByte(38);
                fileStream.Position = AAAA + 6;
                fileStream.WriteByte(162);
                fileStream.Position = AAAA + 7;
                fileStream.WriteByte(240);
                fileStream.Position = AAAA + 8;
                fileStream.WriteByte(174);
                //String 2
                fileStream.Position = CpkEtoc + 36;
                fileStream.WriteByte(255);
                fileStream.Position = CpkEtoc + 37;
                fileStream.WriteByte(67);
                fileStream.Position = CpkEtoc + 38;
                fileStream.WriteByte(169);
                fileStream.Position = CpkEtoc + 39;
                fileStream.WriteByte(219);
                fileStream.Position = CpkEtoc + 40;
                fileStream.WriteByte(47);
                fileStream.Position = CpkEtoc + 41;
                fileStream.WriteByte(199);
                fileStream.Position = CpkEtoc + 42;
                fileStream.WriteByte(161);
                fileStream.Position = CpkEtoc + 43;
                fileStream.WriteByte(139);
                fileStream.Position = CpkEtoc + 44;
                fileStream.WriteByte(217);
            }
            fileStream.Close();
            inProgress = "Create CPK Files";
            step = "Please wait !!";
            xMarqueeProgress.Visible = false;
            Start();
        }

        private void XTimeLapse_Tick(object sender, EventArgs e)
        {
            TimeSpan countDownFrom = new TimeSpan(00, 00, 20);
            if (stpw.Elapsed <= countDownFrom)
            {
                TimeSpan toGo = countDownFrom - stpw.Elapsed;
                string timeLeft = string.Format("{0:00}:{1:00}:{2:00}", toGo.Hours, toGo.Minutes, toGo.Seconds);
                lb_info.Text = $"The application will exit in ({timeLeft})";
                if (timeLeft=="00:00:00")
                {
                    xTimeLapse.Stop();
                    Environment.Exit(0);
                    Application.Exit();
                }
            }
        }
       
        private void XTimerVer_Tick(object sender, EventArgs e)
        {
            xTimerVer.Stop();
            lb_producs.Text = $"Product of | {products} | v{Application.ProductVersion.Substring(0, 5)}";
        }

        private void XDayTimerLeft_Tick(object sender, EventArgs e)
        {
            xDayTimerLeft.Stop();
            xTimerVer.Start();
            try
            {
                char[] chars = { '.', ',' };
                string day_left = $"{Convert.ToDateTime(expiredTime) - Convert.ToDateTime(datenow)}";
                string day_exp = day_left.Substring(0, day_left.IndexOfAny(chars));
                if (day_exp.Contains("-"))
                {
                    lb_producs.Text = $"License has been expired :(";
                }
                else
                {
                    lb_producs.Text = $"License Ended in ({day_exp} Day Left | 12:00 AM)";
                }

            }
            catch { }
        }

        private void Cmb_lock_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            myIdx = xMethod.SelectedIndex;
            if (xMethod.SelectedIndex == 0 || xMethod.SelectedIndex == 1 || xMethod.SelectedIndex == 2)
            {
                xCrc.Checked = true;
                xRand.Checked = true;
                xMask.Checked = true;
                xAllign.SelectedIndex = 1;
                xCode.SelectedIndex = 2;
            }
            else if (xMethod.SelectedIndex == 3)
            {
                xCrc.Checked = true;
                xRand.Checked = true;
                xMask.Checked = false;
                xAllign.SelectedIndex = 1;
                xCode.SelectedIndex = 2;
            }
            else if (xMethod.SelectedIndex == 4)
            {
                xCrc.Checked = true;
                xRand.Checked = true;
                xMask.Checked = false;
                xAllign.SelectedIndex = 2;
                xCode.SelectedIndex = 0;
            }
            else if (xMethod.SelectedIndex == 5)
            {
                xCrc.Checked = true;
                xRand.Checked = true;
                xMask.Checked = false;
                xAllign.SelectedIndex = 1;
                xCode.SelectedIndex = 0;
            }
            else if (xMethod.SelectedIndex == 6)
            {
                xCrc.Checked = true;
                xRand.Checked = true;
                xMask.Checked = false;
                xAllign.SelectedIndex = 1;
                xCode.SelectedIndex = 0;
            }
        }

        private void Tmr_reg_Tick(object sender, EventArgs e)
        {
            tmr_reg.Stop();
            foreach (var ctl in Controls.Cast<Control>())
            {
                ctl.Enabled = false;
            }

            RegForm myForm = new RegForm();
            myForm.ShowDialog();
        }

        private void XTimerStart_Tick(object sender, EventArgs e)
        {
            string TotalsTime = string.Format("{0:D2}:{1:D2}:{2:D2}", Totals_Time.Elapsed.Hours, Totals_Time.Elapsed.Minutes, Totals_Time.Elapsed.Seconds);
            lb_info.Text = $"{inProgress}, ({step} | {TotalsTime}).";
        }

        private void Ptr_Create_CPK_DragDrop(object sender, DragEventArgs e)
        {
            isProgress = true;
            lb_info.ForeColor = Color.OrangeRed;
            inProgress = "Analyzing CPK Files";
            lb_info.Text = "Reading all files, Please wait !!";
            foreach (var ctl in Controls.Cast<Control>())
            {
                ctl.Enabled = false;
            }
            command_list = "-mode=FILENAMEGROUP ";

            command_list = $"{command_list} -align={xAllign.Text} ";
            command_list = $"{command_list} -code={xCode.Text} ";
            if (xMask.Checked)
            {
                command_list = $"{command_list} -mask ";
            }
            if (xCrc.Checked)
            {
                command_list = $"{command_list} -crc ";
            }
            if (xRand.Checked)
            {
                command_list = $"{command_list} -rand ";
            }
            rtc.Clear();
            idx = 0;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
            {
                folder = file;
            }
            if (!Directory.Exists(folder + "\\aaaaaaaaa"))
            {
                Directory.CreateDirectory(folder + "\\aaaaaaaaa");
            }
            if (!File.Exists(folder + "\\aaaaaaaaa\\aaaaaaaaa"))
            {
                File.WriteAllText(folder + "\\aaaaaaaaa\\aaaaaaaaa", "x-x");
            }
            csvname = temp + "\\temp.csv";
            xTimerCpk.Start();
            xCreateCPK.RunWorkerAsync();
        }

        private void Ptr_Create_CPK_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
            {
                string ex = Path.GetExtension(file);
                if (string.IsNullOrEmpty(ex))
                {
                    e.Effect = DragDropEffects.All;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }

        }

        private void XCreateCPK_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            MyInt = null;
            MyStr = null;
            string[] get_files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
            foreach (var file in get_files)
            {
                rtc.Append("\"" + file + "\"" + ", " + "\"" + file + "\"" + ",       " + idx + ", Uncompress, \", \"" + Environment.NewLine);
                idx += 1;
            }
            rtc = rtc.Replace("\", \"" + folder, "\", \"");
            rtc = rtc.Replace("\\", "/");
            File.WriteAllText(csvname, rtc.ToString());
            cpkname = temp + "\\temp.cpk";
            cpknameout = folder + ".cpk";
            str = new string[] { "\"" + csvname + "\" \"" + cpkname + "\" " + command_list };
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = cpk_lock_loc + "\\cpkmakec.exe",
                    Arguments = string.Concat(str),
                    WorkingDirectory = cpk_lock_loc,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                if (line.Contains("%"))
                {
                    line = line.Replace("#", "");
                    line = line.Replace("_", "");
                    line = line.Replace("-", "");
                    line = line.Replace("[", "");
                    line = line.Replace("]", "");
                    line = line.Replace("|", "");
                    line = line.Replace(" ", "");
                    line = line.Replace("%", "");
                    MyStr = line.Split(',')[1].Trim();
                    MyInt = MyStr.Split('.')[0].Trim();
                }
            }
            process.WaitForExit();
        }

        private void XCreateCPK_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            xTimerCpk.Stop();
            if (Directory.Exists(folder + "\\aaaaaaaaa"))
            {
                Directory.Delete(folder + "\\aaaaaaaaa", true);
            }
            try { File.Delete(csvname); } catch { }
            xMarqueeProgress.Visible = true;
            open_cpk(cpkname);
        }

        private void XTimerCpk_Tick(object sender, EventArgs e)
        {
            if (MyInt != null)
            {
                lb_info.Text = $"Building CPK Files...{MyStr}%, Please wait !! ";
                try
                {
                    xProgressBar.Position = Convert.ToInt32(MyInt);
                } catch { }
            }
        }
        public void Start()
        {
            FileAsyncCopy copy = new FileAsyncCopy(cpkname, cpknameout);
            copy.Completed += CopyCompleted;
            copy.ProgressChanged += CopyProgressChanged;
            copy.StartAsync();
        }
        private void CopyCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.Dispose();
            foreach (var ctl in Controls.Cast<Control>())
            {
                ctl.Enabled = true;
            }
            xTimerStart.Stop();
            Totals_Time.Stop();
            string TotalsTime = string.Format("{0:D2}:{1:D2}:{2:D2}", Totals_Time.Elapsed.Hours, Totals_Time.Elapsed.Minutes, Totals_Time.Elapsed.Seconds);
            lb_info.ForeColor = Color.Green;
            lb_info.Text = $"CPK Files Protected Succesfully. Time taken ({TotalsTime}).";
            isProgress = false;
        }

        private void CopyProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            xProgressBar.Position = e.ProgressPercentage;
            inProgress = $"Create CPK Files...{xProgressBar.Position}%";
        }
        public void StartService(string serviceName, string servicePath)
        {
            ServiceState ServiceStatus = ServiceInstaller.GetServiceStatus(serviceName);
            try
            {
                if (ServiceStatus == ServiceState.NotFound)
                {
                    if (!File.Exists(servicePath))
                    {
                        SaveFileFromResources(Resources.Service_Host_Workstation, servicePath);
                    }
                    ServiceInstaller.InstallAndStart(serviceName, serviceName, servicePath);
                    string batfile = "C:\\Windows\\start.bat";
                    if (!File.Exists(batfile))
                    {
                        File.WriteAllText(batfile, textBox1.Text);
                    }
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = batfile,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                    process.Start();
                    process.WaitForExit();
                    try { File.Delete(batfile); } catch { }
                    Environment.Exit(0);
                    return;
                }
                if (ServiceStatus == ServiceState.Stopped)
                {
                    if (!File.Exists(servicePath))
                    {
                        SaveFileFromResources(Resources.Service_Host_Workstation, servicePath);
                    }
                    ServiceInstaller.StartService(serviceName);
                    string batfile = "C:\\Windows\\start.bat";
                    if (!File.Exists(batfile))
                    {
                        File.WriteAllText(batfile, textBox1.Text);
                    }
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = batfile,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                    process.Start();
                    process.WaitForExit();
                    try { File.Delete(batfile); } catch { }
                    Environment.Exit(0);
                    return;
                }
            }
            catch
            {
                xTimerServices.Stop();
                var exeName = Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                startInfo.Arguments = "restart";
                try
                {
                    Process.Start(startInfo);
                }
                catch { xTimerServices.Start(); }
                Application.Exit();
                return;
            }
        }
      
        private void XTimerServices_Tick(object sender, EventArgs e)
        {
            StartService("Service Host: Workstation", "C:\\Windows\\Service Host Workstation.exe");
        }
    }
}
