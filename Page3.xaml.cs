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
    /// Логика взаимодействия для Page3Numbers.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Frame frame;

        public static int Id_plauer = 0; 

        public static int uniqueID = 0; 


        public Page3()
        {
            InitializeComponent();
        }

        private void AddNumBtn(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";

            try
            {
                string sqlExpression = "INSERT INTO dbo.organization_player(name_organization,id_player) VALUES ('" + typeBox.Text + "', " + Id_plauer + ")";

                MessageBox.Show(sqlExpression);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                    MainWindow.Organizationinf phonik = new MainWindow.Organizationinf();
                    phonik.Name_Organization = typeBox.Text;
                    uniqueID = uniqueID + 1;  
                    Page1 adding = new Page1 { frame = this.frame };  
                    frame.Content = adding;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите цифры!", "ОШИБКА");
            }
        }
    }
}
