///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");
var platform = Argument("Platform", "AnyCPU");
var tag = Argument("Tag", "cake");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

var projects = new[] { "Corale.Colore", "Corale.Colore.Tests" };

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Restore")
    .Does(() =>
    {
        DotNetCoreRestore("src/");
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        foreach (var project in projects)
            DotNetCoreBuild($"src/{project}/{project}.csproj");
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetCoreTest($"src/Corale.Colore.Tests/Corale.Colore.Tests.csproj");
    });

Task("Publish")
    .IsDependentOn("Test")
    .Does(() =>
    {
        var settings = new DotNetCorePublishSettings
        {
            Framework = "netstandard1.6",
            Configuration = "Release",
            OutputDirectory = "./publish/",
            VersionSuffix = tag
        };

        DotNetCorePublish("src/Corale.Colore", settings);
    });

Task("Default")
    .IsDependentOn("Test");

RunTarget(target);
