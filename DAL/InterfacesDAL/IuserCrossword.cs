using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesDAL
{
    public interface IuserCrossword
    {
        //List<IuserCrossword> getAll();
        int addCrossword(CrosswordsUser u);
        public List<CrosswordsUser> getCrosswordsByUserBLL(int userId);

    }
}
