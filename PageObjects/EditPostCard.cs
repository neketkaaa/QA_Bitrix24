using atFrameWork2.BaseFramework;
using atFrameWork2.SeleniumFramework;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class EditPostCard
    {
        public EditPostCard AddEmptyPost()
        {
            // добавить пустой пост для проверки ошибки
            new WebItem("//button[@type='submit']", "Отправить заявку на публикацию").Click();
            return new EditPostCard(); 
        }

        public bool isAddErrorPresent()
        {
            // проверка появления ошибки сохранения пустого поста
            string expectedError = "Заголовок не должен быть пустым";
            var errorForm = new WebItem("//div[@class=\"notification is-warning\"]", "Область ошибки отправки поста с пустым заголовком");

            Waiters.WaitForCondition(() =>
            {
                string errorText = errorForm.GetAttribute("textContent").Trim();
                return expectedError == errorText;
            }, 2, 15, "Ожидание обновления формы, содержащей ошибку");

            return errorForm.GetAttribute("textContent").Trim() == expectedError;
        }

        public EditPostCard inputTitle(Post post)
        {
            // ввод заголовка поста
            new WebItem("//input[@name=\"postCaption\"]", "Ввод заголовка поста").SendKeys(post.Title);
            return new EditPostCard();
        }

        public EditPostCard inputDescription(Post post)
        {
            // ввод описания поста
            new WebItem("//textarea[@name=\"postBody\"]", "Ввод описания поста").SendKeys(post.Description);
            return new EditPostCard();
        }

        public NewsLinePage AddPostRequest()
        {
            // отправить заявку на публикацию поста
            new WebItem("//button[@type='submit']", "Отправить заявку на публикацию").Click();
            return new NewsLinePage();
        }

        public EditPostCard inputType(Post post)
        {
            // выбрать тип поста
            if (post.Type == "Обсуждение") new WebItem("//input[@value=\"discussion\"]", "Выбор типа поста Обсуждение").Click();
            if (post.Type == "Объявление") new WebItem("//input[@value=\"announcement\"]", "Выбор типа поста Объявление").Click();
            return new EditPostCard();  
        }
        public NewsLinePage AddPost()
        {
            // добавить пост в ленту
            new WebItem("//button[@type='submit']", "Отправить заявку на публикацию").Click();
            return new NewsLinePage();
        }
    }
}
