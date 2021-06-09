namespace EmguCVExamples
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.originalImageBox = new Emgu.CV.UI.ImageBox();
            this.proccesedImageBox = new Emgu.CV.UI.ImageBox();
            this.loadImageButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.detectShapesButton = new System.Windows.Forms.Button();
            this.convexHullButton = new System.Windows.Forms.Button();
            this.faceDetectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.originalButton = new System.Windows.Forms.Button();
            this.lowerThresholdBar = new System.Windows.Forms.TrackBar();
            this.upperThresholdBar = new System.Windows.Forms.TrackBar();
            this.LowerThresholdLabel = new System.Windows.Forms.Label();
            this.UpperThresholdLabel = new System.Windows.Forms.Label();
            this.stitchImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.proccesedImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerThresholdBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperThresholdBar)).BeginInit();
            this.SuspendLayout();
            // 
            // originalImageBox
            // 
            this.originalImageBox.Location = new System.Drawing.Point(11, 91);
            this.originalImageBox.Margin = new System.Windows.Forms.Padding(2);
            this.originalImageBox.Name = "originalImageBox";
            this.originalImageBox.Size = new System.Drawing.Size(274, 221);
            this.originalImageBox.TabIndex = 2;
            this.originalImageBox.TabStop = false;
            // 
            // proccesedImageBox
            // 
            this.proccesedImageBox.Location = new System.Drawing.Point(704, 91);
            this.proccesedImageBox.Margin = new System.Windows.Forms.Padding(2);
            this.proccesedImageBox.Name = "proccesedImageBox";
            this.proccesedImageBox.Size = new System.Drawing.Size(274, 221);
            this.proccesedImageBox.TabIndex = 3;
            this.proccesedImageBox.TabStop = false;
            // 
            // loadImageButton
            // 
            this.loadImageButton.Location = new System.Drawing.Point(9, 11);
            this.loadImageButton.Margin = new System.Windows.Forms.Padding(2);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(80, 26);
            this.loadImageButton.TabIndex = 4;
            this.loadImageButton.Text = "Load Image";
            this.loadImageButton.UseVisualStyleBackColor = true;
            this.loadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Triangles and Rectangles",
            "Circles",
            "Lines"});
            this.comboBox1.Location = new System.Drawing.Point(291, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(165, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Visible = false;
            // 
            // detectShapesButton
            // 
            this.detectShapesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.detectShapesButton.Location = new System.Drawing.Point(461, 12);
            this.detectShapesButton.Margin = new System.Windows.Forms.Padding(2);
            this.detectShapesButton.Name = "detectShapesButton";
            this.detectShapesButton.Size = new System.Drawing.Size(80, 26);
            this.detectShapesButton.TabIndex = 7;
            this.detectShapesButton.Text = "Detect";
            this.detectShapesButton.UseVisualStyleBackColor = true;
            this.detectShapesButton.Visible = false;
            this.detectShapesButton.Click += new System.EventHandler(this.detectShapesButton_Click);
            // 
            // convexHullButton
            // 
            this.convexHullButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.convexHullButton.Location = new System.Drawing.Point(899, 12);
            this.convexHullButton.Margin = new System.Windows.Forms.Padding(2);
            this.convexHullButton.Name = "convexHullButton";
            this.convexHullButton.Size = new System.Drawing.Size(80, 26);
            this.convexHullButton.TabIndex = 8;
            this.convexHullButton.Text = "Convex";
            this.convexHullButton.UseVisualStyleBackColor = true;
            this.convexHullButton.Click += new System.EventHandler(this.convexHullButton_Click);
            // 
            // faceDetectButton
            // 
            this.faceDetectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.faceDetectButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.faceDetectButton.Location = new System.Drawing.Point(815, 11);
            this.faceDetectButton.Margin = new System.Windows.Forms.Padding(2);
            this.faceDetectButton.Name = "faceDetectButton";
            this.faceDetectButton.Size = new System.Drawing.Size(80, 26);
            this.faceDetectButton.TabIndex = 9;
            this.faceDetectButton.Text = "Face Detect";
            this.faceDetectButton.UseVisualStyleBackColor = true;
            this.faceDetectButton.Visible = false;
            this.faceDetectButton.Click += new System.EventHandler(this.faceDetectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 10;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(692, 13);
            this.applyButton.Margin = new System.Windows.Forms.Padding(2);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(80, 26);
            this.applyButton.TabIndex = 12;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Visible = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Invert",
            "Gray",
            "Canny",
            "Blur"});
            this.comboBox2.Location = new System.Drawing.Point(588, 15);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(98, 21);
            this.comboBox2.TabIndex = 13;
            this.comboBox2.Visible = false;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // originalButton
            // 
            this.originalButton.Location = new System.Drawing.Point(93, 11);
            this.originalButton.Margin = new System.Windows.Forms.Padding(2);
            this.originalButton.Name = "originalButton";
            this.originalButton.Size = new System.Drawing.Size(80, 26);
            this.originalButton.TabIndex = 14;
            this.originalButton.Text = "Load Original";
            this.originalButton.UseVisualStyleBackColor = true;
            this.originalButton.Visible = false;
            this.originalButton.Click += new System.EventHandler(this.originalButton_Click);
            // 
            // lowerThresholdBar
            // 
            this.lowerThresholdBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerThresholdBar.Location = new System.Drawing.Point(538, 42);
            this.lowerThresholdBar.Maximum = 255;
            this.lowerThresholdBar.Name = "lowerThresholdBar";
            this.lowerThresholdBar.Size = new System.Drawing.Size(104, 45);
            this.lowerThresholdBar.TabIndex = 15;
            this.lowerThresholdBar.Visible = false;
            this.lowerThresholdBar.Scroll += new System.EventHandler(this.lowerThresholdBar_Scroll);
            // 
            // upperThresholdBar
            // 
            this.upperThresholdBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.upperThresholdBar.Location = new System.Drawing.Point(648, 42);
            this.upperThresholdBar.Maximum = 255;
            this.upperThresholdBar.Name = "upperThresholdBar";
            this.upperThresholdBar.Size = new System.Drawing.Size(104, 45);
            this.upperThresholdBar.TabIndex = 16;
            this.upperThresholdBar.Value = 200;
            this.upperThresholdBar.Visible = false;
            this.upperThresholdBar.Scroll += new System.EventHandler(this.upperThresholdBar_Scroll);
            // 
            // LowerThresholdLabel
            // 
            this.LowerThresholdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LowerThresholdLabel.AutoSize = true;
            this.LowerThresholdLabel.Location = new System.Drawing.Point(758, 42);
            this.LowerThresholdLabel.Name = "LowerThresholdLabel";
            this.LowerThresholdLabel.Size = new System.Drawing.Size(89, 13);
            this.LowerThresholdLabel.TabIndex = 17;
            this.LowerThresholdLabel.Text = "Low Threshold: 0";
            this.LowerThresholdLabel.Visible = false;
            // 
            // UpperThresholdLabel
            // 
            this.UpperThresholdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpperThresholdLabel.AutoSize = true;
            this.UpperThresholdLabel.Location = new System.Drawing.Point(758, 55);
            this.UpperThresholdLabel.Name = "UpperThresholdLabel";
            this.UpperThresholdLabel.Size = new System.Drawing.Size(91, 13);
            this.UpperThresholdLabel.TabIndex = 18;
            this.UpperThresholdLabel.Text = "High Threshold: 0";
            this.UpperThresholdLabel.Visible = false;
            // 
            // stitchImage
            // 
            this.stitchImage.Location = new System.Drawing.Point(177, 11);
            this.stitchImage.Margin = new System.Windows.Forms.Padding(2);
            this.stitchImage.Name = "stitchImage";
            this.stitchImage.Size = new System.Drawing.Size(80, 26);
            this.stitchImage.TabIndex = 19;
            this.stitchImage.Text = "Stitch Images";
            this.stitchImage.UseVisualStyleBackColor = true;
            this.stitchImage.Click += new System.EventHandler(this.stitchImage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(989, 366);
            this.Controls.Add(this.stitchImage);
            this.Controls.Add(this.UpperThresholdLabel);
            this.Controls.Add(this.LowerThresholdLabel);
            this.Controls.Add(this.upperThresholdBar);
            this.Controls.Add(this.lowerThresholdBar);
            this.Controls.Add(this.originalButton);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.faceDetectButton);
            this.Controls.Add(this.convexHullButton);
            this.Controls.Add(this.detectShapesButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.loadImageButton);
            this.Controls.Add(this.proccesedImageBox);
            this.Controls.Add(this.originalImageBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.proccesedImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerThresholdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperThresholdBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox originalImageBox;
        private Emgu.CV.UI.ImageBox proccesedImageBox;
        private System.Windows.Forms.Button loadImageButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button detectShapesButton;
        private System.Windows.Forms.Button convexHullButton;
        private System.Windows.Forms.Button faceDetectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button originalButton;
        private System.Windows.Forms.TrackBar lowerThresholdBar;
        private System.Windows.Forms.TrackBar upperThresholdBar;
        private System.Windows.Forms.Label LowerThresholdLabel;
        private System.Windows.Forms.Label UpperThresholdLabel;
        private System.Windows.Forms.Button stitchImage;
    }
}

