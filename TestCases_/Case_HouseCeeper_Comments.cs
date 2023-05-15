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
    public class Case_HouseCeeper_Comments : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("Добавление комментариев к Обсуждению", homePage => AddingCommentsToDiscussions(homePage)));
            return caseCollection;
        }

        void AddingCommentsToDiscussions(PortalHomePage homePage)
        {
            var admin = new Account("testlogin", "testlogin", "", "test", "test", "5", "");
            var houseData = new House("TESTHOUSENEW", "", "newtesthouse", 31, "test adress");
            NewsLinePage news = new NewsLinePage();
            DateTime now = DateTime.Now;
            string currentDate = now.ToString("G");
            Post post = new Post(currentDate, currentDate, "Обсуждение");

            // Создать обсуждение
            news = news
                .TopMenu
                .OpenAddPost(houseData)
                .inputTitle(post)
                .inputDescription(post)
                .inputType(post)
                .AddPost();
            // Перейти к разделу обсуждений
            PostPage postPage = news
                .TopMenu
                .OpenDiscussions(houseData)
            // Открыть ранее созданное обсуждение
                .OpenPost(post)
            // Попытаться добавить пустой комментарий - ошибка
                .AddComment();
            bool isAddEmptyCommentErrorPresent = postPage.IsAddEmptyCommentErrorPresent();

            Comment parentComment = new Comment(admin.Name + " " + admin.LastName, "Test" + currentDate, admin.FlatNum, 0);
            Comment childComment = new Comment(admin.Name + " " + admin.LastName, "New Test" + currentDate, admin.FlatNum, parentComment.Level + 1);
            // Ввести комментарий
            postPage
                .inputComment(parentComment)
                // Добавить комментарий
                .AddComment();
            parentComment.СommentData_Logger();
            // Добавить ответ к комментарию
            postPage
                .AddReply()
                .inputComment(childComment)
                .AddComment();
            // Проверить отображаемый номер квартиры, имя пользователя и текст комментариев
            childComment.СommentData_Logger();

            // Вывод результата негативной проверки
            if (!isAddEmptyCommentErrorPresent)
                Log.Error("Не появилась ошибка при отправке пустого комментария");
        }
    }
}