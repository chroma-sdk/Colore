#tool "nuget:?package=GitVersion.CommandLine"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("Target", "Default");
var tag = Argument("Tag", "cake");

var configuration = HasArgument("Configuration")
    ? Argument<string>("Configuration")
    : EnvironmentVariable("CONFIGURATION") != null
        ? EnvironmentVariable("CONFIGURATION")
        : "Release";

var buildNumber = HasArgument("BuildNumber")
    ? Argument<int>("BuildNumber")
    : AppVeyor.IsRunningOnAppVeyor
        ? AppVeyor.Environment.Build.Number
        : TravisCI.IsRunningOnTravisCI
            ? TravisCI.Environment.Build.BuildNumber
            : EnvironmentVariable("BUILD_NUMBER") != null
                ? int.Parse(EnvironmentVariable("BUILD_NUMBER"))
                : 0;

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

var frameworks = new[] { "netstandard1.3", "net451" };

var version = GitVersion(new GitVersionSettings { RepositoryPath = "." });

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
    {
        CleanDirectory("./artifacts");
        CleanDirectory("./publish");
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore("src/");
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            DotNetCoreBuild(
                $"src/{project}/{project}.csproj",
                new DotNetCoreBuildSettings
                {
                    Configuration = configuration,
                    ArgumentCustomization = args => args
                        .Append($"/p:AssemblyVersion={version.AssemblySemVer}")
                        .Append($"/p:NuGetVersion={version.NuGetVersionV2}")
                });
        }
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetCoreTest(
            "src/Corale.Colore.Tests/Corale.Colore.Tests.csproj",
            new DotNetCoreTestSettings
            {
                Configuration = configuration,
                NoBuild = true,
                ArgumentCustomization = args => args
                    .Append("--logger:nunit")
            });

        var testResults = GetFiles("src/Corale.Colore.Tests/TestResults/*.xml");

        CopyFiles(testResults, "./artifacts");

        if (testResults.Count < 1)
        {
            Error("Could not find test result files.");
            return;
        }

        if (!AppVeyor.IsRunningOnAppVeyor)
            return;

        Information("Uploading test results to AppVeyor");
        foreach (var file in testResults)
        {
            Information("Uploading {0}", file);
            AppVeyor.UploadTestResults(file, AppVeyorTestResultsType.NUnit3);
        }
    });

Task("Dist")
    .IsDependentOn("Test")
    .Does(() =>
    {
        foreach (var framework in frameworks)
        {
            var dir = $"./src/Corale.Colore/bin/{configuration}/{framework}/";
            var target = $"./artifacts/colore_{version.SemVer}_{framework}_anycpu.zip";
            Information("Zipping {0} to {1}", dir, target);
            Zip(dir, target, $"{dir}**/*.*");
        }
    });

Task("Publish")
    .IsDependentOn("Test")
    .Does(() =>
    {
        foreach (var framework in frameworks)
        {
            var settings = new DotNetCorePublishSettings
            {
                Framework = framework,
                Configuration = configuration,
                OutputDirectory = $"./publish/{framework}/",
                ArgumentCustomization = args => args
                    .Append($"/p:AssemblyVersion={version.AssemblySemVer}")
                    .Append($"/p:NuGetVersion={version.NuGetVersionV2}")
            };

            DotNetCorePublish("src/Corale.Colore", settings);

            var dir = $"./publish/{framework}/";
            var target = $"./artifacts/colore_{version.SemVer}_{framework}_full.zip";
            Information("Zipping {0} to {1}", dir, target);
            Zip(dir, target, $"{dir}**/*.*");
        }
    });

Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
    {
        MoveFiles(GetFiles("./src/**/*.nupkg"), "./artifacts");
    });

Task("CI")
    .IsDependentOn("Dist")
    .IsDependentOn("Publish")
    .IsDependentOn("Pack");

Task("Default")
    .IsDependentOn("Test");

RunTarget(target);
