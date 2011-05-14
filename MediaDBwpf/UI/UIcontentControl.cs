using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MediaDBwpf.Metadata;
using MediaDBwpf.Database.Metadata;
using System.IO;
using System.Data;
namespace MediaDBwpf
{
    public partial class MainWindow : Window
    {

        private void RefreshFileList()
        {
            //Called when Program first loads and when a new folder is added

            //lsFileBrowser.VirtualListSize = 0;
            int newmediacnt = 0;
            List<MetaData> temp = new List<MetaData>();
            List<MetaData> knownfiles = db.GetItems(MediaDBwpf.Database.Metadata.SqliteMetaData._Tag.tags, "All", MediaDBwpf.Database.Metadata.SqliteMetaData.Datatograb.hash);
            foreach (string F in Appdata.FilePaths) // enumerate base folders
            {
                IEnumerable<string> filelist;
                if (Appdata.scanSubFolders)
                {
                    filelist = GetFiles(F, Appdata.FileExtensions, SearchOption.AllDirectories);
                }
                else { filelist = GetFiles(F, Appdata.FileExtensions, SearchOption.TopDirectoryOnly); }

                foreach (string s in filelist)
                {
                    //treeView1.Nodes.Add(s.Substring(s.LastIndexOf("\\") + 1));
                    MetaData m = new MetaData(s);
                    bool Found = false;
                    System.Threading.Tasks.Parallel.ForEach(knownfiles, item =>
                    {
                        if (m.Hash == item.Hash) { Found = true; }
                    });

                    if (!Found)
                    {
                        // may trigger changed contains(m.hash) to (m) due to getitem change
                        newmediacnt++;
                        temp.Add(m);

                    }

                }
            }
            if (temp.Count > 0)
            {

                db.AddItems(temp);
                temp = null;
            }
            int mediacnt = db.Count();
            //lsFileBrowser.VirtualListSize = mediacnt;
            //lsFileBrowser.Refresh();
            //MediaCache = db.GetItemsbyIDindex(0, 15);
            UpdateUIElementsfromCache();
            UpdateUIDataGrid();


            //toolStripStatusLabel1.Text = mediacnt + " Files Cached." + newmediacnt + " New Files.";
            db.CleanDuplicateHashes();
            return;
        }
        public DataSet DS = new DataSet();
        private void UpdateUIDataGrid()
        {
            string tag;
            bool IsPerson;
            Selecteditems.GetSelectedTag(out tag, out IsPerson);
            DS.Clear();
            DS = db.GetDSItems(SqliteMetaData._Tag.tags, Selecteditems.GetSelectedTag(), SqliteMetaData.Datatograb.all);
            dataGridMainWindow.ItemsSource = DS.DefaultViewManager.AsQueryable();
            


        }


        public static IEnumerable<string> GetFiles(string path, string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(path, searchPattern, searchOption));
        }
        private void UpdateUIElementsfromCache()
        {
            treeview_TagSelector.Items.Clear();
            TreeViewItem a = new TreeViewItem();
            a.Name = "Alll";
            a.Header = "All";
            TreeViewItem p = new TreeViewItem();
            p.Name = "Peoplee";
            p.Header = "People";
            TreeViewItem t = new TreeViewItem();
            t.Name = "Tagss";
            t.Header = "Tags";
            TreeViewItem x = new TreeViewItem();
            x.Name = "Untagged";
            x.Header = "Untagged";
            foreach (string s in Appdata.KnownTags) { TreeViewItem st = new TreeViewItem(); st.Header = s; st.Name = s; st.Tag = "Tag"; t.Items.Add(st); }

            foreach (string s in Appdata.KnownPeople) { TreeViewItem st = new TreeViewItem(); st.Header = s; st.Name = s; st.Tag = "People"; t.Items.Add(st); }
            //a.Items.Add(x);
            //a.Items.Add(p);
            //a.Items.Add(t);
            treeview_TagSelector.Items.Add(p);
            treeview_TagSelector.Items.Add(t);
            treeview_TagSelector.Items.Add(x);
        }

        public class treetag
        {
            public string tag;
            public bool IsPerson;
            public treetag(string tag, bool person)
            {
            this.tag = tag;
                IsPerson = person;

            }
            public override string  ToString()
{
 	 return tag;
}
        }
    }
   
}
