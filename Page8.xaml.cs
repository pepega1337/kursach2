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
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Page8 : Page
    {

        public Frame frame;

        public static int index;
        public static class EditorInfo
        {
            public static int Id_plauer { get; set; }
            public static string Nickname_plauer { get; set; }
            public static double KillDeath { get; set; }
            public static double WinLose { get; set; }
        }

        public Page8()
        {

            InitializeComponent();
            txname.Text = Convert.ToString(EditorInfo.Nickname_plauer);
            txsurname.Text = Convert.ToString(EditorInfo.KillDeath);
            txdadname.Text = Convert.ToString(EditorInfo. WinLose);
           
            

        }

        private void SavingButton(object sender, RoutedEventArgs e)
        {
            //MainWindow.playerinf[index].Id_plauer =Convert.ToInt32( txname.Text);
            MainWindow.playerinf[index].Nickname_plauer = txname.Text;
            MainWindow.playerinf[index].KillDeath = Convert.ToDouble(txsurname.Text);
            MainWindow.playerinf[index].WinLose = Convert.ToDouble(txdadname.Text);

            int uniqueID = MainWindow.playerinf[index].Id_plauer;
            string nickname_plauer = txname.Text;
            double killDeath = Convert.ToDouble(txsurname.Text);
            double winLose = Convert.ToDouble( txdadname.Text);
            

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";
            string sqlExpression = "UPDATE player_inf SET nickname_player='" + nickname_plauer + "', Kill_death='" + killDeath + "', Win_Lose=" + winLose + "WHERE id_player=" + uniqueID;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Обновлено объектов: " + number);
            }
            Page2 adding = new Page2 { frame = this.frame }; //Передача в presenter чтобы не был пустым 
            frame.Content = adding;
            MessageBox.Show(Convert.ToString(index));
        }

        private void RemoveFirstNumberPhonesAll()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";
            string sqlExpression = "DELETE  FROM player_inf WHERE id_player=" + EditorInfo.Id_plauer; // удаляем по айди студентика
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                if (number == 0)
                {
                    MessageBox.Show("Объект не найден");
                }
                else
                {
                    MessageBox.Show("Удалено объектов: " + number);
                }
            }

        }

        private void RemoveButton(object sender, RoutedEventArgs e)
        {
            SaveBtn.IsEnabled = false;
            MessageBox.Show(Convert.ToString(EditorInfo.Id_plauer));
            RemoveFirstNumberPhonesAll();
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";
            string sqlExpression = "DELETE  FROM player_inf WHERE id_player=" + EditorInfo.Id_plauer;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                if (number == 0)
                {
                    MessageBox.Show("Объект не найден");
                }
                else
                {
                    MessageBox.Show("Удалено объектов: " + number);
                    MainWindow.playerinf.RemoveAt(index);
                }
            }
        }

    }
}
