using Database.MySQL;
using EntityUtils.Generator;
using MySql.Data.Entity;
using ShakesAndFidgetLibrary.Models;
using System.Data.Entity;
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
            //new Database.MySQL.Test();
            MySQLManager<User> manager = new MySQLManager<User>();
            Task.Factory.StartNew(() =>
            {
                EntityGenerator<User> generatorC = new EntityGenerator<User>();
                for (int i = 0; i < 300; i++)
                {
                    //User user = generatorC.GenerateItem();
                    User user = new User();
                    user.Mail = Faker.Internet.Email();
                    user.Name = Faker.Name.First();
                    user.Password = Faker.Internet.Password(6,10);
                    manager.Insert(user);
                }
            });
        }
        
    }
}
