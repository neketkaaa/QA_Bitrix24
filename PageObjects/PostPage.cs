using atFrameWork2.BaseFramework;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using ATframework3demo.TestEntities;
using OpenQA.Selenium.DevTools.V108.Profiler;

namespace ATframework3demo.PageObjects
{
    public class PostPage
    {
        public PortalTopMenu TopMenu => new PortalTopMenu();

        public PostPage ConfirmPost()
        {
            new WebItem("//button[@class=\"button is-success \"]", "Подтвердить публикацию поста").Click();
            DriverActions.BrowserAlert(true);
            return new PostPage();
        }

         public string DisplayedTitle(string type)
        {
            string title = "";

            Waiters.WaitForCondition(() =>
            {
                title = new WebItem("//h1[@class=\"title\"]", "Фиксируем заголовок поста").InnerText();
                return title.Contains(type);    
            }, 2, 15, "Ожидание обновления формы, содержащей заголовок");

            title = title.Substring(title.IndexOf(" ") - 10);
            return title;
        }

        public string DisplayedDescription()
        {
            var description = new WebItem("//h2[@class=\"subtitle mt-5\"]", "Фиксируем описание поста").InnerText();
            return description;
        }

        public string DisplayedType()
        {
            var type = new WebItem("//h1[@class=\"title\"]", "Фиксируем тип поста").InnerText();
            type = type.Substring(0, type.IndexOf(" ") - 10).Trim();
            return type;
        }

        public string DisplayedAuthor()
        {
            var author = new WebItem("//h5[@class=\"user-info\"]", "Фиксируем автора поста").InnerText();
            author = author.Trim();
            author = author.Substring(author.IndexOf(":") + 2);
            return author;
        }

        public bool CheckHaveComments()
        {
            if (new WebItem("//h1[contains(text(), \"Комментарии\")]", "Раздел комментариев").WaitElementDisplayed())
                return true;
            else return false;
        }

        public PostPage AddComment()
        {
            new WebItem("//button[contains(text(), \"Отправить\")]", "Клик по кнопке отправки комментария").Click();
            return new PostPage();
        }

        public bool IsAddEmptyCommentErrorPresent()
        {
            string expectedError = "Не заполнено обязательное поле \"CONTENT\"";
            var errorArea = new WebItem("//div[@class=\"notification is-warning\"]", "Область вывода ошибки пустого комментария").InnerText();
            if (errorArea.Contains(expectedError)) return true;
            else return false;
        }

        public PostPage inputComment(Comment parentComment)
        {
            new WebItem("//textarea[@id=\"inputComment\"]", "Поле ввода комментария").SendKeys(parentComment.Text);
            return new PostPage();
        }

        public PostPage AddReply()
        {
            new WebItem("//button[contains(@onclick, \"replyToComment\")]", "Кнопка добавления ответа к комментарию").Click();
            return new PostPage();
        }
    }
}
