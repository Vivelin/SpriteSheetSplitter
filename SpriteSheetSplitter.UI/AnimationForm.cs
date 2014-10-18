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
    public partial class AnimationForm : Form, IDisposable
    {
        private Animation animation;
        private Stream preview;

        public AnimationForm()
        {
            InitializeComponent();
        }

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
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    animation.Save(dialog.FileName);
                    Close();
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
