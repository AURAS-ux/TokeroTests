using Microsoft.Playwright;
using TokeroTests.locators;
using TokeroTests.utils;

namespace TokeroTests.flows
{
    public class FeesAndTimings
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

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "resources", "data.resources");
            RESOURCES = utils.TokeroUtils.ReadPropertiesFromFile(filePath);

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

            await _page.GotoAsync(RESOURCES["tokeroPageEn"] + RESOURCES["feesPage"]);

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
        public async Task FeesInfoPresent()
        {
            ILocator depositsTable = _page.Locator(FeesAndTimingsLocators.feesAndTimingsDepositsTableLocator);
            ILocator rows = depositsTable.Locator("tr");
            int rowCount = await rows.CountAsync();

            bool foundEth = false, foundBtc = false, foundEgld = false, foundNexPay = false, foundEurRevolut = false, foundEurSepa = false;

            for (int i = 0; i < rowCount; i++)
            {
                ILocator row = rows.Nth(i);
                ILocator cells = row.Locator("th, td");
                int cellCount = await cells.CountAsync();

                Assert.NotZero(cellCount, "Row has no cells");
                for (int j = 0; j < cellCount; j++)
                {
                    ILocator cell = cells.Nth(j);
                    if (j != 2)
                    {
                        bool isVisible = await cell.IsVisibleAsync();
                        Assert.IsTrue(isVisible, $"Cell {i},{j} is not visible");
                        switch (cell.TextContentAsync().Result)
                        {
                            case "ETH":
                                foundEth = true;
                                break;
                            case "BTC":
                                foundBtc = true;
                                break;
                            case "EGLD":
                                foundEgld = true;
                                break;
                            case "EURO from NexPay":
                                foundNexPay = true;
                                break;
                            case "EURO from Revolut":
                                foundEurRevolut = true;
                                break;
                            case "EURO from other banks via  SEPA":
                                foundEurSepa = true;
                                break;
                        }
                    }
                }
                await TokeroUtils.HighlightAsync(row);
            }
            Assert.IsTrue(foundEth, "ETH not found in the table");
            Assert.IsTrue(foundBtc, "BTC not found in the table");
            Assert.IsTrue(foundEgld, "EGLD not found in the table");
            Assert.IsTrue(foundNexPay, "EURO from NexPay not found in the table");
            Assert.IsTrue(foundEurRevolut, "EURO from Revolut not found in the table");
            Assert.IsTrue(foundEurSepa, "EURO from other banks via SEPA not found in the table");

            await TokeroUtils.takeScreenShot("FeesInfoPresent", _page);
        }

        [Test]
        public async Task FeesAndTimingsPageExchangeTableButtonsFunctionality()
        {
            ILocator exchangeDiv = _page.Locator(FeesAndTimingsLocators.feesAndTimingExchangeTableLocator);
            ILocator eurButton = exchangeDiv.Locator(FeesAndTimingsLocators.exhangeTableEurButtonLocator);
            await eurButton.ClickAsync();

            ILocator exchangeTable = exchangeDiv.Locator(FeesAndTimingsLocators.exchangeTableLocator);
            ILocator rows = exchangeTable.Locator("tbody tr");
            int rowCount = await rows.CountAsync();

            bool canBuyAero = false, canSellAero = false,
                canBuyBTC = false, canSellBTC = false,
                cantBuyETH = false, cantSellETH = false,
                cantBuyGENNI = false, cantSellGENNI = false,
                canBuyUSDC = false, canSellUSDC = false;

            for (int i = 0; i < rowCount; i++)
            {
                ILocator row = rows.Nth(i);
                ILocator cells = row.Locator("td");
                int cellCount = await cells.CountAsync();
                Console.WriteLine($"Cells count: {cellCount}");

                Assert.NotZero(cellCount, "Row has no cells");
                await TokeroUtils.HighlightAsync(row);

                for (int j = 0; j < cellCount; j++)
                {
                    ILocator cell = cells.Nth(j);
                    string cellText = (await cell.TextContentAsync())?.Trim() ?? string.Empty;
                    Console.WriteLine(cellText);

                    if (cellText == "AERO")
                    {
                        canBuyAero = (await cells.Nth(1).TextContentAsync())?.Trim() == "Included";
                        canSellAero = (await cells.Nth(3).TextContentAsync())?.Trim() == "Included";
                    }

                    if (cellText == "BTC")
                    {
                        canBuyBTC = (await cells.Nth(1).TextContentAsync())?.Trim() == "Included";
                        canSellBTC = (await cells.Nth(3).TextContentAsync())?.Trim() == "Included";
                    }

                    if (cellText == "ETH")
                    {
                        cantBuyETH = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellETH = (await cells.Nth(3).TextContentAsync())?.Trim() == "N/A";
                    }

                    if (cellText == "GENNI")
                    {
                        cantBuyGENNI = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellGENNI = (await cells.Nth(3).TextContentAsync())?.Trim() == "N/A";
                    }

                    if (cellText == "USDC")
                    {
                        canBuyUSDC = (await cells.Nth(1).TextContentAsync())?.Trim() == "Included";
                        canSellUSDC = (await cells.Nth(3).TextContentAsync())?.Trim() == "Included";
                    }
                }
            }

            Assert.IsTrue(canBuyAero, "AERO buy fee is not included");
            Assert.IsTrue(canSellAero, "AERO sell fee is not included");
            Assert.IsTrue(canBuyBTC, "BTC buy fee is not included");
            Assert.IsTrue(canSellBTC, "BTC sell fee is not included");
            Assert.IsTrue(cantBuyETH, "ETH buy fee is included");
            Assert.IsTrue(cantSellETH, "ETH sell fee is included");
            Assert.IsTrue(cantBuyGENNI, "GENNI buy fee is included");
            Assert.IsTrue(cantSellGENNI, "GENNI sell fee is included");
            Assert.IsTrue(canBuyUSDC, "USDC buy fee is not included");
            Assert.IsTrue(canSellUSDC, "USDC sell fee is not included");

            await TokeroUtils.takeScreenShot("FeesAndTimingsPageExchangeTableButtonsFunctionalityEUR", _page);

            ILocator ronButton = exchangeDiv.Locator(FeesAndTimingsLocators.exhangeTableRONButtonLocator);
            await ronButton.ClickAsync();

            exchangeTable = exchangeDiv.Locator(FeesAndTimingsLocators.exchangeTableLocator);
            rows = exchangeTable.Locator("tbody tr");
            rowCount = await rows.CountAsync();

            bool cantBuyAeroRON = false, cantSellAeroRON = false,
                cantBuyBTCRON = false, cantSellBTCRON = false,
                cantBuyETHRON = false, cantSellETHRON = false,
                cantBuyGENNIRON = false, cantSellGENNIRON = false,
                cantBuyUSDCRON = false, cantSellUSDCRON = false;

            for (int i = 0; i < rowCount; i++)
            {
                ILocator row = rows.Nth(i);
                ILocator cells = row.Locator("td");
                int cellCount = await cells.CountAsync();
                Console.WriteLine($"Cells count: {cellCount}");

                Assert.NotZero(cellCount, "Row has no cells");
                await TokeroUtils.HighlightAsync(row);

                for (int j = 0; j < cellCount; j++)
                {
                    ILocator cell = cells.Nth(j);
                    string cellText = (await cell.TextContentAsync())?.Trim() ?? string.Empty;
                    Console.WriteLine(cellText);

                    if (cellText == "AERO")
                    {
                        cantBuyAeroRON = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellAeroRON = (await cells.Nth(3).TextContentAsync())?.Trim() == "Disabled";
                    }

                    if (cellText == "BTC")
                    {
                        cantBuyBTCRON = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellBTCRON = (await cells.Nth(3).TextContentAsync())?.Trim() == "Disabled";
                    }

                    if (cellText == "ETH")
                    {
                        cantBuyETHRON = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellETHRON = (await cells.Nth(3).TextContentAsync())?.Trim() == "Disabled";
                    }

                    if (cellText == "GENNI")
                    {
                        cantBuyGENNIRON = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellGENNIRON = (await cells.Nth(3).TextContentAsync())?.Trim() == "Disabled";
                    }

                    if (cellText == "USDC")
                    {
                        cantBuyUSDCRON = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellUSDCRON = (await cells.Nth(3).TextContentAsync())?.Trim() == "Disabled";
                    }
                }
            }

            Assert.IsTrue(cantBuyAeroRON, "AERO buy fee is not included");
            Assert.IsTrue(cantSellAeroRON, "AERO sell fee is not included");
            Assert.IsTrue(cantBuyBTCRON, "BTC buy fee is not included");
            Assert.IsTrue(cantSellBTCRON, "BTC sell fee is not included");
            Assert.IsTrue(cantBuyETHRON, "ETH buy fee is included");
            Assert.IsTrue(cantSellETHRON, "ETH sell fee is included");
            Assert.IsTrue(cantBuyGENNIRON, "GENNI buy fee is included");
            Assert.IsTrue(cantSellGENNIRON, "GENNI sell fee is included");
            Assert.IsTrue(cantBuyUSDCRON, "USDC buy fee is not included");
            Assert.IsTrue(cantSellUSDCRON, "USDC sell fee is not included");

            await TokeroUtils.takeScreenShot("FeesAndTimingsPageExchangeTableButtonsFunctionalityRON", _page);

            ILocator usdtButton = exchangeDiv.Locator(FeesAndTimingsLocators.exhangeTableUSDTButtonLocator);
            await usdtButton.ClickAsync();

            exchangeTable = exchangeDiv.Locator(FeesAndTimingsLocators.exchangeTableLocator);
            rows = exchangeTable.Locator("tbody tr");
            rowCount = await rows.CountAsync();

            bool cantBuyAeroUSD = false, cantSellAeroUSD = false,
                cantBuyBTCUSD = false, cantSellBTCUSD = false,
                cantBuyETHUSD = false, cantSellETHUSD = false,
                cantBuyGENNIUSD = false, cantSellGENNIUSD = false,
                cantBuyUSDCUSD = false, cantSellUSDCUSD = false;

            for (int i = 0; i < rowCount; i++)
            {
                ILocator row = rows.Nth(i);
                ILocator cells = row.Locator("td");
                int cellCount = await cells.CountAsync();
                Console.WriteLine($"Cells count: {cellCount}");

                Assert.NotZero(cellCount, "Row has no cells");
                await TokeroUtils.HighlightAsync(row);

                for (int j = 0; j < cellCount; j++)
                {
                    ILocator cell = cells.Nth(j);
                    string cellText = (await cell.TextContentAsync())?.Trim() ?? string.Empty;
                    Console.WriteLine(cellText);

                    if (cellText == "AERO")
                    {
                        cantBuyAeroUSD = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellAeroUSD = (await cells.Nth(3).TextContentAsync())?.Trim() == "N/A";
                    }

                    if (cellText == "BTC")
                    {
                        cantBuyBTCUSD = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellBTCUSD = (await cells.Nth(3).TextContentAsync())?.Trim() == "N/A";
                    }

                    if (cellText == "ETH")
                    {
                        cantBuyETHUSD = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellETHUSD = (await cells.Nth(3).TextContentAsync())?.Trim() == "N/A";
                    }

                    if (cellText == "GENNI")
                    {
                        cantBuyGENNIUSD = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellGENNIUSD = (await cells.Nth(3).TextContentAsync())?.Trim() == "N/A";
                    }

                    if (cellText == "USDC")
                    {
                        cantBuyUSDCUSD = (await cells.Nth(1).TextContentAsync())?.Trim() == "N/A";
                        cantSellUSDCUSD = (await cells.Nth(3).TextContentAsync())?.Trim() == "N/A";
                    }
                }
            }

            Assert.IsTrue(cantBuyAeroUSD, "AERO buy fee is not included");
            Assert.IsTrue(cantSellAeroUSD, "AERO sell fee is not included");
            Assert.IsTrue(cantBuyBTCUSD, "BTC buy fee is not included");
            Assert.IsTrue(cantSellBTCUSD, "BTC sell fee is not included");
            Assert.IsTrue(cantBuyETHUSD, "ETH buy fee is included");
            Assert.IsTrue(cantSellETHUSD, "ETH sell fee is included");
            Assert.IsTrue(cantBuyGENNIUSD, "GENNI buy fee is included");
            Assert.IsTrue(cantSellGENNIUSD, "GENNI sell fee is included");
            Assert.IsTrue(cantBuyUSDCUSD, "USDC buy fee is not included");
            Assert.IsTrue(cantSellUSDCUSD, "USDC sell fee is not included");

            await TokeroUtils.takeScreenShot("FeesAndTimingsPageExchangeTableButtonsFunctionalityUSD", _page);
        }

    }
}
