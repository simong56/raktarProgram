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

namespace raktarProgram
{
    /// <summary>
    /// organize sales and show stats
    /// </summary>
    public partial class StatsWindow : Window
    {
        private List<Sale> sales = new List<Sale>();
        public StatsWindow()
        {
            InitializeComponent();
            LoadSales();
            Organize();
        }
        private void Organize()
        {
            var organized = sales
                .GroupBy(s => s.id)
                .Select(g => new Sale(
                    g.Key,
                    g.First().name,
                    g.First().date,
                    g.Sum(s => s.amount)
                ))
                .ToList();
            
            sales = organized;
            StatsGrid.ItemsSource = null;
            StatsGrid.ItemsSource = sales;
        }
        private void LoadSales()
        {
            sales = Sale.LoadFromCsv("data/sales.csv");
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadSales();
            Organize();
        }
    }
}
