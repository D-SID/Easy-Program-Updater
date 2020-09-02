using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace EPU
{
    internal class EPU_Easy_Program_Updater
    {
        #region Settings EPU
        private static string urlProgramFile = @"";
        private static string urlVersionProgramXMLFile = @"";
        private static string nameFile = @"EPU";
        private static string nameDownloadFile = @"new." + nameFile;
        #endregion
        public static  void CheckUpdates()
        {
            if(Version.Parse(GetProgramVersionOnServer()) > Version.Parse(GetProgramVersionOnLocal()))
            {
                DownloadFile();
                Process.Start(nameDownloadFile);
                System.Environment.Exit(0);
            }
            if (File.Exists(nameDownloadFile))
            {
                SetupNewVersionFile();
            }
        }
        static void SetupNewVersionFile()
        {
            File.Delete(nameFile);
            File.Move(nameDownloadFile, nameFile);
        }
        static void DownloadFile()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(urlProgramFile, nameDownloadFile);
            }
        }
        static string GetProgramVersionOnServer()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(urlVersionProgramXMLFile);
            
            var remoteVersion = new Version(xml.GetElementsByTagName("version")[0].InnerText); // Версиях XML
            return remoteVersion.ToString();
        }
        static string GetProgramVersionOnLocal()
        {

            var localVersion = FileVersionInfo.GetVersionInfo(Path.Combine(Directory.GetCurrentDirectory(), 
                Process.GetCurrentProcess().MainModule.FileName));
            return localVersion.ToString();
        }

    }
}
