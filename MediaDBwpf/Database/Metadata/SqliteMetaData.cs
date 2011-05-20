/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using MediaDBwpf.Metadata;
using System.Collections.ObjectModel;
namespace MediaDBwpf.Database.Metadata
{
    class SqliteMetaData
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private List<MetaData> Media;
        private string[] split = { "-" };
        public SqliteMetaData()
        {
            SetConnection();
        }


        private void SetConnection()
        {
            sql_con = new SQLiteConnection
                ("Data Source=cache.db;Version=3;New=False;Compress=True;");
        }

       
        public void InitDB(List<MetaData> m)
        {
            // Called once to create table
            Media = m;
            if (!File.Exists("cache.db"))
            {  CreateDB(); }
            
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            for(int i = 0;i < Media.Count; i++)
            {
                sql_cmd.CommandText = "insert into mediacache (filepath,hash,thumb,tags) VALUES ('" + Media[i].FilePath.Replace("'","''") + "', '" + Media[i].Hash + "', '" + Media[i]._thumbnail + "', '" + Media[i].tagstring + "')";
                sql_cmd.ExecuteNonQuery();
                //DB = new SQLiteDataAdapter(cmdtext, sql_con);
            }
            sql_con.Close();
        }

        public void CreateDB()
        {
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "create table mediacache ( id INTEGER PRIMARY KEY, filepath TEXT NOT NULL, hash TEXT NOT NULL, thumb BLOB, tags TEXT )";
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
            Thread.Sleep(1000);

        }
     
        internal void BulkAddTagToMatchingItems(List<string> tags, string Match)
        {
            ObservableCollection<MetaData> md = GetItems(_Tag.filepath, Match, Datatograb.tags); // Get all items matching our Name string

            // Foreach item add the tags
            foreach (MetaData m in md)
            {
                m.Tags.UnionWith(tags);
                UpdateItem(m, _Tag.tags);
            }
            md.Clear();
            return;
        }
        private void bulkadd(object m, string cmd)
        {
            MetaData Me = (MetaData)m;
            
        }

