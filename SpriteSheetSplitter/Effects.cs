using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteSheetSplitter
{
    /// <summary>
    /// Provides methods that operate on images.
    /// </summary>
    public static class Effects
    {
        /// <summary>
        /// Scales the specified image by a certain factor using nearest-neighbor filtering.
        /// </summary>
        /// <param name="source">The <see cref="T:System.Drawing.Image"/> to scale.</param>
        /// <param name="factor">The factor to multiply the image's dimensions with.</param>
        /// <returns>The scaled bitmap.</returns>
        public static Bitmap Scale(this Image source, float factor)
        {
            var location = new Point(0, 0);
            var size = new Size((int)(source.Width * factor), (int)(source.Height * factor));
            var rect = new Rectangle(location, size);
            return Redraw(source, size, rect);
        }

        /// <summary>
        /// Pads the image.
        /// </summary>
        /// <param name="source">The source image.</param>
        /// <param name="top">The amount of pixels to add to the top of the image.</param>
        /// <param name="right">The amount of pixels to add to the right of the image.</param>
        /// <param name="bottom">The amount of pixels to add to the bottom of the image.</param>
        /// <param name="left">The amount of pixels to add to the left of the image.</param>
        /// <returns>A new bitmap with the specified amount of pixels added.</returns>
        public static Bitmap Pad(this Image source, int top, int right, int bottom, int left)
        {
            var location = new Point(left, top);
            var size = new Size(source.Width + left + right, source.Height + top + bottom);
            var rect = new Rectangle(location, source.Size);
            return Redraw(source, size, rect);
        }

        /// <summary>
        /// Draws the image onto a new bitmap of the specified size onto the specified area.
        /// </summary>
        /// <param name="source">The source image to draw from.</param>
        /// <param name="size">The size of the new bitmap to draw onto.</param>
        /// <param name="rect">A rectangle specified the area of the new bitmap to draw <paramref name="source"/> onto.</param>
        /// <returns>The padded bitmap.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="size"/> is empty.</exception>
        public static Bitmap Redraw(this Image source, Size size, Rectangle rect)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (size.IsEmpty) throw new ArgumentException("Size must not be empty", "size");

            var result = new Bitmap(size.Width, size.Height, source.PixelFormat);
            using (var g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.DrawImage(source, rect);
            }
            return result;
        }
    }
}
