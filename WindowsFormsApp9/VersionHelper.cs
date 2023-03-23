using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeldScanApp
{
    internal class VersionHelper
    {
        public static string AppVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }      
    }
}
