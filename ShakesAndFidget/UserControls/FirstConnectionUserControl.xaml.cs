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
    /// Logique d'interaction pour FirstConnectionUserControl.xaml
    /// </summary>
    public partial class FirstConnectionUserControl : UserControl
    {
        public bool isPlaceholder;

        public FirstConnectionUserControl()
        {
            InitializeComponent();
            Events();
        }

        private void Events()
        {
            Loaded += UserControl_Loaded;
            Character_name.GotFocus += Character_name_GotFocus;
            Character_name.LostFocus += Character_name_LostFocus;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Character_name.Text = "Characters name...";
            isPlaceholder = true;
        }

        private void Character_name_GotFocus(object sender, EventArgs e)
        {
            if (isPlaceholder)
            {
                (sender as TextBox).Text = "";
                isPlaceholder = false;
            }
        }

        private void Character_name_LostFocus(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace((sender as TextBox).Text))
            {
                (sender as TextBox).Text = "Characters name...";
                isPlaceholder = true;
            }
        }
    }
}
