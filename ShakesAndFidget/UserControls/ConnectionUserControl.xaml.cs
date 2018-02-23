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
    /// Logique d'interaction pour ConnectionUserControl.xaml
    /// </summary>
    public partial class ConnectionUserControl : UserControl
    {
        public bool isNamePlaceholder;
        public bool isPasswordPlaceholder;
        public bool isMailPlaceholder;

        public ConnectionUserControl()
        {
            InitializeComponent();
            InitializeVariables();
            Events();
        }

        private void InitializeVariables()
        {
            isNamePlaceholder = true;
            isMailPlaceholder = true;
            isPasswordPlaceholder = true;
        }

        private void Events()
        {
            Name.GotFocus += Name_GotFocus;
            Name.LostFocus += Name_LostFocus;
            Mail.GotFocus += Mail_GetFocus;
            Mail.LostFocus += Mail_LostFocus;
            RealPassword.LostFocus += RealPassword_LostFocus;
            FakePassword.GotFocus += FakePassword_GetFocus;
        }

        private void Name_GotFocus(object sender, EventArgs e)
        {
            if (isNamePlaceholder)
            {
                Name.Text = "";
                isNamePlaceholder = false;
            }
        }

        private void Name_LostFocus(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Name.Text))
            {
                Name.Text = "Enter a name or a pseudonym...";
                isNamePlaceholder = true;
            }
        }

        private void Mail_GetFocus(object sender, EventArgs e)
        {
            if (isMailPlaceholder)
            {
                Mail.Text = "";
                isMailPlaceholder = false;
            }
        }

        private void Mail_LostFocus(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Mail.Text))
            {
                Mail.Text = "Enter a mail address...";
                isMailPlaceholder = true;
            }
        }

        private void RealPassword_LostFocus(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(RealPassword.Password))
            {
                RealPassword.Visibility = System.Windows.Visibility.Collapsed;
                FakePassword.Visibility = System.Windows.Visibility.Visible;
                isPasswordPlaceholder = true;
            }
        }

        private void FakePassword_GetFocus(object sender, EventArgs e)
        {
            if (isPasswordPlaceholder)
            {
                FakePassword.Visibility = System.Windows.Visibility.Collapsed;
                RealPassword.Visibility = System.Windows.Visibility.Visible;
                RealPassword.Focus();
                isPasswordPlaceholder = false;
            }
        }
    }
}
