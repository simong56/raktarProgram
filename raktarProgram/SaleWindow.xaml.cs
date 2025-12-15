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
using System.Windows.Shapes;

namespace raktarProgram
{
    /// <summary>
    /// record sales
    /// </summary>
    public partial class SaleWindow : Window
    {
        private List<Product> products = new List<Product>();
        public SaleWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            products = Product.LoadFromCsv("data/products.csv");
            ProductSelect.ItemsSource = products.Select(p => p.name).ToList();
        }

        private void SellBtn_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ProductSelect.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Válassz ki egy terméket");
                return;
            }
            else if (SaleDatePicker.Text == null)
            {
                MessageBox.Show("Válassz ki egy dátumot");
                return;
            }
            else if (AmountBox.Text == "")
            {
                MessageBox.Show("Adj meg egy mennyiséget");
                return;
            }
            Product selectedProduct = products[selectedIndex];
            int amount = AmountBox.Text == "" ? 0 : int.Parse(AmountBox.Text);

            Sale sale = new Sale(selectedProduct.id, selectedProduct.name, SaleDatePicker.Text, amount);
            SaveSale(sale);
            MessageBox.Show("Eladás rögzítve");
        }

        private void SaveSale(Sale sale)
        {
            using (StreamWriter sw = new StreamWriter("data/sales.csv", true, Encoding.UTF8))
            {
                sw.WriteLine($"{sale.id},{sale.name},{sale.date},{sale.amount}");
            }
        }

        //csak szamok
        private void AmountBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }
    }
}
