using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteSheetSplitter.UI.Controls
{
    /// <summary>
    /// Renders <see cref="ToolStrip"/> objects using system colors and a flat
    /// style, but without the stupid border.
    /// </summary>
    public class BorderlessToolStripRenderer : ToolStripSystemRenderer
    {
        /// <summary>
        /// Doesn't do anything.
        /// </summary>
        /// <param name="e">An object that contains the event data.</param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is MenuStrip)
            {
                // Don't call the base OnRenderToolStripBorder for MenuStrip
                // because it paints a white line underneath it for no reason.
            }
            else
            {
                // ...but still draw the border around menu items
                base.OnRenderToolStripBorder(e);
            }
        }
    }
}
