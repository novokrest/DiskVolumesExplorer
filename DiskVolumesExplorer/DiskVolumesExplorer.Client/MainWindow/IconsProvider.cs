using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace DiskVolumesExplorer.Client
{
    enum IconType
    {
        VolumeIcon
    }

    class IconsProvider
    {
        private static readonly Uri BaseUri = new Uri("pack://application:,,,/DiskVolumesExplorer.Client;component/Images/");
        private static readonly Uri VolumeIconUri = new Uri(BaseUri, "Volume3.png");

        private static readonly Dictionary<IconType, Uri> IconUris = new Dictionary<IconType, Uri>()
        {
            {IconType.VolumeIcon, VolumeIconUri}
        };

        private static readonly Dictionary<IconType, BitmapImage> Cache = new Dictionary<IconType, BitmapImage>();

        public static BitmapImage GetIcon(IconType iconType)
        {
            if (!Cache.ContainsKey(iconType))
            {
                Uri source = IconUris[iconType];
                BitmapImage image = new BitmapImage(source);
                //image.BeginInit();
                //image.UriSource = source;
                //image.DecodePixelHeight = 30;
                //image.DecodePixelWidth = 30;
                //image.EndInit();
                Cache.Add(iconType, image);
            }
            return Cache[iconType];
        }
    }
}
