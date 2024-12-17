using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace СпортТовары
{
    public partial class AddOrEdit : Window
    {
        public Product Product { get; private set; }

        public AddOrEdit(Product product)
        {
            InitializeComponent();

            Product = product ?? new Product();
            DataContext = Product;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveProductToDatabase();
                MessageBox.Show("Товар успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ComboBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void CurrentTabWarning_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже находитесь в этой вкладке.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void SaveProductToDatabase()
        {
            using (var context = new Спортивные_товарыEntities())
            {
                try
                {
                    var existingProduct = context.Product.FirstOrDefault(p => p.ProductArticleNumber == Product.ProductArticleNumber);

                    if (existingProduct == null)
                    {
                        context.Product.Add(Product);
                    }
                    else
                    {
                        existingProduct.ProductName = Product.ProductName;
                        existingProduct.ProductStatus = Product.ProductStatus;
                        existingProduct.ProductCategory = Product.ProductCategory;
                        existingProduct.ProductCost = Product.ProductCost;
                        existingProduct.ProductQuantityInStock = Product.ProductQuantityInStock;
                        existingProduct.ProductSupplier = Product.ProductSupplier;
                        existingProduct.ProductDescription = Product.ProductDescription;
                    }

                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var validationError in ex.EntityValidationErrors)
                    {
                        foreach (var error in validationError.ValidationErrors)
                        {
                            MessageBox.Show($"Ошибка: {error.ErrorMessage}, свойство: {error.PropertyName}",
                                            "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void ButtonSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFileDialog.FileName);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    if (bitmap.PixelWidth != 300 || bitmap.PixelHeight != 200)
                    {
                        MessageBox.Show("Размер изображения должен быть 300x200 пикселей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    Product.ProductPhoto = openFileDialog.FileName;

                    MessageBox.Show("Изображение успешно добавлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    var imageControl = this.FindName("ImageControlName") as Image;
                    if (imageControl != null)
                    {
                        imageControl.Source = new BitmapImage(new Uri(Product.ProductPhoto));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}