using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MediaDBwpf.Metadata;
using MediaDBwpf.Database.Metadata;
using System.IO;
using System.Windows.Data;
using MediaDBwpf.UI;
using System.Data;
using DevZest.Windows.DataVirtualization;
namespace MediaDBwpf
{
    public partial class MainWindow : Window
    {

        void ProgramOpen()
        {
            
            if (File.Exists("config.xml")) { Appdata = UI.AppData.Deserialize(); } else { Appdata = new UI.AppData();  }
            //if (File.Exists("cache.db")) { RefreshFileList();}
            //else { db.CreateDB(); RefreshFileList(); }
            try { RefreshFileList(); }
            catch (Exception e)
            {
               string s = e.InnerException.Message;
            }
            
            BindListView();
           
            CreateEvents();
        }
        DataView dview = new DataView();
        
        private void BindListView()
        {
            //VirtualList<MetaData> data = new VirtualList<MetaData>(this);
            MetaDataList mdc = new MetaDataList();
            foreach (DataRow r in DS.metacache)
            {
                mdc.Add(new MetaData(r));
            }
            //dview = new DataView(DS.metacache);
            //dview.RowFilter = "id < 1000";
            
                      
            //dataGrid1.ItemsSource = dview;
            // dataGrid1.ItemsSource = dview;
            //listView1.DataContext = DS.Tables[0].DefaultView;
            //listView1.ItemsSource = DS.Tables[0].DefaultView;
            listView1.ItemsSource = mdc;
            //listView1.DataContext = dview;
            
        }

    }

    public class SelectedItems
    {
        private string SelectedItemInTagList;
        private bool SelectedTagListItemIsPerson; // tag or person
        private Metadata.MetaData SelectedMetaData;
        private int SelectedDSIndex;
        public delegate void TagListChanged(string TagList,bool IsPerson);
        public delegate void ItemChanged(Metadata.MetaData m);
        public event TagListChanged SelectedTagListItemChanged;
        public event ItemChanged SelectedItemChanged;

        public SelectedItems()
        {
            SelectedTagListItemIsPerson = false;
            SelectedItemInTagList = "All";
        }

        public void SetSelectedTag(string tag, bool IsPerson)
        {
            SelectedTagListItemIsPerson = IsPerson;
            SelectedItemInTagList = tag;
            SelectedTagListItemChanged(tag, IsPerson);// Raise event
        }
        public string GetSelectedTag()
        {
            return SelectedItemInTagList;
        }
        public void GetSelectedTag(out string tag, out bool IsPerson)
        {
            tag = SelectedItemInTagList;
            IsPerson = SelectedTagListItemIsPerson;

        }
        public void SetSelectedItem(int index, Metadata.MetaData ItemSelected)
        {
            SelectedMetaData = ItemSelected;
            SelectedDSIndex = index;
            SelectedItemChanged(SelectedMetaData);
        }
        public MetaData SelectedItem()
        {
            return SelectedMetaData;
        }
    }
}
