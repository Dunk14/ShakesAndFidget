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

        private void InitializeVariables()
        {
            Username.Text = "Characters name...";
            isPlaceholder = true;
        }

        private void Events()
        {
            Username.GotFocus += Username_GotFocus;
            Username.LostFocus += Username_LostFocus;
        }

        private void Username_GotFocus(object sender, EventArgs e)
        {
            if (isPlaceholder)
            {
                Username.Text = "";
                isPlaceholder = false;
            }
        }

        private void Username_LostFocus(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Username.Text))
            {
                Username.Text = "Characters name...";
                isPlaceholder = true;
            }
        }
    }
}
