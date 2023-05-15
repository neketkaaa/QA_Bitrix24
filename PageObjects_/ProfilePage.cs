using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class ProfilePage
    {
        public string DisplayedHouse()
        {
            // фактический дом в профиле
            var house = new WebItem("//section[@class='section']//h1", "Фиксирование фактического дома регистрации").InnerText();
            house = house.Trim();
            house = house.Substring(house.IndexOf(":") + 2);
            return house;
        }

        public string DisplayedName()
        {
            // фактическое имя в профиле
            var name = new WebItem("//input[@id='userName']", "Фиксирование фактического имени").GetAttribute("value");
            return name;
        }

        public string DisplayedLogin()
        {
            // фактический логин в профиле
            var login = new WebItem("//input[@id='userLogin']", "Фиксирование фактического логина").GetAttribute("value");
            return login;
        }

        public string DisplayedLastName()
        {
            // фактическая фамилия в профиле
            var lastName = new WebItem("//input[@id='userLastName']", "Фиксирование фактического логина").GetAttribute("value");
            return lastName;
        }

        public string DisplayedFlat()
        {
            // фактический номер квартиры в профиле
            var flat = new WebItem("//button[@class='button is-black disabled']", "Фиксирование фактического номера квартиы").InnerText();
            return flat;
        }

        public void ProfileData_Logger(House houseData, Account newResident)
        {
            // логгер сравнения фактических данных в профиле с ожидаемыми
            string displayedName = DisplayedName();
            string displayedLastName = DisplayedLastName();
            string displayedLogin = DisplayedLogin();
            string displayedHouse = DisplayedHouse();
            string displayedFlat = DisplayedFlat();

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
    }
}
