using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        public Frame frame;

        public static int tournament_id = 0;

        public static int uniqueID = 0;
        public Page6()
        {
            InitializeComponent();
            Refresh();
        }

        public class Tournament_inf
        {
            public int tournament_id { get; set; }

            public string tournament_name { get; set; }

            public string data_houlding { get; set; }

            public string prize_fund { get; set; }

        }
        public static ObservableCollection<Tournament_inf> organizationinf = new ObservableCollection<Tournament_inf>();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";

            try
            {
                string sqlExpression = "INSERT INTO dbo.tournament_inf(tournament_id,tournament_name,data_houlding,prize_fund) VALUES (" + uniqueID + ", '" + nametur.Text + "','"+ datatur.Text + "','" + monyatur.Text + "')";

                MessageBox.Show(sqlExpression);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                    Tournament_inf phonik = new Tournament_inf();
                    phonik.tournament_name = nametur.Text;
                    phonik.data_houlding = datatur.Text;
                    phonik.prize_fund = monyatur.Text;
                    Page7.uniqueID = uniqueID;
                    Page7 adding = new Page7 { frame = this.frame };
                    frame.Content = adding;
                    
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите цифры!", "ОШИБКА");
            }

        }
        private void Refresh() // Обновление вывода
        {
            MainWindow.playerinf.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM tournament_inf";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {

                        uniqueID = Convert.ToInt32(reader.GetValue(0))+1;
                       
                    }

                }

                reader.Close();

            }
        }
    }
}
