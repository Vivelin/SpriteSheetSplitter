using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteSheetSplitter
{
    /// <summary>
    /// Provides frame image data for events.
    /// </summary>
    public class FrameEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameEventArgs"/> 
        /// class for the specified frame.
        /// </summary>
        /// <param name="frame">The <see cref="Image"/> representing the frame
        /// data.</param>
        /// <param name="index">The index of the frame in the animation.
        /// </param>
        public FrameEventArgs(Image frame, int index)
        {
            Frame = frame;
            Index = index;
        }

        /// <summary>
        /// Gets or sets the <see cref="Image"/> representing the frame data.
        /// </summary>
        public Image Frame { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the animation should be
        /// cancelled before the current frame.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets the index of the current frame in the animation.
        /// </summary>
        public int Index { get; private set; }
    }
}
