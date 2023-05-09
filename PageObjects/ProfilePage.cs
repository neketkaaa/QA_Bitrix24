using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.PageObjects
{
    public class ProfilePage
    {
        public string DisplayedHouse()
        {
            var house = new WebItem("//section[@class='section']//h1", "Фиксирование фактического дома регистрации").InnerText();
            house = house.Trim();
            house = house.Substring(house.IndexOf(":") + 2);
            return house;
        }

        public string DisplayedName()
        {
            var name = new WebItem("//input[@id='userName']", "Фиксирование фактического имени").GetAttribute("value");
            return name;
        }

        public string DisplayedLogin()
        {
            var login = new WebItem("//input[@id='userLogin']", "Фиксирование фактического логина").GetAttribute("value");
            return login;
        }

        public string DisplayedLastName()
        {
            var lastName = new WebItem("//input[@id='userLastName']", "Фиксирование фактического логина").GetAttribute("value");
            return lastName;
        }

        public string DisplayedFlat()
        {
            var flat = new WebItem("//button[@class='button is-black disabled']", "Фиксирование фактического номера квартиы").InnerText();
            return flat;
        }
    }
}
