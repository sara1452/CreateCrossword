using AutoMapper;
using BLL.InterfaceBLL;
using DAL.Functions;
using DAL.Interfaces;
using DAL.Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FunctionBLL
{
    public class userBLL : IuserBLL
    {
        Iuser u;
        IMapper m;
        CrosswordContext c;
        public userBLL(Iuser u,CrosswordContext _c)
        {
            this.u = u;
            c=_c;
            var config = new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile<mapper>();
              });
            m=config.CreateMapper();
        }
        public int addUserBLL(userDTO u1)
        {
            return u.addUser(m.Map<userDTO, User>(u1));
        }

        public List<userDTO> getAllBLL()
        {
            return m.Map<List<User>, List<userDTO>>(u.getAll());
        }

        public userDTO getUserByNameAndPasswordBLL(string name, string password)
        {
            return m.Map<User, userDTO>(u.getUserByNameAndPassword(name, password));
        }
    }
}
