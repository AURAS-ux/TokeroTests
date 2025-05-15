using Microsoft.Playwright;

namespace TokeroTests.utils
{
    public class TokeroUtils
    {
        public static int timeout = 10000;
        public static int slowMo = 1;
        public static bool headless = false;
        public static Dictionary<String, String> ReadPropertiesFromFile(String filePath)
        {
            Dictionary<String, String> properties = new();
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (line.Contains('='))
                {
                    string[] keyValue = line.Split(['='], 2);
                    if (keyValue.Length == 2)
                    {
                        var key = keyValue[0].Trim();
                        var value = keyValue[1].Trim();
                        properties[key] = value;
                    }
                }
            }

            return properties;
        }

        public static async Task HighlightAsync(ILocator locator)
        {
            await locator.EvaluateAsync("el => el.style.outline = '3px solid red'");
        }

        public static async Task takeScreenShot(string fileName, IPage screen)
        {
            await screen.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = $"screeenshots/{fileName}_{DateTime.Now:yyyyMMdd-HHmmss}.png",
                FullPage = false
            });
        }
    }
}
