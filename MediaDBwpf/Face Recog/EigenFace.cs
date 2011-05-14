
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

using MediaDBwpf.Metadata;

namespace MediaDBwpf.Face_Recog
{
    class Facialrecog
    {
        string cascade_name = "C:/OpenCV2.2/data/haarcascades/haarcascade_frontalface_alt.xml";
        int height = 100;
        int width = 80;       
        

        string imgpath = "C://img//";

        //IplImage img;

        public IplImage[] GetfacesFromImage(IplImage Screencap)
        {

            return null;
        }
        public Person CheckDatabase(IplImage FacetoID)
        {

            return null;
        }
        public void detect(int num)
        {
            IplImage img;
            for (int i = 1; i < 8; i++)
            {
                img = Cv.LoadImage(imgpath + i + ".jpg");
                detect_and_draw(img);
                Cv.WaitKey();
                Cv.DestroyWindow("result");
                List<IplImage> faces = IDFace(img);
                if (faces.Count > 0)
                {
                    faces[0].SaveImage(imgpath + "face" + i + ".jpg");
                }

            }



            // 
            // 
            // Cv.ReleaseImage(img);
            // 


        }
        public List<IplImage> IDFace(IplImage facetoid)
        {
            int i = 0;
            List<IplImage> Myfaces = new List<IplImage>();
            CvSeq<OpenCvSharp.CvAvgComp> faces = detect_and_draw(facetoid);
            foreach (CvAvgComp avg in faces)
            {
                IplImage face = facetoid.GetSubImage(avg.Rect);
                IplImage imggrey;
                if (facetoid.NChannels == 3)
                {
                    imggrey = Cv.CreateImage(face.Size, BitDepth.U8, 1);
                    Cv.CvtColor(face, imggrey, ColorConversion.BgrToGray);
                }
                else
                {
                    imggrey = face;
                }
                IplImage imgresized = Cv.CreateImage(new CvSize(width, height), BitDepth.U8, 1);
                Cv.Resize(imggrey, imgresized, Interpolation.Cubic);
                Cv.EqualizeHist(imgresized, imgresized);
                Cv.ReleaseImage(face);
                Cv.ReleaseImage(imggrey);
                Myfaces.Add(imgresized);


                //Cv.ReleaseImage(imgresized);
                i++;
            }
            return Myfaces;

        }
        public CvSeq<OpenCvSharp.CvAvgComp> detect_and_draw(IplImage img)
        {

            OpenCvSharp.CvHaarClassifierCascade cascade;
            OpenCvSharp.CvMemStorage storage = new CvMemStorage();
            int scale = 1;
            IplImage temp = Cv.CreateImage(Cv.Size(img.Size.Width / scale, img.Size.Height / scale), BitDepth.U8, 3);
            CvPoint pt1, pt2;
            int i;
            cascade = (CvHaarClassifierCascade)Cv.LoadHaarClassifierCascade(cascade_name, img.Size);
            storage = Cv.CreateMemStorage(0);
            Cv.NamedWindow("result", WindowMode.NormalGui);
            Cv.ClearMemStorage(storage);
            if (cascade != null)
            {
                CvSeq<OpenCvSharp.CvAvgComp> faces = Cv.HaarDetectObjects(img, cascade, storage);
                for (i = 0; i < (faces.Total); i++)
                {
                    // Create a new rectangle for drawing the face
                    CvRect r = (CvRect)faces.ElementAt(i);
                    //CvRect* r = (CvRect*)cvGetSeqElem(faces, i);
                    pt1.X = r.X * scale;
                    pt2.X = (r.X + r.Width) * scale;
                    pt1.Y = r.Y * scale;
                    pt2.Y = (r.Y + r.Height) * scale;
                    Cv.Rectangle(img, pt1, pt2, Cv.RGB(255, 0, 0), 3, LineType.Link8, 0);
                    // Find the dimensions of the face,and scale it if necessary
                    //pt1.X = r->x * scale;
                    // pt2.X = (r->x + r->width) * scale;
                    // pt1.Y = r->y * scale;
                    // pt2.Y = (r->y + r->height) * scale;

                    // Draw the rectangle in the input image
                    //cvRectangle(img, pt1, pt2, CV_RGB(255, 0, 0), 3, 8, 0);
                }
                Cv.ShowImage("result", img);
                Cv.ReleaseImage(temp);
                return faces;
            }
            return null;

        }
    }
}
