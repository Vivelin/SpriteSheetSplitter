using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SpriteSheetSplitter
{
    class Program
    {
        private static bool outputIndividualFrames = true;

        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener(true));

            var fileName = args.Length > 0 ? args[0] : null;
            var frameHeight = args.Length > 1 ? int.Parse(args[1]) : 16;
            var scaleFactor = args.Length > 2 ? float.Parse(args[2]) : 3.0f;
            var delay = args.Length > 3 ? int.Parse(args[3]) : 120;

            var tileSize = new System.Drawing.Size(frameHeight, frameHeight);

            if (System.IO.File.Exists(fileName))
            {
                var output = System.IO.Path.GetFileNameWithoutExtension(fileName) + ".gif";
                using (var spritesheet = SpriteSheet.FromFile(fileName, tileSize))
                {
                    var framenum = 0;
                    var frames = spritesheet.Split();

                    using (var encoder = new Gif.Components.AnimatedGifEncoder(output))
                    {
                        encoder.Delay = delay;
                        encoder.TransparentColor = System.Drawing.Color.Black;
                        encoder.Repeat = 0;

                        Trace.WriteLine(string.Format("Framerate set to {0:F1} FPS ({1} ms delay)", encoder.FrameRate, encoder.Delay));

                        foreach (var item in frames)
                        {
                            var name = string.Format("{0:0000}.png", framenum);
                            using (var scaled = Effects.Scale(item, scaleFactor))
                            {
                                if (outputIndividualFrames)
                                {
                                    scaled.Save(name, System.Drawing.Imaging.ImageFormat.Png);
                                }

                                encoder.AddFrame(scaled);
                            }

                            item.Dispose();
                            framenum++;
                        }

                    }
                }

                Trace.WriteLine("Done.");
            }
        }
    }
}
