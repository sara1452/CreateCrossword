using DAL.Interfaces;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Functions
{
    public class crosswordsDAL : Icrosswords
    {
        CrosswordContext DB;
        string[,] mat;
        public crosswordsDAL(CrosswordContext dB)
        {
            DB = dB;
            mat = new string[9,13];
        }
        public int AddCrosswordDAL(AllCrossword c1)
        {
            DB.AllCrosswords.Add(c1);
            DB.SaveChanges();
            return c1.CrosswordCode;
        }
        public List<AllCrossword> GetAllCrosswords()
        {
            return DB.AllCrosswords.Include(x=>x.DefinitionCodeNavigation).ToList();
        }
        public string[,] GetCrossword(int id)
        {
            List<AllCrossword> cw= DB.AllCrosswords.Where(x=>x.CrosswordCode == id).ToList();
            foreach(var x in cw)
            {
                int i = 0;
                string word= x.Solve;
                mat[x.I-1, 13-x.J] = x.NumberLocation + word.Substring(i, 1);
                if (x.Across == true)
                {
                    while (word != null && i < word.Length-1)
                    {
                        i++;
                        mat[x.I- 1, 13-x.J - i]= word.Substring(i, 1);
                    }
                }
                else
                {
                    while (word != null&&i<word.Length-1)
                    {
                        i++;
                        mat[x.I+i-1,13-x.J] = word.Substring(i, 1);
                    }
                }
            }
            return mat;
        }
        public List<AllCrossword> GetCrosswordByIdDAL(int id)
        {
            List <AllCrossword>l= DB.AllCrosswords.Where(x => x.CrosswordCode == id).ToList();
            return l;
        }
    }
}
