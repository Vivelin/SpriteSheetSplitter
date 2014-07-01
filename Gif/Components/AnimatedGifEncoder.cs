using System;
using System.Drawing;
using System.IO;

#region Original disclaimer
/**
 * Class AnimatedGifEncoder - Encodes a GIF file consisting of one or
 * more frames.
 * <pre>
 * Example:
 *    AnimatedGifEncoder e = new AnimatedGifEncoder();
 *    e.start(outputFileName);
 *    e.setDelay(1000);   // 1 frame per sec
 *    e.addFrame(image1);
 *    e.addFrame(image2);
 *    e.finish();
 * </pre>
 * No copyright asserted on the source code of this class.  May be used
 * for any purpose, however, refer to the Unisys LZW patent for restrictions
 * on use of the associated LZWEncoder class.  Please forward any corrections
 * to kweiner@fmsware.com.
 *
 * @author Kevin Weiner, FM Software
 * @version 1.03 November 2003
 *
 */
#endregion

namespace Gif.Components
{
    /// <summary>
    /// Encodes a GIF file consisting of one or more frames.
    /// </summary>
    public class AnimatedGifEncoder
    {
        protected int width; // image size
        protected int height;
        protected Color transparent = Color.Empty; // transparent color if given
        protected int transIndex; // transparent index in color table
        protected int repeat = -1; // no repeat
        protected int delay = 0; // frame delay (hundredths of a second)
        protected bool started = false; // ready to output frames
        //	protected BinaryWriter bw;
        protected FileStream fs;

        protected Image image; // current frame
        protected byte[] pixels; // BGR byte array from frame
        protected byte[] indexedPixels; // converted frame indexed to palette
        protected int colorDepth; // number of bit planes
        protected byte[] colorTab; // RGB palette
        protected bool[] usedEntry = new bool[256]; // active palette entries
        protected int palSize = 7; // color table size (bits-1)
        protected int dispose = -1; // disposal code (-1 = use default)
        protected bool closeStream = false; // close stream when finished
        protected bool firstFrame = true;
        protected bool sizeSet = false; // if false, get size from first frame
        protected int sample = 10; // default sample interval for quantizer

        /// <summary>
        /// Gets or sets the delay time in milliseconds between each frame for the last frame 
        /// added and any subsequent frames.
        /// </summary>
        public int Delay
        {
            get { return (int)(delay * 10.0f); }
            set { delay = (int)Math.Round(value / 10.0f); }
        }

