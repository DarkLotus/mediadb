using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using System.Text.RegularExpressions;

using System.Threading;
using OpenCvSharp;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
namespace MediaDBClient
{
    public partial class Form1 : Form
    {
        public AppConfig Options = new AppConfig();
        _Media Media = new _Media();
        ImageList imglist = new ImageList();
        List<MetaData> DisplayList = new List<MetaData>();
        public dbaccess db = new dbaccess();
        public Form1()
        {
            this.Resize += new EventHandler(Form1_Resize);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
            imglist.ImageSize = new Size(75, 75);
            lsFileBrowser.TileSize = new System.Drawing.Size(75, 75);
            lsFileBrowser.LargeImageList = imglist;
            //lsFileBrowser.ItemCheck += new ItemCheckEventHandler(lsFileBrowser_ItemCheck);
            lsFileBrowser.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(lsFileBrowser_ItemSelectionChanged);
            lsFileBrowser.Activation = ItemActivation.Standard;
            lsFileBrowser.ItemActivate += new EventHandler(lsFileBrowser_ItemActivate);
            
            //treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView1_NodeMouseClick);
            //treeView1.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(treeView1_NodeMouseDoubleClick);
            TagList.NodeMouseClick += new TreeNodeMouseClickEventHandler(TagList_NodeMouseClick);
            txtEntry.KeyPress += new KeyPressEventHandler(txtEntry_KeyPress);
            //MediaWithTag.DoubleClick += new EventHandler(MediaWithTag_DoubleClick);
            // Load config/file list etc
            //TagSelector.ItemCheck += new ItemCheckEventHandler(TagSelector_ItemCheck);
         
            _SelectedTags.Add("Untagged");
            if (File.Exists("config.xml"))
            { 
                Options = AppConfig.Deserialize("config.xml");
                Options.MyConfig.KnownTags.Remove(" ");
            }
            if (File.Exists("cache.db"))
            {

                RefreshFileList();
            }
            else
            {
                db.CreateDB();
                RefreshFileList();
                
            }
            lsFileBrowser.VirtualMode = true;

            lsFileBrowser.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(lsFileBrowser_RetrieveVirtualItem);

        }

        void Form1_Resize(object sender, EventArgs e)
        {
            Size mysize = new System.Drawing.Size();
            mysize.Height = this.Size.Height - 60;
            mysize.Width = this.Size.Width - 700;

            //lsFileBrowser.Size = mysize;
            //lsFileBrowser
        }




