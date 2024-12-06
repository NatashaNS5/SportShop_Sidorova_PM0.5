using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace СпортТовары
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private readonly Спортивные_товарыEntities _context;

        public ClientWindow()
        {
            InitializeComponent();
            _context = new Спортивные_товарыEntities();

            if (_context == null)
            {
                MessageBox.Show("Ошибка подключения к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
            else
            {
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            try
            {
                var allProducts = _context.Product.ToList();
                DGproduct.ItemsSource = allProducts;
                UpdateRecordCount(allProducts.Count, allProducts.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text;

            if (string.IsNullOrWhiteSpace(query) || query == "Введите запрос для поиска")
            {
                MessageBox.Show("Введите поисковый запрос.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var filteredProducts = _context.Product
                    .Where(p => p.ProductName.Contains(query) ||
                                p.ProductDescription.Contains(query) ||
                                p.ProductManufacturer.Contains(query))
                    .ToList();

                if (filteredProducts.Any())
                {
                    DGproduct.ItemsSource = filteredProducts;
                }
                else
                {
                    MessageBox.Show("Товары по заданному запросу не найдены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    DGproduct.ItemsSource = new List<Product>();
                }

                UpdateRecordCount(filteredProducts.Count, _context.Product.Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "Введите запрос для поиска";
            SearchBox.Foreground = Brushes.Gray;
            LoadProducts();
        }

        private void UpdateRecordCount(int displayedCount, int totalCount)
        {
            RecordCountText.Text = $"{displayedCount} из {totalCount}";
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "Введите запрос для поиска")
            {
                SearchBox.Text = string.Empty;
                SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Введите запрос для поиска";
                SearchBox.Foreground = Brushes.Gray;
            }
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = DGproduct.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите продукт для заказа.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var editWindow = new Order(selectedProduct);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии окна заказа: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NavigateToLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
        }

        private void CurrentTabWarning_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже находитесь в этой вкладке.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
