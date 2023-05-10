using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.TestEntities
{
    public class Comment
    {
        public Comment(string author, string text, string authorFlats, int level)
        {
            Author = author;
            Text = text;
            AuthorFlats = authorFlats;
            Level = level;
        }
        public string Author { get; set; }
        public string Text { get; set; }
        public string AuthorFlats { get; set; }
        public int Level { get; set; }

        public string DisplayedAuthor(int lvl)
        {
            var textArea = new WebItem($"//article[contains(@class, \"lvl-{lvl}\")]//strong[@id=\"username\"]",
                "Имя автора комментария");
            if (textArea.WaitElementDisplayed())
            {
                string text = textArea.InnerText();
                return text;
            }
            return "";
        }

        public string DisplayedFlatNum(int lvl)
        {
            var textArea = new WebItem($"//article[contains(@class, \"lvl-{lvl}\")]//small",
                "Номер квартиры автора");
            string text = textArea.InnerText();
            return text;
        }

        public string DisplayedText(int lvl)
        {
            var textArea = new WebItem($"//article[@class=\"media comment mt-3 lvl-{lvl}\"]//*[contains(text(), \"Квартира\")]/../..",
                "Текст комментария");
            string text = textArea.InnerText();
            text = text.Substring(text.IndexOf("назад") + 5);
            Console.WriteLine(text);
            return text;
        }

        public void СommentData_Logger()
        {
            string displayedAuthor = DisplayedAuthor(Level);
            string displayedFltNum = DisplayedFlatNum(Level);
            string displayedText = DisplayedText(Level);

            if (displayedAuthor != Author) Log.Error($"Фактичексий автор {displayedAuthor} не совпал с ожидаемым {Author}");
            else Log.Info($"Фактичексий автор {displayedAuthor} совпал с ожидаемым {Author}");

            if (displayedFltNum.Contains(AuthorFlats)) Log.Info($"Фактический номер квартиры {displayedFltNum} совпал с ожидаемым {AuthorFlats}");
            else Log.Error($"Фактический номер квартиры {displayedFltNum} не совпал с ожидаемым {AuthorFlats}");

            if (displayedText.Contains(Text)) Log.Info($"Фактический текст комментария {displayedText} совпал с ожидаемым {Text}");
            else Log.Error($"Фактический текст комментария {displayedText} не совпал с ожидаемым {Text}");
        }
    }
}
