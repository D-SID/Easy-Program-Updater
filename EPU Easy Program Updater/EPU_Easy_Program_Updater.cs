using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace EPU
{
    internal class EPU_Easy_Program_Updater
    {
        #region Settings EPU
        private string urlProgramFile = @"";
        private string urlVersionProgramXMLFile = @"";
        private string nameDownloadFile = @"new." + Process.GetCurrentProcess().MainModule.FileName;
        #endregion
        public static  void CheckUpdates()
        {
            
        }
        string GetProgramVersionOnServer()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(urlVersionProgramXMLFile);
            
            var remoteVersion = new Version(xml.GetElementsByTagName("version")[0].InnerText); // Версиях XML
            return remoteVersion.ToString();
        }
        FileVersionInfo GetProgramVersionOnLocal()
        {

            return FileVersionInfo.GetVersionInfo(Path.Combine(Directory.GetCurrentDirectory(), nameDownloadFile));
        }

    }
}
