using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SpriteSheetSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener(true));

            var fileName = args.Length > 0 ? args[0] : null;
            var frameHeight = args.Length > 1 ? int.Parse(args[1]) : 16;
            var scaleFactor = args.Length > 2 ? float.Parse(args[2]) : 3.0f;
            var delay = args.Length > 3 ? int.Parse(args[3]) : 8;

            if (System.IO.File.Exists(fileName))
            {
                var output = System.IO.Path.GetFileNameWithoutExtension(fileName) + ".gif";
                using (var spritesheet = SpriteSheet.FromFile(fileName))
                {
                    var framenum = 0;
                    var frames = spritesheet.Split(frameHeight);

                    var encoder = new Gif.Components.AnimatedGifEncoder();
                    encoder.Start(output);
                    encoder.Delay = delay;
                    encoder.TransparentColor = System.Drawing.Color.Black;
                    encoder.Repeat = 0;
                    foreach (var item in frames)
                    {
                        var name = string.Format("{0:0000}.png", framenum);
                        using (var scaled = Effects.Scale(item, scaleFactor))
                        {
                            Trace.WriteLine(string.Format("Encoding frame {0}", framenum + 1));
                            scaled.Save(name, System.Drawing.Imaging.ImageFormat.Png);
                            encoder.AddFrame(scaled);
                        }

                        item.Dispose();
                        framenum++;
                    }

                    encoder.Finish();
                    Trace.WriteLine("Done.");
                }
            }
        }
    }
}
