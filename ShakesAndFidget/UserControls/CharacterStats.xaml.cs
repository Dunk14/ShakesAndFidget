using EntityUtils.Reflection;
using ShakesAndFidgetLibrary.Models;
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
    /// Logique d'interaction pour CharacterStats.xaml
    /// </summary>
    public partial class CharacterStats : UserControl
    {
        String[] values = {
            "life            : ",
            "CriticalDamage  : "
        };
        public CharacterStats()
        {
            InitializeComponent();
            
            //Dictionary<String, Object> itemProperties = new Reflectionner().ReadClass<Stats>();
            //int i = 0;
            //foreach (var item in itemProperties)
            //{
            //    Console.WriteLine(item.Key);
            //    Label label = new Label();
            //    label.Content = item.Key;

            //    Grid.SetRow(label, i);

            //    mainGrid.Children.Add(label);
            //    i++;
            //}
        }

        
    }
}
