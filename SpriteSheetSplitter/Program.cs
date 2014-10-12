using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SpriteSheetSplitter.Transformations;

namespace SpriteSheetSplitter
{
    class Program
    {
        private static bool outputIndividualFrames = true;

        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener(true));

            var fileName = args.Length > 0 ? args[0] : null;
            var frameWidth = args.Length > 1 ? int.Parse(args[1]) : 16;
            var frameHeight = args.Length > 2 ? int.Parse(args[2]) : 16;
            var scaleFactor = args.Length > 3 ? float.Parse(args[3]) : 3.0f;
            var delay = args.Length > 4 ? int.Parse(args[4]) : 80;

            var tileSize = new System.Drawing.Size(frameWidth, frameHeight);

            if (System.IO.File.Exists(fileName))
            {
                var output = System.IO.Path.GetFileNameWithoutExtension(fileName) + ".gif";
                using (var spritesheet = SpriteSheet.FromFile(fileName, tileSize))
                using (var anim = new Animation(spritesheet))
                {
                    anim.AddingFrame += (sender, e) =>
                    {
                        if (e.Index == 2)
                        {
                            e.Cancel = true;
                            return;
                        }

                        var name = string.Format("{0:0000}.png", e.Index);
                        var image = e.Frame;
                        var transformation = new ScaleTransformation(scaleFactor);

                        transformation.ApplyTo(ref image);
                        if (outputIndividualFrames)
                            image.Save(name, System.Drawing.Imaging.ImageFormat.Png);

                        e.Frame = image;
                    };

                    anim.Delay = delay;
                    anim.TransparentColor = System.Drawing.Color.Violet;
                    anim.Save(output);
                }

                Trace.WriteLine("Done.");
            }
        }
    }
}
