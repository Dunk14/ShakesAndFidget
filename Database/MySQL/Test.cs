using EntityUtils.Generator;
using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.MySQL
{
    public class Test
    {
        public Test()
        {
            Task.Factory.StartNew(() =>
            {
                EntityGenerator<Stats> generatorStats = new EntityGenerator<Stats>();
                MySQLManager<User> userManager = new MySQLManager<User>();
                MySQLManager<Stats> statsManager = new MySQLManager<Stats>();
                for (int i = 0; i < 300; i++)
                {
                    User user = new User();
                    user.Mail = Faker.Internet.Email();
                    user.Name = Faker.Name.First();
                    user.Password = Faker.Internet.Password(6, 10);
                    userManager.Insert(user);
                }
                for (int i = 0; i < 10; i++)
                {
                    Stats stat = generatorStats.GenerateItem();
                    statsManager.Insert(stat);
                }
            });
        }
    }
}