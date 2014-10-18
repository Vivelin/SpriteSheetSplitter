using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpriteSheetSplitter.UI.Controls;

namespace SpriteSheetSplitter.UI
{
    /// <summary>
    /// Represents a window that lets the user edit a <see cref="SpriteSheet"/>
    /// by defining the size of tiles and which tiles to incorporate into an
    /// animation.
    /// </summary>
    public partial class SpriteSheetForm : Form
    {
        private string fileName;
        private SpriteSheet spriteSheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheetForm"/>
        /// class.
        /// </summary>
        public SpriteSheetForm()
        {
            InitializeComponent();

            mainMenu.Renderer = new BorderlessToolStripRenderer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteSheetForm"/> 
        /// class for the specified <see cref="SpriteSheet"/>.
        /// </summary>
        /// <param name="path">The name of the file to load.</param>
        public SpriteSheetForm(string path)
            : this()
        {
            LoadFormData(path);
        }

        /// <summary>
        /// Occurs when a file has been loaded.
        /// </summary>
        public event EventHandler FileLoaded;

        /// <summary>
        /// Occurs when the tile size has changed.
        /// </summary>
        public event EventHandler TileSizeChanged;

        /// <summary>
        /// Loads a <see cref="SpriteSheet"/>'s data onto the form.
        /// </summary>
        /// <param name="path">The name of the file to load.</param>
        protected void LoadFormData(string path)
        {
            fileName = path;
            spriteSheet = SpriteSheet.FromFile(path);

            Text = System.IO.Path.GetFileName(fileName);
            spriteSheetImage.Image = spriteSheet.Bitmap;

            OnFileLoaded();
        }

        /// <summary>
        /// Raises the <see cref="FileLoaded"/> event.
        /// </summary>
        protected virtual void OnFileLoaded()
        {
            propertiesPanel.Enabled = true;
            animateButton.Enabled = true;

            var handler = FileLoaded;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="TileSizeChanged"/> event.
        /// </summary>
        protected virtual void OnTileSizeChanged()
        {
            spriteSheet.TileSize = new Size(
                (int)frameWidthInput.Value,
                (int)frameHeightInput.Value);

            var handler = TileSizeChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    LoadFormData(dialog.FileName);
                }
            }
        }

        private void frameWidthInput_ValueChanged(object sender, EventArgs e)
        {
            OnTileSizeChanged();
        }

        private void frameHeightInput_ValueChanged(object sender, EventArgs e)
        {
            OnTileSizeChanged();
        }

        private void animateButton_Click(object sender, EventArgs e)
        {
            using (var animationForm = new AnimationForm(spriteSheet))
            {
                Hide();
                if (animationForm.ShowDialog(this) != DialogResult.Cancel)
                    Close();
                else
                    Show();
            }            
        }
    }
}
