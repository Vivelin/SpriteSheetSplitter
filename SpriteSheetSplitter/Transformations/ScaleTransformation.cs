using System.Drawing;

namespace SpriteSheetSplitter.Transformations
{
    /// <summary>
    /// Represents a transformation that scales images using the 
    /// nearest-neighbor algorithm.
    /// </summary>
    public class ScaleTransformation : Transformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleTransformation"/>
        /// class for the specified factor.
        /// </summary>
        /// <param name="factor">A floating point value indicating the factor
        /// by which images are multiplied.</param>
        public ScaleTransformation(float factor)
        {
            Factor = factor;
        }

        /// <summary>
        /// Gets the factor by which images are multiplied.
        /// </summary>
        public float Factor { get; protected set; }

        /// <summary>
        /// Scales the specified <see cref="Image"/> using the nearest-neighbor
        /// algorith.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> to scale.</param>
        public override void ApplyTo(ref Image image)
        {
            var location = new Point(0, 0);
            var size = new Size((int)(image.Width * Factor),
                (int)(image.Height * Factor));
            var rect = new Rectangle(location, size);

            image = Redraw(image, size, rect);
        }

        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        /// <returns>A string indicating the type of transformation and the 
        /// scaling factor.</returns>
        public override string ToString()
        {
            return string.Format("{{Scale by {0:G2}f}}", Factor);
        }
    }
}
