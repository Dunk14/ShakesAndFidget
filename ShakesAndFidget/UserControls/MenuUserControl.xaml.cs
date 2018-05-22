using ShakesAndFidget.Views;
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

namespace ShakesAndFidget.UserControls
{
    /// <summary>
    /// Logique d'interaction pour MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl
    {
        public MenuUserControl()
        {
            InitializeComponent();
            Events();
        }

        private void Events()
        {
            this.Disconnect.Click += Disconnect_Click;
            this.Fight.Click += Fight_Click;
        }

        private void Fight_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.CurrentPage = new FightPage();
        }

        private void Disconnect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.Instance.CurrentPage = new ConnectionPage();
        }
    }
}
