using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class SignInPage
    {
        public SignUpPage OpenSignUp()
        {
            // открытие страниы регистрации
            new WebItem("//a[@href='/sign-up']", "Клик по кноке перехода к странице Регистрации").Click();
            return new SignUpPage();
        }

        public SignInPage inputLogin(Account newResident)
        {
            // ввод логина
            new WebItem("//input[@name='login']", "Ввод в поле Логин").SendKeys(newResident.Login);
            return new SignInPage();
        }

        public SignInPage inputPassword(Account newResident)
        {
            // ввод пароля
            new WebItem("//input[@name='password']", "Ввод в поле Пароль").SendKeys(newResident.Password);
            return new SignInPage();
        }

        public NewsLinePage SignIn()
        {
            // нажатие на кнопку Авторизоваться
            new WebItem("//button[@type='submit']", "Клик по кнопке Авторизоваться").Click();
            return new NewsLinePage();
        }
    }
}
