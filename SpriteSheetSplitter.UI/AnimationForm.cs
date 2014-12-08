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
using SpriteSheetSplitter.Transformations;

namespace SpriteSheetSplitter.UI
{
    /// <summary>
    /// Represents a dialog where an <see cref="Animation"/> can be edited.
    /// </summary>
    public partial class AnimationForm : Form, IDisposable
    {
        private Animation animation;
        private Stream preview;
        private string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationForm"/> class.
        /// </summary>
        public AnimationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationForm"/> class
        /// using the specified <see cref="SpriteSheet"/> as animation source.
        /// </summary>
        /// <param name="spriteSheet">
        /// The <see cref="SpriteSheet"/> object to use as animation source.
        /// </param>
        public AnimationForm(SpriteSheet spriteSheet)
            : this()
        {
            var animation = new Animation(spriteSheet);
            LoadFormData(animation);
        }

        /// <summary>
        /// Occurs when the delay has changed.
        /// </summary>
        public event EventHandler DelayChanged;

        /// <summary>
        /// Occurs when the transformation or any of its properties have 
        /// changed.
        /// </summary>
        public event EventHandler TransformationChanged;

        /// <summary>
        /// Occurs when the <see cref="FileName"/> property changes.
        /// </summary>
        public event EventHandler FileNameChanged;
        			
        /// <summary>
        /// Gets or sets the name of the file to be edited.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (value != fileName)
                {
                    fileName = value;
                    OnFileNameChanged();
                }
            }
        }
				

        /// <summary>
        /// Raises the <see cref="DelayChanged"/> event.
        /// </summary>
        protected virtual void OnDelayChanged()
        {
            animation.Delay = (int)delayInput.Value;
            RenderPreview();

            var handler = DelayChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="TransformationChanged"/> event.
        /// </summary>
        protected virtual void OnTransformationChanged()
        {
            var transformation = new ScaleTransformation((float)scaleInput.Value);
            animation.Transformation = transformation;
            RenderPreview();

            var handler = TransformationChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="FileNameChanged"/> event.
        /// </summary>
        protected virtual void OnFileNameChanged()
        {
            Text = Path.GetFileName(FileName) + " - Animation Editor";

            var handler = FileNameChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Updates the form to display a new <see cref="Animation"/> object.
        /// </summary>
        /// <param name="animation">
        /// The <see cref="Animation"/> to display for editing.
        /// </param>
        protected void LoadFormData(Animation animation)
        {
            this.animation = animation;

            if (animation.Delay > 0) delayInput.Value = animation.Delay;
            if (animation.Transformation is ScaleTransformation)
            {
                var scale = (ScaleTransformation)animation.Transformation;
                scaleInput.Value = (decimal)scale.Factor;
            }

            RenderPreview();
        }

        /// <summary>
        /// Displays or updates the preview animation.
        /// </summary>
        protected void RenderPreview()
        {
            if (preview != null)
            {
                preview.Dispose();
                preview = null;
            }

            preview = new MemoryStream();
            animation.Save(preview);

            preview.Position = 0;
            imageBox.Image = Image.FromStream(preview);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Animated GIF image (*.gif)|*.gif";
                dialog.FileName = Path.GetFileName(FileName);

                var dir = Path.GetDirectoryName(FileName);
                if (!string.IsNullOrEmpty(dir))
                    dialog.InitialDirectory = dir;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    animation.Save(dialog.FileName);
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void delayInput_ValueChanged(object sender, EventArgs e)
        {
            OnDelayChanged();
        }

        private void scaleInput_ValueChanged(object sender, EventArgs e)
        {
            OnTransformationChanged();
        }
    }
}
