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
            HouseList houses = new HouseList();
            // Перейти к странице "О доме"
            AboutHousePage aboutHouse = houses
                .OpenEditor(house)
            // Найти тестового пользователя со статусом жильца
                .FindResident(residentForAdmin)
            // Сделать пользователя председателем
                .MakeAdmin(residentForAdmin);
            // Убедиться, что кнопка поменялась на другую
            aboutHouse
                .ButtonTextCheck(residentForAdmin, "Сделать жильцом", "make");
            // Выйти из аккаунта УК
            NewsLinePage news = aboutHouse
                .TopMenu
                .Exit()
            // Авторизоваться от лица нового Председателя
                .inputLogin(residentForAdmin)
                .inputPassword(residentForAdmin)
                .SignIn();
            // Нажать "О доме"
            aboutHouse = news
                .TopMenu
                .OpenAboutHouse(house)
            // Проверить, что есть доступ к функционалу Председателя
                .SetNumberForLink(residentForAdmin.FlatNum)
                .GetLink();
        }

        void RemovingUserAdmin(PortalHomePage homePage)
        {
            var residentForKickAdmin = new Account("zhilec2", "zhilec2", "", "imonly", "test2", "8", "Председатель");
            var house = new House("TESTHOUSENEW", "", "newtesthouse", 31, "test adress");
            HouseList houses = new HouseList();
            // Перейти к странице "О доме"
            AboutHousePage aboutHouse = houses
                .OpenEditor(house)
            // Найти тестового пользователя со статусом жильца
                .FindResident(residentForKickAdmin)
            // Снять полномочия председателя
                .KickAdmin(residentForKickAdmin);
            // Убедиться, что кнопка поменялась на другую
            aboutHouse
                .ButtonTextCheck(residentForKickAdmin, "Сделать председателем", "remove");
            // Выйти из аккаунта УК
            NewsLinePage news = aboutHouse
                .TopMenu
                .Exit()
            // Авторизоваться от лица бывшего председателя
                .inputLogin(residentForKickAdmin)
                .inputPassword(residentForKickAdmin)
                .SignIn();
            // Нажать "О доме"
            bool transerError = news
                .TopMenu
                .OpenAboutHouse(house)
            // Проверить, что нет доступа к функционалу Председателя
                .CheckTransferError();

            if (transerError) Log.Info("При переходе на статус жильца, функционал Председателя не остался");
            else Log.Error("При переходе на статус жильца, функционал Председателя остался, хотя должен пропасть");
        }
    }
}
