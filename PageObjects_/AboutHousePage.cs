using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class AboutHousePage
    {
        public PortalTopMenu TopMenu => new PortalTopMenu();
        public AboutHousePage GetLink()
        {
            // получение ссылки-приглашения
            var buttonGetLink = new WebItem("//button[@onclick=\"generateLink(event)\"]", "Клик по кнопке Получить ссылку");
            buttonGetLink.Click();
            return new AboutHousePage();
        }

        public bool IsGetErrorPresent()
        {
            // появление ошибки негативной проверки при получении ссылки-приглашения
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
            // фактическое название дома
            var title = new WebItem("//input[@name='houseName']", "Фиксирование поля Название дома").GetAttribute("value");
            return title;
        }

        public string DisplayedPathID()
        {
            // фактичексий уникальный идентификатор дома
            var pathID = new WebItem("//input[@name='uniquePath']", "Фиксирование поля Уникальный идентификатор дом").GetAttribute("value");
            return pathID.Trim();
        }

        public string DisplayedNumOfFlats()
        {
            // фактическое число квартир в доме
            var numOfFlats = new WebItem("//input[@name='numberOfApart']", "Фиксирование поля Количество квартир дома").GetAttribute("value");
            return numOfFlats;
        }

        public string DisplayedAddress()
        {
            // фактический адрес дома
            var address = new WebItem("//input[@name='address']", "Фиксирование поля Адрес дома").GetAttribute("value");
            return address;
        }

        public AboutHousePage FindResident(Account residentForAdmin)
        {
            // поиск конкретного жильца из грида жильцов
            int listUsersPage = int.Parse(residentForAdmin.FlatNum) / 10;
            var gridArea = new WebItem("//div[@class=\"main-grid main-grid-full\"]", "Область грида со списком жильцов");
            if (gridArea.WaitElementDisplayed(10))
            {
                gridArea.Hover();
                if (listUsersPage > 1)
                    new WebItem("//a[contains(@href, 'list=page-2')]", "Навигация по гриду с жильцами").Click();
            }
            else Log.Error("Грид по списком жильцов не появился");
            return new AboutHousePage();
        }

        public AboutHousePage MakeAdmin(Account residentForAdmin)
        {
            // клик по кнопке "сделать председателем"
            new WebItem($"//*[contains(text(), '{residentForAdmin.Name + " " + residentForAdmin.LastName}')]//..//a[contains(@href, \"add-headman\")]", 
                "Клик по кнопке Сделать председателем").Click();
            return new AboutHousePage();
        }

        public void ButtonTextCheck(Account resident, string expectedText, string todo)
        {
            // содержимое кнопки назначение/снятия полномочий председателя

            string btnTxt = "";

            if (todo == "make")
            {
                btnTxt = new WebItem($"//*[contains(text(), '{resident.Name + " " + resident.LastName}')]//..//a[contains(@href, \"delete-headman\")]",
                "Кнопка снятия полномочий председателя").InnerText();
                if (btnTxt.Contains("Сделать жильцом")) Log.Info($"Кнопка изменилась на {expectedText}");
            }

            if (todo == "remove")
            {
                btnTxt = new WebItem($"//*[contains(text(), '{resident.Name + " " + resident.LastName}')]//..//a[contains(@href, \"add-headman\")]",
                "Кнопка выдачи полномочий председателя").InnerText();
                if (btnTxt.Contains("Сделать председателем")) Log.Info($"Кнопка изменилась на {expectedText}");
            }

        }

        public AboutHousePage KickAdmin(Account residentForKickAdmin)
        {
            // сделать председателя простым жильцом
            new WebItem($"//*[contains(text(), '{residentForKickAdmin.Name + " " + residentForKickAdmin.LastName}')]//..//a[contains(@href, \"delete-headman\")]",
                "Клик по кнопке Сделать жильцом").Click();
            return new AboutHousePage();
        }

        public bool CheckTransferError()
        {
            // проверка переноса прав председателя при смене "с жильца" или "на жильца"
            if (new WebItem("//input[@id=\"get-link\"]", "Ссылка приглашение").WaitElementDisplayed())
                return false; 
            else return true;
        }

        public void HouseData_Logger(House house)
        {
            // логгер проверки совпадения ожидаемых данных о доме и фактических
            string displayedTitle = DisplayedTitle();
            string displayedPathID = DisplayedPathID();
            string displayedNumOfFlats = DisplayedNumOfFlats();
            string displayedAddress = DisplayedAddress();

            if (displayedTitle != house.Title) Log.Error($"Фактическое название дома {displayedTitle} не совпало с ожидаемым {house.Title}");
            else Log.Info($"Фактическое название дома {displayedTitle} совпало с ожидаемым {house.Title}");

            if (displayedPathID != house.PathID) Log.Error($"Фактический уникальный идентификатор дома {displayedPathID} не совпал с ожидаемым {house.PathID}");
            else Log.Info($"Фактичексий идентификатор дома {displayedPathID} совпал с ожидамемым {house.PathID}");

            if (displayedNumOfFlats != house.NumberOfApartments.ToString()) 
                Log.Error($"Фактическое количество квартир в доме {displayedNumOfFlats} не совпало с ожидаемым {house.NumberOfApartments.ToString()}");
            else Log.Info($"Фактическое количество квартир в доме {displayedNumOfFlats} совпало с ожидаемым {house.NumberOfApartments.ToString()}");

            if (displayedAddress != house.Adress) Log.Error($"Фактический адрес дома {displayedAddress} не совпал с ожидаемым {house.Adress}");
            else Log.Info($"Фактический адрес дома {displayedAddress} совпал с ожидаемым {house.Adress}");
        }
    }
}
