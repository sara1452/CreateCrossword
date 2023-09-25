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
    public class WordAndDefinitionBLL : IwordAndDefinitionBLL
    {
        IwordAndDefinition w;
        IMapper m;
        CrosswordContext c;
        public WordAndDefinitionBLL(IwordAndDefinition _w,CrosswordContext _c)
        {
            this.w = _w;
            c=_c;
            var config = new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile<mapper>();
              });
            m=config.CreateMapper();
        }
        public List<WordAndDefinitionDTO> GetAllDefinitionsBLL()
        {
            return m.Map<List<WordAndDefinition>, List<WordAndDefinitionDTO>>(w.GetAllDefinitionsDAL());
        }
        public WordAndDefinitionDTO GetDefinitionByIdBLL(int id)
        {
            return m.Map<WordAndDefinition, WordAndDefinitionDTO>(w.GetDefinitionByIdDAL(id));
        }
        public int AddDefinitionBLL(WordAndDefinitionDTO w1)
        {
            return w.AddDefinitionDAL(m.Map<WordAndDefinitionDTO, WordAndDefinition>(w1));
        }

        public List<WordAndDefinitionDTO> GetAllCrosswordsBLL()
        {
            throw new NotImplementedException();
        }
        public void UpdateBLL()
        {
            List<WordAndDefinition> words = w.GetAllDefinitionsDAL();
            int fd = words.FindIndex(x => x.Word.Contains('ך'));
            for (int i = 239; i < words.Count(); i++)
            {
                string ww = "";
                if (ww != "" && ww[0] == ' ')
                    words[i].Word = ww.Substring(1);
                if (ww != "" && ww[ww.Length - 1] == ' ')
                    words[i].Word = ww.Substring(0, ww.Length - 1);
                for (int j = 0; j < words[i].Word.Length; j++)
                {
                    if (words[i].Word[j] >= 'א' && words[i].Word[j] <= 'ת')
                        ww += words[i].Word[j].ToString();
                }
                words[i].Word = ww;
                if (words[i].Word != "")
                {
                    char c = words[i].Word[words[i].Word.Length - 1];
                    if (c == 'ן' || c == 'ם' || c == 'ך' || c == 'ף' || c == 'ץ')
                    {
                        if (c == 'ם')
                            words[i].Word = ww.Substring(0, ww.Length - 1) + 'מ';
                        else
                        if (c == 'ן')
                            words[i].Word = ww.Substring(0, ww.Length - 1) + 'נ';
                        else
                        if (c == 'ך')
                            words[i].Word = ww.Substring(0, ww.Length - 1) + 'כ';
                        else
                        if (c == 'ף')
                            words[i].Word = ww.Substring(0, ww.Length - 1) + 'פ';
                        else
                        if (c == 'ץ')
                            words[i].Word = ww.Substring(0, ww.Length - 1) + 'צ';
                    }
                }
                string d = "";
                for (int j = 0; j < words[i].Definition.Length; j++)
                {
                    if ((words[i].Definition[j] >= 'א' && words[i].Definition[j] <= 'ת') || words[i].Definition[j] == ' ')
                        d += words[i].Definition[j].ToString();
                }
                words[i].Definition = d;
                bool a = w.updateDAL(words[i]);
            }
        }
    }
}

