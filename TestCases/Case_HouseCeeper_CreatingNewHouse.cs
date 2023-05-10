using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using ATframework3demo.PageObjects;
using ATframework3demo.TestEntities;
using atFrameWork2.TestEntities;
using atFrameWork2.SeleniumFramework;


namespace ATframework3demo.TestCases
{
    public class Case_HouseCeeper_CreatingNewHouse : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("Добавление нового дома и его Председателя",
                homePage => CreatingNewHouseAndHouseAdmin(homePage)));
            return caseCollection;
        }

        void CreatingNewHouseAndHouseAdmin(PortalHomePage homePage)
        {
            DateTime now = DateTime.Now;
            string currentDate = now.ToString("G");
            currentDate = currentDate.Replace(".", "");
            currentDate = currentDate.Replace(" ", "");
            currentDate = currentDate.Replace(":", "");
            string today = now.ToString("dd");
            if (today[0] == '0') today = today.Remove('0');
            House house = new House("Test" + currentDate, "", "test" + currentDate, 31, "test" + currentDate);
            Account resident = new Account("login" + currentDate, "login" + currentDate, currentDate + "@houseceeper.com",
                "test" + today, "test" + today, today, "");
            HouseList houseList = new HouseList();

            // Нажать "Добавить новый дом"
            AddHousePage addPage = houseList
                .AddNewHouse()
            // Нажать "добавить дом"
                .AddEmptyHouse();
            // Проверка на незаполнение полей дома - ошибка
            bool isAddErrorPresent_EmptyHouse = addPage.IsAddErrorPresent_EmptyHouse();

            // Заполнить поля дома
            addPage
               .inputTitle(house)
               .inputPathID(house)
               .inputNumOfFlats(house)
               .inputAddress(house)
            // Нажать "добавить дом"
               .AddEmptyHouse();

            // Проверка на незаполнение полей председателя - ошибка
            bool isAddErrorPresent_EmptyAdmin = addPage.IsAddErrorPresent_EmptyAdmin();

            // Заполнить поля председателя
            SignInPage auth = addPage
               .inputTitle(house)
               .inputPathID(house)
               .inputNumOfFlats(house)
               .inputAddress(house)
               .inputName(resident)
               .inputLastName(resident)
               .inputEmail(resident)
               .inputFlatNum(resident)
               .inputLogin(resident)
               .inputPassword(resident)
            // Нажать "добавить дом"
                .AddHouse()
            // Выйти из аккаунта
                .TopMenu
                .Exit();
            // Авторизоваться от лица созданного Председателя
            AboutHousePage aboutHouse = auth
                .inputLogin(resident)
                .inputPassword(resident)
                .SignIn()
            // Перейти к странице "О доме"
                .TopMenu
                .OpenAboutHouse(house);
            // Проверить сохраненные данные о доме
            aboutHouse.HouseData_Logger(house);
            // Проверить доступ к функционалу председателя
            aboutHouse = aboutHouse
                .SetNumberForLink(today)
                .GetLink();
            // Перейти в профиль
            ProfilePage profile = aboutHouse
                .TopMenu
                .OpenProfile();
            // Проверить сохраненные данные о пользователе
            profile.ProfileData_Logger(house, resident);

            // Вывод отсутствия или наличия ошибок в логгер
            if (!isAddErrorPresent_EmptyHouse)
                Log.Error("Не появилась ошибка добавления дома с пустыми обязательными полями информации о Доме");
            if (!isAddErrorPresent_EmptyAdmin)
                Log.Error("Не появилась ошибка добавления дома с пустыми обязательными полями информации о Председателе");
        }
    }
}
