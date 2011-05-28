using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaDBwpf.Metadata;
using System.Windows.Controls;

namespace MediaDBwpf
{
    public static class Static_Helpers
    {
        internal static IEnumerable<Tag> GetTagsfromtagString(string a)
        {
            //returns a tag object that has its tag set to the string. If string is multiple tags or tag tree returns a null named tag with children
            List<Tag> tags = new List<Tag>();
            //Tag r = new Tag();
            string[] full = a.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (full.Count() < 1) { return null; }// maybe return blank tag?
            foreach (string s in full)
            {
                // s is each individual tag, some tags may have depth
                if (!s.Contains("\\"))
                {
                    // No children tags, adds the tag to our list.
                    Tag temp = new Tag(); temp.tag = s;
                    tags.Add(temp);
                }
                else
                {

                    List<string> x = s.Split("\\".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
                    Tag addme = new Tag();
                    for (int i = 0; i < x.Count; i++)
                    {
                        addme.tag = x[i];
                        Tag t = new Tag();
                        if (i < x.Count - 1)
                        {
                            addme.AddChild(t);
                            addme = t;
                        }

                    }
                    addme = addme.findbase();
                    tags.Add(addme);
                }

            }
            return tags;
        }
     

       public static Tag GetSingleTagFromString(string s)
        {
            //returns a tag object that has its tag set to the string. If string is multiple tags or tag tree returns a null named tag with children
            //Tag r = new Tag();

                // s is each individual tag, some tags may have depth
                if (!s.Contains("\\"))
                {
                    // No children tags, adds the tag to our list.
                    Tag temp = new Tag(); temp.tag = s;
                    return temp;
                    //tags.Add(temp);
                }
                else
                {

                    List<string> x = s.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                    Tag addme = new Tag();
                    for (int i = 0; i < x.Count; i++)
                    {
                        addme.tag = x[i];
                        Tag t = new Tag();
                        if (i < x.Count - 1)
                        {
                            addme.AddChild(t);
                            addme = t;
                        }
                    }
                    addme = addme.findbase();
                    return addme;
                    //tags.Add(addme);
                }
      
        }

        internal static string GetTagasstring(List<Tag> _Tag)
        {
            string r = "";
            foreach (Tag q in _Tag)
            {
                if (q.Children.Count == 0)
                {
                    r = r + q.ParentasString() + "\\" + q.tag;
                    /*if (q.Parent != null) 
                    {
                        r = r + "{" + q.tag + "}";
                    } else 
                    {
                        r = r + "{" + q.tag + "}";
                    }
                    */
                    //continue;
                    r = r + "-";
                }
                else { r = r + GetTagasstring(q.Children); }
                
            }
            return r;
        }

        /*internal static string GetTagstring(Tag t)
        {
            if (t.Children.Count == 0)
            {
                return t.ToString();
            }
            string r = "";
            foreach (Tag q in t.Children)
            {
                r = r + "," + t.ToString() + "\\" + GetTagstring(q);
            }
            return r;
        }
        */
        internal static TreeViewItem GetTreeFromTag(Tag tag)
        {
            if (tag == null) { return null; }
            //if (tag.tag == string.IsNullOrWhiteSpace()) { }
            TreeViewItem tbase = new TreeViewItem();
            if (tag.Children.Count == 0) { tbase.Header = tag.ToString(); tbase.Tag = tag; return tbase; }
            tbase.Header = tag.ToString(); tbase.Tag = tag;
            foreach (Tag t in tag.Children)
            {
                tbase.Items.Add(GetTreeFromTag(t));
            }
            return tbase;
        }
        internal static List<TreeViewItem> GetTreefromTagList(List<Tag> tags)
        {
            List<TreeViewItem> mylist = new List<TreeViewItem>();
            foreach (Tag tag in tags)
            {
                mylist.AddRange(GetTreeFromTag2(tag));
            }
            return mylist;
        }
        internal static List<TreeViewItem> GetTreeFromTag2(Tag tag)
        {
            if (tag == null) { return null; }
            List<TreeViewItem> tbase = new List<TreeViewItem>();
            if (tag.Children.Count == 0) {
                TreeViewItem tr = new TreeViewItem();
                tr.Header = tag.tag; tr.Tag = tag; tbase.Add(tr);
                return tbase; 

            }

                TreeViewItem trr = new TreeViewItem();
                trr.Header = tag.tag; trr.Tag = tag;
            foreach (Tag t in tag.Children)
            {
                    trr.Items.Add(GetTreeFromTag2(t).First());
                                //tbase.AddRange(GetTreeFromTag2(t));
            }
            if (string.IsNullOrWhiteSpace(tag.tag))
            {
                foreach (TreeViewItem temp2 in trr.Items)
                {
                    tbase.Add(temp2);
                }
                
            }
            else
            {
                tbase.Add(trr);
            }
            
            return tbase;
        }



        
    }
}
