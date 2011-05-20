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
using System.Collections.ObjectModel;
using MediaDBwpf.Database;
using MediaDBwpf.Database.mediacacheDataSetTableAdapters;
using System.Drawing.Imaging;
using DevZest.Windows.DataVirtualization;
namespace MediaDBwpf
{
    public partial class MainWindow : Window
    {

        private void RefreshFileList()
        {
            
            DataView dv = new DataView(DS.metacache);
                    
            //Called when Program first loads and when a new folder is added
            DS.EnforceConstraints = false;
            mdta.Fill(DS.metacache);
            //lsFileBrowser.VirtualListSize = 0;
            int newmediacnt = 0;
            List<MetaData> temp = new List<MetaData>();
            //ObservableCollection<MetaData> knownfiles = db.GetItems(MediaDBwpf.Database.Metadata.SqliteMetaData._Tag.tags, "All", MediaDBwpf.Database.Metadata.SqliteMetaData.Datatograb.hash);
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
                    MetaData m = new MetaData(s);
                    string x = 
                    dv.RowFilter = "filepath = '" + m.FilePath.Replace("'","''") +"'"; 
                    //treeView1.Nodes.Add(s.Substring(s.LastIndexOf("\\") + 1));
                    if (dv.Count == 0 )
                    {

                        newmediacnt++;
                        temp.Add(m);
                    }
                    
                }
            }
            if (temp.Count > 0)
            {

                //db.AddItems(temp);
                try { additems(temp); }
                catch { }
               
                
                temp = null;
            }
            

            //lsFileBrowser.VirtualListSize = mediacnt;
            //lsFileBrowser.Refresh();
            //MediaCache = db.GetItemsbyIDindex(0, 15);
            UpdateUIElementsfromCache();
            UpdateUIDataGrid();


            //toolStripStatusLabel1.Text = mediacnt + " Files Cached." + newmediacnt + " New Files.";
            //db.CleanDuplicateHashes();
            return;
        }

        private void additems(List<MetaData> temp)
        {
            
            foreach (MetaData m in temp)
            {

                if (m.Thumbnail != null)
                {
                    MemoryStream ms = new MemoryStream();
                    //m._thumbnail.Save(ms, ImageFormat.Jpeg);
                    ms = (MemoryStream)m._thumbnail.StreamSource;
                    DS.metacache.AddmetacacheRow(m.Hash, m.FilePath, ms.ToArray(), m.tagstring, m.peoplestring);
                }
                else
                {
                    DS.metacache.AddmetacacheRow(m.Hash, m.FilePath, null, m.tagstring, m.peoplestring);
                }
                
                //int a = mdta.InsertQuery(m.Hash,m.FilePath, ms.ToArray(), m.tagstring,m.peoplestring);        
            }
            mdta.Update(DS);
        }

        

        mediacacheDataSet DS = new mediacacheDataSet();
        ObservableCollection<MetaData> MD = new ObservableCollection<MetaData>();
        metacacheTableAdapter mdta = new metacacheTableAdapter();
        private void UpdateUIDataGrid()
        {
            string tag;
            bool IsPerson;
            Selecteditems.GetSelectedTag(out tag, out IsPerson);
            //DS.Clear();
           //MD = db.GetItems(SqliteMetaData._Tag.tags, Selecteditems.GetSelectedTag(), SqliteMetaData.Datatograb.all);
            //DS = db.GetDSItems(SqliteMetaData._Tag.tags, Selecteditems.GetSelectedTag(), SqliteMetaData.Datatograb.all);

            DS.Clear();
            mdta.Fill(DS.metacache);
            //DS.metacache.metacacheRowChanged += new mediacacheDataSet.metacacheRowChangeEventHandler(metacache_metacacheRowChanged);

           /* System.Diagnostics.Stopwatch t = new System.Diagnostics.Stopwatch();
            t.Start();
            for (int i = 0; i < DS.metacache.Count - 1; i++)
            {
                MD.Add(new MetaData(DS.metacache[i]));
                
            }
            t.Stop();
            MessageBox.Show("time to add:" + t.ElapsedMilliseconds);
            */
            
            
           // ListViewItem vi = new ListViewItem();
           // UI.ImageView vv = new UI.ImageView();
           // listView1.ItemsSource = MD; listView1.View = vv;
            //    DataTemplate dt = new DataTemplate();

            //dataGridMainWindow.ItemsSource = DS.DefaultViewManager.AsQueryable();
            


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
