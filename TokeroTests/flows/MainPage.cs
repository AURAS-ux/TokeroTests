using Microsoft.Playwright;
using TokeroTests.utils;

namespace TokeroTests.flows
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class MainPage
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
                ViewportSize = null
            });

            _page = await _context.NewPageAsync();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "resources", "data.resources");
            RESOURCES = utils.TokeroUtils.ReadPropertiesFromFile(filePath);

            await _page.GotoAsync(RESOURCES["tokeroPageEn"]);

            ILocator cookieButton = _page.Locator("button:has-text('Accept all cookies')");

            if (await cookieButton.IsVisibleAsync())
            {
                await cookieButton.ClickAsync();
            }
        }

        [TearDown]
        public async Task TearDown()
        {
            await _page.CloseAsync();
        }

        //[Test]
        public async Task TokeroComunitySaleInfoPresent()
        {
            ILocator comunitySaleLabel = _page.Locator(locators.MainPageLocators.tokeroComunitySaleLabelLocator);
            ILocator comunitySaleButton = _page.Locator(locators.MainPageLocators.tokeroComunitySaleButtonLocator);

            await comunitySaleLabel.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(comunitySaleLabel).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(comunitySaleLabel);

            await comunitySaleButton.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(comunitySaleButton).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(comunitySaleButton);

            await utils.TokeroUtils.takeScreenShot("TokeroComunitySaleInfoPresent", _page);
        }

        [Test]
        public async Task TokeroCoursesInfoPresent()
        {
            ILocator coursesLabel = _page.Locator(locators.MainPageLocators.tokeroCoursesLabelLocator);
            ILocator coursesDescription = _page.Locator(locators.MainPageLocators.tokeroCoursesDescriptionLocator);
            ILocator coursesButton = _page.Locator(locators.MainPageLocators.tokeroCoursesButtonLocator);

            await coursesLabel.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(coursesLabel).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(coursesLabel);

            await coursesDescription.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(coursesDescription).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(coursesDescription);

            await coursesButton.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(coursesButton).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(coursesButton);

            await utils.TokeroUtils.takeScreenShot("TokeroCoursesInfoPresent", _page);
        }

        [Test]
        public async Task TokeroTopCoinsInfoPresent()
        {
            ILocator coinsLabel = _page.Locator(locators.MainPageLocators.tokeroTopCointsLabelLocator);
            ILocator coinsTable = _page.Locator(locators.MainPageLocators.tokeroTopCointsTableLocator);

            await coinsLabel.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(coinsLabel).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(coinsLabel);

            await coinsTable.ScrollIntoViewIfNeededAsync();
            await Assertions.Expect(coinsTable).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = utils.TokeroUtils.timeout });
            await utils.TokeroUtils.HighlightAsync(coinsTable);

            await utils.TokeroUtils.takeScreenShot("TokeroTopCoinsInfoPresent", _page);

            ILocator coinRows = coinsTable.Locator("tbody tr");
            int rowsCount = await coinRows.CountAsync();

            Assert.Greater(rowsCount, 0, "No coins found in the table");
            await utils.TokeroUtils.takeScreenShot($"TokeroTopCoinsInfoPresent_CoinsHighlighted", _page);
        }
    }
}
