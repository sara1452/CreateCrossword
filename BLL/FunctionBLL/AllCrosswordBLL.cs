using AutoMapper;
using BLL.InterfaceBLL;
using DAL.Functions;
using DAL.Interfaces;
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
    public class AllCrosswordBLL : IAllCrosswordBLL
    {
        Icrosswords cw;
        IMapper m;
        CrosswordContext c;
        IwordAndDefinition w;
        IuserCrossword u;
        public AllCrosswordBLL(Icrosswords _cw, CrosswordContext _c, IwordAndDefinition _w)
        {
            cw= _cw;
            c= _c;
            w= _w;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<mapper>();
            });
            m = config.CreateMapper();
        }
        public int AddCrosswordBLL(crosswordDTO c1)
        {
            return cw.AddCrosswordDAL(m.Map<crosswordDTO, AllCrossword>(c1));
        }
        public List<crosswordDTO> GetAllCrosswordsBLL()
        {
            return m.Map <List<AllCrossword>, List<crosswordDTO>>(cw.GetAllCrosswords());
        }
        public List<crosswordDTO> GetCrosswordByIdBLL(int id)
        {
            List<crosswordDTO>  l=m.Map<List<AllCrossword>,List<crosswordDTO>>(cw.GetCrosswordByIdDAL(id));
            for (int i = 0; i < l.Count(); i++)
            {
                l[i].DefinitionName = w.GetAllDefinitionsDAL().FirstOrDefault(x => x.WordCode == l[i].DefinitionCode).Definition;
            }
            return l;
        }
        public List<crosswordDTO> NewCrossword(int idCrossword, int size)
        {
            //רשימה שמקבלת את כל המילים לשיבוץ
            List<WordAndDefinition> words = w.GetAllDefinitionsDAL();
            //מילוי המטריצה הסטטית בכל המילים
            staticClass.fillData(words);
            Create c1 = new Create(size);
            Random rnd = new Random();
            //הגדרת המטריצה
            Square[][] crossword = new Square[size][];
            //שליחה לפונקציה שתגדיר את המטריצה
            crossword = c1.CreateEmptyGrid(crossword, size);
            //בחירת מספר רנדומלי שיוגרל בחיפוש מילה למרכז המטריצה
            int r = rnd.Next(22);
            //הגרלת מילה רנדומלית 
            while (staticClass.mat[r, 4] == null)
                r = rnd.Next(22);
            //הגדרת מילה ראשונה לתשבץ
            string startWord = staticClass.mat[r, 4][rnd.Next(staticClass.mat[r, 4].Count())];
            //מציאת מרכז המטריצה
            int startX = size / 2;
            int startY = size / 2 + startWord.Length / 2;
            //שיבוץ המילה הראשונה במטריצה
            c1.Place(startWord, crossword, startX, startY, eDirection.Across);
            //הגדרת משתנה לפי שכיחויות"
            char chars = c1.letter(crossword);
            //לולאה שבודקת כל עוד אפשר לשבץ עוד מילים רנדומליות בתשבץ
            do
            {
                //לולאה שבודקת אם עדיין אפשר לשבץ עוד מילה בתשבץ
                while (c1.CanAddWord(crossword) && chars != '\0')
                {
                    //שליחה לפונ שבודקת איזו מילה אפשר לשבץ ומשבצת
                    crossword = c1.CheckPlace(crossword, chars);
                    //שליחה לפונקציה שתשבץ את המילה החדשה שהתקבלה במערך השכיחויות
                    chars = c1.letter(crossword);
                }
                //הגדרת רשימה שתכיל את כל תחילת המילים שאינן משמעותיות
                List<IAndJ> list = new List<IAndJ>();
                //הורדת מילים לא אמיתיות
                c1.Correct(crossword, list);
                c1.ChangeLetters(crossword, list);
                c1.SearchPlace(crossword);
                chars = c1.letter(crossword);
            } while (c1.SearchPlace(crossword));
            c1.AddBlack(crossword);
            //List<IAndJ> l = new List<IAndJ>();
            List<AllCrossword> all = new List<AllCrossword>();
            //לעשות פונקציה שמוצאת את כל המילים 
            c1.ListWords(crossword, all,idCrossword,w,cw);
            return m.Map<List<AllCrossword>,List<crosswordDTO>>(all);
        }
    }
}
