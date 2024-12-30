using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;

namespace RentForRoom.App_Start
{
    public class Processing
    {
        public static string ConvertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace(":", "").Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(" ", "-").Replace("&", "").Replace("%", "").Replace(@"\", "-").Replace("–", "-").Replace("---", "-").Replace("--", "-").Replace(".", "").Replace("+", "").Replace("\"", "").Replace("/", "").Replace("?", "").Replace(",", "").Replace("[", "").Replace("]", "").Replace("!", "").ToLower();
        }
        public static string UrlImages(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace(":", "").Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(" ", "-").Replace("&", "").Replace("%", "").Replace(@"\", "-").Replace("---", "-").Replace("--", "-").Replace("+", "").Replace("\"", "").Replace("/", "").Replace("?", "").Replace(",", "").Replace("[", "").Replace("]", "").Replace("!", "").ToLower();

        }
    }
}