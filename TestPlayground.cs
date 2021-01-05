using PlaywrightSharp;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PlaywrightDemo
{
    public class TestPlayground : IDisposable
    {
        public IBrowser Browser;
        private string _PageUrl = "http://www.uitestingplayground.com/";
        public TestPlayground()
        {
            if (Browser == null)
            {
                Browser = Task.Run(() => GetBrowserAsync()).Result;
            }
        }

        private async Task<IBrowser> GetBrowserAsync()
        {
            await Playwright.InstallAsync();
            var playwright = await Playwright.CreateAsync();
            return await playwright.Chromium.LaunchAsync(headless: true);
        }

        public IPage page;
        public void Dispose()
        {
            page?.CloseAsync();
        }


        [Fact]
        public async Task ClickHidingButton()
        {
            var context = await Browser.NewContextAsync();
            page = await context.NewPageAsync();
            await page.GoToAsync(_PageUrl);
            await page.ClickAsync("//a[text()='Scrollbars']");
            await page.ClickAsync("#hidingButton");

        }

        [Fact]
        public async Task WaitForProgress()
        {
            var context = await Browser.NewContextAsync();
            page = await context.NewPageAsync();
            await page.GoToAsync(_PageUrl);
            await page.ClickAsync("//a[text()='Progress Bar']");
            await page.ClickAsync("#startButton");
            await page.GetInnerTextAsync("#progressBar[aria-valuenow='75']");
            await page.ClickAsync("#stopButton");
            var outcome = await page.GetInnerTextAsync("#result");

        }


        [Fact]
        public async Task DynamicTable()
        {
            var context = await Browser.NewContextAsync();
            page = await context.NewPageAsync();
            await page.GoToAsync(_PageUrl);
            await page.ClickAsync("//a[text()='Dynamic Table']");

            var columns = await page.GetInnerHtmlAsync("//span[text()='CPU']/..");
            int CPUColumnNumber = GetColumnNumber(columns);

            var ChromeCPUText = await page.GetInnerTextAsync($"//span[text()='Chrome']/../span[{CPUColumnNumber}]");
            var ComparisonText = await page.GetInnerTextAsync(".bg-warning");
            Assert.Equal(ComparisonText, $"Chrome CPU: {ChromeCPUText}");

        }

        private int GetColumnNumber(string columns)
        {
            var headings = columns.Split('>');
            for (int i = 3; i < headings.Length; i++)
            {
                if (headings[i].StartsWith("CPU"))
                {
                    return ((i - 1) / 2) + 1;
                }
            }

            return -1;
        }

    }
}
