using DAL.InterfacesDAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FunctionsDAL
{
    public class WordAndDefinitionFuncDAL:IwordAndDefinition
    {
        CrosswordContext DB;

        public WordAndDefinitionFuncDAL(CrosswordContext _DB)
        {
            DB = _DB;
        }
        public List<WordAndDefinition> GetAllDefinitionsDAL()
        {
            return DB.WordAndDefinitions.Include(x=>x.AllCrosswords).ToList();
        }

        public int AddDefinitionDAL(WordAndDefinition w1)
        {
            DB.WordAndDefinitions.Add(w1);
            DB.SaveChanges();
            return w1.WordCode;
        }
        public WordAndDefinition GetDefinitionByIdDAL(int id)
        {
            return DB.WordAndDefinitions.FirstOrDefault(x => x.WordCode == id);
        }
        public bool updateDAL(WordAndDefinition w)
        {
            WordAndDefinition w1=DB.WordAndDefinitions.FirstOrDefault(x => x.WordCode == w.WordCode);
            if (w1==null) return false;
            w1.Word = w.Word;
            w1.Definition=w.Definition;
            DB.SaveChanges();
            return true;
        }
    }
}

