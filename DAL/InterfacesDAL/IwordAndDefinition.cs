using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesDAL
{
    public interface IwordAndDefinition
    {
        List<WordAndDefinition> GetAllDefinitionsDAL();
        WordAndDefinition GetDefinitionByIdDAL(int id);
        int AddDefinitionDAL(WordAndDefinition w);
        public bool updateDAL(WordAndDefinition w);
    }
}
