using MySql.Data.Entity;
using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.MySQL
{
    public class UserManager : MySQLManager<User>
    {
        public async Task<User> GetByName(String name, String password)
        {
            //return await this.DbSetUser.FirstOrDefaultAsync(a => a.Name == name) as T;
            //this.DbSetT.SqlQuery();
            List<User> users = this.DbSetT.Where<User>(x => (x.Name == name && x.Password == password)).ToList();
            if (users.Count == 1)
            {
                return users[0];
            }
            return null;
        }
    }
}
