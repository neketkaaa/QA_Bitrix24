using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class HouseList
    {
        public static WebItem HouseLink(string houseTitle, string housePathID) =>
            new WebItem($"//a[@href='/house/{housePathID}/about']", $"Ссылка на дом {houseTitle}");

        public AboutHousePage OpenEditor(TestEntities.House houseData)
        {
            // открытие страницы информации о доме из списка домов
            HouseLink(houseData.Title, houseData.PathID).Click();
            return new AboutHousePage();
        }

        public AddHousePage AddNewHouse()
        {
           new WebItem("//a[@href='/add-house']", "Добавить новый дом").Click();
           return new AddHousePage();
        }
    }
}
