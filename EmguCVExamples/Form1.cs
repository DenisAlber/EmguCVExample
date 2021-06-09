using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConvexHullExample;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.ImgHash;
using Emgu.CV.Stitching;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using FaceDetectionExample;

namespace EmguCVExamples
{
    public partial class Form1 : Form
    {
        private Mat image;
        private Mat originalImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            if (filePath.Length == 0) return;

            originalImage = new Mat(filePath);
            image = originalImage;
            originalImageBox.Size = image.Size;
            originalImageBox.Image = image;
            proccesedImageBox.Location = new Point(originalImageBox.Location.X+10 + originalImageBox.Size.Width, originalImageBox.Location.Y);
            proccesedImageBox.Image = null;
            label1.Text = "";
            detectShapesButton.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            applyButton.Visible = true;
            faceDetectButton.Visible = true;
            originalButton.Visible = true;
        }

        private void InvertImageColors()
        {
            if (image == null) return;
            originalImageBox.Image = image;
            proccesedImageBox.Size = image.Size;
            var invert = new Mat();
            CvInvoke.BitwiseNot(image, invert);
            proccesedImageBox.Image = invert;
            image = invert;
        }

        private void Bgr2Gray()
        {
            if (image == null) return;
            originalImageBox.Image = image;
            proccesedImageBox.Size = image.Size;
            Mat gray = new Mat();
            CvInvoke.CvtColor(image, gray, ColorConversion.Bgr2Gray);
            proccesedImageBox.Image = image.ToImage<Gray, byte>();
            image = gray;
        }

        private void Canny()
        {
            if (image == null) return;
            originalImageBox.Image = image;
            proccesedImageBox.Size = image.Size;
            var canny = new Mat();
            CvInvoke.Canny(image, canny, lowerThresholdBar.Value, upperThresholdBar.Value);
            proccesedImageBox.Image = canny;
        }

        private void GaussianBlur()
        {
            if (image == null) return;
            originalImageBox.Image = image;
            proccesedImageBox.Size = image.Size;
            var blur = new Mat();
            CvInvoke.GaussianBlur(image, blur, new Size(5,5), 1.5);
            proccesedImageBox.Image = blur;
            image = blur;
        }

