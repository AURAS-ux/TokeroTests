namespace TokeroTests.locators
{
    public class MainPageLocators
    {
        public static string tokeroComunitySaleLabelLocator = "text=TOKERO Token Community Sale";
        public static string tokeroComunitySaleButtonLocator = "a:has-text('Join The Community Sale')";

        public static string tokeroCoursesLabelLocator = "text=Learn about crypto";
        public static string tokeroCoursesDescriptionLocator = "xpath=//h2[contains(text(), 'Learn about crypto')]/following-sibling::p[1]";
        public static string tokeroCoursesButtonLocator = "xpath=//a[@href='/en/academy/courses/' and contains(normalize-space(), 'Explore the courses')]";

        public static string tokeroTopCointsLabelLocator = "text=Discover the 6 coins and tokens available today on TOKERO:";
        public static string tokeroTopCointsTableLocator = "xpath=//table[contains(@class, 'coinsArea')]";
    }
}
