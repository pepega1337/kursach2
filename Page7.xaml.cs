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
    /// Логика взаимодействия для Page7.xaml
    /// </summary>
    public partial class Page7 : Page
    {
        public Frame frame;

        public static int uniqueID = 0;

        public Page7()
        {
            InitializeComponent();
            Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";

            try
            {
                string sqlExpression = "INSERT INTO dbo.tournament_organization(name_organization,tournament_id) VALUES ('" + typeBox.Text + "', " + uniqueID + ")";

                MessageBox.Show(sqlExpression);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));


                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите цифры!", "ОШИБКА");
            }

        }

        private void Refresh() // Обновление вывода
        {
            for (int i = 0; i < MainWindow.organizationinf.Count; i++)
            {
                typeBox.Items.Add(MainWindow.organizationinf[i].Name_Organization);
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page1 adding = new Page1 { frame = this.frame };
            frame.Content = adding;
        }
    }
}
