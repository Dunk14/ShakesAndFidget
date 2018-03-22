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
        public List<User> GetByNameAndPassword(String name, String password)
        {
            List<User> users = this.DbSetT.Where<User>(x => (x.Name == name && x.Password == password)).ToList();
            return users;
        }

        public User GetByName(String name)
        {
            List<User> users = this.DbSetT.Where<User>(x => x.Name == name).ToList();
            if (users.Count == 1)
                return users[0];
            return null;
        }

        public User GetByMail(String mail)
        {
            List<User> users = this.DbSetT.Where<User>(x => x.Mail == mail).ToList();
            if (users.Count == 1)
                return users[0];
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

        public async Task<bool> InsertUser(String mail, String name, String password)
        {
            int insertion = await Insert(new User(mail, name, password, new List<Character>()));
            if (insertion == 1)
                return true;
            return false;
        }
    }
}
