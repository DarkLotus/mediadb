using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MediaDBwpf.Metadata;
using MediaDBwpf.Database.Metadata;
using System.IO;
namespace MediaDBwpf
{
    public partial class MainWindow : Window
    {

        void ProgramOpen()
        {
            dataGridMainWindow.AutoGenerateColumns = true;
            if (File.Exists("config.xml")) { Appdata = UI.AppData.Deserialize(); } else { Appdata = new UI.AppData();  }
            if (File.Exists("cache.db")) { RefreshFileList();}
            else { db.CreateDB(); RefreshFileList(); }
            
           // dataGridMainWindow.ItemsSource = DS.Tables.AsQueryable();
            CreateEvents();
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
