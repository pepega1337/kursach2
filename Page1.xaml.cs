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

namespace kursachlar
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Frame frame;
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page2 adding = new Page2 { frame = this.frame }; //Передача в presenter чтобы не был пустым 
            frame.Content = adding;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddingUser adding = new AddingUser { frame = this.frame }; //Передача в presenter чтобы не был пустым 
            frame.Content = adding;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Page5 adding = new Page5 { frame = this.frame }; //Передача в presenter чтобы не был пустым 
            frame.Content = adding;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Page6 adding = new Page6 { frame = this.frame }; //Передача в presenter чтобы не был пустым 
            frame.Content = adding;
        }
    }
}
