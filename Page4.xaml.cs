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
    /// Логика взаимодействия для AddingUser.xaml
    /// </summary>
    public partial class AddingUser : Page
    {
        public Frame frame;

        public AddingUser()
        {
            InitializeComponent();

        }

        private void AddUserBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";
                int uniqueID = 0;
                if (MainWindow.playerinf.Count > 0)
                {
                    uniqueID = MainWindow.playerinf[MainWindow.playerinf.Count - 1].Id_plauer + 1;
                }
                string name = txname.Text;
                double surname = Convert.ToDouble(txsurname.Text);
                double dadname = Convert.ToDouble(txdadname.Text);

                string sqlExpression = "INSERT INTO player_inf(id_player, nickname_player, Kill_death, Win_Lose) VALUES("+uniqueID+ ",'" + name + "', " + surname + ", " + dadname + ")";
                MessageBox.Show(sqlExpression);



                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        int number = command.ExecuteNonQuery();
                        MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                        MainWindow.Plauerinf fileInfo = new MainWindow.Plauerinf();
                        fileInfo.Id_plauer = uniqueID;
                        fileInfo.Nickname_plauer = name;
                        fileInfo.KillDeath = Convert.ToDouble(surname);
                        fileInfo.WinLose = Convert.ToDouble (dadname);
                        MainWindow.playerinf.Add(fileInfo);

                        Page3 phonePage = new Page3 { frame = this.frame }; //Открываем добавление номерков
                        Page3.Id_plauer = uniqueID; // студентик                   
                        if (MainWindow.organizationinf.Count > 0)
                        {
                          
                        }
                        else
                        {
                            Page3.uniqueID = uniqueID;
                        }

                        frame.Content = phonePage; //открываем мобилку страничную добавку
                    }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный ввод", "ОШИБКА");
            }

        }
    }
}
