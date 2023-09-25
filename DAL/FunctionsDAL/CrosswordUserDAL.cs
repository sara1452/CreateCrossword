using DAL.InterfacesDAL;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FunctionsDAL
{
    public class CrosswordUserDAL : IuserCrossword
    {
        CrosswordContext DB;
        public CrosswordUserDAL(CrosswordContext _DB)
        {
           this.DB = _DB;
        }
        public int addCrossword(CrosswordsUser u)
        {
            DB.CrosswordsUsers.Add(u);
            DB.SaveChanges();
            return u.CrosswordCode;
        }

        public List<CrosswordsUser> getCrosswordsByUserBLL(int userId)
        {
            return DB.CrosswordsUsers.Where(x => x.UserCode == userId).ToList();
        }
    }
}
