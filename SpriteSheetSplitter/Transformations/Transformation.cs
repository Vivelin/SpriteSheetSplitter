using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SpriteSheetSplitter.Transformations
{
    /// <summary>
    /// An abstract base class that provides functionality for image 
    /// transformations.
    /// </summary>
    public abstract class Transformation
    {
        /// <summary>
        /// Applies the transformation to the specified <see cref="Image"/>.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> to transform.</param>
        public abstract void ApplyTo(ref Image image);

        /// <summary>
        /// Draws the image onto a new bitmap of the specified size onto the 
        /// specified area.
        /// </summary>
        /// <param name="source">The source image to draw from.</param>
        /// <param name="size">The size of the new bitmap to draw onto.</param>
        /// <param name="target">A rectangle specifying the area of the new 
        /// bitmap to draw the <paramref name="source"/> image onto.</param>
        /// <returns>The padded bitmap.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref 
        /// name="source"/> is null.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="size"/> 
        /// is empty.</exception>
        protected static Bitmap Redraw(Image source, Size size, Rectangle target)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (size.IsEmpty) throw new ArgumentException("Size must not be " 
                + "empty", "size");

            var result = new Bitmap(size.Width, size.Height, source.PixelFormat);
            using (var g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.DrawImage(source, target);
            }
            return result;
        }
    }
}
