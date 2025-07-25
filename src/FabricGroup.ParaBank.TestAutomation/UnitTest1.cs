using Microsoft.Playwright;
using FluentAssertions;

namespace FabricGroup.ParaBank.TestAutomation;

public class SmokeTests : IAsyncLifetime
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;

    [Fact]
    public async Task CanNavigateToParaBank()
    {
        // Arrange
        var page = await _browser.NewPageAsync();
        
        // Act
        await page.GotoAsync("https://parabank.parasoft.com/");
        
        // Assert
        var title = await page.TitleAsync();
        title.Should().Contain("ParaBank");
        
        await page.CloseAsync();
    }

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
    }

    public async Task DisposeAsync()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}
