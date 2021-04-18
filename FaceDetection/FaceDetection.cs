using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceDetectionExample
{
    public class FaceDetection
    {
        private static readonly CascadeClassifier CascadeClassifierHumanFace = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        private static readonly CascadeClassifier CascadeClassifierCatFace = new CascadeClassifier("haarcascade_frontalcatface.xml");

        public static (Mat, bool?) Detection(Mat img)
        {
            bool? isHuman = null;
            Bitmap bitmap = img.ToBitmap();
            var gray = new Mat();
            CvInvoke.CvtColor(img, gray, ColorConversion.Bgr2Gray);
            var rectangles = CascadeClassifierHumanFace.DetectMultiScale(gray, 1.4, 0);

            if (rectangles.Length == 0)
            {
                rectangles = CascadeClassifierCatFace.DetectMultiScale(gray, 1.4, 0);
                if(rectangles.Length != 0) isHuman = false;
            }
            else
            {
                isHuman = true;
            }

            foreach (var rectangle in rectangles)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red, 1))
                    {
                        graphics.DrawRectangle(pen, rectangle);
                    }
                }
            }

            return (bitmap.ToMat(), isHuman);
        }

    }
}
