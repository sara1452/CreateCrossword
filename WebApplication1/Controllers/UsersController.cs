
using BLL.FunctionBLL;
using BLL.InterfaceBLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IuserBLL u1;
        public UsersController(IuserBLL u)
        {
            u1 = u;
        }
        [HttpGet("GetAllUsers")]
        public List<userDTO> getAllUsers()
        {
            return u1.getAllBLL();
        }
        [HttpPost("addUser")]
        public int addUser(userDTO u)
        {
            return u1.addUserBLL(u);
        }
        [HttpGet("getUserByNameAndPassword/{name}/{password}")]
        public userDTO getUserByNameAndPassword(string name,string password)
        {
            return u1.getUserByNameAndPasswordBLL(name,password);
        }

    }
}
