using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterfaceBLL
{
    public interface IuserBLL
    {
        List<userDTO> getAllBLL();
        userDTO getUserByNameAndPasswordBLL(string name, string password);
        public int addUserBLL(userDTO u);
    }
}
