using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MediaDBClient
{

    
    public class _AppConfig
    {
        public HashSet<string> FilePaths = new HashSet<string>();
        public bool scanSubFolders = true;
        public HashSet<string> KnownTags = new HashSet<string>();
        
    }
    public class AppConfig
    {
        public _AppConfig MyConfig = new _AppConfig();
        public string[] FileExtensions = { "*.avi", "*.mpg", "*.mpeg", "*.wmv" };


        public HashSet<string> Filepaths()
        {           
            return MyConfig.FilePaths;
        }
        public void AddFilePath(string path)
        {
            MyConfig.FilePaths.Add(path);
            
        }
        public bool ScanSubFolders()
        {
            return MyConfig.scanSubFolders;
        }
        public AppConfig()
        {


        }

        public static void Serialize(string file, AppConfig c)
        {
            if (!File.Exists(file))
            {
                // File.Create(file);
            }
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(c.GetType());
            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, c);
            writer.Flush();
            writer.Close();
        }
        public static AppConfig Deserialize(string file)
        {
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(
                  typeof(AppConfig));

            StreamReader reader = File.OpenText(file);
            AppConfig c = (AppConfig)xs.Deserialize(reader);
            reader.Close();
            return c;
        }

        public void Add()
        {
            _AppConfig newprofile = new _AppConfig();

            /*newprofile.name = "test";
            newprofile.id = 12345;
            newprofile.lastseen = "67 76tdfdf8756ff";
            newprofile.market = 90000;
            newprofile.marketaverage = 11111;
            newprofile.quantity = 11;

            realm.Add(newprofile);
             */
        }
    }
}
