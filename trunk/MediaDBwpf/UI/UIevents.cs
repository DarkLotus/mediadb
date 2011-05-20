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
            DS.Tables.CollectionChanged += new System.ComponentModel.CollectionChangeEventHandler(datasetTables_CollectionChanged);
            
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

            //VirtualListItem<MetaData> my = (VirtualListItem<MetaData>)e.AddedItems[0];
            //MetaData m = my.Data;
            //DS.metacache.DefaultView.Sort = "filepath";

           // DS.metacache.DefaultView.RowFilter = "id < 50";
            //Selecteditems.SetSelectedItem()
        }

        void Selecteditems_SelectedTagListItemChanged(string TagList, bool IsPerson)
        {
            // Everything we want to do when the tag/person we have selected has changed
        }

        void Selecteditems_SelectedItemChanged(MetaData m)
        {
            // We have selected a new meteadata item
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
            if (ti.Name != ti.Header.ToString()) { return; }// We have clicked on People or Tags so do nothing
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
            UpdateUIDataGrid();
        }
        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            //WindowsFormsHost folderdialog = new WindowsFormsHost();
            System.Windows.Forms.FolderBrowserDialog folderdialog = new System.Windows.Forms.FolderBrowserDialog();


            if (folderdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) { Appdata.AddFilePath(folderdialog.SelectedPath); Appdata.Serialize(); RefreshFileList(); }
            
        }

        
    }
}
