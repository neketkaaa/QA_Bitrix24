using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.TestEntities;
using OpenQA.Selenium.DevTools.V108.Profiler;

namespace ATframework3demo.PageObjects
{
    public class PostPage
    {
        public PortalTopMenu TopMenu => new PortalTopMenu();

        public PostPage ConfirmPost()
        {
            // Подтвердить публикацию поста
            new WebItem("//button[@class=\"button is-success \"]", "Подтвердить публикацию поста").Click();
            DriverActions.BrowserAlert(true);
            return new PostPage();
        }

         public string DisplayedTitle(string type)
        {
            // фактический заголовок поста
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
            // фактическое описание поста
            var description = new WebItem("//h2[@class=\"subtitle mt-5\"]", "Фиксируем описание поста").InnerText();
            return description;
        }

        public string DisplayedType()
        {
            // фактический тип поста
            var type = new WebItem("//h1[@class=\"title\"]", "Фиксируем тип поста").InnerText();
            type = type.Substring(0, type.IndexOf(" ") - 10).Trim();
            return type;
        }

        public string DisplayedAuthor()
        {
            // фактический автор поста
            var author = new WebItem("//h5[@class=\"user-info\"]", "Фиксируем автора поста").InnerText();
            author = author.Trim();
            author = author.Substring(author.IndexOf(":") + 2);
            return author;
        }

        public bool CheckHaveComments()
        {
            // проверка на содержание раздела комментариев
            if (new WebItem("//h1[contains(text(), \"Комментарии\")]", "Раздел комментариев").WaitElementDisplayed())
                return true;
            else return false;
        }

        public PostPage AddComment()
        {
            // кнопка добавить комментарий
            new WebItem("//button[contains(text(), \"Отправить\")]", "Клик по кнопке отправки комментария").Click();
            return new PostPage();
        }

        public bool IsAddEmptyCommentErrorPresent()
        {
            // проверка ошибки отправки пустого комментария
            string expectedError = "Не заполнено обязательное поле \"CONTENT\"";
            var errorArea = new WebItem("//div[@class=\"notification is-warning\"]", "Область вывода ошибки пустого комментария").InnerText();
            if (errorArea.Contains(expectedError)) return true;
            else return false;
        }

        public PostPage inputComment(Comment parentComment)
        {
            // ввести комментарий
            new WebItem("//textarea[@id=\"inputComment\"]", "Поле ввода комментария").SendKeys(parentComment.Text);
            return new PostPage();
        }

        public PostPage AddReply()
        {
            // добавить ответ к комментарию
            new WebItem("//button[contains(@onclick, \"replyToComment\")]", "Кнопка добавления ответа к комментарию").Click();
            return new PostPage();
        }

        public void PostData_Logger(Post post, Account user)
        {
            // логгер проверки совпадения фактических данных поста с ожидаемыми
            string displayedTitle = DisplayedTitle("Обсуждение");
            string displayedDescription = DisplayedDescription();
            string displayedType = DisplayedType();
            string displayedAuthor = DisplayedAuthor();

            if (displayedTitle != post.Title) 
                Log.Error($"Заголовок поста '{displayedTitle}' не совпал с ожидаемым '{post.Title}'");
            else 
                Log.Info($"Заголовок поста '{displayedTitle}' совпал с ожидаемым '{post.Title}'");

            if (displayedDescription != post.Description) Log.Error($"Описание поста '{displayedDescription}' не совпало с ожидаемым '{post.Description}'");
            else Log.Info($"Описание поста '{displayedDescription}' совпало с ожидаемым '{post.Description}'");

            if (displayedType != post.Type) Log.Error($"Тип поста '{displayedType}' не совпал с ожидаемым '{post.Type}'");
            else Log.Info($"Тип поста '{displayedType}' совпал с ожидаемым '{post.Type}'");

            if (displayedAuthor != user.Name + " " + user.LastName) 
                Log.Error($"Имя автора поста '{displayedAuthor}' не совпало с ожидаемым '{user.Name + " " + user.LastName}'");
            else
                Log.Info($"Имя автора поста '{displayedAuthor}' совпало с ожидаемым '{user.Name + " " + user.LastName}'");
        }
    }
}
