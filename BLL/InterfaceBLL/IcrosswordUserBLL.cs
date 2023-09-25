using BLL.FunctionBLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterfaceBLL
{
    public interface IcrosswordUserBLL
    {
        public int AddCrosswordByUser(UserCrosswordDTO u);
        public List<UserCrosswordDTO> getCrosswordsByUserBLL(int userId);
    }
}
