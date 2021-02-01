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
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace kursachlar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool isadmin = true;
        public class Plauerinf
        {
            public int Id_plauer { get; set; }
            public string Nickname_plauer { get; set; }
            public double KillDeath { get; set; }
            public double WinLose { get; set; }
        }
        public class Organizationinf
        {
            public string Name_Organization { get; set; }

        }
        public static ObservableCollection<Plauerinf> playerinf = new ObservableCollection<Plauerinf>();
        public static ObservableCollection<Organizationinf> organizationinf = new ObservableCollection<Organizationinf>();



        private void Refresh()
        {
            MainWindow.playerinf.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM player_inf";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов

                    while (reader.Read()) // построчно считываем данные
                    {
                        Plauerinf playerinfo = new Plauerinf();
                        playerinfo.Id_plauer = Convert.ToInt32(reader.GetValue(0));
                        playerinfo.Nickname_plauer = Convert.ToString(reader.GetValue(1));
                        playerinfo.KillDeath = Convert.ToDouble(reader.GetValue(2));
                        playerinfo.WinLose = Convert.ToDouble(reader.GetValue(3));
                       
                        playerinf.Add(playerinfo);
                    }

                }

                reader.Close();


            }

        }
        private void RefreshOrganization()
        {
            MainWindow.organizationinf.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Organization_inf";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Organizationinf organizationinfo = new Organizationinf();
                        organizationinfo.Name_Organization = Convert.ToString(reader.GetValue(0));
                        organizationinf.Add(organizationinfo);
                    }

                }

                reader.Close();

            }
        }
        public MainWindow()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";

            // Создание подключения
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Открываем подключение
                connection.Open();
                //MessageBox.Show("Подключение открыто");

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
                // MessageBox.Show("Подключение закрыто...");
            }
            Refresh();
            RefreshOrganization();
            InitializeComponent();
            Page9 page9 = new Page9();
            page9.frame = MainFrame;
            MainFrame.Content = page9;

        }
    }
}
