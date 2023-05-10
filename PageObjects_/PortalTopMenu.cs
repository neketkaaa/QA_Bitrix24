using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using ATframework3demo.TestEntities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.PageObjects
{
    public class PortalTopMenu
    {
        public SignInPage Exit()
        {
            // выход из аккаунта нажатием на соответствующую кнопку
            new WebItem("//a[contains(@href, \"/logout\")]", "Кнопка выхода из аккаунта").Click();
            return new SignInPage();
        }

        public ProfilePage OpenProfile()
        {
            // переход на страницу с профилем пользователя
            new WebItem("//a[@href='/profile']", "Кнопка Перейти в Профиль").Click();
            return new ProfilePage();
        }

        public EditPostCard OpenAddPost(House house)
        {
            // переход на страницу добавления нового поста
            WebItem AddPostLink = new WebItem($"//a[@href='/house/{house.PathID}/add-post']", "Открытие страницы создания постов Добавить пост");
            if (AddPostLink.WaitElementDisplayed(10))
            {
                AddPostLink.Click();
            }
            else Log.Error($"Не найдено: Кнопка добавить пост");
            return new EditPostCard();
        }

        public NewsLinePage OpenNewsLine(House house)
        {
            // переход на страницу Лента
            new WebItem($"//a[@href='/house/{house.PathID}']", "Перейти к Ленте новостей").Click();
            return new NewsLinePage();
        }

        public DiscussionsListPage OpenDiscussions(House house)
        {
            // переход на страницу Обсуждения
            new WebItem($"//a[@href='/house/{house.PathID}/discussions']", "Перейти к Ленте новостей").Click();
            return new DiscussionsListPage();
        }

        public AboutHousePage OpenAboutHouse(House house)
        {
            // переход на страницу Объявления
            new WebItem($"//a[@href='/house/{house.PathID}/about']", "Перейти к странице О доме").Click();
            return new AboutHousePage();
        }
    }
}
