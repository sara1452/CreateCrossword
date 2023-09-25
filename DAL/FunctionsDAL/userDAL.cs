using DAL.Interfaces;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Functions
{
    public class userDAL : Iuser
    {
        CrosswordContext DB;
        public userDAL(CrosswordContext _DB)
        {
            DB = _DB;
        }
        public int addUser(User user)
        {
            DB.Users.Add(user);
            DB.SaveChanges();
            return user.UserCode;
        }
        public List<User> getAll()
        {
            return DB.Users.ToList();
        }
        public User getUserByNameAndPassword(string name, string password)
        {
            return DB.Users.FirstOrDefault(x => x.UserName == name && x.UserPassword == password);
        }
    }
}
