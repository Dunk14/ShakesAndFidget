using Database.MySQL;
using EntityUtils.Generator;
using MySql.Data.Entity;
using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShakesAndFidget
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            new Database.MySQL.Test();
        }
        
    }
}
