using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PES_CPK_Solution
{
    public class FileAsyncCopy
    {
        private string _source;
        private string _target;
        BackgroundWorker _worker;
        public FileAsyncCopy(string source, string target)
        {
            //if (!File.Exists(source))
            //    throw new FileNotFoundException(string.Format(@"Source file was not found. FileName: {0}", source));

            _source = source;
            _target = target;
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = false;
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += DoWork;
        }


        private void DoWork(object sender, DoWorkEventArgs e)
        {
            FileStream FsOut = new FileStream(_target, FileMode.Create);
            FileStream FsIn = new FileStream(_source, FileMode.Open);
            byte[] bt = new byte[1048756];
            int readByte;
            while ((readByte = FsIn.Read(bt, 0, bt.Length)) > 0)
            {
                FsOut.Write(bt, 0, readByte);
                _worker.ReportProgress((int)(FsIn.Position * 100 / FsIn.Length));
            }
            FsIn.Close();
            FsOut.Close();
            try { File.Delete(_source); } catch { }
        }

        public event ProgressChangedEventHandler ProgressChanged
        {
            add { _worker.ProgressChanged += value; }
            remove { _worker.ProgressChanged -= value; }
        }

        public event RunWorkerCompletedEventHandler Completed
        {
            add { _worker.RunWorkerCompleted += value; }
            remove { _worker.RunWorkerCompleted -= value; }
        }
        public void StartAsync()
        {
            _worker.RunWorkerAsync();
        }
    }
}
