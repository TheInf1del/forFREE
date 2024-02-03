using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.InteropServices;

namespace PES_CPK_Solution
{
	public class IniFile
	{
		public string path;
		public IniFile(string INIPath)
		{
			path = INIPath;
		}

		[DllImport("kernel32", CharSet=CharSet.None, ExactSpelling=false)]
		private extern static int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		public string ReadValue(string Section, string Key)
		{
			StringBuilder temp = new StringBuilder(255);
			GetPrivateProfileString(Section, Key, "", temp, 255, path);
			return temp.ToString();
		}
		[DllImport("kernel32", CharSet=CharSet.None, ExactSpelling=false)]
		private extern static long WritePrivateProfileString(string section, string key, string val, string filePath);

		public void WriteValue(string Section, string Key, string Value)
		{
			WritePrivateProfileString(Section, Key, Value, path);
		}
	}
}