using DAL.Functions;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Interfaces
{
    public interface Iuser
    {
        List<User> getAll();
        User getUserByNameAndPassword(string name, string password);
        int addUser(User u);
    }
}
