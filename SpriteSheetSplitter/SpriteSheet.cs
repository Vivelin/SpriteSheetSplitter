using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpriteSheetSplitter
{
    /// <summary>
    /// Represents an image consisting of multiple tiles or sprites.
    /// </summary>
    public class SpriteSheet : IDisposable
    {
        private Bitmap bitmap;
        private Size tileSize;

        /// <summary>
        /// Initializes a new instance of the SpriteSheet class for the specified bitmap using a specific tile size.
        /// </summary>
        /// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap"/> that contains the sprite sheet image.</param>
        /// <param name="tileSize">A <see cref="T:System.Drawing.Size"/> containing the dimensions of each individual tile.</param>
        public SpriteSheet(Bitmap bitmap, Size tileSize)
        {
            this.bitmap = bitmap;
            this.TileSize = tileSize;
        }

        /// <summary>
        /// Gets or sets the size of each sprite.
        /// </summary>
        public Size TileSize
        {
            get { return tileSize; }
            set
            {
                if (value != tileSize)
                {
                    tileSize = value;
                    OnTileSizeChanged(value);
                }
            }
        }

        /// <summary>
        /// Gets the number of rows of sprites contained in the sprite sheet.
        /// </summary>
        public int Rows
        {
            get { return bitmap.Height / TileSize.Height; }
        }

        /// <summary>
        /// Gets the number of columns of sprites contained in the sprite sheet.
        /// </summary>
        public int Columns
        {
            get { return bitmap.Width / TileSize.Width; }
        }

        /// <summary>
        /// Gets the number of sprites contained in the sprite sheet.
        /// </summary>
        public int Count
        {
            get { return Rows * Columns; }
        }

        /// <summary>
        /// Creates a SpriteSheet from the specified file using the specific tile size.
        /// </summary>
        /// <param name="path">The full path to the file to open.</param>
        /// <param name="tileSize">The size of each individual sprite in the sprite sheet.</param>
        /// <returns>A new SpriteSheet instance for the specified file.</returns>
        public static SpriteSheet FromFile(string path, Size tileSize)
        {
            Bitmap bitmap = Bitmap.FromFile(path) as Bitmap;
            return new SpriteSheet(bitmap, tileSize);
        }

        /// <summary>
        /// Returns the tile at the specified position.
        /// </summary>
        /// <param name="x">The index of the row that contains the tile to retrieve.</param>
        /// <param name="y">The index of the column that contains the tile to retrieve.</param>
        /// <returns>A new <see cref="T:System.Drawing.Bitmap"/> containing the specified tile.</returns>
        public Bitmap GetTile(int x, int y)
        {
            if (x >= Columns) throw new IndexOutOfRangeException("x cannot exceed the number of columns");
            if (y >= Rows) throw new IndexOutOfRangeException("y cannot exceed the number of rows");

            var origin = new Point(x * TileSize.Width, y * TileSize.Height);
            var rect = new Rectangle(origin, TileSize);
            return bitmap.Clone(rect, bitmap.PixelFormat);
        }

        /// <summary>
        /// Returns a collection of the sprites contained in the sprite sheet, enumerated row-by-row.
        /// </summary>
        /// <returns>A collection of bitmaps containing the sprites.</returns>
        public IEnumerable<Bitmap> Split()
        {
            return Split(TraverselOrder.RowMajor);
        }

        /// <summary>
        /// Returns a collection of the sprites contained in the sprite sheet, enumerated in the specified order.
        /// </summary>
        /// <param name="order">The order in which the sprites are enumerated.</param>
        /// <returns>A collection of bitmaps containing the sprites.</returns>
        public IEnumerable<Bitmap> Split(TraverselOrder order)
        {
            switch (order)
            {
                case TraverselOrder.RowMajor:
                    {
                        for (int y = 0; y < Rows; y++)
                        {
                            for (int x = 0; x < Columns; x++)
                            {
                                yield return GetTile(x, y);
                            }
                        }
                    }
                    break;
                case TraverselOrder.ColumnMajor:
                    {
                        for (int x = 0; x < Columns; x++)
                        {
                            for (int y = 0; y < Rows; y++)
                            {
                                yield return GetTile(x, y);
                            }
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("order", order.ToString() + " is not a supported traversal order.");
            }
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return string.Format("{{{0}x{1} tiles}}", Rows, Columns);
        }

        /// <summary>
        /// Releases any resources used by this class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Occurs when the tile size was changed.
        /// </summary>
        /// <param name="tileSize">The new tile size.</param>
        protected void OnTileSizeChanged(Size tileSize)
        {
            CheckMargin();
        }

        /// <summary>
        /// Determines whether splitting the sprite sheet would leave a margin of pixels at the 
        /// bottom and/or right side of the image.
        /// </summary>
        protected bool CheckMargin()
        {
            var marginBottom = bitmap.Height % TileSize.Height;
            var marginRight = bitmap.Width % TileSize.Width;
            var hasMargin = false;

            if (marginBottom > 0)
            {
                hasMargin = true;
                Trace.WriteLine(string.Format("Warning: would leave {0} row(s) of pixels", marginBottom));
            }

            if (marginRight > 0)
            {
                hasMargin = true;
                Trace.WriteLine(string.Format("Warning: would leave {0} column(s) of pixels", marginRight));
            }

            return hasMargin;
        }

        /// <summary>
        /// Releases the unmanaged resources and optionally the managed resources.
        /// </summary>
        /// <param name="disposing">True to release managed resources.</param>
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

        /// <summary>
        /// Represents methods for traversing the sprite sheet.
        /// </summary>
        public enum TraverselOrder
        {
            /// <summary>
            /// Indicates a row-by-row traversal of the sprite sheet.
            /// </summary>
            RowMajor,

            /// <summary>
            /// Indicates a column-by-column traversal of the sprite sheet.
            /// </summary>
            ColumnMajor
        }
    }
}
