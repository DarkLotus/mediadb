using System;
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
namespace MediaDBClient
{
    public partial class dbaccess
    {
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
                sql_cmd.Parameters.Add("@var", DbType.String).Value = m.tagstring();
            }
            if (t == _Tag.thumb)
            {
                MemoryStream ms = new MemoryStream(); m.Thumbnail().Save(ms, ImageFormat.Jpeg);
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
            List<MetaData> md = new List<MetaData>();
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
}
