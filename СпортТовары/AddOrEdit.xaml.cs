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
    /// <summary>
    /// Логика взаимодействия для AddOrEdit.xaml
    /// </summary>
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
            DialogResult = true; 
            Close();
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
    }
}