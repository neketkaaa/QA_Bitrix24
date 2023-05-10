using atFrameWork2.BaseFramework.LogTools;
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

        public NewsLinePage OpenHouse(House house)
        {
            WebItem HouseLink = new WebItem($"//a[@href='/house/{house.PathID}']", $"Ссылка на дом {house.Title}");
            if (HouseLink.WaitElementDisplayed(10))
            {
                HouseLink.Click();
            }
            else Log.Error($"Не найдено: Дом {house.PathID} - {house.Title} не найден");
            return new NewsLinePage();
        }
    }
}
