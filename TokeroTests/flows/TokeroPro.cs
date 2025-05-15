using Microsoft.Playwright;
using TokeroTests.locators;
using TokeroTests.utils;

namespace TokeroTests.flows
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class TokeroPro
    {
        private IBrowser _browser;
        private IPage _page;
        private IBrowserContext _context;
        private IPlaywright _playwright;

        public static Dictionary<String, String> RESOURCES;
        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = TokeroUtils.headless,
                SlowMo = TokeroUtils.slowMo
            });

            _context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = null // launches full screen
            });

            _page = await _context.NewPageAsync();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "resources", "data.resources");
            RESOURCES = utils.TokeroUtils.ReadPropertiesFromFile(filePath);

            await _page.GotoAsync(RESOURCES["tokeroPageEn"] + RESOURCES["proPage"]);

            ILocator cookieButton = _page.Locator("button:has-text('Accept all cookies')");

            if (await cookieButton.IsVisibleAsync())
            {
                await cookieButton.ClickAsync();
            }
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.CloseAsync();
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Test]
        public async Task CreateAccountButtonIsPresent()
        {
            ILocator createAccountButton = _page.Locator(TokeroProPageLocators.createAccountButtonLocator);
            await createAccountButton.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(createAccountButton).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(createAccountButton);
            await utils.TokeroUtils.takeScreenShot("CreateAccountButtonIsPresent", _page);
        }

        [Test]
        public async Task CreateAccountPageFieldsArePresent()
        {
            ILocator createAccountButton = _page.Locator(TokeroProPageLocators.createAccountButtonLocator);
            await createAccountButton.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(createAccountButton).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(createAccountButton);
            Task<IPage> newPageTask = _context.WaitForPageAsync();
            await createAccountButton.ClickAsync();
            IPage registerPage = await newPageTask;

            await registerPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            Assert.IsTrue(registerPage.Url.Contains("/register"), "Register page did not open as expected");


            ILocator emailInput = registerPage.Locator(TokeroProPageLocators.emailInputLocator);
            ILocator passwordInput = registerPage.Locator(TokeroProPageLocators.passwordInputLocator);
            ILocator confirmPasswordInput = registerPage.Locator(TokeroProPageLocators.confirmPasswordInputLocator);
            ILocator tocAcceptInput = registerPage.Locator(TokeroProPageLocators.tocAcceptInputLocator);
            ILocator registerButton = registerPage.Locator(TokeroProPageLocators.registerButtonLocator);

            await emailInput.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(emailInput).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(emailInput);

            await passwordInput.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(passwordInput).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(passwordInput);

            await confirmPasswordInput.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(confirmPasswordInput).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(confirmPasswordInput);

            await tocAcceptInput.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(tocAcceptInput).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(tocAcceptInput);

            await registerButton.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(registerButton).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(registerButton);

            await utils.TokeroUtils.takeScreenShot("CreateAccountPageFieldsArePresent", registerPage);
            await registerPage.CloseAsync();
        }

        [Test]
        public async Task CreateAccountPageEmptyCheck()
        {
            ILocator createAccountButton = _page.Locator(TokeroProPageLocators.createAccountButtonLocator);
            await createAccountButton.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(createAccountButton).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(createAccountButton);
            Task<IPage> newPageTask = _context.WaitForPageAsync();
            await createAccountButton.ClickAsync();
            IPage registerPage = await newPageTask;

            await registerPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            Assert.IsTrue(registerPage.Url.Contains("/register"), "Register page did not open as expected");


            ILocator emailInput = registerPage.Locator(TokeroProPageLocators.emailInputLocator);
            ILocator passwordInput = registerPage.Locator(TokeroProPageLocators.passwordInputLocator);
            ILocator confirmPasswordInput = registerPage.Locator(TokeroProPageLocators.confirmPasswordInputLocator);
            ILocator tocAcceptInput = registerPage.Locator(TokeroProPageLocators.tocAcceptInputLocator);
            ILocator registerButton = registerPage.Locator(TokeroProPageLocators.registerButtonLocator);

            await registerButton.ClickAsync();

            ILocator emptyEmailError = registerPage.Locator(TokeroProPageLocators.emptyEmailErrorLocator);
            ILocator emptyPasswordError = registerPage.Locator(TokeroProPageLocators.emprtyPasswordErrorLocator);
            ILocator emptyConfirmPasswordError = registerPage.Locator(TokeroProPageLocators.emptyConfirmPasswordErrorLocator);
            ILocator emptyTocError = registerPage.Locator(TokeroProPageLocators.emptyTocErrorLocator);

            await emptyEmailError.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(emptyEmailError).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(emptyEmailError);

            await emptyPasswordError.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(emptyPasswordError).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(emptyPasswordError);

            await emptyConfirmPasswordError.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(emptyConfirmPasswordError).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(emptyConfirmPasswordError);

            await emptyTocError.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(emptyTocError).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(emptyTocError);

            await utils.TokeroUtils.takeScreenShot("CreateAccountPageEmptyCheck", registerPage);

            await registerPage.CloseAsync();
        }
    }
}
