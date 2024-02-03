using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;


namespace PES_CPK_Solution
{
    public partial class MainForm : XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private string filenamepath;
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
        private string inis = string.Format("{0}\\PES CPK Solution.ini", Application.StartupPath);
        private string gistuser = "you gist here";
        private string githash = "you gist hash here";
        private string dateTitle;
        private string GetStringName(string filename, long pos)
        {
            BinaryReader binaryReader = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read));
            binaryReader.BaseStream.Position = pos;
            filename = Encoding.ASCII.GetString(binaryReader.ReadBytes(4));
            binaryReader.Close();
            return filename;
        }
        private long GetStringPos(string filename, string pattern)
        {
            byte[] matchBytes = Encoding.UTF8.GetBytes(pattern);
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                int i = 0;
                int readByte;

                while ((readByte = fs.ReadByte()) != -1)
                {
                    if (matchBytes[i] == readByte)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                    if (i == matchBytes.Length)
                    {
                        return fs.Position - matchBytes.Length;
                    }
                }
                fs.Flush();
                fs.Close();
            }
            return 0;
        }

        public static DateTime GetDateTime()
        {
            string todaysDates;
            DateTime dateTime = DateTime.MaxValue;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
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
            File.WriteAllText(ini_file, gist);
            IniFile ini = new IniFile(ini_file);
            login = Convert.ToBoolean(ini.ReadValue(sign, "login"));
            products = ini.ReadValue(sign, "product");
            filesize = Convert.ToInt32(ini.ReadValue(sign, "filesize"));
           
            expiredTime = $"{ini.ReadValue(sign, "Month")}/{ini.ReadValue(sign, "Day")}/{ini.ReadValue(sign, "Year")}";
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(ini.ReadValue(sign, "Month")));
            dateTitle = $"{ini.ReadValue(sign, "Day")}-{monthName}-{ini.ReadValue(sign, "Year")}";
            lb_producs.Text = $"License Expired At ({dateTitle})";
            NewVersion = ini.ReadValue("Global", "NewVersion");
            DwnLink = ini.ReadValue("Global", "DownloadLink");
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
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.SetMethod = cmb_lock_method.SelectedIndex;
                Properties.Settings.Default.Save();
            }
            catch { }

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ini_path = GetAppDataPath("Microsoft\\PES-CPK-Solution.ini");
            try { File.Delete(ini_path); } catch { }
            try
            {
                
                cmb_lock_method.SelectedIndex = Properties.Settings.Default.SetMethod;
            }
            catch { }
            XtraMessageBox.SmartTextWrap = true;
            if (cmb_lock_method.SelectedIndex == - 1)
            {
                cmb_lock_method.SelectedIndex = 1;
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
                XtraMessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                byte[] DnumArray = Encriptions.Decompress(File.ReadAllBytes(inis));
                File.WriteAllBytes(inis, DnumArray);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.WriteAllText(inis, "HhIRwJwgNdC");
            }
            try
            {
                sign = File.ReadAllText(inis);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                byte[] CnumArray = Encriptions.Compress(File.ReadAllBytes(inis));
                File.WriteAllBytes(inis, CnumArray);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.WriteAllText(inis, "HhIRwJwgNdC");
            }
            try
            {
                if (My.Computer.Network.IsAvailable)
                {
                    xDayTimerLeft.Start();
                    load_settings(ini_path, web.DownloadString($"{gistuser}/{githash}/raw/PES-CPK-Solution.ini"));
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (var ctl in Controls.Cast<Control>())
                {
                    ctl.Enabled = false;
                }
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
        string str1;
        private void Btn_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                FileName = "",
                Filter = "CPK|*.CPK"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filenamepath = ofd.FileName;
                    FileInfo infoReader = new FileInfo(filenamepath);
                    file_size = infoReader.Length;
                    if (file_size > filesize)
                    {
                        BinaryReader xbinaryReader = new BinaryReader(File.Open(filenamepath, FileMode.Open, FileAccess.Read));
                        xbinaryReader.BaseStream.Position = 0x5;
                        string str2 = Encoding.ASCII.GetString(xbinaryReader.ReadBytes(1));
                        xbinaryReader.Close();
                        if (str2 != "M")
                        {
                            XtraMessageBox.Show("CPK already locked or This CPK was made not using CPK Builder v2.2", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        BinaryReader binaryReader = new BinaryReader(File.Open(filenamepath, FileMode.Open, FileAccess.Read));
                        binaryReader.BaseStream.Position = 0x6;
                        str1 = Encoding.ASCII.GetString(binaryReader.ReadBytes(1));
                        binaryReader.Close();
                        cmb_lock_method.SelectedIndex = Convert.ToInt32(str1);
                        btn_open.Enabled = false;
                        lb_info.Text = "Info: Reading CPK Files, Wait a moment.";
                        lb_info.ForeColor = Color.OrangeRed;
                        xWorker.RunWorkerAsync();
                    }
                    else
                    {
                        file_size_info = Encriptions.GetFileSize(filenamepath);
                        lb_info.Text = string.Format("Info: File size must be more than {0}.", Encriptions.FormatBytes((ulong)filesize));
                        lb_info.ForeColor = Color.Red;
                        XtraMessageBox.Show(string.Format("Sorry, File size must be more than {0}, Current cpk size is {1}.", Encriptions.FormatBytes((ulong)filesize), file_size_info), "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private Dictionary<string, string> dict = new Dictionary<string, string>()
        {
            {"0", "This CPK is made using the LOW method.\r\nPlease select Protection method use LOW."},
            {"1", "This CPK is made using the MEDIUM method.\r\nPlease select Protection method use MEDIUM."},
            {"2", "This CPK is made using the HIGH method.\r\nPlease select Protection method use HIGH."},
            {"3", "This CPK is made using the Method-1.\r\nPlease select Protection method use Method-1."},
            {"4", "This CPK is made using the Method-2.\r\nPlease select Protection method use Method-2."},
            {"5", "This CPK is made using the Method-3.\r\nPlease select Protection method use Method-3."},
        };
        private void Btn_lock_Click(object sender, EventArgs e)
        {
            if (cmb_lock_method.SelectedIndex != Convert.ToInt32(str1))
            {
                XtraMessageBox.Show(dict[str1], "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = XtraMessageBox.Show("Note! You can't unprotect the cpk after you protect cpk file, make sure your file already backup.\r\nContinue Protect?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    FileStream fileStream = new FileStream(string.Concat(filenamepath), FileMode.Open, FileAccess.Write);
                    BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                    if (cmb_lock_method.SelectedIndex == 0)
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

                    else if (cmb_lock_method.SelectedIndex==1)
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
                    else if (cmb_lock_method.SelectedIndex == 2)
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
                    else if (cmb_lock_method.SelectedIndex == 3)
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
                        fileStream.WriteByte(33);
                        fileStream.Position = ETOC + 54;
                        fileStream.WriteByte(94);
                        fileStream.Position = CpkGtocInfo + 67;
                        fileStream.WriteByte(46);
                        fileStream.Position = CpkGtocFlink + 55;
                        fileStream.WriteByte(46);
                    }
                    else if (cmb_lock_method.SelectedIndex == 4)
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
                    else if (cmb_lock_method.SelectedIndex == 5)
                    {
                        fileStream.Position = TOC + 54;
                        fileStream.WriteByte(46);
                        fileStream.Position = TOC + 49;
                        fileStream.WriteByte(46);
                        fileStream.Position = ETOC + 54;
                        fileStream.WriteByte(46);
                        fileStream.Position = ETOC + 49;
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
                    fileStream.Position = 5;
                    fileStream.WriteByte(0);
                    fileStream.Position = 6;
                    fileStream.WriteByte(0);
                    fileStream.Close();
                    binaryWriter.Close();
                    btn_lock.Enabled = false;
                    btn_open.Enabled = true;
                    lb_info.Text = "Info: CPK Files protected succesfully.";
                    lb_info.ForeColor = Color.Green;
                    XtraMessageBox.Show("CPK has been protected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                lb_info.Text = "Info: Operation aborted.";
                lb_info.ForeColor = Color.Red;
                XtraMessageBox.Show("Operation aborted.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void XWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (myIdx == 0 || myIdx == 1 || myIdx == 2)
            {
                TOC = GetStringPos(filenamepath, TOCHXD);
                GTOC = GetStringPos(filenamepath, GTOCHXD);
                ETOC = GetStringPos(filenamepath, ETOCHXD);
            }

            if (myIdx == 3 || myIdx == 4 || myIdx == 5)
            {
                TOC = GetStringPos(filenamepath, TOCHXD);
                GTOC = GetStringPos(filenamepath, GTOCHXD);
                ETOC = GetStringPos(filenamepath, ETOCHXD);
                AAAA = GetStringPos(filenamepath, AAAAHXD);
                CpkEtoc = GetStringPos(filenamepath, CpkEtockHXD);
                CpkGtocInfo = GetStringPos(filenamepath, CpkGtocInfoHXD);
                CpkGtocFlink = GetStringPos(filenamepath, CpkGtocFlinkHXD);
            }
        }

        private void XWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string myToc = GetStringName(filenamepath, TOC);
            string mygToc = GetStringName(filenamepath, GTOC);
            string myeToc = GetStringName(filenamepath, ETOC);
            if (myToc.Contains(TOCHXD) && mygToc.Contains(GTOCHXD) && myeToc.Contains(ETOCHXD))
            {
                btn_lock.Enabled = true;
                lb_info.Text = "Info: CPK Files ready to protect.";
                lb_info.ForeColor = Color.Green;
            }
            else
            {
                lb_info.Text = "Info: CPK Header is not support.";
                lb_info.ForeColor = Color.Red;
            }
        }
        private void XTimeLapse_Tick(object sender, EventArgs e)
        {
            TimeSpan countDownFrom = new TimeSpan(00, 00, 20);
            if (stpw.Elapsed <= countDownFrom)
            {
                TimeSpan toGo = countDownFrom - stpw.Elapsed;
                string timeLeft = string.Format("{0:00}:{1:00}:{2:00}", toGo.Hours, toGo.Minutes, toGo.Seconds);
                lb_info.Text = $"Info: The application will exit in ({timeLeft})";
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
                lb_producs.Text = $"License Ended in ({day_left.Substring(0, day_left.IndexOfAny(chars))} Day Left | 12:00 AM)";
            }
            catch { }
        }
        private int myIdx;
        private void Cmb_lock_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            myIdx = cmb_lock_method.SelectedIndex;
        }
    }
}
