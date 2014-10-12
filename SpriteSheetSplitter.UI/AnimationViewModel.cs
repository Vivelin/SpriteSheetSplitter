using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpriteSheetSplitter.UI
{
    /// <summary>
    /// Represents the view model of an <see cref="Animation"/>.
    /// </summary>
    public class AnimationViewModel : INotifyPropertyChanged
    {
        private Animation animation;
        private BitmapImage spriteSheetImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationViewModel"/>
        /// class using dummy data.
        /// </summary>
        public AnimationViewModel()
        {
            var path = @"D:\Steam Games\SteamApps\common\Crypt of the NecroDancer\data\entities\slime_green.png";
            var spriteSheet = SpriteSheet.FromFile(path, new System.Drawing.Size(26, 26));
            this.animation = new Animation(spriteSheet);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationViewModel"/>
        /// class for the specified <see cref="Animation"/>.
        /// </summary>
        /// <param name="animation">The <see cref="Animation"/> to use in the
        /// view model.</param>
        public AnimationViewModel(Animation animation)
        {
            this.animation = animation;
        }

        /// <summary>
        /// Occurs when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets a <see cref="BitmapImage"/> representing the sprite sheet 
        /// image.
        /// </summary>
        public BitmapImage SpriteSheetImage
        {
            get
            {
                if (spriteSheetImage == null)
                {
                    // fml
                    using (var stream = new System.IO.MemoryStream())
                    {
                        animation.SpriteSheet.Bitmap.Save(stream, 
                            System.Drawing.Imaging.ImageFormat.Png);
                        stream.Position = 0;

                        spriteSheetImage = new BitmapImage();
                        spriteSheetImage.BeginInit();
                        spriteSheetImage.StreamSource = stream;
                        spriteSheetImage.CacheOption = BitmapCacheOption.OnLoad;
                        spriteSheetImage.EndInit();
                    }
                }
                return spriteSheetImage;
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that has 
        /// changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                handler(this, args);
            }
        }
    }
}
