using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface Icrosswords
    {
        List<AllCrossword> GetAllCrosswords();
        string[,] GetCrossword(int id);
        int AddCrosswordDAL(AllCrossword c1);
        public List<AllCrossword> GetCrosswordByIdDAL(int id);
    }
}
