using atFrameWork2.BaseFramework;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class AddHousePage
    {
        public AddHousePage AddEmptyHouse()
        {
            // добавить пустой дом для проверки ошибок
            new WebItem("//button[@type='submit']", "Добавить дом").Click();
            return new AddHousePage();
        }

        public bool IsAddErrorPresent_EmptyHouse()
        {
            // проверка наличия ошибок без заполнения информации о доме
            var innerText = new WebItem("//div[@class=\"errors mt-3\"]", "Область вывода ошибок информации о доме").InnerText();
            string nameError = "Не заполнено обязательное поле \"NAME\"";
            string pathError = "Не заполнено обязательное поле \"UNIQUE_PATH\"";
            string flatNumError = "Не заполнено обязательное поле \"NUMBER_OF_APARTMENT\"";
            string adress = "Значение поля \"ADDRESS\" недостаточно длинное. Минимальная длина: 1.";


            if (innerText.Contains(flatNumError) && innerText.Contains(nameError) && 
                innerText.Contains(pathError) && innerText.Contains(adress)) return true;

            return false;
        }

            public AddHousePage inputTitle(House house)
        {
            // ввод названия дома
            new WebItem("//input[@name=\"houseName\"]", "Поле Название дома").SendKeys(house.Title);
            return new AddHousePage();
        }

        public AddHousePage inputPathID(House house)
        {
            // ввод уникального идентификатора дома
            new WebItem("//input[@name='uniquePath']", "Поле Уникальный идентификатор дома").SendKeys(house.PathID);
            return new AddHousePage();
        }

        public AddHousePage inputNumOfFlats(House house)
        {
            // ввод количества квартир в доме
            new WebItem("//input[@name='numberOfApart']", "Поле Количество квартир дома").SendKeys((house.NumberOfApartments).ToString());
            return new AddHousePage();
        }

        public AddHousePage inputAddress(House house)
        {
            // ввод адреса дома
            new WebItem("//input[@name='address']", "Поле Адрес дома").SendKeys(house.Adress);
            return new AddHousePage();
        }

        public bool IsAddErrorPresent_EmptyAdmin()
        {
            // проверка наличия ошибок без заполнения информации о председателе
            var innerText = new WebItem("//div[@class=\"errors mt-3\"]", "Область вывода ошибок информации о доме").InnerText();
            string loginError = "Логин должен быть не менее 3 символов.";
            string passwdError = "Пароль должен быть не менее 6 символов длиной.";
            string emailError = "Неверный email.";
            string flatNumError = "Введите номер квартиры председателя";


            if (innerText.Contains(flatNumError) && innerText.Contains(loginError) && innerText.Contains(passwdError) && innerText.Contains(emailError)) return true;

            return false;
        }

        public AddHousePage inputName(Account resident)
        {
            // ввод имени председателя
            new WebItem("//input[@name='headmanName']", "Поле Имя Председателя").SendKeys(resident.Name);
            return new AddHousePage();
        }

        public AddHousePage inputLastName(Account resident)
        {
            // ввод фамилии председателя
            new WebItem("//input[@name='headmanLastname']", "Поле Фамилия Председателя").SendKeys(resident.LastName);
            return new AddHousePage();
        }

        public AddHousePage inputEmail(Account resident)
        {
            // ввод почты председателя
            new WebItem("//input[@name='headmanEmail']", "Поле Email Председателя").SendKeys(resident.Email);
            return new AddHousePage();
        }

        public AddHousePage inputFlatNum(Account resident)
        {
            // ввод номера квартиры председателя
            new WebItem("//input[@name='headmanApartmentNumber']", "Поле Номер квартиры Председателя").SendKeys(resident.FlatNum);
            return new AddHousePage();
        }

        public AddHousePage inputLogin(Account resident)
        {
            // ввод логина председателя
            new WebItem("//input[@name='headmanLogin']", "Поле Логин Председателя").SendKeys(resident.Login);
            return new AddHousePage();
        }

        public AddHousePage inputPassword(Account resident)
        {
            // ввод пароля председателя
            new WebItem("//input[@name='headmanPassword']", "Поле Пароль Председателя").SendKeys(resident.Password);
            return new AddHousePage();
        }

        public NewsLinePage AddHouse()
        {
            // кнопка добавить дом
            new WebItem("//button[@type='submit']", "Добавить дом").Click();
            return new NewsLinePage();
        }
    }
}
