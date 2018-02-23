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
            this.InsertData().Wait();
        }
        public async Task InsertData()
        {
            await Task.Factory.StartNew(() =>
            {
                EntityGenerator<Stats> generatorStats = new EntityGenerator<Stats>();
                MySQLManager<User> userManager = new MySQLManager<User>();
                MySQLManager<Stats> statsManager = new MySQLManager<Stats>();
                for (int i = 0; i < 10; i++)
                {
                    String FirstName = Faker.Name.First();
                    User user = new User();
                    user.Mail = Faker.Internet.Email();
                    user.Name = FirstName;
                    user.Password = UserManager.CalculateMD5Hash(FirstName);
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