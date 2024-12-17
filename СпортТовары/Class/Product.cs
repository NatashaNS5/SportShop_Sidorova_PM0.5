using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace СпортТовары.Class
{
    public class Product
    {
        public string ProductPhotoPath { get; set; }

        public BitmapImage ProductPhoto
        {
            get
            {
                if (string.IsNullOrEmpty(ProductPhotoPath)) return null;

                try
                {
                    var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ProductPhotoPath);

                    if (File.Exists(imagePath))
                    {
                        return new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке изображения: {ex.Message}");
                }

                return null; 
            }
        }
    }
}
