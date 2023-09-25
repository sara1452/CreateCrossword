using BLL.InterfaceBLL;
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
    public enum eDirection { Across, Down, fail }
    public class Create
    {
        int size { get; set; }
        IwordAndDefinition IWord;
        Icrosswords Iall;
        mapper m;
        public Create(int size,IwordAndDefinition wo,Icrosswords _Iall)
        {
            this.size = size;
            IWord = wo;
            Iall = _Iall;
        }
        public Create(int size)
        {
            this.size = size;
        }
        public Square[][] CreateEmptyGrid(Square[][] crossword, int size)
        {
            //הגדרת המטריצה כמטריצה מסוג square

            for (int i = 0; i < size; i++)
                crossword[i] = new Square[size];
            for (int i = 0; i < crossword.Length; i++)
            {
                for (int j = 0; j < crossword[0].Length; j++)
                {
                    crossword[i][j] = new Square();
                }
            }
            return crossword;
        }
        public void Place(string word, Square[][] crossword, int x, int y, eDirection e)
        {
            //פונקציה שממקמת את המילה בתשבץ, לפני המילה ולאחריה היא משחירה

            //תנאי  הבודק האם המילה צריכה להשתבץ במאוזן
            if (e == eDirection.Across)
            {
                //בדיקה האם תחילת המילה ממוקמת בתחילת התשבץ כדי לדעת האם להשחיר לפני המילה
                if (y != size - 1)
                    //השחרת המשבצת הקודמת למילה
                    crossword[x][y + 1].letter = '#';
                //מעבר על המילה ושיבוצה במטריצת התשבץ
                for (int i = 0; i < word.Length; i++)
                    //בדיקה האם התו הוא אות
                    if (word[i] >= 'א' && word[i] <= 'ת')
                    {
                        //שיבוץ האות בתשבץ
                        crossword[x][y].letter = word[i];
                        //הגדרה ששובצה מילה במאונך במשבצת זו
                        crossword[x][y--].isWordAcross = true;
                    }
                //בדיקה שסוף המילה אינה בסוף התשבץ
                if (y >= 0)
                    //השחרת המשבצת שאחרי המילה
                    crossword[x][y].letter = '#';
            }
            //מעבר על שיבוץ מילה במאונך
            else
            {
                //בדיקה האם המילה לא משתבצת מראש התשבץ
                if (x != 0)
                    //השחרת המשבצת לפני המילה
                    crossword[x - 1][y].letter = '#';
                //מעבר על המילה ושיבוצה במטריצת התשבץ
                for (int i = 0; i < word.Length; i++)
                    //בדיקה האם התו הוא אות
                    if (word[i] >= 'א' && word[i] <= 'ת')
                    {
                        //הצבת התו במטריצת התשבץ
                        crossword[x][y].letter = word[i];
                        //הגדרת המשבצת כמשבצת ששובצה בה מילה לאורך
                        crossword[x++][y].isWordDown = true;
                    }
                //בדיקה האם סוף המילה אינה בסוף התשבץ
                if (x <= size - 1)
                    //השחרת המשבצת שלאחר המילה
                    crossword[x][y].letter = '#';
            }
        }
        public bool SearchPlace(Square[][] crossword)
        {
            Random r = new Random();
            //פונ שבודקת האם אפשר לשבץ עוד מילה רנדומלית
            //בתנאי שיש 2 שורות רצופות ולכל אחת מהן 3 משבצות 

            //מעבר על אורך התשבץ
            for (int i = 0; i < crossword.Length; i++)
            {
                //מעבר על רוחב התשבץ
                for (int j = crossword[i].Length - 1; j > 2 && i < size-3; j--)
                {
                    //בדיקה אם 6 מקומות פנויים
                    if (crossword[i][j].letter == '.' && crossword[i][j - 1].letter == '.' && crossword[i][j - 2].letter == '.' &&
                        crossword[i][j - 3].letter == '.' && crossword[i + 1][j].letter == '.' && crossword[i + 1][j - 1].letter == '.' && crossword[i + 1][j - 2].letter == '.')
                    {
                        string word;
                        //שיבוץ מילה רנדומלית
                        do
                        {
                            word = staticClass.mat[0, 0][r.Next(staticClass.mat[0, 0].Count())];
                        } while (word.Length > 3);
                        Place(word, crossword, i, j, eDirection.Across);
                        //מחזיר תשובה שהתשבץ שונה
                        return true;
                    }
                }
            }
            //מחזיר תשובה שהתשבץ לא שונה
            return false;
        }
        public bool CanAddWord(Square[][] crossword)
        {
            //פונ שבודקת האם ניתן להוסיף עוד מילה בתשבץ
            //ע"י בדיקה האם יש לפחות 3 משבצות ריקות

            //מעבר על אורך התשבץ
            for (int i = 1; i < crossword.Length - 1; i++)
            {
                //מעבר על רוחב התשבץ
                for (int j = 1; j < crossword[i].Length - 1; j++)
                {
                    //בדיקה אם יש 3 משבצות ריקות לאורך או לרוחב
                    if ((crossword[i][j].letter == '.' && crossword[i][j + 1].letter == '.' && crossword[i][j - 1].letter == '.') ||
                        (crossword[i][j].letter == '.' && crossword[i + 1][j].letter == '.' && crossword[i - 1][j].letter == '.'))
                        //מחזיר תשובה שאפשר
                        return true;
                }
            }
            //מחזיר תשובה שאא
            return false;
        }
        public char letter(Square[][] crossword)
        {
            //פונ שבודקת את כל האותיות שאין אות לפניהן או אחריהן או מעליהן או מתחתיהן\

            //הגדרת מערך שיכיל את אותיות הא ב וכל אות הקיימת בתשבץ שאפשר לשבץ ממנה מילה תתווסף למערך זה
            char[] chars = new char[27];
            //מעבר על אורך התשבץ
            for (int i = 0; i < size; i++)
            {
                //מעבר על רוחב התשבץ
                for (int j = 0; j < size; j++)
                {
                    //בדיקה אם המשבצת מכילה אות ואם האות היא אינה חלק ממילה במאוזן או במאונך
                    if (crossword[i][j].letter >= 'א' && crossword[i][j].letter <= 'ת' && (!crossword[i][j].isWordAcross || !crossword[i][j].isWordDown))
                    {
                        //הגדרת משתנה שיכיל את המיקום של האות במערך השכיחויות
                        int y = Array.IndexOf(staticClass.letters, crossword[i][j].letter);
                        //הצבה במערך המכיל את האותיות לפי שכיחות הקיימות בתשבץ
                        chars[y] = staticClass.letters[y];
                    }
                }
            }
            //הגדרת משתנה שיעבור על העאיברים במערך השכיחויות בתשבץ
            int k = 26;
            //מעבר על מערך השכיחויות כל עוד הוא לא מכיל אות
            while (k > 0 && chars[k--] == '\0') ;
            //החזרת האות שהוחזרה- האות הפחות שכיחה
            return chars[k + 1];
        }
        public Square[][] CheckPlace(Square[][] crossword, char unCommonLetter)
        {
            //הפונ בודקת האם ניתן לשבץ מילה בתשבץ ודואגת לשיבוצה לבסוף

            //הגדרת משתנים ללולאות
            int i = 0, j = 0;
            //כמות המילים המתאימות לשיבוץ לאחר הצבת המילה
            int countWords = 0;
            //בדיקה האם אפשר לשבץ את המילה בתשבץ
            bool canPlace = false;
            //הגדר משתנה שיכיל את המילה שמסתדרת עם הכי הרבה מילים
            string bestWord = "";
            //הגדרת משתנה להשוואה על המילה הטובה ביותר
            int bestIndex = 0;
            //מטריצת עזר שתשבץ את המילה הטובה ביותר
            Square[][] bestCrossword = CreateCopyCrossword(crossword);
            //הגדרת דגל לבדיקה אם שובצה מילה
            bool flag = false;
            //יצירת העתק של התשבץ כדי להוסיף אליו את המילה החדשה שנוכל לבדוק את הצירופים
            Square[][] crossword2 = null;
            //חיפוש המיקום בתשבץ המכיל את האות הפחות שכיחה
            for (i = 0; i < size; i++)
            {
                for (j = size - 1; j >= 0; j--)
                {
                    //בדיקה האם האות בתשבץ שווה לאות הרצויה
                    
                    if (crossword[i][j].letter.Equals(unCommonLetter))
                    {
                        //בדיקה האם זו אות שאינה משובצת גם למאוזן וגם למאונך
                        if (!crossword[i][j].isWordAcross || !crossword[i][j].isWordDown)
                        {
                            //הגדרת רשימת מחרוזות שתכיל את כל המילים המתאימות להשתבץ
                            List<string> listWords = new List<string>();
                            //הצבה במשתנה את כל המילים המתאימות
                            listWords = CheckCombFromCrossword(crossword, i, j);
                            //מציאת אורך המילה המקסימלי שאפשר לשבץ
                            int maxIndex = FindStart(crossword, i, j);
                            //בדיקה על 10 מילים מי הכי מתאימה
                            for (int k = 0; k < 10; k++)
                            {
                                //הצבה במערך עזר 
                                crossword2 = CreateCopyCrossword(crossword);
                                //הגדרה של המילה שתיבחר לשיבוץ
                                string choosenWord = null;
                                if (listWords != null)
                                {
                                    //בדיקה האם יש בך שיבוץ קודם מאוזן
                                    if (crossword2[i][j].isWordDown)
                                        //מגריל מילה כדי לשבצה בתשבץ
                                        choosenWord = RandWord(listWords, unCommonLetter, maxIndex - j);
                                    else
                                     //בדיקה אם יש בך שיבוץ קודם מאונך
                                     if (crossword2[i][j].isWordAcross)
                                        //שליחה לפונ שמגרילה מילה כדי לשבצה בתשבץ
                                        choosenWord =RandWord(listWords, unCommonLetter, i - maxIndex);
                                }
                                else
                                {
                                    //אם לא הצליח תאפס את המיקום 
                                    crossword[i][j].isWordDown = true;
                                    crossword[i][j].isWordAcross = true;
                                    break;
                                }
                                //מציאת האינדקס במילה החדשה המכיל את האות שנבחרה 
                                int choosenWordi = choosenWord.IndexOf(unCommonLetter);
                                //בדיקה באיזה כיוון אפשר לשבץ את המילה
                                eDirection e = whereToPlace(crossword2, choosenWord, i, j + choosenWordi, i - choosenWordi, j);
                                //בדיקה אם אפשר לשבץ לאורך
                                if (e == eDirection.Across)
                                {
                                    canPlace = CanPlace(crossword, choosenWord, i, j + choosenWordi, eDirection.Across);
                                    if (canPlace)
                                    {
                                        Place(choosenWord, crossword2, i, j + choosenWordi, eDirection.Across);
                                        countWords = checkWords(crossword2, choosenWord, i, j + choosenWordi, eDirection.Across, unCommonLetter);
                                    }
                                }
                                //בדיקה אם כיוון השיבוץ הוא לרוחב
                                else if (e == eDirection.Down)
                                {
                                    //בדיקה אם אפשר לשבץ מילה
                                    canPlace = CanPlace(crossword, choosenWord, i - choosenWordi, j, eDirection.Down);
                                    if (canPlace)
                                    {
                                        Place(choosenWord, crossword2, i - choosenWordi, j, eDirection.Down);
                                        countWords = checkWords(crossword2, choosenWord, i - choosenWordi, j, eDirection.Down, unCommonLetter);
                                    }
                                }
                                //בדיקה אם המילה שהשתבצה יצרה יותר צירופים מהמילה הקודמת
                                if (e != eDirection.fail && countWords > bestIndex)
                                {
                                    //הצבת כמות המילים המירבית
                                    bestIndex = countWords;
                                    //התשבץ הטוב ביותר לאחר שיבוץ
                                    bestCrossword = CreateCopyCrossword(crossword2);
                                    //הצבת הדגל ששובצה מילה
                                    flag = true;
                                }
                                //בדיקה אם המילה לא יצרה כלל צירופים ולכן אין להשוות עם עוד מילים
                                if (bestIndex == 1000)
                                    break;
                                //אם הוא לא הצליח לשבץ שום מילה תצא
                                if (k == 9 && bestIndex == 0)
                                {
                                    //תגדיר כאילו שובצה מילה כי אא לשבץ שם מילה
                                    bestCrossword[i][j].isWordDown = true;
                                    bestCrossword[i][j].isWordAcross = true;
                                    flag = true;
                                }
                            }
                        }
                    }
                    if (flag == true)
                        break;
                }
                if (flag == true)
                    break;
            }
            return bestCrossword;
        }
        public string RandWord(List<string> listWords, char unCommonLetter, int maxIndex)
        {
            //פונ שמגרילה מילה רלוונטית לפי האות הפחות שכיחה שהתקבלה

            //הגדרת משתנה שיכיל את המילה
            string word = "";
            //הגדרת רשימה המכילה את כל המילים המכילות את האות הרצויה
            listWords = listWords.Where(x => x.IndexOf(unCommonLetter) <= maxIndex).ToList();
            //בדיקה אם הרשימה לא ריקה
            if (listWords.Count == 0)
                return word;
            Random random = new Random();
            //הגדרת מילה מתוך הרשימה
            word = listWords[random.Next(listWords.Count)];
            //הגדרת המיקום של האות במילה
            int r = word.IndexOf(unCommonLetter);
            //מחזיר את המילה
            return word;
        }
        public eDirection whereToPlace(Square[][] crossword, string word, int iAcross, int jAcross, int iDown, int jDown)
        {
            //פונקציה שבודקת האם לשבץ את המילה במאוזן או מאונך

            //הגדרת דגל לדעת אם המילה מתאימה
            bool flag = true;
            //בדיקה אם המילה לא חורגת מגבולות התשבץ
            if (jAcross - word.Length + 1 < 0)
                //הגדרה כמילה לא מתאימה
                flag = false;
            else
                //מעבר על המילה
                for (int i = 0; i < word.Length; i++)
                {
                    //בדיקה שהמילה יכולה להשתבץ רק במקום שיש בו נקודה או אות תואמת לאות של המילה[I]
                    if (jAcross - i < size && crossword[iAcross][jAcross - i].letter != '.' && crossword[iAcross][jAcross - i].letter != word[i])
                    {
                        flag = false;
                        break;
                    }
                }
            if (flag)
                //אם זה הצליח אז תחזיר מאוזן
                return eDirection.Across;
            else
            {
                //בדיקה שהמילה לא חורגת מגבולות המערך 
                if (iDown < 0 || iDown + word.Length - 1 >= size)
                    return eDirection.fail;
                //מעבר על המילה
                for (int i = 0; i < word.Length; i++)
                {
                    //בדיקה שהיא מתאימה
                    if (crossword[iDown + i][jDown].letter != '.' && crossword[iDown + i][jDown].letter != word[i])
                    {
                        return eDirection.fail;
                    }
                }
            }
            return eDirection.Down;

        }
        public bool CanPlace(Square[][] crossword, string word, int iStart, int jStart, eDirection e)
        {
            //פונ שבודקת האם אפשר לשבץ את המילה בתשבץ

            int i;
            //בדיקה אם המילה צריכה להשתבץ במאוזן והיא נכנסת באורך של התשבץ
            if (eDirection.Across == e && jStart - (word.Length - 1) >= 0)
            {
                if (jStart + 1 >= size)
                    return false;
                if (crossword[iStart][jStart + 1].letter > 'א' && crossword[iStart][jStart + 1].letter < 'ת')
                    return false;
                if (crossword[iStart][jStart + 1].letter != '.' && crossword[iStart][jStart + 1].letter != '#')
                    return false;
                for (i = 0; i < word.Length; i++)
                {
                    if (crossword[iStart][jStart - i].letter != '.' && crossword[iStart][jStart - i].letter != word[i])
                        return false;
                }
                if (jStart - i + 1 < 0)
                    return false;
                if (jStart - i > 0 && crossword[iStart][jStart - i].letter > 'א' && crossword[iStart][jStart - i].letter < 'ת')
                    return false;
            }
            else
            if (eDirection.Down == e && iStart + word.Length - 1 < crossword.Length)
            {
                if (iStart - 1 >= 0)
                    if (crossword[iStart - 1][jStart].letter != '.' && crossword[iStart - 1][jStart].letter != '#')
                        return false;
                for (i = 0; i < word.Length; i++)
                {
                    if (crossword[iStart + i][jStart].letter != '.' && crossword[iStart + i][jStart].letter != word[i])
                        return false;
                }
                if (iStart + word.Length < size && crossword[iStart + word.Length][jStart].letter != '.' && crossword[iStart + word.Length][jStart].letter != '#')
                    return false;
            }
            else
                return false;
            return true;
        }
        public List<string> CheckCombFromCrossword(Square[][] crossword, int i, int j)
        {
            //פונ שבודקת את הצירוף הקיים כדי לבדוק איזו מילה נוכל לשבץ שם

            //הגדרת מחרוזת שתכיל את הצירוף
            string combination = "";
            //בדיקה אם האות אינה חלק ממילה במאוזן
            if (!crossword[i][j].isWordAcross)
            {
                //בדיקה אם מוצבות אותיות סביב האות שהגיעה
                if (j > 0 && j + 1 < size && ((crossword[i][j + 1].letter >= 'א' && crossword[i][j + 1].letter <= 'ת') || (crossword[i][j - 1].letter >= 'א' && crossword[i][j - 1].letter <= 'ת')))
                    combination = TheComination(crossword, i, j);
            }
            //בדיקה האם האות אינה חלק ממילה במאונך
            else
            if (!crossword[i][j].isWordDown)
            {
                //בדיקה אם מוצבות אותיות סביב האות שהגיעה
                if (i > 0 && i + 1 < size && ((crossword[i + 1][j].letter >= 'א' && crossword[i + 1][j].letter <= 'ת') || (crossword[i - 1][j].letter >= 'א' && crossword[i - 1][j].letter <= 'ת')))
                    combination = TheComination(crossword, i, j);
            }
            if (combination == "")
                combination = crossword[i][j].letter.ToString();

            List<string> list = CheckCombination(combination);
            //שליחה לפונקציה שתחזיר את כל המילים שמכילות את האות הפחות שכיחה
            return CountCombination(list, combination);
        }
        public int FindStart(Square[][] crossword, int iStart, int jStart)
        {
            //פונקציה שבודקת עד כמה אותיות לפני האות אפשר לשבץ

            //בדיקה אם שובצה במיקום זה מילה לאורך
            if (crossword[iStart][jStart].isWordAcross == true)
            {
                //מעבר לאחור עד בהיכן פנוי
                while (iStart > 0 && crossword[--iStart][jStart].letter == '.') ;
                //תחזיר את המיקום
                return iStart;
            }
            //אם שובצה במיקום זה מילה לרוחב
            else
            {            
                //מעבר לאחור עד בהיכן פנוי
                while (jStart < 9 && crossword[iStart][++jStart].letter == '.') ;
                //תחזיר את המיקום
                return jStart;
            }
        }
        public int checkWords(Square[][] crossword2, string word, int iStart, int jStart, eDirection e, char unCommonLetter)
        {
            //מקבל תשבץ כאילו המילה השניה שובצה ובודק האם זה תיקני

            //הגדרת רשימה שמכילה את המילים הרלוונטיות
            List<string> allWords = new List<string>();
            //יצירת משתנה שיכיל את השורה או העמודה לבדוק מה האותיות שאפשר לשבץ
            List<char> newArr = new List<char>();
            bool flag = false;
            int i = 0, j = 0;
            //הגדרת משתנה שיכיל את כמות המילים שנוכל לשבץ לאחר שיבוץ המילה הנוכחית
            int count = 1000, bestCount = 1000;
            int lengthCrssword;
            if (e == eDirection.Down)
            {
                for (j = 0; j < word.Length; j++)
                {
                    //הגדרת רשימת עזר שתכיל את השורה שבה נמצאת המילה החדשה
                    newArr = new List<char>();
                    //הגדרת  רשימה שתכיל את המילים המתאימות לאותו מיקום
                    allWords = new List<string>();
                    flag = false;
                    lengthCrssword = jStart;
                    string combination = TheComination(crossword2, iStart, jStart);
                    if (combination != null && combination.Length > 1 && !crossword2[i][j].isWordAcross)
                    {
                        allWords = CheckCombination(combination);
                        count = CountCombination(allWords, combination).Count();
                    }
                    if (count < bestCount)
                        bestCount = count;
                    iStart++;
                }
                return bestCount;
            }
            else
            {
                for (i = 0; i < word.Length; i++)
                {
                    if (word[i] >= 'א' && word[i] <= 'ת')
                    {
                        //משתנה שירוץ על רוחב התשבץ
                        lengthCrssword = jStart;
                        //הגדרת רשימת עזר שתכיל את השורה שבה נמצאת המילה החדשה
                        newArr = new List<char>();
                        //הגדרת  רשימה שתכיל את המילים המתאימות לאותו מיקום
                        allWords = new List<string>();
                        flag = false;
                        string combination = TheComination(crossword2, iStart, lengthCrssword);
                        if (combination != null && combination.Length > 1 && !crossword2[i][j].isWordDown)
                        {
                            allWords = CheckCombination(combination);
                            count = CountCombination(allWords, combination).Count();
                        }
                        if (count < bestCount)
                            bestCount = count;
                        jStart--;
                    }
                }
            }
            return count;
        }
        public string TheComination(Square[][] crossword, int iStart, int jStart)
        {
            //פונ שמוצאת את הצירוף הקיים

            //הגדרת משתנה של צירוף האותיות הקיים בתשבץ כדי לבדוק אם קיימת מילה המכילה צירוף זה
            string combination = null;
            //אם הוגדרה במיקום זה מילה במאונך
            if (!crossword[iStart][jStart].isWordAcross)
            {
                //כל עוד לא שובצה אות לפניו
                while (jStart < size&& !crossword[iStart][jStart].isWordAcross && crossword[iStart][jStart].letter >= 'א'
                    && crossword[iStart][jStart++].letter <= 'ת') ;
                //משתנה שרץ על אורך התשבץ
                jStart--;
                //תחילת הצירוף יכיל את האות הראשונה של הצירוף
                combination = crossword[iStart][jStart--].letter.ToString();
                //תוסיף את המשך הצירוף
                while (jStart > 0 && crossword[iStart][jStart].letter >= 'א' &&
                    crossword[iStart][jStart].letter <= 'ת')
                    combination += crossword[iStart][jStart--].letter.ToString();
            }
            //אם המילה ששובצה היא במאוזן
            else if (!crossword[iStart][jStart].isWordDown)
            {
                //כל עוד לא שובצה אות לפניו
                while (crossword[iStart][jStart].letter >= 'א' &&
                        crossword[iStart--][jStart].letter <= 'ת' && iStart > 0) ;
                //משתנה שירוץ על אורך התשבץ
                iStart++;
                //תחילת הצירוף יכיל את האות הראשונה של הצירוף
                combination = crossword[iStart++][jStart].letter.ToString();
                //תוסיף את המשך הצירוף
                while (iStart < size && !crossword[iStart][jStart].isWordDown && crossword[iStart][jStart].letter >= 'א' &&
                    crossword[iStart][jStart].letter <= 'ת' && iStart > 0)
                    combination += crossword[iStart++][jStart].letter.ToString();
            }
            //תחזיר את הצירוף שהמילה יצרה
            return combination;
        }
        public List<string> CheckCombination(string combination)
        {
            //פונקציה שמציבה ברשימה את כל המילים המכילות את האות הפחות שכיחה 
            //ומחזירה את הרשימה 

            //הגדרת הרשימה
            List<string> finalWords = new List<string>();
            //שליחה לפונ שתביא את האות שכיחה מתוך הצירוף
            char comb = LetterFromCombination(combination);
            //מעבר על המטריצה שתביא את את כל המילים המכילות את האות הפחות שכיחה
            for (int k = 0; k < 7; k++)
            {
                //בדיקה אם הרשימה לא ריקה
                if (staticClass.mat[comb - 'א', k] != null)
                    //תוסיף את כל המילים לרשימה
                    finalWords.AddRange(staticClass.mat[comb - 'א', k]);
            }
            //תחזיר את רשימת המילים שנוצרו
            return finalWords;
        }
        public Square[][] CreateCopyCrossword(Square[][] crossword)
        {
            //פונ זו יוצרת העתק של המטריצה שקיבלה

            //הגדרת גודל התשבץ
            Square[][] copy = new Square[size][];
            //שליחה לפונקציה שתיצור מטריצה
            copy = CreateEmptyGrid(copy, size);
            //מעבר על אורך התשבץ
            for (int i = 0; i < crossword.Length; i++)
            {
                //מעבר על רוחב התשבץ
                for (int j = 0; j < crossword[0].Length; j++)
                {
                    //יצירת העתק של כל הנתונים בתשבץ
                    copy[i][j].letter = crossword[i][j].letter;
                    copy[i][j].num = crossword[i][j].num;
                    copy[i][j].isWordAcross = crossword[i][j].isWordAcross;
                    copy[i][j].isWordDown = crossword[i][j].isWordDown;
                }
            }
            return copy;
        }
        public List<string> CountCombination(List<string> lst, string combination)
        {
            //פונ שמביאה רשימה של כמות המילים המכילות את הצירוף
            lst=lst.Where(item => item.Contains(combination)).ToList();
            return lst;
        }
        public char LetterFromCombination(string combination)
        {
            //הגדרת אינדקס השכיחות
            int index;
            //הגדרת אינדקס השכיחות הטוב ביותר
            int best = 0;
            //מעבר על הצירוף שהתקבל
            for (int i = 0; i < combination.Length; i++)
            {
                //הצבת האות מהצירוף
                index = Array.IndexOf(staticClass.letters, combination[i]);
                //הצבת האינדקס הגדול ביותר כי זו האות הפחות שכיחה
                if (index > best)
                    best = index;
            }
            //תחזיר את האות הפחות שכיחה
            return staticClass.letters[best];
        }
        public void AddBlack(Square[][] crossword)
        {
            for (int i = 0; i < crossword.Length; i++)
            {
                for (int j = 0; j < crossword[i].Length; j++)
                {
                    if (crossword[i][j].letter == '.')
                        crossword[i][j].letter = '#';
                }
            }
        }
        public void Correct(Square[][] crossword, List<IAndJ> list)
        {
            //פונ שבודקת האם כל המילים בתשבץ הן מילים קיימות

            //מעבר על התשבץ לחיפוש המילים
            for (int i = 0; i < crossword.Length; i++)
            {
                for (int j = crossword[i].Length - 1; j >= 0; j--)
                {
                    //בדיקה האם התו הוא # - מכאן מתחילה מילה
                    if (crossword[i][j].letter == '#')
                    {
                        if (j > 0 &&CheckNewWord(crossword, new IAndJ(i, j - 1), eDirection.Across) == false)
                            list.Add(new IAndJ(i, j - 1));
                        if (i<size-2&&!CheckNewWord(crossword, new IAndJ(i + 1, j), eDirection.Down))
                            list.Add(new IAndJ(i + 1, j));
                    }
                }
            }
        }
        public void ChangeLetters(Square[][] crossword, List<IAndJ> list)
        {
            Square[][] crossword2 = CreateCopyCrossword(crossword);
            foreach (IAndJ item in list)
            {
                for (int i = 0; i < 27; i++)
                {
                    crossword2[item.I][item.J].letter = Convert.ToChar(i + 'א');
                    if (CheckNewWord(crossword, item, eDirection.Across) && CheckNewWord(crossword, item, eDirection.Down))
                        crossword[item.I][item.J].letter = Convert.ToChar(i + 'א');
                }
            }
        }
        public bool CheckNewWord(Square[][] crossword, IAndJ item, eDirection e)
        {
            if (crossword[item.I][item.J].letter == '#' || crossword[item.I][item.J].letter == '.')
                return true;
            if (e == eDirection.Across)
            {
                int j = item.J;
                string combination = "";
                while (j > 0 && crossword[item.I][j].letter >= 'א' && crossword[item.I][j].letter <= 'ת')
                    combination += crossword[item.I][j--].letter.ToString();
                if (!staticClass.mat[crossword[item.I][item.J].letter - 'א', 0].Contains(combination))
                    return false;
            }
            else
            {
                int i = item.I;
                if (crossword[item.I][item.J].letter == '#')
                    return true;
                string combination = "";
                while (i < 10 && crossword[i][item.J].letter >= 'א' && crossword[i][item.J].letter <= 'ת')
                    combination += crossword[i++][item.J].letter.ToString();
                if (combination==""||combination.Length <2 || !staticClass.mat[crossword[item.I][item.J].letter - 'א', 0].Contains(combination))
                    return false;
            }
            return true;
        }
        public void ListWords(Square[][] crossword, List<AllCrossword> list,int idCrossword,IwordAndDefinition IWord,Icrosswords IAll)
        {
            //פונ שבודקת האם כל המילים בתשבץ הן מילים קיימות
            //מעבר על התשבץ לחיפוש המילים

            int count = 1;
             List<AllCrossword>l= IAll.GetAllCrosswords();
            for (int i = 0; i < crossword.Length; i++)
            {
                for (int j = crossword[i].Length - 1; j >= 0; j--)
                {
                    if (crossword[i][j].letter >= 'א' && crossword[i][j].letter <= 'ת')
                    {
                        int a = CheckTheWords(crossword, i, j, eDirection.Across);
                        int d = CheckTheWords(crossword, i, j, eDirection.Down);
                        AllCrossword all = new AllCrossword();
                        string word = "";
                        if (a > 0 && d > 0)
                        {
                            for (int k = 0; k < a; k++)
                                word += crossword[i][j-k].letter.ToString();
                            all.CrosswordCode = idCrossword;
                            all.NumberLocation = count++;
                            all.DefinitionCode = IWord.GetAllDefinitionsDAL().FirstOrDefault(x => x.Word == word).WordCode;
                            all.Across = true;
                            all.Down = false;
                            all.I = i;
                            all.J = j;
                            all.AmountLetters = a;
                            all.Solve = word;
                            list.Add(all);
                            IAll.AddCrosswordDAL(all);
                            word = "";
                            for (int k = 0; k < d; k++)
                                word += crossword[i+k][j].letter.ToString();
                            all.Id = 0;
                            all.DefinitionCode = IWord.GetAllDefinitionsDAL().FirstOrDefault(x => x.Word == word).WordCode;
                            all.Across = false;
                            all.Down = true;
                            all.AmountLetters = d;
                            all.Solve = word;
                            list.Add(all);
                            IAll.AddCrosswordDAL(all);
                        }
                        else if (a > 0)
                        {
                            word = "";
                            for (int k = 0; k < a; k++)
                                word += crossword[i][j-k].letter.ToString();
                            all.CrosswordCode = idCrossword;
                            all.NumberLocation = count++;
                            all.DefinitionCode = IWord.GetAllDefinitionsDAL().FirstOrDefault(x => x.Word == word).WordCode;
                            all.Across = true;
                            all.Down = false;
                            all.I = i;
                            all.J = j;
                            all.AmountLetters = a;
                            all.Solve = word;
                            list.Add(all);
                            IAll.AddCrosswordDAL(all);
                        }
                        else if (d > 0)
                        {
                            word = "";
                            for (int k = 0; k < d; k++)
                                word += crossword[i+k][j].letter.ToString();
                            all.Id = 0;
                            all.CrosswordCode = idCrossword;
                            all.NumberLocation = count++;
                            List<WordAndDefinition> w1 = IWord.GetAllDefinitionsDAL();
                            all.DefinitionCode = w1.FirstOrDefault(x => x.Word == word).WordCode;
                            all.Across = false;
                            all.Down = true;
                            all.I= i;
                            all.J= j;
                            all.AmountLetters = d;
                            all.Solve = word;
                            list.Add(all);
                            IAll.AddCrosswordDAL(all);

                        }
                    }
                }
            }
        }
        public int CheckTheWords(Square[][]crossword,int i1,int j1,eDirection e)
        {
            //פונ שבודקת האם הצירוף הוא מילה כדי שנדע למספר

            if (e == eDirection.Across)
            {
                if (j1 == size - 1 || crossword[i1][j1 + 1].letter == '#')
                {
                    int j = j1;
                    string combination = "";
                    while (j > 0 && crossword[i1][j].letter >= 'א' && crossword[i1][j].letter <= 'ת')
                        combination += crossword[i1][j--].letter.ToString();
                    if (staticClass.mat[crossword[i1][j1].letter - 'א', 0].Contains(combination))
                        return combination.Length;
                    return 0;
                }
            }
            else if(i1==0 || crossword[i1-1][j1].letter == '#')
            {
                int i = i1;
                string combination = "";
                while (i < 10 && crossword[i][j1].letter >= 'א' && crossword[i][j1].letter <= 'ת')
                    combination += crossword[i++][j1].letter.ToString();
                if (staticClass.mat[crossword[i1][j1].letter - 'א', 0].Contains(combination))
                    return combination.Length;
            }
            return 0;
        }
        public List<AllCrossword> ListAllCrossword(Square[][] crossword, List<IAndJ> list,List<AllCrossword>all,int idCrossword)
        {
            List<WordAndDefinition> words = IWord.GetAllDefinitionsDAL();
            //int count = 0;
            List<AllCrossword> result = new List<AllCrossword>();
            foreach (IAndJ item in list)
            {
                if (crossword[item.I - 1][item.J].letter == '#')
                {
                    string word = crossword[item.I - 1][item.J].letter.ToString();
                    int i = item.I;
                    while (crossword[i][item.J].letter != '#')
                    word+=crossword[i++][item.J].letter.ToString();
                    words.FirstOrDefault(xxx => xxx.Word == word);
                    AllCrossword cc=new AllCrossword();
                    cc.CrosswordCode = idCrossword;
                    cc.NumberLocation = i++;
                    cc.Across = true;
                    cc.Down = false;
                    cc.I = item.I;
                    cc.J = item.J;
                    cc.AmountLetters = word.Length;
                    Iall.AddCrosswordDAL(cc);
                    all.Add(cc);
                }
            }
            return all;
        }
        public void AddNumbers(Square[][] crossword, List<crosswordDTO> all)
        {
            int i = 0;
            foreach (crosswordDTO item in all)
            {
                crossword[item.I][item.J].num = i;
                i++;
            }
        }
    }
}