using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

    public class ImageTile
    {
        private Image image;
        private Size size;

        public ImageTile(string inputFile, int xSize, int ySize)
        {
            if (!File.Exists(inputFile)) throw new FileNotFoundException();

            image = Image.FromFile(inputFile);
            size = new Size(xSize, ySize);
        }

        public void GenerateTiles(string outputPath)
        {
            int xMax = image.Width;
            int yMax = image.Height;
            int tileWidth = xMax / size.Width;
            int tileHeight = yMax / size.Height;

            if (!Directory.Exists(outputPath)) { Directory.CreateDirectory(outputPath); }

            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    string outputFileName = Path.Combine(outputPath, string.Format("{0}_{1}.jpg", x, y));

                    Rectangle tileBounds = new Rectangle(x * tileWidth, y * tileHeight, tileWidth+100, tileHeight+100);

                    Bitmap target = new Bitmap(tileWidth, tileHeight);

                    using (Graphics graphics = Graphics.FromImage(target))
                    {
                        graphics.DrawImage(
                            image,
                            new Rectangle(0, 0, tileWidth, tileHeight),
                            tileBounds,
                            GraphicsUnit.Pixel);
                    }

                    target.Save(outputFileName, ImageFormat.Png);
                }
            }
        }
    }
}
