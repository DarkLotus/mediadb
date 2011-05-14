using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenCvSharp;
using System.Drawing;


namespace MediaDBwpf.Metadata
{
    public class Person
    {
        public Image Thumbnail = null;
        public List<IplImage> Knowfaces = null;
        public string Name = null;
        public List<string> Tags = null;
        public Person()
        {
           // CREARE DB People (INDEX, COLORTHUMB BLOB, NAME STRING,, TAGS STRING, PROCCESSED GREY IMGS BLOB ARRAY? 
        }

    }
}
