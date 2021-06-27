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
            this.loadImageButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.UpperThresholdLabel = new System.Windows.Forms.Label();
            this.originalImageBox = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // loadImageButton
            // 
            this.loadImageButton.Location = new System.Drawing.Point(12, 14);
            this.loadImageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(107, 32);
            this.loadImageButton.TabIndex = 4;
            this.loadImageButton.Text = "Load Image";
            this.loadImageButton.UseVisualStyleBackColor = true;
            this.loadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 10;
            // 
            // UpperThresholdLabel
            // 
            this.UpperThresholdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpperThresholdLabel.AutoSize = true;
            this.UpperThresholdLabel.Location = new System.Drawing.Point(817, 9);
            this.UpperThresholdLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UpperThresholdLabel.Name = "UpperThresholdLabel";
            this.UpperThresholdLabel.Size = new System.Drawing.Size(56, 16);
            this.UpperThresholdLabel.TabIndex = 18;
            this.UpperThresholdLabel.Text = "Zoom: 1";
            // 
            // originalImageBox
            // 
            this.originalImageBox.Location = new System.Drawing.Point(57, 63);
            this.originalImageBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.originalImageBox.Name = "originalImageBox";
            this.originalImageBox.Size = new System.Drawing.Size(761, 426);
            this.originalImageBox.TabIndex = 2;
            this.originalImageBox.TabStop = false;
            this.originalImageBox.Click += new System.EventHandler(this.originalImageBox_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(886, 517);
            this.Controls.Add(this.UpperThresholdLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadImageButton);
            this.Controls.Add(this.originalImageBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loadImageButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label UpperThresholdLabel;
        private Emgu.CV.UI.ImageBox originalImageBox;
    }
}

