using System;
using System.Drawing;

namespace SpriteSheetSplitter
{
    /// <summary>
    /// Represents padding space on all sides of a rectangle.
    /// </summary>
    public struct Padding
    {
        /// <summary>
        /// Represents a <see cref="Padding"/> structure with its properties 
        /// left uninitialized.
        /// </summary>
        public static readonly Padding Empty = default(Padding);

        private int top;
        private int right;
        private int bottom;
        private int left;

        /// <summary>
        /// Initializes a new instance of the <see cref="Padding"/> structure,
        /// specifying a single value for all four sides.
        /// </summary>
        /// <param name="padding">The padding on all sides.</param>
        public Padding(int padding)
            : this(padding, padding, padding, padding) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Padding"/> structure,
        /// specifying a horizontal and vertical padding.
        /// </summary>
        /// <param name="vertical">The top and bottom padding.</param>
        /// <param name="horizontal">The left and right padding.</param>
        public Padding(int vertical, int horizontal)
            : this(vertical, horizontal, vertical, horizontal) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Padding"/> structure,
        /// specifying top, bottom and horizontal padding.
        /// </summary>
        /// <param name="top">The top padding.</param>
        /// <param name="horizontal">The left and right padding.</param>
        /// <param name="bottom">The bottom padding.</param>
        public Padding(int top, int horizontal, int bottom)
            : this(top, horizontal, bottom, horizontal) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Padding"/> structure,
        /// specifiying all four values.
        /// </summary>
        /// <param name="top">The top padding.</param>
        /// <param name="right">The right padding.</param>
        /// <param name="bottom">The bottom padding.</param>
        /// <param name="left">The left padding.</param>
        public Padding(int top, int right, int bottom, int left)
        {
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;
        }

        /// <summary>
        /// Gets the top component of the <see cref="Padding"/> structure.
        /// </summary>
        public int Top
        {
            get { return top; }
        }

        /// <summary>
        /// Gets the right component of the <see cref="Padding"/> structure.
        /// </summary>
        public int Right
        {
            get { return right; }
        }

        /// <summary>
        /// Gets the bottom component of the <see cref="Padding"/> structure.
        /// </summary>
        public int Bottom
        {
            get { return bottom; }
        }

        /// <summary>
        /// Gets the left component of the <see cref="Padding"/> structure.
        /// </summary>
        public int Left
        {
            get { return left; }
        }

        /// <summary>
        /// Gets the combined amount of horizontal padding.
        /// </summary>
        public int Horizontal
        {
            get { return left + right; }
        }

        /// <summary>
        /// Gets the combined amount of vertical padding.
        /// </summary>
        public int Vertical
        {
            get { return top + bottom; }
        }

        /// <summary>
        /// Tests whether two <see cref="Padding"/> structures have equal 
        /// padding on all sides.
        /// </summary>
        /// <param name="left">The <see cref="Padding"/> structure that is on 
        /// the left of the equality operator.</param>
        /// <param name="right">The <see cref="Padding"/> structure that is on 
        /// the right of the equality operator.</param>
        /// <returns><c>true</c> if the two <see cref="Padding"/> structures
        /// have equal padding on all sides, <c>false</c> otherwise.</returns>
        public static bool operator ==(Padding left, Padding right)
        {
            return left.Top == right.Top && left.Right == right.Right &&
                left.Bottom == right.Bottom && left.Left == right.Left;
        }

        /// <summary>
        /// Tests whether two <see cref="Padding"/> structures differ in 
        /// padding on all sides.
        /// </summary>
        /// <param name="left">The <see cref="Padding"/> structure that is on 
        /// the left of the inequality operator.</param>
        /// <param name="right">The <see cref="Padding"/> structure that is on 
        /// the right of the inequality operator.</param>
        /// <returns><c>true</c> if the two <see cref="Padding"/> structures
        /// differ on any side, <c>false</c> otherwise.</returns>
        public static bool operator !=(Padding left, Padding right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Adds the padding of a <see cref="Padding"/> structure to the width 
        /// and height of a <see cref="Size"/> structure.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to add.</param>
        /// <param name="padding">The <see cref="Padding"/> to add.</param>
        /// <returns>A <see cref="Size"/> structure that is the result of the
        /// addition operator.</returns>
        public static Size operator +(Size size, Padding padding)
        {
            return new Size(size.Width + padding.Horizontal,
                size.Height + padding.Vertical);
        }

        /// <summary>
        /// Adds the padding of a <see cref="Padding"/> structure to the 
        /// dimensions of a <see cref="Rectangle"/> structure.
        /// </summary>
        /// <param name="rect">The <see cref="Rectangle"/> to add.</param>
        /// <param name="padding">The <see cref="Padding"/> to add.</param>
        /// <returns>A <see cref="Rectangle"/> structure that is the result of 
        /// the addition operator.</returns>
        public static Rectangle operator +(Rectangle rect, Padding padding)
        {
            return Rectangle.FromLTRB(rect.Left + padding.Left,
                rect.Top + padding.Top, rect.Right + padding.Right,
                rect.Bottom + padding.Bottom);
        }

        /// <summary>
        /// Test whether <paramref name="obj"/> is a <see cref="Padding"/> 
        /// structure with the same properties of this <see cref="Padding"/>
        /// structure.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to test.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is a <see 
        /// cref="Padding"/> structure and equal to this <see cref="Padding"/>
        /// structure, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Padding))
                return false;

            var padding = (Padding)obj;
            return padding == this;
        }

        /// <summary>
        /// Calculates and retrieves a hash code based on the top, right, 
        /// bottom and left margins.
        /// </summary>
        /// <returns>A hash code based on the top, right, bottom and left 
        /// margins.</returns>
        public override int GetHashCode()
        {
            uint num = (uint)this.Top;
            uint num2 = (uint)this.Right;
            uint num3 = (uint)this.Bottom;
            uint num4 = (uint)this.Left;
            return (int)(num ^ (num2 << 13 | num2 >> 19) 
                ^ (num3 << 26 | num3 >> 6) ^ (num4 << 7 | num4 >> 25));
        }

        /// <summary>
        /// Returns a string representation of this <see cref="Padding"/> 
        /// structure.
        /// </summary>
        /// <returns>A string containing the top, right, bottom and left 
        /// components of this <see cref="Padding"/> structure.</returns>
        public override string ToString()
        {
            return string.Format("{{Top={0}, Right={1}, Bottom={2}, Left={3}}}",
                Top, Left, Bottom, Right);
        }
    }
}
