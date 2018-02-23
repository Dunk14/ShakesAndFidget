using MySql.Data.Entity;
using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
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

        public static string CalculateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