        /// <summary>
        /// Gets or sets frame rate in frames per second. Equivalent to setting <c>Delay</c> to 
        /// <c>1000/fps</c>.
        /// </summary>
        public float FrameRate
        {
            get { return (float)(delay / 100f); }
            set
            {
                if (value > 0)
                {
                    delay = (int)Math.Round(100f / value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the way in which the last frame (any subsequent frames) are to be treated 
        /// after being displayed. By default, if a transparent color has been set, it will restore
        /// to the transparent color, otherwise, none is specified.
        /// </summary>
        public DisposalMethods DisposalMethod
        {
            get { return (DisposalMethods)dispose; }
            set { dispose = (int)value; }
        }

        /// <summary>
        /// Sets the number of times the set of GIF frames
        /// should be played.  Default is 1; 0 means play
        /// indefinitely.  Must be invoked before the first
        /// image is added.
        /// </summary>
        public int Repeat
        {
            get { return repeat; }
            set { repeat = value; }
        }

        /// <summary>
        /// Gets or sets the color that will be rendered as transparent for the last added frame 
        /// and any subsequent frames. Use <c>null</c> to indicate no transparent color.
        /// </summary>
        public Color TransparentColor
        {
            get { return transparent; }
            set { transparent = value; }
        }

        /// <summary>
        /// Gets or sets quality of color quantization (conversion of images to the maximum 256 
        /// colors allowed by the GIF specification). Lower values (minimum = 1) produce better 
        /// colors, but slow processing significantly. 10 is the default, and produces good color 
        /// mapping at reasonable speeds. Values greater than 20 do not yield significant 
        /// improvements in speed.
        /// </summary>
        public int Quality
        {
            get { return sample; }
            set
            {
                if (value < 1)
                    sample = 1;
                sample = value;
            }
        }

        /// <summary>
        /// Gets or sets the GIF frame size. The default size is the size of the first frame added
        /// if this property is not set.
        /// </summary>
        public Size Size
        {
            get { return new Size(width, height); }
            set { SetSize(value.Width, value.Height); }
        }

        /// <summary>
        /// Adds next GIF frame.
        /// </summary>
        /// <param name="im">The <see cref="System.Drawing.Image"/> containing frame to write.</param>
        /// <returns>True if successful.</returns>
        /// <remarks>
        /// The frame is not written immediately, but is actually deferred until the next frame is 
        /// received so that timing data can be inserted. Invoking <c>Finish()</c> flushes all 
        /// frames. If <c>Size</c> was not set, the size of the first frame is used for all 
        /// subsequent frames.
        /// </remarks>
        public bool AddFrame(Image im)
        {
            if ((im == null) || !started)
            {
                return false;
            }
            bool ok = true;
            try
            {
                if (!sizeSet)
                {
                    // use first frame's size
                    SetSize(im.Width, im.Height);
                }
                image = im;
                GetImagePixels(); // convert to correct format if necessary
                AnalyzePixels(); // build color table & map pixels
                if (firstFrame)
                {
                    WriteLSD(); // logical screen descriptior
                    WritePalette(); // global color table
                    if (repeat >= 0)
                    {
                        // use NS app extension to indicate reps
                        WriteNetscapeExt();
                    }
                }
                WriteGraphicCtrlExt(); // write graphic control extension
                WriteImageDesc(); // image descriptor
                if (!firstFrame)
                {
                    WritePalette(); // local color table
                }
                WritePixels(); // encode and write pixel data
                firstFrame = false;
            }
            catch (IOException e)
            {
                ok = false;
            }

            return ok;
        }

        /// <summary>
        /// Flushes any pending data and closes output file.
        /// If writing to an OutputStream, the stream is not
        /// closed.
        /// </summary>
        public bool Finish()
        {
            if (!started) return false;
            bool ok = true;
            started = false;
            try
            {
                fs.WriteByte(0x3b); // gif trailer
                fs.Flush();
                if (closeStream)
                {
                    fs.Close();
                }
            }
            catch (IOException e)
            {
                ok = false;
            }

            // reset for subsequent use
            transIndex = 0;
            fs = null;
            image = null;
            pixels = null;
            indexedPixels = null;
            colorTab = null;
            closeStream = false;
            firstFrame = true;

            return ok;
        }

        /// <summary>
        /// Sets the GIF frame size. The default size is the size of the first frame added if this 
        /// method is not invoked.
        /// </summary>
        /// <param name="w">Integer value indicating the width of the GIF in pixels.</param>
        /// <param name="h">Integer value indicating the height of the GIF in pixels.</param>
        public void SetSize(int w, int h)
        {
            if (started && !firstFrame) return;
            width = w;
            height = h;
            if (width < 1) width = 320;
            if (height < 1) height = 240;
            sizeSet = true;
        }

        /// <summary>
        /// Initiates GIF file creation on the given stream.  The stream
        /// is not closed automatically.
        /// </summary>
        /// <param name="os">OutputStream on which GIF images are written.</param>
        /// <returns>false if initial write failed.</returns>
        public bool Start(FileStream os)
        {
            if (os == null) return false;
            bool ok = true;
            closeStream = false;
            fs = os;
            try
            {
                WriteString("GIF89a"); // header
            }
            catch (IOException e)
            {
                ok = false;
            }
            return started = ok;
        }

        /// <summary>
        /// Initiates writing of a GIF file with the specified name.
        /// </summary>
        /// <param name="file">String containing output file name.</param>
        /// <returns>false if open or initial write failed.</returns>
        public bool Start(String file)
        {
            bool ok = true;
            try
            {
                //			bw = new BinaryWriter( new FileStream( file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None ) );
                fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                ok = Start(fs);
                closeStream = true;
            }
            catch (IOException e)
            {
                ok = false;
            }
            return started = ok;
        }

        /// <summary>
        /// Analyzes image colors and creates color map.
        /// </summary>
        protected void AnalyzePixels()
        {
            int len = pixels.Length;
            int nPix = len / 3;
            indexedPixels = new byte[nPix];
            NeuQuant nq = new NeuQuant(pixels, len, sample);
            // initialize quantizer
            colorTab = nq.Process(); // create reduced palette
            // convert map from BGR to RGB
            //			for (int i = 0; i < colorTab.Length; i += 3) 
            //			{
            //				byte temp = colorTab[i];
            //				colorTab[i] = colorTab[i + 2];
            //				colorTab[i + 2] = temp;
            //				usedEntry[i / 3] = false;
            //			}
            // map image pixels to new palette
            int k = 0;
            for (int i = 0; i < nPix; i++)
            {
                int index =
                    nq.Map(pixels[k++] & 0xff,
                    pixels[k++] & 0xff,
                    pixels[k++] & 0xff);
                usedEntry[index] = true;
                indexedPixels[i] = (byte)index;
            }
            pixels = null;
            colorDepth = 8;
            palSize = 7;
            // get closest match to transparent color if specified
            if (transparent != Color.Empty)
            {
                //transIndex = FindClosest(transparent);
                transIndex = nq.Map(transparent.B, transparent.G, transparent.R);
            }
        }

        /// <summary>
        /// Returns index of palette color closest to c
        /// </summary>
        protected int FindClosest(Color c)
        {
            if (colorTab == null) return -1;
            int r = c.R;
            int g = c.G;
            int b = c.B;
            int minpos = 0;
            int dmin = 256 * 256 * 256;
            int len = colorTab.Length;
            for (int i = 0; i < len; )
            {
                int dr = r - (colorTab[i++] & 0xff);
                int dg = g - (colorTab[i++] & 0xff);
                int db = b - (colorTab[i] & 0xff);
                int d = dr * dr + dg * dg + db * db;
                int index = i / 3;
                if (usedEntry[index] && (d < dmin))
                {
                    dmin = d;
                    minpos = index;
                }
                i++;
            }
            return minpos;
        }

        /// <summary>
        /// Extracts image pixels into byte array "pixels"
        /// </summary>
        protected void GetImagePixels()
        {
            int w = image.Width;
            int h = image.Height;
            //		int type = image.GetType().;
            if ((w != width)
                || (h != height)
                )
            {
                // create new image with right size/format
                Image temp =
                    new Bitmap(width, height);
                Graphics g = Graphics.FromImage(temp);
                g.DrawImage(image, 0, 0);
                image = temp;
                g.Dispose();
            }
            /*
                ToDo:
                improve performance: use unsafe code 
            */
            pixels = new Byte[3 * image.Width * image.Height];
            int count = 0;
            Bitmap tempBitmap = new Bitmap(image);
            for (int th = 0; th < image.Height; th++)
            {
                for (int tw = 0; tw < image.Width; tw++)
                {
                    Color color = tempBitmap.GetPixel(tw, th);
                    pixels[count] = color.R;
                    count++;
                    pixels[count] = color.G;
                    count++;
                    pixels[count] = color.B;
                    count++;
                }
            }

            //		pixels = ((DataBufferByte) image.getRaster().getDataBuffer()).getData();
        }

        /// <summary>
        /// Writes Graphic Control Extension
        /// </summary>
        protected void WriteGraphicCtrlExt()
        {
            fs.WriteByte(0x21); // extension introducer
            fs.WriteByte(0xf9); // GCE label
            fs.WriteByte(4); // data block size
            int transp, disp;
            if (transparent == Color.Empty)
            {
                transp = 0;
                disp = 0; // dispose = no action
            }
            else
            {
                transp = 1;
                disp = 2; // force clear if using transparent color
            }
            if (dispose >= 0)
            {
                disp = dispose & 7; // user override
            }
            disp <<= 2;

            // packed fields
            fs.WriteByte(Convert.ToByte(0 | // 1:3 reserved
                disp | // 4:6 disposal
                0 | // 7   user input - 0 = none
                transp)); // 8   transparency flag

            WriteShort(delay); // delay x 1/100 sec
            fs.WriteByte(Convert.ToByte(transIndex)); // transparent color index
            fs.WriteByte(0); // block terminator
        }

        /// <summary>
        /// Writes Image Descriptor
        /// </summary>
        protected void WriteImageDesc()
        {
            fs.WriteByte(0x2c); // image separator
            WriteShort(0); // image position x,y = 0,0
            WriteShort(0);
            WriteShort(width); // image size
            WriteShort(height);
            // packed fields
            if (firstFrame)
            {
                // no LCT  - GCT is used for first (or only) frame
                fs.WriteByte(0);
            }
            else
            {
                // specify normal LCT
                fs.WriteByte(Convert.ToByte(0x80 | // 1 local color table  1=yes
                    0 | // 2 interlace - 0=no
                    0 | // 3 sorted - 0=no
                    0 | // 4-5 reserved
                    palSize)); // 6-8 size of color table
            }
        }

        /// <summary>
        /// Writes Logical Screen Descriptor
        /// </summary>
        protected void WriteLSD()
        {
            // logical screen size
            WriteShort(width);
            WriteShort(height);
            // packed fields
            fs.WriteByte(Convert.ToByte(0x80 | // 1   : global color table flag = 1 (gct used)
                0x70 | // 2-4 : color resolution = 7
                0x00 | // 5   : gct sort flag = 0
                palSize)); // 6-8 : gct size

            fs.WriteByte(0); // background color index
            fs.WriteByte(0); // pixel aspect ratio - assume 1:1
        }

        /// <summary>
        /// Writes Netscape application extension to define
        /// repeat count.
        /// </summary>
        protected void WriteNetscapeExt()
        {
            fs.WriteByte(0x21); // extension introducer
            fs.WriteByte(0xff); // app extension label
            fs.WriteByte(11); // block size
            WriteString("NETSCAPE" + "2.0"); // app id + auth code
            fs.WriteByte(3); // sub-block size
            fs.WriteByte(1); // loop sub-block id
            WriteShort(repeat); // loop count (extra iterations, 0=repeat forever)
            fs.WriteByte(0); // block terminator
        }

        /// <summary>
        /// Writes color table
        /// </summary>
        protected void WritePalette()
        {
            fs.Write(colorTab, 0, colorTab.Length);
            int n = (3 * 256) - colorTab.Length;
            for (int i = 0; i < n; i++)
            {
                fs.WriteByte(0);
            }
        }

        /// <summary>
        /// Encodes and writes pixel data
        /// </summary>
        protected void WritePixels()
        {
            LZWEncoder encoder =
                new LZWEncoder(width, height, indexedPixels, colorDepth);
            encoder.Encode(fs);
        }

        /// <summary>
        /// Write 16-bit value to output stream, LSB first
        /// </summary>
        protected void WriteShort(int value)
        {
            fs.WriteByte(Convert.ToByte(value & 0xff));
            fs.WriteByte(Convert.ToByte((value >> 8) & 0xff));
        }

        /// <summary>
        /// Writes string to output stream
        /// </summary>
        protected void WriteString(String s)
        {
            char[] chars = s.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                fs.WriteByte((byte)chars[i]);
            }
        }

        /// <summary>
        /// Represents ways in which the graphic can be treated after being displayed.
        /// </summary>
        public enum DisposalMethods
        {
            /// <summary>
            /// No disposal specified. The decoder is not required to take any action.
            /// </summary>
            None = 0,

            /// <summary>
            /// Indicates that the graphic is to be left in place.
            /// </summary>
            DoNotDispose = 1,

            /// <summary>
            /// Indicates that the area used by the graphic must be restored to the background 
            /// color.
            /// </summary>
            RestoreToBackgroundColor = 2,

            /// <summary>
            /// Indicates that the decoder is requried to restore the area overwritten by the 
            /// graphic with what was there prior to rendering the graphic.
            /// </summary>
            RestoreToPrevious = 3
        }
    }

}