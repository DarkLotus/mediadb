using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Media.Imaging;
namespace MediaDBwpf.Metadata
{
    public class MetaData : IEquatable<MetaData>
    {
        public HashSet<string> People = new HashSet<string>();
        public HashSet<string> Tags = new HashSet<string>();
        public string FilePath;
        public string Hash;
        public int id;
        //public string thumbnail = null;
        public BitmapImage _thumbnail = null;
        //public delegate void UpdateEventHandler(MetaData m);
        public event EventHandler Update;
        public MetaData(string filepath)
        {
            FilePath = filepath;
            Hash = CalculateMD5Hash(Filename + (new FileInfo(FilePath).Length));
            //Thumbnail();
        }
        public MetaData(DataRow row)
        {
            string[] split = { "," };
            id = Convert.ToInt32(row.ItemArray[5]);
            FilePath = row.ItemArray[1].ToString();
            Hash = row.ItemArray[0].ToString();
            Byte[] img = (byte[])row.ItemArray[2];
            if ((img != null) && (img.Count() > 0)) { try {
                _thumbnail = new BitmapImage();
                _thumbnail.BeginInit();
                _thumbnail.StreamSource= new MemoryStream(img);
                _thumbnail.EndInit();
            } 
            catch { _thumbnail = null; } }
            
            Tags.UnionWith(row.ItemArray[3].ToString().Split(split, StringSplitOptions.RemoveEmptyEntries));
            People.UnionWith(row.ItemArray[4].ToString().Split(split, StringSplitOptions.RemoveEmptyEntries));
           // Hash = CalculateMD5Hash(Filename + (new FileInfo(FilePath).Length));
            //Thumbnail();
        }
        public MetaData()
        { }

        public BitmapImage Thumbnail
        {
            get
            {
                if (_thumbnail != null)
                {
                    return _thumbnail;
                    //byte[] ia = Convert.FromBase64String(thumbnail); MemoryStream ms = new MemoryStream(ia, 0, ia.Length);
                    //while (!ms.CanRead) { Thread.Sleep(10);  }
                    //return Image.FromStream(ms, true);
                }
                else
                {
                    ShellFile file = ShellFile.FromFilePath(FilePath);
                    MemoryStream ms = new MemoryStream();
                    file.Thumbnail.Bitmap.Save(ms, ImageFormat.Jpeg);
                    _thumbnail.BeginInit();
                    _thumbnail.StreamSource = ms;
                    _thumbnail.EndInit();
                    //dbaccess db = new dbaccess();
                    //db.UpdateItem(this);
                    if (this.Update != null)
                    {
                        this.Update(this, new EventArgs());
                    }
                    return _thumbnail;
                }
            }
            set { }

        }
        public string tagstring
        {
            get {             string r = "";
            foreach (string s in Tags)
            {
                r = r + "," + s;
            }
            return r; }
            set { }

        }
        public string peoplestring
        {
            get
            {
                string r = "";
                foreach (string s in People)
                {
                    r = r + "," + s;
                }
                return r;
            }
            set { }

        }
        public string Filename
        {
            get { return FilePath.Substring(FilePath.LastIndexOf("\\") + 1); }
            set { }
            
        }
        public override string ToString()
        {
            return FilePath.Substring(FilePath.LastIndexOf("\\") + 1);
        }



        private string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


        public bool Equals(MetaData other)
        {
            // Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            // Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            // Check whether the objects’ properties are equal.
            return Hash.Equals(other.Hash);// &&               Textual.Equals(other.Textual);
        }
        public override int GetHashCode()
        {
            // may want to make this use hash to be safe
            // Get the hash code for the Textual field if it is not null.
            int hashTextual = Hash == null ? 0 : Hash.GetHashCode();

            // Get the hash code for the Digital field.
            //int hashDigital = Digital.GetHashCode();

            // Calculate the hash code for the object.
            return hashTextual;//hashDigital ^ hashTextual;
        }
    }
}
