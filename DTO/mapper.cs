using AutoMapper;
using DAL.Functions;
using DAL.FunctionsDAL;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class mapper:Profile
    {
        public mapper()
        {
            CreateMap<User,userDTO>();
            CreateMap<userDTO,User>();
            CreateMap<AllCrossword, crosswordDTO>()
                .ForMember(x => x.DefinitionName, map => map.MapFrom(y => y.DefinitionCodeNavigation.Definition));
                //.ForMember(x => x.DefinitionCode, map => map.MapFrom(y => y.DefinitionCodeNavigation.WordCode));
            CreateMap<crosswordDTO, AllCrossword>();
            CreateMap<CrosswordsUser,UserCrosswordDTO>();
            CreateMap<UserCrosswordDTO,CrosswordsUser>();
            CreateMap<WordAndDefinition, WordAndDefinitionDTO>();
            CreateMap<WordAndDefinitionDTO, WordAndDefinition>();
        }
    }
}
//עוד קריאה למסד נתונים לחפש את הכתובת
