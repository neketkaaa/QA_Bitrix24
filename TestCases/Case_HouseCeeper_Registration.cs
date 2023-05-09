using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using ATframework3demo.PageObjects;
using ATframework3demo.TestEntities;
using atFrameWork2.TestEntities;
using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.TestCases
{
    public class Case_HouseCeeper_Registration : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("Первичная регистрация по ссылке-приглашению", homePage => RegistrationByKey(homePage)));
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

        void RegistrationByKey(PortalHomePage homePage)
        {
            var houseData = new House("TESTHOUSENEW", "", "newtesthouse", 31, "test adress"); // тестовый дом
            HouseList houseList = new HouseList();
            DateTime now = DateTime.Now;                    //
            string currentDay = now.ToString("dd");         // подготовка к генерации тестовых данных
            currentDay = currentDay.Replace("0", "");       //
            // Перейти к Редактировать
            AboutHousePage houseCard = houseList
                .OpenEditor(houseData);
            // Нажать получить ссылку
            houseCard.GetLink();
            // Проверить без ввода - ошибка
            bool isGetErrorPresent = houseCard.IsGetErrorPresent();
            
            if (!isGetErrorPresent)
            {
                Log.Error("Не появилась ошибка при получении ссылки-приглашения без ввода номера квартиры");
            }
            else
            {   
                // Ввести номер квартиры
                string keyLink = houseCard
                    .SetNumberForLink(currentDay)
                // Нажать "Получить ссылку-приглашение"
                    .GetLink()
                // Зафиксировать ключ
                    .SaveKeyLink();
                // Выйти из аккаунта УК
                SignUpPage signUpPage = houseCard
                    .TopMenu
                    .Exit()
                // Нажать "Зарегистрироваться"
                    .OpenSignUp()
                // Нажать зарегистрироваться
                    .EmptySignUp();
                // Проверить без ввода - ошибка
                bool isEmptyKeyErrorPresent = signUpPage.IsEmptyKeyErrorPresent();

                if (!isEmptyKeyErrorPresent)
                {
                    Log.Error("Не появилась ошибка регистрации с пустым обязательным полем ключа");
                }
                else
                {
                    //  Ввести ключ
                    signUpPage = signUpPage
                        .inputKeyLink(keyLink)
                    // Нажать зарегистрироваться
                        .EmptySignUp();
                    // Проверить появление ошибок регистрации
                    bool isEmptyRequiredInfo = signUpPage.IsEmptyRequiredInfo();

                    if (!isEmptyRequiredInfo)
                    {
                        Log.Error("Не появилась ошибка заполнения обязательного поля логина");
                        Log.Error("Не появилась ошибка заполнения обязательного поля пароля");
                        Log.Error("Не появилась ошибка заполнения обязательного поля email");
                    }
                    else
                    {
                        string currentDate = now.ToString("G");             //
                        currentDate = currentDate.Replace('.', '_');        //  подготовка тестовых данных 
                        currentDate = currentDate.Replace(' ', '_');        //  
                        currentDate = currentDate.Replace(':', '_');        //

                        var newResident = new Account("login" + currentDate, "login" + currentDate,
                            currentDate + "@houseceeper.com", currentDate, currentDate, currentDay, "");

                        // Заполнить поля, ввести ключ
                        SignInPage signInPage = signUpPage
                            .inputKeyLink(keyLink)
                            .inputName(newResident)
                            .inputLastName(newResident)
                            .inputLogin(newResident)
                            .inputEmail(newResident)
                            .inputPassword(newResident)
                            // Нажать зарегистрироваться
                            .SignUp()
                            // Выйти из аккаунта
                            .TopMenu
                            .Exit();
                        // Ввести логин и пароль
                        ProfilePage profile = signInPage
                            .inputLogin(newResident)
                            .inputPassword(newResident)
                            .SignIn()
                        // Проверить сохраненные данные
                            .TopMenu
                            .OpenProfile();
                        string displayedName = profile.DisplayedName();
                        string displayedLastName = profile.DisplayedLastName();
                        string displayedLogin = profile.DisplayedLogin();
                        string displayedHouse = profile.DisplayedHouse();
                        string displayedFlat = profile.DisplayedFlat();
                        Logging(displayedHouse, displayedLastName, displayedLogin, displayedName, houseData, newResident, displayedFlat);
                    }
                }
            }
        }
    }
}
