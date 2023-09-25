using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class WordAndDefinitionDTO
    {
        public int WordCode { get; set; }

        public string Word { get; set; } = null!;

        public string Definition { get; set; }
    }
}
