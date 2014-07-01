using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteSheetSplitter
{
    public static class Effects
    {
        /// <summary>
        /// Scales the specified image by a certain factor using nearest-neighbor filtering.
        /// </summary>
        /// <param name="image">The <see cref="T:System.Drawing.Image"/> to scale.</param>
        /// <param name="factor">The factor to multiply the image's dimensions with.</param>
        /// <returns>The scaled bitmap.</returns>
        public static Bitmap Scale(Image image, float factor)
        {
            var width = (int)(image.Width * factor);
            var height = (int)(image.Height * factor);
            var result = new Bitmap(width, height, image.PixelFormat);
            using (var g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half; // Not setting this can result in "half pixels" added or missing
                g.DrawImage(image, 0, 0, width, height);
            }
            return result;
        }
    }
}
