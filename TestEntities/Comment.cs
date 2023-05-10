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

        public string DisplayedAuthor(Comment comment)
        {
            var textArea = new WebItem($"//article[contains(@class, \"lvl-{comment.Level}\")]//strong[@id=\"username\"]",
                "Имя автора комментария");
            if (textArea.WaitElementDisplayed())
            {
                string text = textArea.InnerText();
                return text;
            }
            return "";
        }

        public string DisplayedFlatNum(Comment comment)
        {
            var textArea = new WebItem($"//article[contains(@class, \"lvl-{comment.Level}\")]//small",
                "Номер квартиры автора");
            string text = textArea.InnerText();
            return text;
        }

        public string DisplayedText(Comment comment)
        {
            var textArea = new WebItem($"//article[@class=\"media comment mt-3 lvl-{comment.Level}\"]//*[contains(text(), \"Квартира\")]/../..",
                "Текст комментария");
            string text = textArea.InnerText();
            text = text.Substring(text.IndexOf("назад") + 5);
            Console.WriteLine(text);
            return text;
        }
    }
}
