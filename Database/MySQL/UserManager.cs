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
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class UserManager<T> : DbContext where T : User
    {
        public UserManager(String connectionString = null) :
            base (connectionString == null
                ? connectionString
                : "Server=localhost;Port=3306;Database=game;Uid=root;Pwd=")
        {
        }

        public UserManager(DbConnection existingConncetion, bool contextOwnsConnection) :
            base(existingConncetion, contextOwnsConnection)
        {
        }
            
        public DbSet<T> DbSetUser { get; set; }

        //public async Task<T> GetByName(String name)
        //{
        //    return this.DbSetUser.Where(a => a.Name == name);
        //}
    }
}
