using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BLL.InterfaceBLL
{
    public interface IAllCrosswordBLL
    {
        List<crosswordDTO> GetAllCrosswordsBLL();
        public int AddCrosswordBLL(crosswordDTO c1);
        public List<crosswordDTO> GetCrosswordByIdBLL(int id);
        public List<crosswordDTO> NewCrossword(int idCrossword, int size);
    }
}
