using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using ATframework3demo.PageObjects;
using ATframework3demo.TestEntities;
using atFrameWork2.TestEntities;
using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.TestCases
{
    public class Case_HouseCeeper_HouseHeadman : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("Назначение пользователя с правами жильца председателем",
                homePage => MakingUserAdmin(homePage)));
            caseCollection.Add(new TestCase("Снятие полномочий председателя у пользователя",
                homePage => RemovingUserAdmin(homePage)));
            return caseCollection;
        }

        void MakingUserAdmin(PortalHomePage homePage)
        {
            var residentForAdmin = new Account("zhilec", "zhilec", "", "imonly", "test", "7", "Житель");
            var house = new House("TESTHOUSENEW", "", "newtesthouse", 31, "test adress");
            // Перейти к тестовому дому
            // Нажать "О доме"
            // Найти тестового пользователя со статусом жильца
            // Сделать пользователя председателем
            // Убедиться, что кнопка поменялась на другую
            // Выйти из аккаунта УК
            // Авторизоваться от лица нового Председателя
            // Нажать "О доме"
            // Проверить, что есть доступ к функционалу Председателя
        }

        void RemovingUserAdmin(PortalHomePage homePage)
        {
            var residentForKickAdmin = new Account("zhilec2", "zhilec2", "", "imonly", "test2", "8", "Председатель");
            var house = new House("TESTHOUSENEW", "", "newtesthouse", 31, "test adress");
        }
    }
}
