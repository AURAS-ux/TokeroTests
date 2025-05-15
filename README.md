# TokeroTests

TokeroTests is an automated UI testing project for the Tokero web application, built using [Microsoft Playwright](https://playwright.dev/dotnet/) and NUnit. The tests are designed to validate the presence and visibility of key UI elements and flows on the Tokero main page.

## Features

- Automated browser testing using Playwright for .NET
- NUnit-based test structure with parallel execution support
- Utility methods for common actions (highlighting, screenshots, etc.)
- Resource-driven configuration for test data and URLs
- Headless and slow-motion browser options for flexible test execution

## Project Structure

TokeroTests/ ├── flows/ │   └── MainPage.cs         # Main test class for Tokero main page UI flows ├── locators/ │   └── MainPageLocators.cs # (Assumed) Locators for main page elements ├── utils/ │   └── TokeroUtils.cs      # Utility methods for Playwright and test helpers ├── resources/ │   └── data.resources      # Resource file with test data and URLs └── TokeroTests.csproj      # Project file targeting .NET 8 and C# 12


## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (required by Playwright)
- [Microsoft.Playwright](https://www.nuget.org/packages/Microsoft.Playwright)
- [NUnit](https://nunit.org/)

### Setup

1. **Install dependencies:**

   dotnet restore
   
2. **Install Playwright browsers:**

   pwsh bin/Debug/net8.0/playwright.ps1 install
   
   Or, if using the CLI:

   dotnet tool install --global Microsoft.Playwright.CLI playwright install
   
3. **Configure resources:**

   - Edit `resources/data.resources` to set the correct URLs and test data.

### Running Tests

Run all tests using the .NET CLI:

dotnet test

Tests can be run in headless or headed mode, and with slow motion, by configuring the `TokeroUtils` settings.

## Key Test Flows

- **TokeroCoursesInfoPresent:** Verifies the presence and visibility of the courses section.
- **TokeroTopCoinsInfoPresent:** Checks the top coins table and ensures it contains data.
- **TokeroComunitySaleInfoPresent:** (Commented out) Validates the community sale section.

## Utilities

- **Highlighting elements:** For visual debugging during test runs.
- **Screenshot capture:** Automatically saves screenshots for test steps.
- **Resource file loading:** Centralizes test data and URLs.

## Contributing

Contributions are welcome! Please ensure your code follows the existing style and includes appropriate tests.

## License

This project is for demonstration and internal testing purposes. Please refer to your organization's licensing policies.

---

*This README was generated based on the current codebase structure and usage patterns.*

