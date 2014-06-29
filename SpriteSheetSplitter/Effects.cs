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
        public static Bitmap Scale(Image image, float factor)
        {
            var width = (int)(image.Width * factor);
            var height = (int)(image.Height * factor);
            var result = new Bitmap(width, height, image.PixelFormat);
            using (var g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(image, 0, 0, width, height);
            }
            return result;
        }
    }
}
