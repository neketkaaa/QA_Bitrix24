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
        void AddingPostByRequest(PortalHomePage homePage)
        {
            var simpleResident = new Account("simple", "simple", "", "житель", "житель", "3", "");
            var admin = new Account("testlogin", "testlogin", "", "test", "test", "5", "");
            var houseData = new House("TESTHOUSENEW", "", "newtesthouse", 31, "test adress");
            DateTime now = DateTime.Now;
            string currentDate = now.ToString("G");
            NewsLinePage news = new NewsLinePage();

            // Перейти к странице добавления поста
            EditPostCard editPostCard = news
                .TopMenu
                .OpenAddPost(houseData)
            // Нажать "Добавить пост"
                .AddEmptyPost();
            // Проверить ошибку при незаполнении
            bool isAddErrorPresent = editPostCard.isAddErrorPresent();

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
            postPage.PostData_Logger(post, simpleResident);
            // Открыть обсуждения
            postPage = postPage
                .TopMenu
                .OpenDiscussions(houseData)
            // Найти и открыть пост (проверка на отображение на странице обсуждений)
                .OpenPost(post);
            // Вывод результата сравнения фактичексих данных с ожидаемыми в лог
            postPage.PostData_Logger(post, simpleResident);


            // Вывод результата негативной проверки в логгер
            if (!isAddErrorPresent)
                Log.Error("Отсутствует ошибка при сохранении поста с незаполненным обязательным полем заголовка");
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
            finalPostPage.PostData_Logger(post, admin);

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
            finalPostPage.PostData_Logger(post, admin);

            // Проверить отсутствие функционала комментариев
            bool checkComments = finalPostPage.CheckHaveComments();
            if (checkComments) Log.Error("На странице поста есть раздел комментариев, а этого для Объявлений быть не должно");
            else Log.Info("На странице поста отсутствует раздел комментирования, так как тип поста Объявление");
        }
    }
}
