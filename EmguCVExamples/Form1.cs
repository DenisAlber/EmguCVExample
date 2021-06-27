using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConvexHullExample;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.ImgHash;
using Emgu.CV.Ocl;
using Emgu.CV.Stitching;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using FaceDetectionExample;

namespace EmguCVExamples
{
    public partial class Form1 : Form
    {
        private List<List<Mat>> pyramid;
        private int zoom = 1;

        Size ImageSize = new Size(1084,932);
        public Form1()
        {
            InitializeComponent();
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            //GenerateTiles();
            var fileContent = string.Empty;
            var path = String.Empty;
            string[] filePaths = null;
            using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())
            {
                DialogResult result = openFileDialog.ShowDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.SelectedPath))
                {
                    //Get the path of specified file
                   filePaths = Directory.GetFiles(openFileDialog.SelectedPath);

                }
            }

            if (filePaths == null) return;
            //var filePaths = Directory.GetFiles(path, "*.*");
            var imageCollection = new List<Mat>();

            foreach (var filePath in filePaths)
            {
                imageCollection.Add(new Mat(filePath));
            }

            CreatePyramidShit(imageCollection);
            GetTile();
            //pyramid = BuildPyramid(imageCollection);
            //Mat img = null;

            //img = ImageStitcher(pyramid[0]);
         
            //if (img == null)
            //{
            //    return;
            //}

            //originalImageBox.Size = new Size(1466, 866);
            //originalImageBox.Image = img;
            //label1.Text = "";
            //originalImageBox.OnZoomScaleChange += FitZoomSize;
            //originalImageBox.MouseWheel += ZoomIn;


        }

        public List<List<Mat>> BuildPyramid(List<Mat> imageCollection)
        {
            Mat oke = new Mat();
            CvInvoke.BuildPyramid(imageCollection.First(), oke, 5);
            CvInvoke.Imwrite("C:\\Users\\Denis\\OneDrive - Duale Hochschule Baden-Württemberg Stuttgart\\BA\\Sample_Pictures\\Output26.jpg", oke);
            
            List<List<Mat>> pyrList = new List<List<Mat>>();
            foreach (var image in imageCollection)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        if (pyrList.Count == 0)
                        {
                            pyrList.Add(new List<Mat>());
                        }
                        
                        pyrList.First().Add(image);
                        
                        continue;
                    }

                    Mat res = new Mat();
                    CvInvoke.PyrDown(pyrList[i - 1][imageCollection.IndexOf(image)], res, BorderType.Reflect101);

                    if (pyrList.Count < i + 1)
                    {
                        pyrList.Add(new List<Mat>());
                    }
                    pyrList[i].Add(res);
                }
            }
            pyrList.Reverse();
            return pyrList;
        }

        private void GenerateTiles()
        {
            var tiler = new ImageTile("C:\\Users\\Denis\\OneDrive - Duale Hochschule Baden-Württemberg Stuttgart\\BA\\Sample_Pictures\\Output.jpg", 8,8);
            tiler.GenerateTiles("C:\\Users\\Denis\\OneDrive - Duale Hochschule Baden-Württemberg Stuttgart\\BA\\Sample_Pictures\\Sample2");
        }

        private void stitchImage_Click(object sender, EventArgs e)
        {
            //CvInvoke.Imwrite("C:\\Users\\Denis\\OneDrive - Duale Hochschule Baden-Württemberg Stuttgart\\BA\\Sample_Pictures\\Output3.jpg", ImageStitcher());

        }

        private Mat ImageStitcher(List<Mat> imageCollection)
        {
            //for (int i = 0; i < 6; i++)
            //{
            //    var num = i + 1;
            //    imageCollection.Add(new Mat(path + num + ".png"));
            //}
            //Declare the Mat object that will store the final output
            Mat output = new Mat();

            //Declare a vector to store all images from the list
            VectorOfMat matVector = new VectorOfMat();

            //Push all images in the list into a vector
            foreach (Mat img in imageCollection)
            {
                matVector.Push(img);
            }

            //Declare a new stitcher
            Stitcher stitcher = new Stitcher(Stitcher.Mode.Scans);

            SIFT detector = new SIFT();
            //Declare the type of detector that will be used to detect keypoints
            //Brisk detector = new Brisk();
            //Here are some other detectors that you can try
            //ORBDetector detector = new ORBDetector();

            //KAZE detector = new KAZE();
            //AKAZE detector = new AKAZE();
            //ORBDetector detector = new ORBDetector();
            //Set the stitcher class to use the specified detector declared above
            stitcher.SetFeaturesFinder(detector);
            //var blender = new MultiBandBlender();
            //stitcher.SetBlender(blender);

            //Stitch the images together
            stitcher.Stitch(matVector, output);

            return output;
        }

        private void originalImageBox_Click(object sender, EventArgs e)
        {
            
        }

        public void FitZoomSize(object sender, EventArgs e)
        {
            originalImageBox.SetZoomScale(1, originalImageBox.AutoScrollOffset);
        }

        public void CreatePyramidShit(List<Mat> ImageCollection)
        {
            foreach (var image in ImageCollection)
            {
                var bitmap = new Bitmap(image.Width + (image.Width / 2), image.Height);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawImage(image.ToBitmap(), 0, 0);
                    var pyrDownImage = image;
                    var height = 0;

                    for (int i = 0; i < 6; i++)
                    {
                        var pyrDownImageTmp = new Mat();
                        CvInvoke.PyrDown(pyrDownImage, pyrDownImageTmp);
                        g.DrawImage(pyrDownImageTmp.ToBitmap(), image.Width, height);
                        height += pyrDownImageTmp.Height;

                        pyrDownImage = pyrDownImageTmp;
                    }
                }

                bitmap.Save("C:\\Users\\Denis\\OneDrive - Duale Hochschule Baden-Württemberg Stuttgart\\BA\\Sample_Pictures\\Pyramid\\Output" + ImageCollection.IndexOf(image) +".bmp");
            }
        }

        public void GetTile()
        {
            Bitmap myBitmap = new Bitmap("C:\\Users\\Denis\\OneDrive - Duale Hochschule Baden-Württemberg Stuttgart\\BA\\Sample_Pictures\\Output3323.bmp");

            // Clone a portion of the Bitmap object.
            Rectangle cloneRect = new Rectangle(ImageSize.Width, 0, ImageSize.Width / 2, ImageSize.Height/2);
            System.Drawing.Imaging.PixelFormat format =
                myBitmap.PixelFormat;
            Bitmap cloneBitmap = myBitmap.Clone(cloneRect, format);

            cloneBitmap.Save("C:\\Users\\Denis\\OneDrive - Duale Hochschule Baden-Württemberg Stuttgart\\BA\\Sample_Pictures\\Output332______3.bmp");
        }

        public void ZoomIn(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (zoom == 4) return;
                zoom++;
                UpperThresholdLabel.Text = "Zoom: " + zoom;
            }
            else
            {
                if (zoom == 1) return;
                zoom--;
                UpperThresholdLabel.Text = "Zoom: " + zoom;
            }



            //UpperThresholdLabel.Text = "Zoom: " + originalImageBox.ZoomScale.ToString(CultureInfo.CurrentCulture);
            //if (originalImageBox.ZoomScale < 1)
            //{
            //    originalImageBox.SetZoomScale(1, originalImageBox.AutoScrollOffset);

            //}
            //else if (originalImageBox.ZoomScale > 8)
            //{
            //    originalImageBox.SetZoomScale(8, originalImageBox.AutoScrollOffset);

            //}

            Mat img = null;
            if (zoom == 1)
            {
                img = ImageStitcher(pyramid[0]);
            }
            if (zoom == 2)
            {
                img = ImageStitcher(pyramid[1]);
            }
            if (zoom == 3)
            {
                img = ImageStitcher(pyramid[2]);
            }
            if (zoom == 4)
            {
                img = ImageStitcher(pyramid[3]);
            }

            if (img == null)
            {
                return;
            }

            originalImageBox.Image = img;
        }
    }

    public class ImagePyramid
    {
        private List<Stage> _stages = new List<Stage>();

        public ImagePyramid(List<Mat> images, int levels)
        {
            _stages.Add(new Stage(images));

            for (int i = 1; i < 4; i++)
            {
                var tmpImages = _stages[i - 1].GetImages();

                var downSampledImages = new List<Mat>();

                foreach (var tmpImage in tmpImages)
                {
                    var result = new Mat();
                    CvInvoke.PyrDown(tmpImage, result);
                    downSampledImages.Add(result);
                }

                _stages.Add(new Stage(downSampledImages));
            }
        }

        public Stage GetStage(int level)
        {
            return _stages[level];
        }
    }

    public class Stage
    {
        private List<Tile> _tiles = new List<Tile>();
        public Stage(List<Mat> images)
        {
            
            foreach (var image in images)
            {
                _tiles.Add(new Tile(image));
            }

        }

        public List<Mat> GetImages()
        {
            List<Mat> Images = new List<Mat>();
            foreach (var tile in _tiles)
            {
                Images.Add(tile.GetImage());
            }

            return Images;
        }
    }

    public class Tile
    {
        private readonly Mat _image;

        public Tile(Mat image)
        {
            _image = image;
        }

        public Mat GetImage()
        {
            return _image;
        }
    }
}