        public Mat ProcessImage(Mat img)
        {
            using (UMat gray = new UMat())
            using (UMat cannyEdges = new UMat())
            using (Mat triangleRectangleImage = new Mat(img.Size, DepthType.Cv8U, 3)) //image to draw triangles and rectangles on
            using (Mat circleImage = new Mat(img.Size, DepthType.Cv8U, 3)) //image to draw circles on
            using (Mat lineImage = new Mat(img.Size, DepthType.Cv8U, 3)) //image to drtaw lines on
            {
                //Convert the image to grayscale and filter out the noise
                CvInvoke.CvtColor(img, gray, ColorConversion.Bgr2Gray);

                //Remove noise
                CvInvoke.GaussianBlur(gray, gray, new Size(3, 3), 1);

                #region circle detection
                double cannyThreshold = 180.0;
                double circleAccumulatorThreshold = 120;
                CircleF[] circles = CvInvoke.HoughCircles(gray, HoughModes.Gradient, 2.0, 20.0, cannyThreshold,
                    circleAccumulatorThreshold, 5);
                #endregion

                #region Canny and edge detection
                double cannyThresholdLinking = 120.0;
                CvInvoke.Canny(gray, cannyEdges, cannyThreshold, cannyThresholdLinking);
                LineSegment2D[] lines = CvInvoke.HoughLinesP(
                    cannyEdges,
                    1, //Distance resolution in pixel-related units
                    Math.PI / 45.0, //Angle resolution measured in radians.
                    20, //threshold
                    30, //min Line width
                    10); //gap between lines
                #endregion

                #region Find triangles and rectangles
                List<Triangle2DF> triangleList = new List<Triangle2DF>();
                List<RotatedRect> boxList = new List<RotatedRect>(); //a box is a rotated rectangle
                using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                {
                    CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List,
                        ChainApproxMethod.ChainApproxSimple);
                    int count = contours.Size;
                    for (int i = 0; i < count; i++)
                    {
                        using (VectorOfPoint contour = contours[i])
                        using (VectorOfPoint approxContour = new VectorOfPoint())
                        {
                            CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05,
                                true);
                            if (CvInvoke.ContourArea(approxContour, false) > 250
                            ) //only consider contours with area greater than 250
                            {
                                if (approxContour.Size == 3) //The contour has 3 vertices, it is a triangle
                                {
                                    Point[] pts = approxContour.ToArray();
                                    triangleList.Add(new Triangle2DF(
                                        pts[0],
                                        pts[1],
                                        pts[2]
                                    ));
                                }
                                else if (approxContour.Size == 4) //The contour has 4 vertices.
                                {
                                    #region determine if all the angles in the contour are within [80, 100] degree
                                    bool isRectangle = true;
                                    Point[] pts = approxContour.ToArray();
                                    LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                    for (int j = 0; j < edges.Length; j++)
                                    {
                                        double angle = Math.Abs(
                                            edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                        if (angle < 80 || angle > 100)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                    }

                                    #endregion

                                    if (isRectangle) boxList.Add(CvInvoke.MinAreaRect(approxContour));
                                }
                            }
                        }
                    }
                }
                #endregion

                #region draw triangles and rectangles
                triangleRectangleImage.SetTo(new MCvScalar(0));
                foreach (Triangle2DF triangle in triangleList)
                {
                    CvInvoke.Polylines(triangleRectangleImage, Array.ConvertAll(triangle.GetVertices(), Point.Round),
                        true, new Bgr(Color.DarkBlue).MCvScalar, 2);
                }

                foreach (RotatedRect box in boxList)
                {
                    CvInvoke.Polylines(triangleRectangleImage, Array.ConvertAll(box.GetVertices(), Point.Round), true,
                        new Bgr(Color.DarkOrange).MCvScalar, 2);
                }

                //Drawing a light gray frame around the image
                CvInvoke.Rectangle(triangleRectangleImage,
                    new Rectangle(Point.Empty,
                        new Size(triangleRectangleImage.Width - 1, triangleRectangleImage.Height - 1)),
                    new MCvScalar(120, 120, 120));
                //Draw the labels
                CvInvoke.PutText(triangleRectangleImage, "Triangles and Rectangles", new Point(20, 20),
                    FontFace.HersheyDuplex, 0.5, new MCvScalar(120, 120, 120));
                #endregion

                #region draw circles
                circleImage.SetTo(new MCvScalar(0));
                foreach (CircleF circle in circles)
                    CvInvoke.Circle(circleImage, Point.Round(circle.Center), (int)circle.Radius,
                        new Bgr(Color.Brown).MCvScalar, 2);

                //Drawing a light gray frame around the image
                CvInvoke.Rectangle(circleImage,
                    new Rectangle(Point.Empty, new Size(circleImage.Width - 1, circleImage.Height - 1)),
                    new MCvScalar(120, 120, 120));
                //Draw the labels
                CvInvoke.PutText(circleImage, "Circles", new Point(20, 20), FontFace.HersheyDuplex, 0.5,
                    new MCvScalar(120, 120, 120));
                #endregion

                #region draw lines
                lineImage.SetTo(new MCvScalar(0));
                foreach (LineSegment2D line in lines)
                    CvInvoke.Line(lineImage, line.P1, line.P2, new Bgr(Color.Green).MCvScalar, 2);
                //Drawing a light gray frame around the image
                CvInvoke.Rectangle(lineImage,
                    new Rectangle(Point.Empty, new Size(lineImage.Width - 1, lineImage.Height - 1)),
                    new MCvScalar(120, 120, 120));
                //Draw the labels
                CvInvoke.PutText(lineImage, "Lines", new Point(20, 20), FontFace.HersheyDuplex, 0.5,
                    new MCvScalar(120, 120, 120));
                #endregion

                var res = new Mat();
                switch (comboBox1.Text)
                {
                    case "Triangles and Rectangles":
                        CvInvoke.VConcat(new Mat[]{triangleRectangleImage}, res);
                        return res;
                    case "Circles":
                        CvInvoke.VConcat(new Mat[] { circleImage }, res);
                        return res;
                    case "Lines":
                        CvInvoke.VConcat(new Mat[] { lineImage }, res);
                        return res;
                    default:
                        break;
                }

                Mat result = new Mat();
                CvInvoke.VConcat(new Mat[] { img, triangleRectangleImage, circleImage, lineImage }, result);
                return result;
            }
        }

        private void detectShapesButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length == 0) return;
            var img2 = ProcessImage(image);
            proccesedImageBox.Size = img2.Size;
            proccesedImageBox.Image = img2;

        }

        private void convexHullButton_Click(object sender, EventArgs e)
        {
            image = ConvexHull.CreateRandomPoints();
            label1.Text = "";
            originalImageBox.Size = image.Size;
            originalImageBox.Image = image;
            proccesedImageBox.Location = new Point(originalImageBox.Location.X + 10 + originalImageBox.Size.Width, originalImageBox.Location.Y);

            image = ConvexHull.DrawConvexHull(image);
            proccesedImageBox.Size = image.Size;
            proccesedImageBox.Image = image;

        }

        private void faceDetectButton_Click(object sender, EventArgs e)
        {
            var (imgFaceDetection, isHuman) = FaceDetection.Detection(image);
            proccesedImageBox.Size = imgFaceDetection.Size;
            proccesedImageBox.Image = imgFaceDetection;

            if (isHuman != null)
            {
                if (isHuman == true)
                {
                    label1.Text = "Human Detected!";
                }
                else
                {
                    label1.Text = "Cat Detected!";
                }
            }
        }

  

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text.Length == 0) return;
            
            switch (comboBox2.Text)
            {
                case "Invert":
                    InvertImageColors();
                    break;
                case "Gray":
                    Bgr2Gray();
                    break;
                case "Canny":
                    Canny();
                    break;
                case "Blur":
                    GaussianBlur();
                    break;
            }
        }

        private void originalButton_Click(object sender, EventArgs e)
        {
            image = originalImage;
            originalImageBox.Image = image;
        }

        private void lowerThresholdBar_Scroll(object sender, EventArgs e)
        {
            LowerThresholdLabel.Text = "Low Threshold: " + lowerThresholdBar.Value.ToString();
        }

        private void upperThresholdBar_Scroll(object sender, EventArgs e)
        {
            UpperThresholdLabel.Text = "High Threshold: " + upperThresholdBar.Value.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "Canny")
            {
                upperThresholdBar.Visible = false;
                lowerThresholdBar.Visible = false;
                LowerThresholdLabel.Visible = false;
                UpperThresholdLabel.Visible = false;
            }
            else
            {
                upperThresholdBar.Visible = true;
                lowerThresholdBar.Visible = true;
                LowerThresholdLabel.Visible = true;
                UpperThresholdLabel.Visible = true;
            }
        }

        private void stitchImage_Click(object sender, EventArgs e)
        {
            var path =
                "C:\\Users\\Denis\\OneDrive - Duale Hochschule Baden-Württemberg Stuttgart\\BA\\Sample_Pictures\\picture";

            var imageCollection = new List<Mat>();

            for (int i = 0; i < 6; i++)
            {
                var num = i + 1;
                imageCollection.Add(new Mat(path + num + ".png"));
            }
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

            //Declare the type of detector that will be used to detect keypoints
            Brisk detector = new Brisk();

            //Here are some other detectors that you can try
            //ORBDetector detector = new ORBDetector();
            //KAZE detector = new KAZE();
            //AKAZE detector = new AKAZE();

            //Set the stitcher class to use the specified detector declared above
            stitcher.SetFeaturesFinder(detector);

            //Stitch the images together
            stitcher.Stitch(matVector, output);

            CvInvoke.Imwrite("C:\\Users\\Denis\\OneDrive - Duale Hochschule Baden-Württemberg Stuttgart\\BA\\Sample_Pictures\\Output.png", output);

        }
    }
}
