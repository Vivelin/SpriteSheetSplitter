using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpriteSheetSplitter
{
    class SpriteSheet : IDisposable
    {
        private Bitmap bitmap;

        public SpriteSheet(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            Trace.WriteLine(string.Format("Bitmap: {0}x{1} ({2})", bitmap.Size.Width, bitmap.Size.Height, bitmap.PixelFormat));
        }

        public static SpriteSheet FromFile(string path)
        {
            Bitmap bitmap = Bitmap.FromFile(path) as Bitmap;
            return new SpriteSheet(bitmap);
        }

        public IEnumerable<Bitmap> Split(int frameHeight)
        {
            // Determine size/number of frames
            var frameWidth = bitmap.Size.Width;
            var numFrames = (bitmap.Size.Height / frameHeight);
            Trace.WriteLine(string.Format("Splitting in frames of {0}x{1} would result in {2} frames", frameWidth, frameHeight, numFrames));
            if (bitmap.Size.Height % numFrames > 0)
            {
                Trace.WriteLine(string.Format("Warning: would leave {0} rows of pixels", (bitmap.Size.Height % numFrames)));
            }

            // Split the spritesheet
            for (int i = 0; i < numFrames; i++)
            {
                var x = 0;
                var y = i * frameHeight;
                var rect = new Rectangle(x, y, frameWidth, frameHeight);
                yield return bitmap.Clone(rect, bitmap.PixelFormat);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                    bitmap = null;
                }
            }
        }
    }
}
