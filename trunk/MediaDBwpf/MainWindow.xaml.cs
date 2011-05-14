using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaDBwpf.Metadata;
using System.IO;

namespace MediaDBwpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UI.AppData Appdata;
        SelectedItems Selecteditems = new SelectedItems();
        Database.Metadata.SqliteMetaData db = new Database.Metadata.SqliteMetaData();
        public MainWindow()
        {
            InitializeComponent();
            ProgramOpen();
            
            
            
        }


       
     

        

    }
    
}
