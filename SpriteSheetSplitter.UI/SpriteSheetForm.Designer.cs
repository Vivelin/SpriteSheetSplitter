namespace SpriteSheetSplitter.UI
{
    partial class SpriteSheetForm
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
            this.animateButton = new System.Windows.Forms.Button();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.columnMajorButton = new System.Windows.Forms.RadioButton();
            this.rowMajorButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.frameHeightInput = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.frameWidthInput = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagePanel = new SpriteSheetSplitter.UI.Controls.DoubleBufferedPanel();
            this.spriteSheetImage = new System.Windows.Forms.PictureBox();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameHeightInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameWidthInput)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spriteSheetImage)).BeginInit();
            this.SuspendLayout();
            // 
            // animateButton
            // 
            this.animateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.animateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.animateButton.Enabled = false;
            this.animateButton.Location = new System.Drawing.Point(577, 430);
            this.animateButton.Name = "animateButton";
            this.animateButton.Size = new System.Drawing.Size(75, 23);
            this.animateButton.TabIndex = 1;
            this.animateButton.Text = "Animate...";
            this.animateButton.UseVisualStyleBackColor = true;
            this.animateButton.Click += new System.EventHandler(this.animateButton_Click);
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesPanel.Controls.Add(this.columnMajorButton);
            this.propertiesPanel.Controls.Add(this.rowMajorButton);
            this.propertiesPanel.Controls.Add(this.label3);
            this.propertiesPanel.Controls.Add(this.frameHeightInput);
            this.propertiesPanel.Controls.Add(this.label2);
            this.propertiesPanel.Controls.Add(this.frameWidthInput);
            this.propertiesPanel.Controls.Add(this.label1);
            this.propertiesPanel.Enabled = false;
            this.propertiesPanel.Location = new System.Drawing.Point(502, 27);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(150, 397);
            this.propertiesPanel.TabIndex = 2;
            // 
            // columnMajorButton
            // 
            this.columnMajorButton.AutoSize = true;
            this.columnMajorButton.Location = new System.Drawing.Point(6, 141);
            this.columnMajorButton.Name = "columnMajorButton";
            this.columnMajorButton.Size = new System.Drawing.Size(104, 19);
            this.columnMajorButton.TabIndex = 6;
            this.columnMajorButton.TabStop = true;
            this.columnMajorButton.Text = "&Column-major";
            this.columnMajorButton.UseVisualStyleBackColor = true;
            // 
            // rowMajorButton
            // 
            this.rowMajorButton.AutoSize = true;
            this.rowMajorButton.Checked = true;
            this.rowMajorButton.Location = new System.Drawing.Point(6, 116);
            this.rowMajorButton.Name = "rowMajorButton";
            this.rowMajorButton.Size = new System.Drawing.Size(84, 19);
            this.rowMajorButton.TabIndex = 5;
            this.rowMajorButton.TabStop = true;
            this.rowMajorButton.Text = "&Row-major";
            this.rowMajorButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "&Order:";
            // 
            // frameHeightInput
            // 
            this.frameHeightInput.Location = new System.Drawing.Point(6, 67);
            this.frameHeightInput.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.frameHeightInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frameHeightInput.Name = "frameHeightInput";
            this.frameHeightInput.Size = new System.Drawing.Size(141, 23);
            this.frameHeightInput.TabIndex = 3;
            this.frameHeightInput.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.frameHeightInput.ValueChanged += new System.EventHandler(this.frameHeightInput_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Frame &height:";
            // 
            // frameWidthInput
            // 
            this.frameWidthInput.Location = new System.Drawing.Point(6, 18);
            this.frameWidthInput.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.frameWidthInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frameWidthInput.Name = "frameWidthInput";
            this.frameWidthInput.Size = new System.Drawing.Size(141, 23);
            this.frameWidthInput.TabIndex = 1;
            this.frameWidthInput.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.frameWidthInput.ValueChanged += new System.EventHandler(this.frameWidthInput_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Frame &width:";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mainMenu.Size = new System.Drawing.Size(664, 24);
            this.mainMenu.TabIndex = 3;
            this.mainMenu.Text = "Main Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // imagePanel
            // 
            this.imagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePanel.AutoScroll = true;
            this.imagePanel.BackgroundImage = global::SpriteSheetSplitter.UI.Properties.Resources.Transparency;
            this.imagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePanel.Controls.Add(this.spriteSheetImage);
            this.imagePanel.Location = new System.Drawing.Point(12, 27);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(484, 397);
            this.imagePanel.TabIndex = 4;
            // 
            // spriteSheetImage
            // 
            this.spriteSheetImage.BackColor = System.Drawing.Color.Transparent;
            this.spriteSheetImage.Location = new System.Drawing.Point(-1, -1);
            this.spriteSheetImage.Margin = new System.Windows.Forms.Padding(0);
            this.spriteSheetImage.Name = "spriteSheetImage";
            this.spriteSheetImage.Size = new System.Drawing.Size(100, 50);
            this.spriteSheetImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.spriteSheetImage.TabIndex = 0;
            this.spriteSheetImage.TabStop = false;
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Image = global::SpriteSheetSplitter.UI.Properties.Resources.Open_6529;
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.openImageToolStripMenuItem.Text = "&Open image...";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // SpriteSheetForm
            // 
            this.AcceptButton = this.animateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 465);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.propertiesPanel);
            this.Controls.Add(this.animateButton);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "SpriteSheetForm";
            this.Text = "Sprite Sheet Editor";
            this.propertiesPanel.ResumeLayout(false);
            this.propertiesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameHeightInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameWidthInput)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.imagePanel.ResumeLayout(false);
            this.imagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spriteSheetImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button animateButton;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown frameHeightInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown frameWidthInput;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.PictureBox spriteSheetImage;
        private Controls.DoubleBufferedPanel imagePanel;
        private System.Windows.Forms.RadioButton columnMajorButton;
        private System.Windows.Forms.RadioButton rowMajorButton;
        private System.Windows.Forms.Label label3;
    }
}