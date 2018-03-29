using LoggerUtil;
using ShakesAndFidget.Views;
using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ShakesAndFidget
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region Singleton
        private static MainWindow instance;
        private static Logger logger;

        public MainWindow()
        {
            this.DataContext = this;
            this.CurrentPage = new ConnectionPage();
            this.Loaded += MainWindow_Loaded;
            instance = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            logger = new Logger(new List<Alert> { Alert.TOAST }, new List<Mode> { Mode.CONSOLE });
        }

        public static MainWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new MainWindow();
                return instance;
            }
        }

        public static Logger Logger
        {
            get
            {
                if (logger == null)
                    logger = new Logger(new List<Alert> { Alert.TOAST }, new List<Mode> { Mode.CONSOLE });
                return logger;
            }
        }
        #endregion

        #region StaticVariables
        #endregion

        #region Constants
        #endregion

        #region Variables
        #endregion

        #region Attributs
        private Page currentPage;

        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }

        private ICharacter currentCharacter;

        public ICharacter CurrentCharacter
        {
            get { return currentCharacter; }
            set { currentCharacter = value; }
        }
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region StaticFunctions
        #endregion

        #region Functions
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

    }
}