        /*internal void UpdateItem(MetaData m)
        {
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();

            sql_cmd.CommandText = "update mediacache SET tags='" + m.tagstring() + "' WHERE hash='" + m.Hash + "'";//(filepath,hash,thumb) VALUES ('" + m.FilePath.Replace("'", "''") + "', '" + m.Hash + "', '" + m.thumbnail + "')";
                sql_cmd.ExecuteNonQuery();
                //DB = new SQLiteDataAdapter(cmdtext, sql_con);

            sql_con.Close();
        }
        internal void UpdateItemThumb(MetaData m)
        {
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            MemoryStream ms = new MemoryStream();
            m.Thumbnail().Save(ms, ImageFormat.Jpeg);
            sql_cmd.CommandText = "update mediacache SET thumb='" + ms.ToArray() + "' WHERE hash='" + m.Hash + "'";//(filepath,hash,thumb) VALUES ('" + m.FilePath.Replace("'", "''") + "', '" + m.Hash + "', '" + m.thumbnail + "')";
            sql_cmd.ExecuteNonQuery();
            //DB = new SQLiteDataAdapter(cmdtext, sql_con);

            sql_con.Close();
        }
        
        public enum _Tag
        {
            filepath = 0,
            hash = 1,
            thumb = 2,
            tags = 3
        }
        internal void AddItem(MetaData m)
        {
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            MemoryStream ms = new MemoryStream();
            m.Thumbnail.Save(ms, ImageFormat.Jpeg);
            sql_cmd.CommandText = "insert into mediacache (filepath,hash,thumb,tags) VALUES ('" + m.FilePath.Replace("'", "''") + "', '" + m.Hash + "', '" + ms.ToArray() + "', '" + m.tagstring + "')";
            sql_cmd.ExecuteNonQuery();
            //DB = new SQLiteDataAdapter(cmdtext, sql_con);

            sql_con.Close();
        }

        internal void AddItems(List<MetaData> md)
        {
            ObservableCollection<MetaData> list = GetItems(SqliteMetaData._Tag.tags, "All", SqliteMetaData.Datatograb.hash);
            
            sql_con.Open();
            SQLiteCommand  cmd = sql_con.CreateCommand();
                         
            SQLiteTransaction trans = sql_con.BeginTransaction();
            cmd.Transaction = trans;
            foreach (MetaData m in md)
            {
                if (!list.Contains(m))
                {
                    _addItem(cmd, m);
            
                }

            }
            //System.Threading.Tasks.Parallel.ForEach(md, item => _addItem(trans, item));
            trans.Commit();
            
            //DB = new SQLiteDataAdapter(cmdtext, sql_con);

            sql_con.Close();
        }
        internal void _addItem(SQLiteCommand cmd, MetaData m)
        {
            byte[] img = null;
            MemoryStream ms = new MemoryStream();
            if (m._thumbnail != null)
            {
                m.Thumbnail.Save(ms, ImageFormat.Jpeg);
                img = ms.ToArray();
            }


            cmd.CommandText = "insert into mediacache (filepath,hash,thumb,tags) VALUES (@fp,@ha,@th,@ta)";
            cmd.Parameters.Add("@fp", DbType.String).Value = m.FilePath;
            cmd.Parameters.Add("@ha", DbType.String).Value = m.Hash;
            cmd.Parameters.Add("@th", DbType.Binary).Value = img;
            cmd.Parameters.Add("@ta", DbType.String).Value = m.tagstring;
            //cmd.CommandText = "insert into mediacache (filepath,hash,thumb,tags) VALUES ('" + m.FilePath.Replace("'", "''") + "', '" + m.Hash + "', '" + img + "', '" + m.tagstring() + "')";
            cmd.ExecuteNonQuery();
        }
        internal int Count()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "SELECT COUNT(*) FROM mediacache";
            int cnt = Convert.ToInt32(sql_cmd.ExecuteScalar());
          

            sql_con.Close();
            return cnt;
            
        }
         private List<MetaData> GetItemsbyTagList(List<string> tag)
        {
             List<MetaData> temp = new List<MetaData>();
            foreach (string s in tag)
            {
                temp.AddRange(GetItems(_Tag.tags,s,Datatograb.all));//GetItemsbyTag(s));
            }
            return temp;
        }
         public DataSet GetDSItems(SqliteMetaData._Tag PropertyToMatch, string StringToMatch, SqliteMetaData.Datatograb GrabThis)
         {
             SetConnection();
             sql_con.Open();
             sql_cmd = sql_con.CreateCommand();
             // add params depending on what to get
             // return the list
             //Grabthis currently unused enable to optimize memory
             sql_cmd.CommandText = "SELECT * FROM mediacache WHERE @val LIKE @ma";
             sql_cmd.CommandText = sql_cmd.CommandText.Replace("@val", PropertyToMatch.ToString());

             if ((string.IsNullOrWhiteSpace(StringToMatch)) || ("Untagged" == StringToMatch))
             {
                 //sql_cmd.Parameters.Add("@ma", DbType.String).Value = "NULL";
                 sql_cmd.CommandText = sql_cmd.CommandText.Replace("LIKE", "=");
                 sql_cmd.CommandText = sql_cmd.CommandText.Replace("@ma", "'-'");
             }
             else if (PropertyToMatch == _Tag.tags)
             {
                 //if we are searching tags search for tag + % 
                 sql_cmd.CommandText = sql_cmd.CommandText.Replace("@ma", "'%" + StringToMatch + "%'");// +"%";
             }
             if ("All" == StringToMatch)
             {
                 // means we are searching by tag and we want all items any tag
                 //sql_cmd.Parameters.RemoveAt("@ma");
                 //sql_cmd.Parameters.Add("@ma", DbType.String).Value = "%";
                 sql_cmd.CommandText = "Select * FROM mediacache";
                 //sql_cmd.CommandText = sql_cmd.CommandText + " OR Null";
             }
             //fallover
             sql_cmd.CommandText = sql_cmd.CommandText.Replace("@ma", "'" + StringToMatch + "'");
             SQLiteDataAdapter myAdaptor = new SQLiteDataAdapter(sql_cmd.CommandText, sql_con.ConnectionString);
             DataSet ds = new DataSet();
             myAdaptor.Fill(ds);
             sql_con.Close();
             return ds;
         }
         public ObservableCollection<MetaData> GetItems(SqliteMetaData._Tag PropertyToMatch, string StringToMatch, SqliteMetaData.Datatograb GrabThis) 
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            // add params depending on what to get
            // return the list
            //Grabthis currently unused enable to optimize memory
            sql_cmd.CommandText = "SELECT * FROM mediacache WHERE @val LIKE @ma";
            sql_cmd.CommandText = sql_cmd.CommandText.Replace("@val", PropertyToMatch.ToString());
            
            if ((string.IsNullOrWhiteSpace(StringToMatch)) || ("Untagged" == StringToMatch))
            {
                //sql_cmd.Parameters.Add("@ma", DbType.String).Value = "NULL";
                sql_cmd.CommandText = sql_cmd.CommandText.Replace("LIKE", "=");
                sql_cmd.CommandText = sql_cmd.CommandText.Replace("@ma", "'-'");
            }
            else if (PropertyToMatch == _Tag.tags)
            {
                //if we are searching tags search for tag + % 
                sql_cmd.CommandText =  sql_cmd.CommandText.Replace("@ma","'%" + StringToMatch + "%'");// +"%";
            }
            if ("All" == StringToMatch)
            {
                // means we are searching by tag and we want all items any tag
                //sql_cmd.Parameters.RemoveAt("@ma");
                //sql_cmd.Parameters.Add("@ma", DbType.String).Value = "%";
                sql_cmd.CommandText = "Select * FROM mediacache";
                //sql_cmd.CommandText = sql_cmd.CommandText + " OR Null";
            }
            //fallover
            sql_cmd.CommandText = sql_cmd.CommandText.Replace("@ma", "'" + StringToMatch + "'");
            SQLiteDataAdapter myAdaptor = new SQLiteDataAdapter(sql_cmd.CommandText, sql_con.ConnectionString);
            DataSet ds = new DataSet();
            myAdaptor.Fill(ds);
            
            SQLiteDataReader r = null;
            r = sql_cmd.ExecuteReader(CommandBehavior.Default);
            ObservableCollection<MetaData> m = new ObservableCollection<MetaData>();
            while (r.Read())
            {
                
                MetaData md = new MetaData();
                switch(GrabThis)
                {       
                    case Datatograb.all:
                        md.FilePath = r["filepath"].ToString();
                        md.Hash = r["hash"].ToString();
                        md.id = Convert.ToInt32(r["id"]);
                        md.Tags.UnionWith(r["tags"].ToString().Split(split, StringSplitOptions.RemoveEmptyEntries));
                        if(r["thumb"] != DBNull.Value) 
                        {
                            byte[] b = (byte[])r["thumb"];
                            if ((b != null) && (b.Length > 50)) { md._thumbnail = Image.FromStream(new MemoryStream(b));  }
                        }
                        break;
                    case Datatograb.filepath:
                        md.FilePath = r["filepath"].ToString();
                        break;
                    case Datatograb.hash:
                        md.Hash = r["hash"].ToString();
                        break;
                    case Datatograb.tags:
                        md.Tags.UnionWith(r["tags"].ToString().Split(split, StringSplitOptions.RemoveEmptyEntries));
                        break;
                    case Datatograb.thumb:
                        md._thumbnail = Image.FromStream(new MemoryStream((byte[])r["thumb"]));
                        break;
                }                           
                m.Add(md);
            }
            sql_con.Close();
            return m;

        }
        public enum Datatograb
        {
            filepath = 0,
            hash = 1,
            tags = 2,
            thumb = 3,
            all = 4
        }
        /*public List<MetaData> GetItemsbyTag(string tag)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "SELECT * FROM mediacache WHERE tags LIKE '%" + tag + "%'";
            if (tag == "Untagged") { sql_cmd.CommandText = "SELECT * FROM mediacache WHERE tags like ''"; }
            SQLiteDataReader r = null;
            r = sql_cmd.ExecuteReader(CommandBehavior.Default);
            List<MetaData> m = new List<MetaData>();
            while (r.Read())
            {
                MetaData md = new MetaData();
                md.FilePath = r["filepath"].ToString();
                md.Hash = r["hash"].ToString();
                if ((r["thumb"] != DBNull.Value) && (r["thumb"] != null))
                {
                    md._thumbnail = Image.FromStream(new MemoryStream((byte[])r["thumb"]));
                }
                
                md.Tags.UnionWith(r["tags"].ToString().Split(split, StringSplitOptions.RemoveEmptyEntries));
                m.Add(md);
                
            }
            sql_con.Close();
            return m;
        }
        

        public Dictionary<int, MetaData> _tempstore = new Dictionary<int, MetaData>();
        public string[] _tempstoretags = null;
        public MetaData GetIndexedItemByTags(List<string> tags, int index)
        {

            if (_tempstoretags == null)
            {
                int xx = 0;
                _tempstore.Clear();
                _tempstoretags = null;
                foreach (MetaData mm in GetItemsbyTagList(tags))
                {
                    _tempstore.Add(xx, mm);
                    xx++;
                }
                _tempstoretags = tags.ToArray();
                return _tempstore[index];
            }
            if (tags[0] != _tempstoretags[0])
            {
                int xx = 0;
                _tempstore.Clear();
                _tempstoretags = null;
                foreach (MetaData mm in GetItemsbyTagList(tags))
                {
                    _tempstore.Add(xx, mm);
                    xx++;
                }
                _tempstoretags = tags.ToArray();
            }
            try { return _tempstore[index]; }
            catch { return null; }
            
        }
        public Dictionary<int, MetaData> GetItemsbyIDindex(int lower, int upper)
        {
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "SELECT * FROM mediacache WHERE id BETWEEN " + lower + " AND " + upper;
            SQLiteDataReader r = null;
            r = sql_cmd.ExecuteReader(CommandBehavior.Default);
            Dictionary<int, MetaData> m = new Dictionary<int, MetaData>();
            while (r.Read())
            {
                MetaData md = new MetaData();
                md.FilePath = r["filepath"].ToString();
                md.Hash = r["hash"].ToString();
                md._thumbnail = Image.FromStream(new MemoryStream((byte[])r["thumb"]));
                md.Tags.UnionWith(r["tags"].ToString().Split(split, StringSplitOptions.RemoveEmptyEntries));
                m.Add(Convert.ToInt32(r["id"]), md);
            }
            sql_con.Close();
            return m;
        }
        public void UpdateItem(MetaData m, _Tag t)
        {
            if (sql_con.State != ConnectionState.Closed)
            {
                sql_con.Close();
            }
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "update mediacache SET @collum = @var WHERE hash=@ha";
            sql_cmd.CommandText = sql_cmd.CommandText.Replace("@collum", t.ToString());
            sql_cmd.Parameters.Add("@ha", DbType.String).Value = m.Hash;
            if (t == _Tag.tags)
            {
                sql_cmd.Parameters.Add("@var", DbType.String).Value = m.tagstring;
            }
            if (t == _Tag.thumb)
            {
                MemoryStream ms = new MemoryStream(); m.Thumbnail.Save(ms, ImageFormat.Jpeg);
                sql_cmd.Parameters.Add("@var", DbType.Object).Value = ms.ToArray();
            }
            if (t == _Tag.filepath)
            {
                sql_cmd.Parameters.Add("@var", DbType.String).Value = m.FilePath;
            }

            //sql_cmd.CommandText = "update mediacache SET tags='" + m.tagstring() + "' WHERE hash='" + m.Hash + "'";//(filepath,hash,thumb) VALUES ('" + m.FilePath.Replace("'", "''") + "', '" + m.Hash + "', '" + m.thumbnail + "')";
            sql_cmd.ExecuteNonQuery();

            sql_con.Close();
        }
        public void CleanDuplicateHashes()
        {
            ObservableCollection<MetaData> md = new ObservableCollection<MetaData>();
            md = GetItems(_Tag.tags, "All", Datatograb.all);
            HashSet<MetaData> dontremove = new HashSet<MetaData>();
            List<MetaData> remove = new List<MetaData>();
            foreach (MetaData m in md)
            {
                if (!dontremove.Contains(m))
                {
                    dontremove.Add(m);
                }
                else
                {
                    remove.Add(m);
                }
            }
            foreach (MetaData m in remove)
            {
                RemoveItem(m);
            }
        }
        public void RemoveItem(MetaData m)
        {
            if (sql_con.State != ConnectionState.Closed)
            {
                sql_con.Close();
            }
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "DELETE FROM mediacache Where id=@ha";
            sql_cmd.CommandText = sql_cmd.CommandText.Replace("@ha", "'" + m.id + "'");
            sql_cmd.ExecuteNonQuery();
        }

    }
}*/
