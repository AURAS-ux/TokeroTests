namespace TokeroTests.locators
{
    public class FeesAndTimingsLocators
    {
        public static string feesAndTimingsPageLocator = "xpath=//h1[contains(text(),'Fees and timings')]";
        public static string feesAndTimingsDepositsTableLocator = "xpath=//h2[text()='Deposits']/following-sibling::table";
        public static string feesAndTimingExchangeTableLocator = "xpath=//h2[text()='Exchange']/following-sibling::div";
        public static string exhangeTableEurButtonLocator = "text='EUR'";
        public static string exhangeTableRONButtonLocator = "text='RON'";
        public static string exhangeTableUSDTButtonLocator = "text='USDT'";
        public static string exchangeTableLocator = "xpath=//p[@class='mud-typography mud-typography-body1']//table[1]";
    }
}
