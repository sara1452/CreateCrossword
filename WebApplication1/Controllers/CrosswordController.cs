using BLL.InterfaceBLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ExcelDataReader;
using System.Data;
using System.IO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrosswordController : ControllerBase
    {
        IAllCrosswordBLL c1;
        IwordAndDefinitionBLL b1;
        IcrosswordUserBLL u1;
        public CrosswordController(IAllCrosswordBLL c, IwordAndDefinitionBLL b, IcrosswordUserBLL u)
        {
            c1 = c;
            b1 = b;
            u1 = u;
        }
        [HttpGet("getAllCrosswords")]
        public List<crosswordDTO> getALL()
        {
            return c1.GetAllCrosswordsBLL();
        }
        [HttpGet("aaa")]
        public bool aaa()
        {

            string s1 = System.IO.File.ReadAllText(@"C:\Users\Sara\Desktop\מילים.txt", Encoding.UTF8);
            string[] arr = s1.Split('\n');
            int x = 1000;
            foreach (string s in arr)
            {
                string[] vs = s.Split('-');
                WordAndDefinitionDTO w = new WordAndDefinitionDTO();
                w.WordCode = x++;
                w.Word = vs[0];
                w.Definition = vs[1];
                b1.AddDefinitionBLL(w);
            }
            return true;
        }

        [HttpPut("newCrossword")]
        public List<crosswordDTO> NewCrossword([FromBody] UserCrosswordDTO u)
        {
            try
            {
            int code = u1.AddCrosswordByUser(u);
            return c1.NewCrossword(code, u.Length);
            }
            catch (Exception e)
            {
                return null;
            }
           
        }
        [HttpPost("update")]
        public void u()
        {
            b1.UpdateBLL();
        }
        [HttpGet("GetCrosswordById/{id}")]
        public List<crosswordDTO> GetCrosswordBLL(int id)
        {
            return c1.GetCrosswordByIdBLL(id);
        }
        [HttpGet("GetListCrosswordsByUser/{id}")]
        public List<UserCrosswordDTO> GetListCrosswordsByUser(int id)
        {
            return u1.getCrosswordsByUserBLL(id);
        }
    }
}
