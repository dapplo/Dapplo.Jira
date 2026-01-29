// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dapplo.Jira.Tests;

public class ScreenTests : TestBase
{
    public ScreenTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task TestGetAllScreensAsync()
    {
        var screens = await Client.Screen.GetAllScreensAsync(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(screens);
        Assert.NotNull(screens.Values);
        Log.Info().WriteLine("Found {0} screens", screens.Values.Count);
        
        foreach (var screen in screens.Values)
        {
            Assert.NotNull(screen.Name);
            Log.Info().WriteLine("Screen: {0} (ID: {1})", screen.Name, screen.Id);
        }
    }

    [Fact]
    public async Task TestGetScreenAsync()
    {
        // First get all screens to get a valid screen ID
        var screens = await Client.Screen.GetAllScreensAsync(cancellationToken: TestContext.Current.CancellationToken);
        
        if (screens?.Values?.Count > 0)
        {
            var firstScreen = screens.Values.First();
            
            // Get the specific screen by ID
            var screen = await Client.Screen.GetScreenAsync(firstScreen.Id, cancellationToken: TestContext.Current.CancellationToken);
            
            Assert.NotNull(screen);
            Assert.Equal(firstScreen.Name, screen.Name);
            Log.Info().WriteLine("Retrieved screen: {0}", screen.Name);
        }
        else
        {
            Log.Info().WriteLine("No screens found, skipping screen retrieval test");
        }
    }

    [Fact]
    public async Task TestGetScreenTabsAsync()
    {
        // First get all screens to get a valid screen ID
        var screens = await Client.Screen.GetAllScreensAsync(cancellationToken: TestContext.Current.CancellationToken);
        
        if (screens?.Values?.Count > 0)
        {
            var firstScreen = screens.Values.First();
            
            // Get tabs for the screen
            var tabs = await Client.Screen.GetScreenTabsAsync(firstScreen.Id, cancellationToken: TestContext.Current.CancellationToken);
            
            Assert.NotNull(tabs);
            Log.Info().WriteLine("Found {0} tabs for screen {1}", tabs.Count, firstScreen.Name);
            
            foreach (var tab in tabs)
            {
                Assert.NotNull(tab.Name);
                Log.Info().WriteLine("Tab: {0} (ID: {1})", tab.Name, tab.Id);
            }
        }
        else
        {
            Log.Info().WriteLine("No screens found, skipping tabs test");
        }
    }

    [Fact]
    public async Task TestGetScreenFieldsAsync()
    {
        // First get all screens to get a valid screen ID
        var screens = await Client.Screen.GetAllScreensAsync(cancellationToken: TestContext.Current.CancellationToken);
        
        if (screens?.Values?.Count > 0)
        {
            var firstScreen = screens.Values.First();
            
            // Get tabs for the screen
            var tabs = await Client.Screen.GetScreenTabsAsync(firstScreen.Id, cancellationToken: TestContext.Current.CancellationToken);
            
            if (tabs?.Count > 0)
            {
                var firstTab = tabs.First();
                
                // Get fields for the tab
                var fields = await Client.Screen.GetScreenFieldsAsync(firstScreen.Id, firstTab.Id, cancellationToken: TestContext.Current.CancellationToken);
                
                Assert.NotNull(fields);
                Log.Info().WriteLine("Found {0} fields for screen {1}, tab {2}", fields.Count, firstScreen.Name, firstTab.Name);
                
                foreach (var field in fields)
                {
                    Assert.NotNull(field.Id);
                    Log.Info().WriteLine("Field: {0} (ID: {1})", field.Name, field.Id);
                }
            }
            else
            {
                Log.Info().WriteLine("No tabs found for screen, skipping fields test");
            }
        }
        else
        {
            Log.Info().WriteLine("No screens found, skipping fields test");
        }
    }

    [Fact]
    public async Task TestGetAvailableScreenFieldsAsync()
    {
        // First get all screens to get a valid screen ID
        var screens = await Client.Screen.GetAllScreensAsync(cancellationToken: TestContext.Current.CancellationToken);
        
        if (screens?.Values?.Count > 0)
        {
            var firstScreen = screens.Values.First();
            
            // Get available fields for the screen
            var fields = await Client.Screen.GetAvailableScreenFieldsAsync(firstScreen.Id, cancellationToken: TestContext.Current.CancellationToken);
            
            Assert.NotNull(fields);
            Log.Info().WriteLine("Found {0} available fields for screen {1}", fields.Count, firstScreen.Name);
            
            foreach (var field in fields)
            {
                Assert.NotNull(field.Id);
                Log.Info().WriteLine("Available field: {0} (ID: {1})", field.Name, field.Id);
            }
        }
        else
        {
            Log.Info().WriteLine("No screens found, skipping available fields test");
        }
    }
}
