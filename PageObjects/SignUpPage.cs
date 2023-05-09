using atFrameWork2.BaseFramework;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class SignUpPage
    {

        public bool IsEmptyKeyErrorPresent()
        {
            // проверка ошибки при попытке регистрации с пустыми данными
            string expectedError = "Неверный ключ";
            var errorForm = new WebItem("//div[@class=\"notification is-warning\"]", "Область ошибки неправильного ключа");

            Waiters.WaitForCondition(() =>
            {
                string errorText = errorForm.GetAttribute("textContent").Trim();
                return expectedError == errorText;
            }, 2, 15, "Ожидание обновления формы, содержащей ошибку");

            return errorForm.GetAttribute("textContent").Trim() == expectedError;
        }

        public SignUpPage EmptySignUp()
        {
            // попытка регистрации с пустыми, незаполненными полями
            new WebItem("//button[@type='submit']", "Клик по кнопке Зарегистрироваться").Click();
            return new SignUpPage();
        }

        public SignUpPage inputName(Account newResident)
        {
            // ввод имени
            new WebItem("//input[@name='firstname']", "Ввод в поле Имя").SendKeys(newResident.Name);
            return new SignUpPage();
        }

        public SignUpPage inputLastName(Account newResident)
        {
            // ввод фамилии
            new WebItem("//input[@name='lastname']", "Ввод в поле Фамилия").SendKeys(newResident.LastName);
            return new SignUpPage();
        }

        public SignUpPage inputLogin(Account newResident)
        {
            // ввод логина
            new WebItem("//input[@name='login']", "Ввод в поле Логин").SendKeys(newResident.Login);
            return new SignUpPage();
        }

        public SignUpPage inputPassword(Account newResident)
        {
            // ввод пароля
            new WebItem("//input[@name='password']", "Ввод в поле Пароль").SendKeys(newResident.Password);
            return new SignUpPage();
        }

        public SignUpPage inputEmail(Account newResident)
        {
            // ввод email
            new WebItem("//input[@name='email']", "Ввод в поле Почта").SendKeys(newResident.Email);
            return new SignUpPage();
        }

        public SignUpPage inputKeyLink(string keyLink)
        {
            // ввод ключа
            new WebItem("//input[@name='key']", "Ввод в поле ключа").SendKeys(keyLink);
            return new SignUpPage();
        }

        public NewsLinePage SignUp()
        {
            // нажатие на кнопку Зарегистрироваться
            new WebItem("//button[@type='submit']", "Клик по кнопке Зарегистрироваться").Click();
            return new NewsLinePage();
        }

        public bool IsEmptyRequiredInfo()
        {
            // проверка наличия ошибок при вводе ключа, но без ввода логина, пароля и email
            var innerText = new WebItem("//div[@class=\"errors mt-3\"]", "Область вывода ошибок Логина, Пароля и email").InnerText();
            string loginError = "Логин должен быть не менее 3 символов";
            string passwdError = "Пароль должен быть не менее 6 символов длиной";
            string emailError = "Неверный email";

            if (innerText.Contains(loginError) && innerText.Contains(passwdError) && innerText.Contains(emailError)) return true;

            return false;
        }
    }
}
