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
            MySQLManager<User> UserManager = new MySQLManager<User>();
            User User1 = new User("toto", "test", "test");
            User User2 = new User("tata", "test", "test");
            UserManager.Insert(User1);
            UserManager.Insert(User2);
        }
    }
}