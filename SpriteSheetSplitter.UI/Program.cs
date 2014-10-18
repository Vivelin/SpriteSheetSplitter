using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteSheetSplitter.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            SpriteSheetForm mainForm;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                var path = args[0];
                mainForm = new SpriteSheetForm(path);
            }
            else
            {
                mainForm = new SpriteSheetForm();
            }

            Application.Run(mainForm);
        }
    }
}
