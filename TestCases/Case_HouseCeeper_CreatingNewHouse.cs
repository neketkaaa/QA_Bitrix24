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

        void Logging(string displayedHouse, string displayedLastName, string displayedLogin, string displayedName, House houseData, Account newResident, string displayedFlat)
        {
            if (displayedHouse != houseData.Title) Log.Error($"Название дома '{displayedHouse}' не совпало с ожидаемым '{houseData.Title}'");
            else Log.Info($"Название дома '{displayedHouse}' совпало с ожидаемым '{houseData.Title}'");

            if (displayedLastName != newResident.LastName) Log.Error($"Фамилия пользователя '{displayedLastName}' не совпала с ожидаемой '{newResident.LastName}'");
            else Log.Info($"Фамилия пользователя '{displayedLastName}' совпала с ожидаемой '{newResident.LastName}'");

            if (displayedLogin != newResident.Login) Log.Error($"Логин пользователя '{displayedLogin}' не совпал с ожидаемым '{newResident.Login}'");
            else Log.Info($"Логин пользователя '{displayedLogin}' совпал с ожидаемым '{newResident.Login}'");

            if (displayedName != newResident.Name) Log.Error($"Имя пользователя '{displayedName}' не совпало с ожидаемым '{newResident.Name}'");
            else Log.Info($"Имя пользователя '{displayedName}' совпало с ожидаемым '{newResident.Name}'");

            if (displayedFlat != newResident.FlatNum) Log.Error($"Номер квартиры пользователя '{displayedFlat}' не совпал с ожидаемым '{newResident.FlatNum}'");
            else Log.Info($"Номер квартиры пользователя '{displayedFlat}' совпал с ожидаемым '{newResident.FlatNum}'");
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
            House house = new House("Test"+currentDate, "", "test"+currentDate, 31, "test"+currentDate);
            Account resident = new Account("login"+currentDate, "login"+currentDate, currentDate+"@houseceeper.com",
                "test"+today, "test" + today, today, "");
            HouseList houseList = new HouseList();
            // Нажать "Добавить новый дом"
            AddHousePage addPage = houseList
                .AddNewHouse()
            // Нажать "добавить дом"
                .AddEmptyHouse();
            // Проверка на незаполнение полей дома - ошибка
            bool isAddErrorPresent_EmptyHouse = addPage.IsAddErrorPresent_EmptyHouse();

            if(!isAddErrorPresent_EmptyHouse)
            {
                Log.Error("Не появилась ошибка добавления дома с пустыми обязательными полями информации о Доме");
            }
            else
            {
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
                if(!isAddErrorPresent_EmptyAdmin)
                {
                    Log.Error("Не появилась ошибка добавления дома с пустыми обязательными полями информации о Председателе");
                }
                else
                {
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
                    string displayedTitle = aboutHouse.DisplayedTitle();
                    string displayedPathID = aboutHouse.DisplayedPathID();
                    string displayedNumOfFlats = aboutHouse.DisplayedNumOfFlats();
                    string displayedAddress = aboutHouse.DisplayedAddress();
                    // Проверить доступ к функционалу председателя
                    aboutHouse = aboutHouse
                        .SetNumberForLink(today)
                        .GetLink();
                    // Перейти в профиль
                    ProfilePage profile = aboutHouse
                        .TopMenu
                        .OpenProfile();
                    // Проверить сохраненные данные о пользователе
                    string displayedName = profile.DisplayedName();
                    string displayedLastName = profile.DisplayedLastName();
                    string displayedLogin = profile.DisplayedLogin();
                    string displayedHouse = profile.DisplayedHouse();
                    string displayedFlat = profile.DisplayedFlat();
                    Logging(displayedHouse, displayedLastName, displayedLogin, displayedName, house, resident, displayedFlat);
                }
            }
        }
    }
}
