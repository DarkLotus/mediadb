using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MediaDBwpf.Metadata
{
    public class Tagselector
    {
        TreeViewItem People = new TreeViewItem();
        TreeViewItem tags = new TreeViewItem();
        public TreeViewItem Tags = new TreeViewItem();
        public Tagselector()
        {
            People.Header = "People";
            tags.Header = "Tags";
            Tags.Items.Add(People);
            Tags.Items.Add(tags);
        }
        public override string ToString()
        {
            return this.People.Header.ToString();
        }
        
    }

}
