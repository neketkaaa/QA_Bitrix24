using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using ATframework3demo.PageObjects;
using ATframework3demo.TestEntities;
using atFrameWork2.TestEntities;
using atFrameWork2.SeleniumFramework;
using System;

namespace ATframework3demo.TestCases
{
    public class Case_HouseCeeper_AddingPost : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("Публикация поста по заявке от жильца", homePage => AddingPostByRequest(homePage)));
            caseCollection.Add(new TestCase("Публикация поста типа Обсуждение", homePage => AddingDiscussion(homePage)));
            caseCollection.Add(new TestCase("Публикация поста типа Объявление", homePage => AddingAnnouncement(homePage)));
            return caseCollection;
        }
        void Logging(PostPage postPage, Post post, Account user)
        {
            string displayedTitle = postPage.DisplayedTitle("Обсуждение");
            string displayedDescription = postPage.DisplayedDescription();
            string displayedType = postPage.DisplayedType();
            string displayedAuthor = postPage.DisplayedAuthor();

            if (displayedTitle != post.Title) Log.Error($"Заголовок поста '{displayedTitle}' не совпал с ожидаемым '{post.Title}'");
            else Log.Info($"Заголовок поста '{displayedTitle}' совпал с ожидаемым '{post.Title}'");

            if (displayedDescription != post.Description) Log.Error($"Описание поста '{displayedDescription}' не совпало с ожидаемым '{post.Description}'");
            else Log.Info($"Описание поста '{displayedDescription}' совпало с ожидаемым '{post.Description}'");

            if (displayedType != post.Type) Log.Error($"Тип поста '{displayedType}' не совпал с ожидаемым '{post.Type}'");
            else Log.Info($"Тип поста '{displayedType}' совпал с ожидаемым '{post.Type}'");

            if (displayedAuthor != user.Name + " " + user.LastName)
                Log.Error($"Имя автора поста '{displayedAuthor}' не совпало с ожидаемым '{user.Name + " " + user.LastName}'");
            else
                Log.Info($"Имя автора поста '{displayedAuthor}' совпало с ожидаемым '{user.Name + " " + user.LastName}'");

        }
        void AddingPostByRequest(PortalHomePage homePage)
        {
            var simple = new Account("simple", "simple", "", "житель", "житель", "3", "");
            var admin = new Account("testlogin", "testlogin", "", "test", "test", "5", "");
            var houseData = new House("TESTHOUSENEW", "", "newtesthouse", 31, "test adress");
            NewsLinePage news = new NewsLinePage();

            // Перейти к странице добавления поста
            EditPostCard editPostCard = news
                .TopMenu
                .OpenAddPost(houseData)
            // Нажать "Добавить пост"
                .AddEmptyPost();
            // Проверить ошибку при незаполнении
            bool isAddErrorPresent = editPostCard.isAddErrorPresent();
            if (!isAddErrorPresent)
            {
                Log.Error("Отсутствует ошибка при сохранении поста с незаполненным обязательным полем заголовка");
            }
            else
            {
                DateTime now = DateTime.Now;
                string currentDate = now.ToString("G");
                Post post = new Post(currentDate, currentDate, "Обсуждение");
                // Заполнить поле заголовка, описание
                editPostCard
                    .inputTitle(post)
                    .inputDescription(post)
                // Нажать Отправить пост на публикацию
                    .AddPostRequest();
                // Выйти из аккаунта жильца
                news = news
                    .TopMenu
                    .Exit()
                // Авторизоваться от лица Председателя
                    .inputLogin(admin)
                    .inputPassword(admin)
                    .SignIn();
                // Перейти к заявке на создание поста
                PostPage postPage = news
                    .OpenPost(post)
                // Нажать опубликовать
                    .ConfirmPost()
                // Вернуться в ленту
                    .TopMenu
                    .OpenNewsLine(houseData)
                // Открыть новый пост
                    .OpenPost(post);
                // Сверить итоговые данные
                Logging(postPage, post, simple);
                // Открыть обсуждения
                postPage = postPage
                    .TopMenu
                    .OpenDiscussions(houseData)
                // Найти и открыть пост (проверка на отображение на странице обсуждений)
                    .OpenPost(post);
                Logging(postPage, post, simple);
            }
        }

        void AddingDiscussion(PortalHomePage homePage)
        {
            var house = new House("TESTHOUSENEW", "", "newtesthouse", 31, "test adress");
            DateTime now = DateTime.Now;
            string currentDate = now.ToString("G");
            Post post = new Post(currentDate, currentDate, "Обсуждение");
            var admin = new Account("admin", "adminadmin", "", "Управляющая", "Компания", "", "");

            HouseList houseList = new HouseList();

            var finalPostPage = houseList
            // Переходим к тестовому дому
                .OpenHouse(house)
            // Переходим к странице создания поста
                .TopMenu
                .OpenAddPost(house)
            // Ввод заголовка
                .inputTitle(post)
            // Ввод содержимого
                .inputDescription(post)
            // Выбор типа поста
                .inputType(post)
            // Добавляем пост
                .AddPost()
                .TopMenu
            // Открываем ленту новостей
                .OpenNewsLine(house)
            // Переходим к тестовому посту
                .OpenPost(post);
            // Проверяем сохраненные данные
            Logging(finalPostPage, post, admin);

            // Проверить наличие функционала комментариев
             bool checkComments = finalPostPage.CheckHaveComments();
            if (!checkComments) Log.Error("На странице поста отсутствует раздел комментариев, хотя это должно быть для Обсуждений");
            else Log.Info("На странице поста присутствует раздел комментариев, как и должно быть для постов типа Обсуждение");
        }

        void AddingAnnouncement(PortalHomePage homePage)
        {
            var house = new House("TESTHOUSENEW", "", "newtesthouse", 31, "test adress");
            DateTime now = DateTime.Now;
            string currentDate = now.ToString("G");
            Post post = new Post(currentDate, currentDate, "Объявление");
            var admin = new Account("admin", "adminadmin", "", "Управляющая", "Компания", "", "");

            HouseList houseList = new HouseList();

            var finalPostPage = houseList
            // Переходим к тестовому дому
                .OpenHouse(house)
            // Переходим к странице создания поста
                .TopMenu
                .OpenAddPost(house)
            // Ввод заголовка
                .inputTitle(post)
            // Ввод содержимого
                .inputDescription(post)
            // Выбор типа поста
                .inputType(post)
            // Добавляем пост
                .AddPost()
                .TopMenu
            // Открываем ленту новостей
                .OpenNewsLine(house)
            // Переходим к тестовому посту
                .OpenPost(post);
            // Проверяем сохраненные данные
            Logging(finalPostPage, post, admin);

            // Проверить отсутствие функционала комментариев
            bool checkComments = finalPostPage.CheckHaveComments();
            if (checkComments) Log.Error("На странице поста есть раздел комментариев, а этого для Объявлений быть не должно");
            else Log.Info("На странице поста отсутствует раздел комментирования, так как тип поста Объявление");
        }
    }
}
