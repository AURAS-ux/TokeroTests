namespace TokeroTests.locators
{
    public class TokeroProPageLocators
    {
        public static string createAccountButtonLocator = "xpath=//a[contains(@href,'/register/') and text()='Create a new account']";
        public static string emailInputLocator = "xpath=//input[@type='email' and @id='email']";
        public static string passwordInputLocator = "xpath=//input[@type='password' and @id='registerPassword']";
        public static string confirmPasswordInputLocator = "xpath=//input[@type='password' and @id='confirmPassword']";
        public static string tocAcceptInputLocator = "xpath=//input[@type='checkbox' and @id='confirmTermsAndConditions']";
        public static string registerButtonLocator = "xpath=//button[@type='submit' and text()='Register']";
        public static string emptyEmailErrorLocator = "xpath=//span[@class='text-danger' and @data-testid='email-error' and text()='The field is required']";
        public static string emprtyPasswordErrorLocator = "xpath=//div[@class='text-danger' and @data-testid='password-error' and text()='The field is required']";
        public static string emptyConfirmPasswordErrorLocator = "xpath=//div[@class='text-danger' and @data-testid='confirm-passowrd-error' and text()='The field is required']";
        public static string emptyTocErrorLocator = "xpath=//span[@class='text-danger' and @data-testid='terms-and-conditions-error' and text()='The field is required']";
    }
}
