using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class crosswordDTO
    {
        public int Id { get; set; }

        public int CrosswordCode { get; set; }

        public int NumberLocation { get; set; }

        public int DefinitionCode { get; set; }

        public bool Across { get; set; }

        public bool Down { get; set; }

        public int I { get; set; }

        public int J { get; set; }

        public int AmountLetters { get; set; }

        public string Solve { get; set; }
        //public string wholeCrossword { get; set; }

        public string DefinitionName { get; set; }
    }
}
