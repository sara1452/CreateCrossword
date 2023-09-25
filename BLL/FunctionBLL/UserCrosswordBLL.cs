using AutoMapper;
using BLL.InterfaceBLL;
using DAL.InterfacesDAL;
using DAL.Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FunctionBLL
{
    public class UserCrosswordBLL : IcrosswordUserBLL
    {
        IuserCrossword u;
        IMapper m;
        CrosswordContext c;
        public UserCrosswordBLL(IuserCrossword u, CrosswordContext _c)
        {
            this.u = u;
            c = _c;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<mapper>();
            });
            m = config.CreateMapper();
        }
        public int AddCrosswordByUser(UserCrosswordDTO c1)
        {
            return u.addCrossword(m.Map <UserCrosswordDTO,CrosswordsUser>(c1));
        }
        public List<UserCrosswordDTO> getCrosswordsByUserBLL(int userId)
        {
            return m.Map<List<CrosswordsUser>, List<UserCrosswordDTO>>(u.getCrosswordsByUserBLL(userId));
        }
    }
}
