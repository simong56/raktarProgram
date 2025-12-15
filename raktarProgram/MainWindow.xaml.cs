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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace raktarProgram
{
    /// <summary>
    /// login
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<User> LoadUsers()
        {
            string csvFilePath = "data/users.csv";
            try
            {
                List<User> users = User.LoadFromCsv(csvFilePath);
                return users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a felhasználók betöltésekor!: {ex.Message}");
                return null;
            }
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwdBox.Password;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Kérem, töltse ki a felhasználónevet és jelszót!");
                return;
            }
            List<User> users = LoadUsers();
            var matchingUser = users.FirstOrDefault(u => u.username == username && u.password == password);

            if (matchingUser != null)
            {
                ProductsWindow productsWindow = new ProductsWindow();
                productsWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Érvénytelen felhasználónév vagy jelszó!");
            }
        }
    }
}
