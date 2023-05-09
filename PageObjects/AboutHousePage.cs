using atFrameWork2.BaseFramework;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class AboutHousePage
    {
        public PortalTopMenu TopMenu => new PortalTopMenu();
        public AboutHousePage GetLink()
        {
            // ссылка на дом
            var buttonGetLink = new WebItem("//button[@onclick=\"generateLink(event)\"]", "Клик по кнопке Получить ссылку");
            buttonGetLink.Click();
            return new AboutHousePage();
        }

        public bool IsGetErrorPresent()
        {
            string expectedError = "Неверный номер квартиры";
            var errorForm = new WebItem("//form[@method='get']//span[@id='invite-link']", "Область ошибки получения ссылки");

            Waiters.WaitForCondition(() =>
            {
                string errorText = errorForm.GetAttribute("textContent");
                return expectedError == errorText;
            }, 2, 15, "Ожидание обновления формы, содержащей ошибку");

            return errorForm.GetAttribute("textContent") == expectedError;
        }

        public AboutHousePage SetNumberForLink(string currentDay)
        {
            // ввод номера квартиры для получения ссылки-приглашения
            new WebItem("//input[@id=\"get-link\"]", "Поле ввода номра квартиры для получения ссылки").SendKeys(currentDay);
            return new AboutHousePage();
        }

        public string SaveKeyLink()
        {
            //получение ключа из ссылки-приглашения
            string keyLink = "";
            string keyPath = "sign-up?key=";

            Waiters.WaitForCondition(() =>
            {
                keyLink = new WebItem("//form[@method='get']//span[@id='invite-link']", "Область вывода ссылки-приглашения")
                                    .GetAttribute("textContent");
                return keyLink.Contains(keyPath);
            }, 2, 15, "Ожидание обновления формы, содержащей ссылку-приглашение");

            keyLink = new WebItem("//form[@method='get']//span[@id='invite-link']", "Область вывода ссылки-приглашения")
                .GetAttribute("textContent");
            keyLink = keyLink.Split('=').Last();
            return keyLink;
        }

        public string DisplayedTitle()
        {
            var title = new WebItem("//input[@name='houseName']", "Фиксирование поля Название дома").GetAttribute("value");
            return title;
        }

        public string DisplayedPathID()
        {
            var pathID = new WebItem("//input[@name='uniquePath']", "Фиксирование поля Уникальный идентификатор дом").GetAttribute("value");
            return pathID.Trim();
        }

        public string DisplayedNumOfFlats()
        {
            var numOfFlats = new WebItem("//input[@name='numberOfApart']", "Фиксирование поля Количество квартир дома").GetAttribute("value");
            return numOfFlats;
        }

        public string DisplayedAddress()
        {
            var address = new WebItem("//input[@name='address']", "Фиксирование поля Адрес дома").GetAttribute("value");
            return address;
        }

    }
}
