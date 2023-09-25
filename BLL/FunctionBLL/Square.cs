using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FunctionBLL
{
    public class Square
    {
        public int num { get; set; }
        public char letter { get; set; }
        public bool isWordAcross { get; set; }
        public bool isWordDown { get; set; }
        public Square()
        {
            this.num = 0;
            this.letter = '.';
            this.isWordAcross = false;
            this.isWordDown = false;
        }
        public Square(int num, char letter, bool isWordAcross, bool isWordDown)
        {
            this.num = num;
            this.letter = letter;
            this.isWordAcross = isWordAcross;
            this.isWordDown = isWordDown;
        }
    }
}
