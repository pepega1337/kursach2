using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace kursachlar
{
    /// <summary>
    /// Логика взаимодействия для Page10.xaml
    /// </summary>
    public partial class Page10 : Page
    {
        public Frame frame;
        public Page10()
        {
            InitializeComponent();
        }

        bool isadmin = true;
        string rol = "User";

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";
            try
            {
                string sqlExpression = "INSERT INTO dbo.[User](Login,Password,Role) VALUES ('" + Log.Text + "','" + Pas.Text + "','"+ rol + "')";

                MessageBox.Show(sqlExpression);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));

                    Page1 page1 = new Page1 { frame = this.frame };
                    frame.Content = page1;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите цифры!", "ОШИБКА");
            }
        }

        private void Rol_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Admin");
            rol = "Admin";
        }
        private void Rol_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("User");
            rol = "User";
        }
    }
}
