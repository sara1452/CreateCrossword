using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterfaceBLL
{
    public interface IwordAndDefinitionBLL
    {
        List<WordAndDefinitionDTO> GetAllDefinitionsBLL();
        WordAndDefinitionDTO GetDefinitionByIdBLL(int id);
        public int AddDefinitionBLL(WordAndDefinitionDTO w1);
        public void UpdateBLL();
    }
}
