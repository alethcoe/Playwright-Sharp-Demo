//using PlaywrightSharp;
//using System;
//using System.Threading.Tasks;
//using Xunit;

//namespace PlaywrightDemo
//{
//    public class PlaywrightTests: IDisposable
//    {
        


//        //[Fact]
//        //public async Task FirstTest()
//        //{
//        //    await Playwright.InstallAsync();
//        //    using var playwright = await Playwright.CreateAsync();
//        //    await using var browser = await playwright.Chromium.LaunchAsync(headless: false);
//        //    var page = await browser.NewPageAsync();
//        //    await page.GoToAsync("http://www.stackoverflow.com");
            
//        //}


//        public IBrowser Browser;
//        public PlaywrightTests()
//        {
//            if (Browser == null)
//            {
//                Browser = Task.Run(() => GetBrowserAsync()).Result;
//            }
//        }

//        private async Task<IBrowser> GetBrowserAsync()
//        {
//            await Playwright.InstallAsync();
//            var playwright = await Playwright.CreateAsync();
//            return await playwright.Chromium.LaunchAsync(headless: false);
//        }

//        public IPage page;
//        public void Dispose()
//        {
//            page?.CloseAsync();
//        }




//        //css selectors - https://playwright.dev/#version=v1.5.1&path=docs%2Fselectors.md&q=

//        [Fact]
//        public async Task ValidateDevelopersTitle()
//        {
//            var context = await Browser.NewContextAsync();
//            page = await context.NewPageAsync();
//            await page.GoToAsync("http://www.stackoverflow.com");
//            var developersLinkText = (await page.GetTextContentAsync("a[href=\"#for-developers\"]")).Trim();
//            Assert.Equal("For developers", developersLinkText);

//        }

//        //input options - https://playwright.dev/#version=v1.5.1&path=docs%2Finput.md&q=text-input
//        [Fact]
//        public async Task SearchCSharp()
//        {
//            var context = await Browser.NewContextAsync();
//            page = await context.NewPageAsync();
//            await page.GoToAsync("http://www.stackoverflow.com");

//            var searchBar = "input[name=\"q\"]";
//            await page.TypeAsync(searchBar, "c#");
//            await page.ClickAsync(searchBar);
//            await page.PressAsync(searchBar, "Enter");

//            var headLineText = (await page.GetTextContentAsync(".fs-headline1")).Trim();
//            Assert.Equal("Questions tagged [c#]", headLineText);

//        }


//        //notice no waits
//        //default timeout is 30 seconds but you can configure it


//        //ABORT REQUESTS
//        //https://playwright.dev/#version=v1.5.1&path=docs%2Fnetwork.md&q=

//        //stop image downloads during tests

//        [Fact]
//        public async Task StopImageDownloads()
//        {

//            var context = await Browser.NewContextAsync();
//            page = await context.NewPageAsync();
                     

//            // Abort based on the request type
  
//            await page.RouteAsync("**", (route, _) =>
//            {
//                if (route.Request.ResourceType == ResourceType.Image) {
//                    route.AbortAsync();
//                }
//                route.ContinueAsync();
//            });

//            await page.GoToAsync("http://www.stackoverflow.com");
//            var headLineText = (await page.GetTextContentAsync(".fs-headline1")).Trim();
//        }

  
//    }
//}
