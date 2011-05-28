using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MediaDBwpf.Metadata;
using MediaDBwpf.Database.Metadata;
using System.IO;
using System.Windows.Forms.Integration;
using System.Data;
using DevZest.Windows.DataVirtualization;
namespace MediaDBwpf
{
    public partial class MainWindow : Window
    {
        void CreateEvents()
        {
            treeview_TagSelector.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(treeview_TagSelector_SelectedItemChanged);
            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
            Selecteditems.SelectedItemChanged += new SelectedItems.ItemChanged(Selecteditems_SelectedItemChanged);
            Selecteditems.SelectedTagListItemChanged += new SelectedItems.TagListChanged(Selecteditems_SelectedTagListItemChanged);
            listView1.SelectionChanged += new SelectionChangedEventHandler(listView1_SelectionChanged);
            //DS.Tables.CollectionChanged += new System.ComponentModel.CollectionChangeEventHandler(datasetTables_CollectionChanged);
            listView1.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(listView1_MouseDoubleClick);
            vlccont.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(vlccont_MouseDoubleClick);
            txttaginput.KeyDown += new System.Windows.Input.KeyEventHandler(txttaginput_KeyDown);
        }

                private void btnRemoveSelectedTag_Click(object sender, RoutedEventArgs e)
        {
            MetaData m = Selecteditems.SelectedItem();
            if (lsTagsonItem.SelectedIndex > -1)
            {
                TreeViewItem tvi = (TreeViewItem)lsTagsonItem.SelectedItem;
                Tag t = (Tag)tvi.Tag;
                Selecteditems.SelectedItem().RemoveTag(t);// Tags.Remove(lsTagsonItem.SelectedItem.ToString());
                Selecteditems.SelecteditemDView.Row["tags"] = Selecteditems.SelectedItem().tagstring;
                mdta.UpdateQuery(m.Hash, m.FilePath, m.ThumbnailasByte, m.tagstring, m.peoplestring, m.id);
            }
        }

        void txttaginput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Adds the tag to the Selected media item

            MetaData m = Selecteditems.SelectedItem();
                if (e.Key == System.Windows.Input.Key.Enter)
            {

                    Selecteditems.SelectedItem().AddTag(Static_Helpers.GetSingleTagFromString(txttaginput.Text));
                    Selecteditems.SelecteditemDView.Row["tags"] = Selecteditems.SelectedItem().tagstring;
                    //mdta.Update(Selecteditems.SelectedItem().GetasRow());
                    mdta.UpdateQuery(m.Hash, m.FilePath, m.ThumbnailasByte, m.tagstring, m.peoplestring, m.id);
                    Appdata.AddKnownTag(Static_Helpers.GetSingleTagFromString(txttaginput.Text));
                txttaginput.Text = "";
                UpdateUIElementsfromCache();
            }
        }

        void vlccont_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            vlccont.ToggleFullscreen();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            vlccont.TogglePause();
        }

        void listView1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataRowView dr = (DataRowView)listView1.SelectedItem;

            MetaData m = new MetaData(dr.Row);
            System.Windows.Controls.Image i = (Image)e.OriginalSource;
            if (listView1.SelectedIndex != -1)
            {
                vlccont.Play(m.FilePath, EasyVLC.VlcMediaType.FilePath);
                //we clicked this item
                //Vlccontrol.Pla
            }
        }

        void datasetTables_CollectionChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            if (e.Action == System.ComponentModel.CollectionChangeAction.Add)
            {
                DataRow dr = (DataRow)e.Element;

            }
        }

        void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 1) { return; }
            //VirtualListItem<MetaData> my = (VirtualListItem<MetaData>)e.AddedItems[0];
            DataRowView me = (DataRowView)e.AddedItems[0];
            Selecteditems.SelecteditemDView = me;
            MetaData m = new MetaData(me.Row);

            Selecteditems.SetSelectedItem(m.id, m);
            //DS.metacache.DefaultView.Sort = "filepath";

           // DS.metacache.DefaultView.RowFilter = "id < 50";
            //Selecteditems.SetSelectedItem()
        }

        void Selecteditems_SelectedTagListItemChanged(string TagList, bool IsPerson)
        {
            // Everything we want to do when the tag/person we have selected has changed
            if (IsPerson)
            { //DS.metacache.DefaultView.RowFilter = "people like '" + TagList + "'"; 
            }
            else
            {
                PopulateMediaList();
            }
            
            
        }



        void Selecteditems_SelectedItemChanged(MetaData m)
        {
            // We have selected a new meteadata item

            PopulateItemsDetails();
        }



        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Appdata.Serialize();
        }

        void treeview_TagSelector_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
            bool p = false;
            TreeView t = (TreeView)sender;
            TreeViewItem ti = (TreeViewItem)t.SelectedItem;
            if (ti == null) { return; }
            if ((ti.Header == "People") || (ti.Header == "Tags")) { return; }// We have clicked on People or Tags so do nothing
            if (ti.Tag == "Tag")
            {
                p = false;
            }
            else if (ti.Tag == "People") 
            {
                p = true; // sets flag is person to true so we know what kind of tag is selected.
            }
            //Edge case for untagged since p is false

            Selecteditems.SetSelectedTag(ti.Header.ToString(), p);
            
            //UpdateUIElementsfromCache();
        }
        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            //WindowsFormsHost folderdialog = new WindowsFormsHost();
            System.Windows.Forms.FolderBrowserDialog folderdialog = new System.Windows.Forms.FolderBrowserDialog();


            if (folderdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) { Appdata.AddFilePath(folderdialog.SelectedPath); Appdata.Serialize(); RefreshFileList(); }
            
        }

        
    }
}
