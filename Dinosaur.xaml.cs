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
using System.Windows.Shapes;

namespace temanpd
{
    /// <summary>
    /// Interaction logic for Dinosaur.xaml
    /// </summary>
    public partial class Dinosaur : Window
    {
        public Dinosaur()
        {
            InitializeComponent();
        }

        private void open_Main(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }
    }
}
