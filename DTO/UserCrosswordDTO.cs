using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserCrosswordDTO
    {
        public int CrosswordCode { get; set; }

        public string CrosswordName { get; set; } = null!;

        public int UserCode { get; set; }

        public DateTime ProductionDate { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }
    }
}
