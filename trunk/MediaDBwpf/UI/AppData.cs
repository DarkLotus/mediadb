using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MediaDBwpf.UI
{
    public class AppData
    {
        // load all the crap we care about between sessions from xml?
        public string[] FileExtensions = { "*.avi", "*.mpg", "*.mpeg", "*.wmv" };
        public HashSet<string> FilePaths = new HashSet<string>();
        public bool scanSubFolders = true;
        
        public HashSet<string> KnownTags = new HashSet<string>();
        public HashSet<string> KnownPeople = new HashSet<string>();
        private const string file = "config.xml";
        public HashSet<string> Filepaths()
        {
            return FilePaths;
        }
        public void AddFilePath(string path)
        {
            FilePaths.Add(path);

        }

        public AppData()
        {
            

        }

        public void Serialize()
        {
            AppData c = this;
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
        public static AppData Deserialize()
        {
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(
                  typeof(AppData));

            StreamReader reader = File.OpenText(file);
            AppData c = (AppData)xs.Deserialize(reader);
            reader.Close();
            return c;
        }

    }

}
