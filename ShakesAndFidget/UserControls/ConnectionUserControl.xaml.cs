using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public bool isNameValid;
        public bool isMailValid;
        public bool isPasswordValid;

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
            isNameValid = false;
            isMailValid = false;
            isPasswordValid = false;
        }

        private void Events()
        {
            Name.GotFocus += Name_GotFocus;
            Name.LostFocus += Name_LostFocus;
            //Mail.GotFocus += Mail_GetFocus;
            //Mail.LostFocus += Mail_LostFocus;
            RealPassword.LostFocus += RealPassword_LostFocus;
            FakePassword.GotFocus += FakePassword_GetFocus;
            Name.TextChanged += Name_TextChanged;
            //Mail.TextChanged += Mail_TextChanged;
            RealPassword.PasswordChanged += RealPassword_PasswordChanged;
        }

        private void Name_GotFocus(object sender, EventArgs e)
        {
            if (isNamePlaceholder)
            {
                (sender as TextBox).Text = "";
                isNamePlaceholder = false;
            }
        }

        private void Name_LostFocus(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace((sender as TextBox).Text))
            {
                isNamePlaceholder = true;
                (sender as TextBox).Text = "Enter a name or a pseudonym...";
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void Name_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length < 3 && !isNamePlaceholder)
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                NameMessage.Foreground = new SolidColorBrush(Color.FromRgb(255, 50, 50));
                NameMessage.Content = "Name must be 3 characters long at least";
                isNameValid = false;
            }
            else if (Regex.IsMatch((sender as TextBox).Text, @"[\s]+") && !isNamePlaceholder) {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                NameMessage.Foreground = new SolidColorBrush(Color.FromRgb(255, 50, 50));
                NameMessage.Content = "Name doesn't accept whitespaces";
                isNameValid = false;
            }
            else
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                NameMessage.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                NameMessage.Content = "";
                isNameValid = true;
            }
        }

        private void Mail_GetFocus(object sender, EventArgs e)
        {
            if (isMailPlaceholder)
            {
                (sender as TextBox).Text = "";
                isMailPlaceholder = false;
            }
        }

        private void Mail_LostFocus(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace((sender as TextBox).Text))
            {
                (sender as TextBox).Text = "Enter a mail address...";
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                isMailPlaceholder = true;
                MailMessage.Content = "";
            }
        }

        private void Mail_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch((sender as TextBox).Text, @"^([A-Z|a-z|0-9](\.|_){0,1})+[A-Z|a-z|0-9]\@([A-Z|a-z|0-9])+((\.){0,1}[A-Z|a-z|0-9]){2}\.[a-z]{2,3}$")
                && !isMailPlaceholder)
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255,200,200));
                MailMessage.Foreground = new SolidColorBrush(Color.FromRgb(255, 50, 50));
                MailMessage.Content = "Do you even know what is an email address bro ?..";
                isMailValid = false;
            }
            else
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                MailMessage.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                MailMessage.Content = "";
                isMailValid = true;
            }
        }

        private void RealPassword_LostFocus(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace((sender as PasswordBox).Password))
            {
                (sender as PasswordBox).Visibility = System.Windows.Visibility.Collapsed;
                FakePassword.Visibility = System.Windows.Visibility.Visible;
                isPasswordPlaceholder = true;
                PasswordMessage.Content = "";
            }
        }

        private void FakePassword_GetFocus(object sender, EventArgs e)
        {
            if (isPasswordPlaceholder)
            {
                (sender as TextBox).Visibility = System.Windows.Visibility.Collapsed;
                RealPassword.Visibility = System.Windows.Visibility.Visible;
                RealPassword.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                RealPassword.Focus();
                isPasswordPlaceholder = false;
            }
        }

        private void RealPassword_PasswordChanged(object sender, EventArgs e)
        {
            if ((sender as PasswordBox).Password.Length < 6 && !isPasswordPlaceholder)
            {
                (sender as PasswordBox).Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                PasswordMessage.Foreground = new SolidColorBrush(Color.FromRgb(255, 50, 50));
                PasswordMessage.Content = "Password must be 6 characters long at least";
                isPasswordValid = false;
            }
            else if (Regex.IsMatch((sender as PasswordBox).Password, @"[\s]+") && !isPasswordPlaceholder)
            {
                (sender as PasswordBox).Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                PasswordMessage.Foreground = new SolidColorBrush(Color.FromRgb(255, 50, 50));
                PasswordMessage.Content = "Password doesn't accept whitespaces";
                isPasswordValid = false;
            }
            else
            {
                (sender as PasswordBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                PasswordMessage.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                PasswordMessage.Content = "";
                isPasswordValid = true;
            }
        }

        public bool isFormValidForSubscription()
        {
            return isNameValid && isMailValid && isPasswordValid ? true : false;
        }

        public bool isFormValidForLogIn()
        {
            return isNameValid && isPasswordValid ? true : false;
        }
    }
}
