using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Page9 : Page
    {
        public Frame frame;
        string CurLogin = ""; //текущий логин
        string CurPassword = ""; //текущий пароль
        string CurRole = ""; //текущая роль
        public Page9()
        {
            InitializeComponent();
        }

        private void RegBtn(object sender, RoutedEventArgs e)
        {
            Page10 page10 = new Page10 { frame = this.frame };
            frame.Content = page10;
        }

        private void LogBtn(object sender, RoutedEventArgs e)
        {
            FindUser(lg.Text,ps.Text);
        }
        private void FindUser(string log, string pass) // метод с аргументами log и pass для поиска юзера из БД
        {


            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True"; //подключение к бд
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM [User] WHERE [Login] = '" + log + "' AND [Password] = '" + pass + "';"; //выполняем запрос на поиск логина и пароля в БД
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если запрос нашёл записи
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        CurLogin = Convert.ToString(reader.GetValue(0));
                        CurPassword = Convert.ToString(reader.GetValue(1));
                        CurRole = Convert.ToString(reader.GetValue(2));
                    }

                    switch (CurRole) //проверка на роль у юзера и открытие нужной ему формы
                    {
                        case "Admin":
                            Page1 page1 = new Page1 { frame = this.frame };
                            frame.Content = page1;
                            break;
                        case "User":
                            Page1 page15 = new Page1 { frame = this.frame };
                            frame.Content = page15;
                            //Page2.ShowExitButton(false);
                            MainWindow.isadmin = false;
                            break;
                        default:
                            MessageBox.Show("Ваша роль неопределена, свяжитесь с администратором", "ОШИБКА");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Логин или пароль неверны", "ОШИБКА");
                }

                reader.Close();

            }
        }
    }
}
