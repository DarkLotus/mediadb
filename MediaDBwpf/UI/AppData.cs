using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MediaDBwpf.Metadata;
using System.Windows.Controls;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;
using System.Runtime;
using System.Runtime.Serialization;
namespace MediaDBwpf.UI
{
    
    public class AppData
    {
        // load all the crap we care about between sessions from xml?
        public string[] FileExtensions = { "*.avi", "*.mpg", "*.mpeg", "*.wmv" };
        public HashSet<string> FilePaths = new HashSet<string>();
        public bool bScanSubFolders = true;
        public bool bCacheThumbs = true;

        //public HashSet<string> KnownTags = new HashSet<string>();
        public HashSet<string> KnownPeople = new HashSet<string>();


        public List<Tag> _KnownTags = new List<Tag>();
        private const string file = "config.xml";


        public List<TreeViewItem> GetKnownTagsAsTree()
        {

                List<TreeViewItem> r = new List<TreeViewItem>();
                foreach(Tag t in _KnownTags) {
                    r.AddRange(Static_Helpers.GetTreeFromTag2(t));
                }
                return r;
           
        }
        public List<string> GetKnownTagsAsStringList()
        {
            
                List<string> mylist = new List<string>();
                foreach (Tag t in _KnownTags)
                {
                    mylist.AddRange(TagtoString(t));
                }
                return mylist;
        
        }
        private List<string> TagtoString(Tag t) {
            List<string> mylist = new List<string>();
            if (t.Children.Count == 0)
            {
                mylist.Add(t.ToString());
                return mylist;
            }
            string r = "";
            foreach (Tag q in t.Children)
            {
                mylist.AddRange(TagtoString(q));
                //r = r + "," + t.ToString() + "\\" + TagtoString(q);
            }
            return mylist;
        }

        public void AddKnownTag(Tag t)
        {
            _addtag(ref _KnownTags, t);
            this.Serialize();
        }
        private void _addtag(ref List<Tag> mylist, Tag addme) 
        {
            if (mylist.Contains(addme)) { if (addme.Children.Count < 1) { return; } }
            foreach (Tag t in mylist)
            {
                if (t == addme)
                {
                    _addtag(ref t.Children, addme.Children[0]);
                }
            }
            // assume tag wasnt in list
            mylist.Add(addme);
            return;
 
        }


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
            
            //obta.InsertQuery("AppData", this)
            AppData c = this;
            if (!File.Exists(file))
            {
                // File.Create(file);
            }
                 //System.Xml.Serialization.XmlSerializer xs
            //   = new System.Xml.Serialization.XmlSerializer(c.GetType());

            FileStream fs = new FileStream(file, FileMode.Create);
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
            NetDataContractSerializer ser =
                new NetDataContractSerializer();
            ser.WriteObject(writer, c);
            //xs.Serialize(writer, c);
            writer.Flush();
            writer.Close();
        }
        public static AppData Deserialize()
        {
            FileStream fs = new FileStream(file,
       FileMode.Open);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            NetDataContractSerializer ser = new NetDataContractSerializer();

            // Deserialize the data and read it from the instance.
            AppData c =
                (AppData)ser.ReadObject(reader, true);
            fs.Close();
            return c;
            /*System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(
                  typeof(AppData));

            StreamReader reader = File.OpenText(file);
            AppData c = (AppData)xs.Deserialize(reader);
            reader.Close();
            return c;*/
        }

    }

}
