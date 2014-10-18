namespace SpriteSheetSplitter.UI
{
    partial class AnimationForm
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                if (preview != null)
                {
                    preview.Dispose();
                    preview = null;
                }
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.scaleInput = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.delayInput = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.imagePanel = new SpriteSheetSplitter.UI.Controls.DoubleBufferedPanel();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scaleInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayInput)).BeginInit();
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.scaleInput);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.delayInput);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(502, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 412);
            this.panel1.TabIndex = 0;
            // 
            // scaleInput
            // 
            this.scaleInput.DecimalPlaces = 1;
            this.scaleInput.Location = new System.Drawing.Point(6, 68);
            this.scaleInput.Name = "scaleInput";
            this.scaleInput.Size = new System.Drawing.Size(141, 23);
            this.scaleInput.TabIndex = 3;
            this.scaleInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scaleInput.ValueChanged += new System.EventHandler(this.scaleInput_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "&Scale:";
            // 
            // delayInput
            // 
            this.delayInput.Location = new System.Drawing.Point(6, 19);
            this.delayInput.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.delayInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.delayInput.Name = "delayInput";
            this.delayInput.Size = new System.Drawing.Size(141, 23);
            this.delayInput.TabIndex = 1;
            this.delayInput.Value = new decimal(new int[] {
            167,
            0,
            0,
            0});
            this.delayInput.ValueChanged += new System.EventHandler(this.delayInput_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Delay (ms):";
            // 
            // imagePanel
            // 
            this.imagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePanel.BackgroundImage = global::SpriteSheetSplitter.UI.Properties.Resources.Transparency;
            this.imagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePanel.Controls.Add(this.imageBox);
            this.imagePanel.Location = new System.Drawing.Point(12, 12);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(484, 412);
            this.imagePanel.TabIndex = 2;
            // 
            // imageBox
            // 
            this.imageBox.BackColor = System.Drawing.Color.Transparent;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(482, 410);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(577, 430);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // AnimationForm
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 465);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AnimationForm";
            this.Text = "Animation Editor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scaleInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayInput)).EndInit();
            this.imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Controls.DoubleBufferedPanel imagePanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.NumericUpDown delayInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown scaleInput;
        private System.Windows.Forms.Label label2;
    }
}