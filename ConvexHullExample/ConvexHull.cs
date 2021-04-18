using System;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace ConvexHullExample
{
    public class ConvexHull
    {
        private static PointF[] _pts;
        public static Mat CreateRandomPoints()
        {
            #region Create some random points
            Random r = new Random();
            _pts = new PointF[200];
            for (int i = 0; i < _pts.Length; i++)
            {
                _pts[i] = new PointF((float)(100 + r.NextDouble() * 400), (float)(100 + r.NextDouble() * 400));
            }
            #endregion

            Mat img = new Mat(600, 600, DepthType.Cv8U, 3);
            img.SetTo(new MCvScalar(255.0, 255.0, 255.0));
            //Draw the points 
            foreach (PointF p in _pts)
                CvInvoke.Circle(img, Point.Round(p), 3, new MCvScalar(0.0, 0.0, 0.0));

            return img;

        }
        public static Mat DrawConvexHull(Mat img)
        {
           
            //Find and draw the convex hull

            Stopwatch watch = Stopwatch.StartNew();
            PointF[] hull = CvInvoke.ConvexHull(_pts, true);
            watch.Stop();
            CvInvoke.Polylines(
                img,
#if NETFX_CORE
   Extensions.ConvertAll<PointF, Point>(hull, Point.Round),
#else
                Array.ConvertAll<PointF, Point>(hull, Point.Round),
#endif
                true, new MCvScalar(255.0, 0.0, 0.0));
            return img;
        }
    }
}
