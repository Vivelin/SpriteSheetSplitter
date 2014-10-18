using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteSheetSplitter.UI.Controls
{
    /// <summary>
    /// Used to group collections of controls.
    /// </summary>
    public class DoubleBufferedPanel : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleBufferedPanel"/>.
        /// </summary>
        public DoubleBufferedPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        /// <summary>
        /// Causes the control to be redrawn and raises the <see 
        /// cref="ScrollableControl.Scroll"/> event.
        /// </summary>
        /// <param name="se">A <see cref="ScrollEventArgs"/> that contains the
        /// event data.</param>
        protected override void OnScroll(ScrollEventArgs se)
        {
            Invalidate();

            base.OnScroll(se);
        }
    }
}
