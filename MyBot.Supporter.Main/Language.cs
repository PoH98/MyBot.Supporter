using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Net.Http;

namespace MyBot.Supporter.Main
{
    public class Languages
    {
        private static string[] LanguageText;
        public static string AddNewProfile_Button;
        public static string AdsBlock_CheckBox;
        public static string AllSelected;
        public static string Battery;
        public static string CloseRunningBot_Button;
        public static string Close_LID_CheckBox;
        public static string CopySettings_Button;
        public static string CSV_Writer_Button;
        public static string DestroyingPerformanceMode_Button;

        public static void LoadLanguage()
        {
            if (!File.Exists(Database.Location + "Text.lang"))
            {
                File.WriteAllText(Database.Location + "Text.lang ", Characters.ENText);
            }
            LanguageText = File.ReadAllLines(Database.Location + "Text.lang");
        }
        public static void TranslateLanguage(string Language)
        {
            switch (Language)
            {
                case "CN":
                    File.WriteAllText(Database.Location + "Text.lang", Characters.CNText);
                    break;
                case "EN":
                    File.WriteAllText(Database.Location + "Text.lang", Characters.ENText);
                    break;
                default:
                    Translator(Language);
                    break;

            }
        }
        private static void Translator(string languagepair)
        {
            File.WriteAllText(Database.Location + "Translate.tmp", Characters.CNText);
            string[] temp = File.ReadAllLines(Database.Location + "Translate.tmp");
            for (int x = 0; x < temp.Length; x++)
            {
                temp[x] = GoogleTranslate(temp[x],"Zh-CN|" + languagepair);
            }
            File.Delete(Database.Location + "Translate.tmp");
            File.WriteAllLines(Database.Location + "Text.lang", temp);
        }
        private static string GoogleTranslate(string input, string languagePair)
        {
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;
            result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
            result = result.Substring(result.IndexOf(">") + 1);
            result = result.Substring(0, result.IndexOf("</span>"));
            return result.Trim();
        }
    }
}
