using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteSheetSplitter
{
    /// <summary>
    /// Represents an animation based on a <see cref="SpriteSheet"/>.
    /// </summary>
    public class Animation : IDisposable
    {
        private SpriteSheet spriteSheet;
        private int delay;
        private Color transparentColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class,
        /// using the specified <see cref="SpriteSheet"/> as source.
        /// </summary>
        /// <param name="source">The <see cref="SpriteSheet"/> containing the
        /// animation's frames.</param>
        public Animation(SpriteSheet source)
        {
            SpriteSheet = source;
        }

        /// <summary>
        /// Represents the method that will handle an event that has frame 
        /// image data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An object that contains the event data.</param>
        public delegate void FrameEventHandler(object sender, FrameEventArgs e);

        /// <summary>
        /// Occurs when the <see cref="SpriteSheet"/> property changes.
        /// </summary>
        public event EventHandler SpriteSheetChanged;

        /// <summary>
        /// Occurs when the <see cref="Delay"/> property changes.
        /// </summary>
        public event EventHandler DelayChanged;

        /// <summary>
        /// Occurs when the <see cref="TransparentColor"/> property changes.
        /// </summary>
        public event EventHandler TransparentColorChanged;

        /// <summary>
        /// Occurs before a frame is being rendered.
        /// </summary>
        public event FrameEventHandler AddingFrame;

        /// <summary>
        /// Gets or sets the <see cref="SpriteSheet"/> object that acts as the
        /// animation's source.
        /// </summary>
        public SpriteSheet SpriteSheet
        {
            get { return spriteSheet; }
            set
            {
                if (value != spriteSheet)
                {
                    spriteSheet = value;
                    OnSpriteSheetChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the delay between the animation's frames in 
        /// milliseconds.
        /// </summary>
        public int Delay
        {
            get { return delay; }
            set
            {
                if (value != delay)
                {
                    delay = value;
                    OnDelayChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color that appears as transparent in the resulting
        /// animation.
        /// </summary>
        public Color TransparentColor
        {
            get { return transparentColor; }
            set
            {
                if (value != transparentColor)
                {
                    transparentColor = value;
                    OnTransparentColorChanged();
                }
            }
        }

        /// <summary>
        /// Returns a collection of frames in the <see cref="Animation"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the frames
        /// in the <see cref="Animation"/>.</returns>
        public IEnumerable<Image> GetFrames()
        {
            return SpriteSheet.Split();
        }

        /// <summary>
        /// Saves the animation to the specified file as an animated GIF.
        /// </summary>
        /// <param name="path">The name of the file to which to save the
        /// animation.</param>
        public void Save(string path)
        {
            using (var stream = new FileStream(path, FileMode.Create,
                FileAccess.Write, FileShare.None))
            {
                Save(stream);
            }
        }

        /// <summary>
        /// Saves the animation to the specified stream as an animated GIF.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> where the animation
        /// will be saved.</param>
        public void Save(Stream stream)
        {
            using (var encoder = new Gif.Components.AnimatedGifEncoder(stream))
            {
                encoder.Delay = Delay;
                encoder.TransparentColor = TransparentColor;
                encoder.Repeat = 0;

                // Gross.
                var frames = GetFrames();
                var e = frames.GetEnumerator();
                var i = 0;
                while (e.MoveNext())
                {
                    var frame = e.Current;

                    if (!OnAddingFrame(ref frame, i))
                        break;

                    encoder.AddFrame(frame);

                    frame.Dispose();
                    i++;
                }
            }
        }

        /// <summary>
        /// Releases all resources used by this <see cref="Animation"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Optionally releases all managed resources used by this <see 
        /// cref="Animation"/>.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release managed resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (spriteSheet != null)
                {
                    spriteSheet.Dispose();
                    spriteSheet = null;
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="SpriteSheetChanged"/> event.
        /// </summary>
        protected virtual void OnSpriteSheetChanged()
        {
            var handler = SpriteSheetChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="DelayChanged"/> event.
        /// </summary>
        protected virtual void OnDelayChanged()
        {
            var handler = DelayChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="TransparentColorChanged"/> event.
        /// </summary>
        protected virtual void OnTransparentColorChanged()
        {
            var handler = TransparentColorChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="AddingFrame"/> event and returns a value
        /// indicates whether the frame should be added or not.
        /// </summary>
        /// <returns><c>false</c> if the animation should be stopped before the
        /// frame is added to the animation, <c>true</c> otherwise.</returns>
        protected virtual bool OnAddingFrame(ref Image frame, int index)
        {
            var handler = AddingFrame;
            if (handler != null)
            {
                var args = new FrameEventArgs(frame, index);
                handler(this, args);
                frame = args.Frame;
                return !args.Cancel;
            }
            return true;
        }
    }
}
