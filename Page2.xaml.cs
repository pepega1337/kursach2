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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    /// 

    public partial class Page2 : Page
    {
        public Frame frame;
        public static ObservableCollection<Button> ButtonList = new ObservableCollection<Button>(); //костыльная коллекция для обращения к нестатичной кнопке

        public Page2()
        {
            InitializeComponent();
            Refresh();
            FileInfoView.ItemsSource = MainWindow.playerinf;
            ButtonList.Add(iz);
            if (!MainWindow.isadmin){ iz.Visibility = Visibility.Hidden; }

        }

        public static void ShowExitButton(bool show) //метод для показа/скрытия кнопки выхода
        {
            if (show)
            {
                ButtonList[0].Visibility = Visibility.Visible; // обращение к элементу в костыльном списке
            }
            else
            {
                ButtonList[0].Visibility = Visibility.Hidden;
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
                command.CommandText = "SELECT * FROM player_inf";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        MainWindow.Plauerinf playerinfo = new MainWindow.Plauerinf();
                        playerinfo.Id_plauer = Convert.ToInt32(reader.GetValue(0));
                        playerinfo.Nickname_plauer = Convert.ToString(reader.GetValue(1));
                        playerinfo.KillDeath = Convert.ToDouble(reader.GetValue(2));
                        playerinfo.WinLose = Convert.ToDouble(reader.GetValue(3));
                        FileInfoView.ItemsSource = MainWindow.playerinf;
                        MainWindow.playerinf.Add(playerinfo);
                        //MessageBox.Show("1231231231231231231231231231231231");
                    }

                }

                reader.Close();

            }
        }

        private void FindLike(string execcomm) // Обновление вывода
        {
            MainWindow.playerinf.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=cubercock;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = execcomm;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        MainWindow.Plauerinf playerinfo = new MainWindow.Plauerinf();
                        playerinfo.Id_plauer = Convert.ToInt32(reader.GetValue(0));
                        playerinfo.Nickname_plauer = Convert.ToString(reader.GetValue(1));
                        playerinfo.KillDeath = Convert.ToDouble(reader.GetValue(2));
                        playerinfo.WinLose = Convert.ToDouble(reader.GetValue(3));
                        FileInfoView.ItemsSource = MainWindow.playerinf;
                        MainWindow.playerinf.Add(playerinfo);
                    }

                }

                reader.Close();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        //    MessageBox.Show(Convert.ToString(FileInfoView.SelectedItem));
        //    MessageBox.Show(Convert.ToString(FileInfoView.SelectedIndex));
        //    MessageBox.Show(MainWindow.playerinf[FileInfoView.SelectedIndex].Name);
        //    MainWindow.playerinf.RemoveAt(FileInfoView.SelectedIndex);
            
        }

        private void Refreshing(object sender, RoutedEventArgs e) //Кнопка обновления
        {
            //Refresh();
            //FileInfoView.ItemsSource = MainWindow.playerinf;
            //MessageBox.Show(MainWindow.playerinf[1].Name);
            //Page2 editor = new Page2 { frame = this.frame }; //Перевызываем кек
            //frame.Content = editor;
        }

        private void SearchForNumBtn(object sender, RoutedEventArgs e)
        {

        }

        private void OpenEditor(object sender, RoutedEventArgs e) //Кнопка редактора
        {
            try
            {
                Page8 editor = new Page8 { frame = this.frame }; //Передача в presenter чтобы не был пустым 

                
                editor.txname.Text = MainWindow.playerinf[FileInfoView.SelectedIndex].Nickname_plauer;
                editor.txsurname.Text = Convert.ToString(MainWindow.playerinf[FileInfoView.SelectedIndex].KillDeath);
                editor.txdadname.Text = Convert.ToString( MainWindow.playerinf[FileInfoView.SelectedIndex].WinLose);
                Page8.index = FileInfoView.SelectedIndex;
                Page8.EditorInfo.Id_plauer = MainWindow.playerinf[FileInfoView.SelectedIndex].Id_plauer;
                Page8.EditorInfo.Nickname_plauer = MainWindow.playerinf[FileInfoView.SelectedIndex].Nickname_plauer;
                Page8.EditorInfo.KillDeath = MainWindow.playerinf[FileInfoView.SelectedIndex].KillDeath;
                Page8.EditorInfo.WinLose = MainWindow.playerinf[FileInfoView.SelectedIndex].WinLose;
                frame.Content = editor;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Элемент не выбран", "ОШИБКА");
            }
        }

        private void InputCheck(object sender, KeyEventArgs e)
        {
           // MessageBox.Show(Convert.ToString(e.Key));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //surnameBox.SelectionStart = 0;
            //surnameBox.SelectionLength = 10;
            surnameBox.Select(0, 10);
        }

        private void surnameBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            surnameBox.Select(0, 10);
        }

        private void FindByFam(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show(surnameBox.Text);
            //FindLike(surnameBox.Text);
            //phoneBox.Text = ""; // to avoid user`s mind bump
            FindLike("SELECT * FROM player_inf WHERE nickname_player LIKE '" + surnameBox.Text + "%'");

        }

        private void FindByPhone(object sender, TextChangedEventArgs e)
        {
            //surnameBox.Text = ""; // to avoid user`s mind bump
            FindLike("SELECT * FROM player_inf WHERE Mphone LIKE '" + phoneBox.Text + "%' OR Hphone LIKE '" + phoneBox.Text + "%'");

        }

        private void CreateReportBtn(object sender, RoutedEventArgs e)
        {
            //ReportLog reporter = new ReportLog { frame = this.frame }; //Открываем отчёт
            //frame.Content = reporter;
        }

        private void NumEditorBtn(object sender, RoutedEventArgs e)
        {
            //Page4NumEdit numeditor = new Page4NumEdit { frame = this.frame }; //Открываем редактор номерка
            //frame.Content = numeditor;
        }

        private void FileInfoView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            //try
            //{
            //    Page4NumEdit.idOfStud = MainWindow.playerinf[FileInfoView.SelectedIndex].Id; // передаём айдиху студака, чтоб его номерки вывести, но заранее, т.к перемка поздно апдейтится и листвью багует
            //}
            //catch
            //{
            //    MessageBox.Show("Something went wrong");
            //}
        }
    }
}
