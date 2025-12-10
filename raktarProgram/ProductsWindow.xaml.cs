using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace raktarProgram
{
    public partial class ProductsWindow : Window
    {
        private List<Product> products = new List<Product>();

        public ProductsWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            products = Product.LoadFromCsv("products.csv");
            productsGrid.ItemsSource = products;
        }

        private void SaveProducts()
        {
            using (var sw = new StreamWriter("products.csv", false, Encoding.UTF8))
            {
                sw.WriteLine("id,name,price");
                foreach (var p in products.OrderBy(p => p.id))
                {
                    sw.WriteLine($"{p.id},{p.name},{p.price}");
                }
            }
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string idText = newProductIdBox.Text.Trim();
            string name = newProductNameBox.Text.Trim();
            string priceText = newProductPriceBox.Text.Trim();

            if (string.IsNullOrEmpty(idText) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(priceText))
            {
                MessageBox.Show("Kérem, töltse ki az összes mezőt!");
                return;
            }

            if (!int.TryParse(idText, out int id) || !int.TryParse(priceText, out int price) || price <= 0)
            {
                MessageBox.Show("Az ID és az ár pozitív egész szám kell legyen!");
                return;
            }

            if (products.Any(p => p.id == id))
            {
                MessageBox.Show("Már létezik ilyen azonosítójú termék!");
                return;
            }

            products.Add(new Product(id, name, price));
            RefreshGrid();
            SaveProducts();
            ClearInputs();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string idText = delProductIdBox.Text.Trim();
            if (string.IsNullOrEmpty(idText) || !int.TryParse(idText, out int id))
            {
                MessageBox.Show("Érvényes termék ID-t adjon meg!");
                return;
            }

            var productToDelete = products.FirstOrDefault(p => p.id == id);
            if (productToDelete == null)
            {
                MessageBox.Show("Nem található ilyen ID-jű termék!");
                return;
            }

            products.Remove(productToDelete);
            RefreshGrid();
            SaveProducts();
            delProductIdBox.Clear();
        }

        private void RefreshGrid()
        {
            productsGrid.ItemsSource = null;
            productsGrid.ItemsSource = products;
        }

        private void ClearInputs()
        {
            newProductIdBox.Clear();
            newProductNameBox.Clear();
            newProductPriceBox.Clear();
        }
    }
}