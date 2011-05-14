using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using MediaDBwpf.Metadata;
using System.Drawing;

namespace MediaDBwpf.Face_Recog
{
    public class Facialrecogg
    {
        string cascade_name = "C:/OpenCV2.2/data/haarcascades/haarcascade_frontalface_alt.xml";
        //Used for Facial Recog
        EigenObjectRecognizer imgRecognizer;
        Image<Gray,Byte>[] KnownFacearray;
        string[] KnowFaceLabelarray;
        MCvTermCriteria termCritera;
        
        //Used for Facial Detection
        HaarCascade haarcasc;
        public Facialrecogg()
        {
            // Load the Cascade we use to detect faces
            haarcasc = new HaarCascade(cascade_name);
        }


        public Image<Gray,Byte> DetectFaceInImage(Image<Gray,Byte> ImgToScan)
        {
            List<Image<Gray, Byte>> facesfound = new List<Image<Gray, byte>>();
            MCvAvgComp[][] faces = ImgToScan.DetectHaarCascade(haarcasc,1,4,Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,new Size(20,20));
        foreach(MCvAvgComp[] face in faces) {
            facesfound.Add(ImgToScan.GetSubRect(face[0].rect));
            ImgToScan.Draw(face[0].rect, new Gray(100), 2);
        }
        Emgu.CV.UI.ImageViewer iv = new Emgu.CV.UI.ImageViewer(ImgToScan);

        return facesfound[0];
        }


        public string FindMatchingFaces(Image<Gray,Byte> FaceToMatch)
        {
            if(KnowFaceLabelarray == null) {
                LoadArrays();
            }
            FaceToMatch._EqualizeHist();
            
            termCritera = new MCvTermCriteria(0.001);
            imgRecognizer = new EigenObjectRecognizer(KnownFacearray,KnowFaceLabelarray,500,ref termCritera);
            return imgRecognizer.Recognize(FaceToMatch);


        
        }

        private void LoadArrays()
        {
            int dbsize = MediaDBwpf.Database.People.SqlitePeople.Getfacedb(out KnownFacearray,out KnowFaceLabelarray);

        }
        
    }



       
    }
