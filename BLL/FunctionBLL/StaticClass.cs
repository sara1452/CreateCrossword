using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BLL.FunctionBLL
{
    public static class staticClass
    {
        public static List<string>[,] mat { get; set; }
        public static char[] letters { get; set; }
        public static List<string> words { get; set; }
        public static void fillData(List<WordAndDefinition>words)
        {
            //מטריצה למילוי הנתונים של המילים 
            mat = new List<string>[27, 8];
            //קריאה מקובץ נתונים של רמת השכיחות של האותיות
            string s2 = File.ReadAllText(@"C:\\Users\\Sara\\Desktop\\שנה ב\\project\\ConsoleApp1\\ConsoleApp1\\bin\\Debug\\letters.txt", Encoding.UTF8);
            //הצבת השכיחויות לפי הסדר במערך
            string[] l = s2.Split("\r\n");
            //העברה למערך שיכיל רק את השכיחות ללא אות
            letters = new char[l.Length];
            //מעבר על כל המערך
            for (int i = 0; i < l.Length; i++)
            {
                //שיבוץ רק את האות הרלוונטית
                letters[i] = l[i][0];
            }
            //מעבר על כל רשימת המילים
            for (int i = 1; i < words.Count(); i++)
            {
                //אם המילה מכילה עד שש אותיות
                if (words[i].Word.Length <= 6)
                {
                    //תעבור על אורך המילה ותשבץ כל מילה כמות פעמים לפי אורכה
                    for (int j = 0; j < words[i].Word.Length; j++)
                    {
                        //בדיקה אם הרשימה ריקה
                        if (words[i].Word[j] <= 'ת' && words[i].Word[j] >= 'א' && mat[words[i].Word[j] - 'א', j] == null)
                            //הגדרת הרשימה
                            mat[words[i].Word[j] - 'א', j] = new List<string>();
                        //שיבוץ המילה במיקום
                        mat[words[i].Word[j] - 'א', j].Add(words[i].Word);
                    }
                }
            }
        }
    }
}