        void TagSelector_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked)
            {
                if (!_lastclickedItem.Tags.Contains(TagSelector.Items[e.Index].ToString()))
                {
                    _lastclickedItem.Tags.Add(TagSelector.Items[e.Index].ToString());
                    db.UpdateItem(_lastclickedItem,dbaccess._Tag.tags);
                    db._tempstoretags = null;
                }
            }
            if (e.CurrentValue == CheckState.Unchecked)
            {
                if (_lastclickedItem.Tags.Contains(TagSelector.Items[e.Index].ToString()))
                {
                    _lastclickedItem.Tags.Remove(TagSelector.Items[e.Index].ToString());
                    db.UpdateItem(_lastclickedItem,dbaccess._Tag.tags);
                    db._tempstoretags = null; // resets cache
                }
            }
            if (!Options.MyConfig.KnownTags.Contains(txtEntry.Text)) { Options.MyConfig.KnownTags.Add(txtEntry.Text); }
            updategui();
        }
        
        void lsFileBrowser_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //MetaData m = db.GetItem(e.ItemIndex + 1);
            MetaData m = null;
            m = db.GetIndexedItemByTags(_SelectedTags,e.ItemIndex);

            if (m == null)
            {      
                return;
            }
            m.Update += new EventHandler(m_Update);
            lsFileBrowser.VirtualListSize = db._tempstore.Count;    
            ListViewItem lvi = new ListViewItem(m.Filename()); 	// create a listviewitem object
            lvi.Name = m.Filename(); 		// assign the text to the item
            lvi.Tag = m;
            if (!imglist.Images.ContainsKey(m.Hash)) { imglist.Images.Add(m.Hash, m.Thumbnail()); }

            lvi.ImageIndex = imglist.Images.IndexOfKey(m.Hash);
            e.Item = lvi; 		// assign item to event argument's item-property
            
        }

        void m_Update(object sender, EventArgs e)
        {

            MetaData m = (MetaData)sender;
            db.UpdateItem(m,dbaccess._Tag.thumb);
            m.Update -= m_Update;
        }
        private MetaData _lastclickedItem;
        void lsFileBrowser_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _lastclickedItem = (MetaData)e.Item.Tag;
            _Lastclickedtreeviewindex = e.ItemIndex;

            MetaData m = _lastclickedItem;// (MetaData)lsFileBrowser.Items[e.ItemIndex].Tag;
            foreach (string s in m.Tags)
            {    
                ListViewItem i = new ListViewItem();
                i.Text = s;
                i.Tag = m;

            }
            FileInfo f = new FileInfo(m.FilePath);
            
                double numbea1 = (double)f.Length / 1024 / 1024;
                numbea1 = Math.Round(numbea1, 2);
                txtBoxFileInfo.Text = numbea1.ToString();

            pictureBox1.Image = m.Thumbnail();
            TagSelector.Items.Clear();
            TagSelector.ItemCheck -= TagSelector_ItemCheck;
            foreach (string s in Options.MyConfig.KnownTags)
            {
                if (m.Tags.Contains(s)) { TagSelector.Items.Add(s, true); } else { TagSelector.Items.Add(s); }
                if (!bulktagList.Items.Contains(s)) { bulktagList.Items.Add(s); }
                
            }
            TagSelector.ItemCheck += new ItemCheckEventHandler(TagSelector_ItemCheck);

            
        }


        void lsFileBrowser_ItemActivate(object sender, EventArgs e)
        {
                MetaData m = (MetaData)lsFileBrowser.Items[_Lastclickedtreeviewindex].Tag;
                panel1.Play(m.FilePath, EasyVLC.VlcMediaType.FilePath);
                
           
        }
        private List<string> _SelectedTags = new List<string>();

        void TagList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            lsFileBrowser.Items.Clear();
            _SelectedTags.Clear();
            _SelectedTags.Add(e.Node.Text);
            db.GetIndexedItemByTags(_SelectedTags, 0);
            lsFileBrowser.VirtualListSize = db._tempstore.Count;
            lsFileBrowser.Refresh();
            return;

        }

        void txtEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Adds the tag to the Selected media item
            if (e.KeyChar == (char)13)
            {
                if (!_lastclickedItem.Tags.Contains(txtEntry.Text)) 
                {
                    _lastclickedItem.Tags.Add(txtEntry.Text);
                    db.UpdateItem(_lastclickedItem,dbaccess._Tag.tags);
                    db._tempstoretags = null; // resets cache
                }
                if (!Options.MyConfig.KnownTags.Contains(txtEntry.Text)) { Options.MyConfig.KnownTags.Add(txtEntry.Text); AppConfig.Serialize("config.xml", Options); }
                
                txtEntry.Text = "";
                updategui() ;
            }
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Media.Serialize("cache.xml", Media);
            AppConfig.Serialize("config.xml", Options);
        }

        
        private void RefreshFileList()
        {
            // Called when Program Loads and when a new Folder is added.
            // Syncs up the cache with whats listed in your scan paths

            lsFileBrowser.VirtualListSize = 0;
            int newmediacnt = 0;
            List<MetaData> temp = new List<MetaData>();
            List<MetaData> knownfiles = db.GetItems(dbaccess._Tag.tags, "All", dbaccess.Datatograb.hash);
            foreach (string F in Options.Filepaths()) // enumerate base folders
            {
                IEnumerable<string> filelist;
                if (Options.ScanSubFolders())
                {
                    filelist = GetFiles(F, Options.FileExtensions, SearchOption.AllDirectories); }
                else { filelist = GetFiles(F,Options.FileExtensions,SearchOption.TopDirectoryOnly); }
                
                foreach (string s in filelist)
                {
                    //treeView1.Nodes.Add(s.Substring(s.LastIndexOf("\\") + 1));
                    MetaData m = new MetaData(s);
                    bool Found = false;
                    System.Threading.Tasks.Parallel.ForEach(knownfiles, item => 
                        {
                            if(m.Hash == item.Hash) { Found = true; }
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
            lsFileBrowser.VirtualListSize = mediacnt;
            lsFileBrowser.Refresh();
            //MediaCache = db.GetItemsbyIDindex(0, 15);
            if (!Options.MyConfig.KnownTags.Contains("Untagged")) { Options.MyConfig.KnownTags.Add("Untagged"); }
            Options.MyConfig.KnownTags.Add("All");
            updategui();
            toolStripStatusLabel1.Text = mediacnt + " Files Cached." + newmediacnt + " New Files.";
            db.CleanDuplicateHashes();
            return;
                     
            //_Media.Serialize("cache.xml", Media);

            lsFileBrowser.VirtualListSize = Media.__Media.Count;

            foreach (MetaData m in Media.__Media)
            {
                //if (!imglist.Images.ContainsKey(m.Hash)) { imglist.Images.Add(m.Hash, m.Thumbnail()); }
                /*
                if (!lsFileBrowser.Items.ContainsKey(m.Filename()))
                {
                    ListViewItem i = new ListViewItem(m.Filename());
                    i.Tag = (object)m;
                    i.ImageKey = m.Hash;
                    i.Name = m.Filename();
                    lsFileBrowser.Items.Add(i);
                }*/
            }
            foreach (MetaData d in Media.__Media)
            {
                foreach (string s in d.Tags)
                { if ((!Options.MyConfig.KnownTags.Contains(s)) && (s.Length > 2)) { Options.MyConfig.KnownTags.Add(s); } }
            }
            updategui();
        }
        void updategui()
        {  
            TagList.Nodes.Clear();
            //treeView1.Nodes.Clear();
            //TagList.Nodes.Add("Untagged");
            //foreach (MetaData s in Media.__Media){ treeView1.Nodes.Add(s.ToString()); }
            foreach (string s in Options.MyConfig.KnownTags)
            {
                TagList.Nodes.Add(s); 
            }
        }


        public static IEnumerable<string> GetFiles(string path, string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(path, searchPattern, searchOption));
        }
         


        private int _Lastclickedtreeviewindex = 0;

       

        private void ResetView_Click(object sender, EventArgs e)
        {
            panel1.SnapShot("temp.jpg");
            Face.Facialrecogg fr = new Face.Facialrecogg();
            Image<Gray, Byte> myimg = new Image<Gray, byte>("temp.jpg");
            Image<Gray,Byte> theface = fr.DetectFaceInImage(myimg);
                        pictureBox1.Image = theface.ToBitmap();
                        theface.Bitmap.Save("temp2", ImageFormat.Jpeg);
                        

            }

           
            
        

        private void importFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) { Options.AddFilePath(folderBrowserDialog1.SelectedPath); AppConfig.Serialize("config.xml", Options); RefreshFileList(); }
            
        }

        private void btnUpdatetagsfromchkbox_Click(object sender, EventArgs e)
        {
            _lastclickedItem.Tags.Clear();
            foreach(object o in TagSelector.CheckedItems )
            {
                string s = (string)o;
                _lastclickedItem.Tags.Add(s); // need to stop duplicates
            }
        
            db.UpdateItem(_lastclickedItem,dbaccess._Tag.tags);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            panel1.TogglePause();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options op = new Options(this);
            op.Show();

        }

        private void btnBulkTag_Click(object sender, EventArgs e)
        {
            List<string> s = new List<string>();

            foreach(object o in bulktagList.CheckedItems)
            {
                s.Add((string)o);
            }

            db.BulkAddTagToMatchingItems(s, txtBulkFilename.Text);
            txtBulkFilename.Clear();
        }

        private void toolStripDeleteTag_Click(object sender, EventArgs e)
        {

            string s = TagList.SelectedNode.Text;
            List<MetaData> md = new List<MetaData>();
            md = db.GetItems(dbaccess._Tag.tags, s, dbaccess.Datatograb.all);
            foreach (MetaData m in md)
            {
                m.Tags.Remove(s);
                db.UpdateItem(m, dbaccess._Tag.tags);

            }
            Options.MyConfig.KnownTags.Remove(s);
            TagList.Nodes.RemoveByKey(s);

            // loop thru all the loaded files for that tag have to assume they are already loaded.
            // then update each item to be the same - that tag

        }

    }
}
