using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace СпортТовары
{
    public partial class ManagerWindow : Window
    {
        private readonly Спортивные_товарыEntities _context;
        private ObservableCollection<Product> _products;

        public ManagerWindow()
        {
            InitializeComponent();

            try
            {
                _context = new Спортивные_товарыEntities();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void LoadProducts()
        {
            try
            {
                var allProducts = _context.Product.ToList();
                _products = new ObservableCollection<Product>(allProducts);
                DGproduct.ItemsSource = _products;
                UpdateRecordCount(_products.Count, allProducts.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(query) || query == "Введите запрос для поиска")
            {
                MessageBox.Show("Введите поисковый запрос.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var lowerQuery = query.ToLower();
                var filteredProducts = _context.Product
                    .Where(p => p.ProductName.ToLower().Contains(lowerQuery) ||
                                p.ProductDescription.ToLower().Contains(lowerQuery) ||
                                p.ProductManufacturer.ToLower().Contains(lowerQuery))
                    .ToList();

                if (filteredProducts.Any())
                {
                    _products = new ObservableCollection<Product>(filteredProducts);
                    DGproduct.ItemsSource = _products;
                }
                else
                {
                    MessageBox.Show("Товары по заданному запросу не найдены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    DGproduct.ItemsSource = new ObservableCollection<Product>();
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
