using System.Drawing;

namespace SpriteSheetSplitter.Transformations
{
    /// <summary>
    /// Represents a transformation that applies padding to an image.
    /// </summary>
    public class PaddingTransformation : Transformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaddingTransformation"/>
        /// class using the padding information specified in the <see 
        /// cref="Padding"/> structure.
        /// </summary>
        /// <param name="padding">A <see cref="Padding"/> structure specifying
        /// the padding to add on each side.</param>
        public PaddingTransformation(Padding padding)
        {
            Padding = padding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaddingTransformation"/>
        /// class, specifying all sides of the padding.
        /// </summary>
        /// <param name="top">The amount of pixels to add to the left.</param>
        /// <param name="right">The amount of pixels to add to the right.</param>
        /// <param name="bottom">The amount of pixels to add to the bottom.</param>
        /// <param name="left">The amount of pixels to add to the left.</param>
        public PaddingTransformation(int top, int right, int bottom, int left)
        {
            Padding = new Padding(top, right, bottom, left);
        }

        /// <summary>
        /// Gets a <see cref="Padding"/> structure containing the amount of 
        /// pixels to add on each side.
        /// </summary>
        public Padding Padding { get; protected set; }

        /// <summary>
        /// Adds an amount of pixels to each side of the image.
        /// </summary>
        /// <param name="image">The image to pad.</param>
        public override void ApplyTo(ref System.Drawing.Image image)
        {
            var location = new Point(Padding.Left, Padding.Top);
            var size = image.Size + Padding;
            var target = new Rectangle(location, image.Size);
            image = Redraw(image, size, target);
        }

        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        /// <returns>A string indicating the type of transformation and its 
        /// properties.</returns>
        public override string ToString()
        {
            return string.Format("{{Pad, {0}}}", Padding);
        }
    }
}
