#addin nuget:?package=Cake.DocFx&version=1.0.0

#tool dotnet:?package=GitVersion.Tool&version=5.7.0
#tool dotnet:?package=dotnet-reportgenerator-globaltool&version=4.8.13

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("Target", "Default");
var configuration = HasArgument("Configuration")
    ? Argument<string>("Configuration")
    : EnvironmentVariable("CONFIGURATION") != null
        ? EnvironmentVariable("CONFIGURATION")
        : "Release";

///////////////////////////////////////////////////////////////////////////////
// GLOBALS
///////////////////////////////////////////////////////////////////////////////

var isGitHubActions = GitHubActions.IsRunningOnGitHubActions;
var isCi = isGitHubActions || EnvironmentVariable("CI") == "true";
var isWindows = IsRunningOnWindows();
var cover = isGitHubActions || HasArgument("Cover");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

var solution = "./Colore.sln";
var mainProject = "./src/Colore/Colore.csproj";
var testProject = "./tests/Colore.Tests/Colore.Tests.csproj";
var frameworks = new List<string>();

GitVersion version = null;

Setup(ctx =>
{
    var envVars = EnvironmentVariables();
    Information("Environment variables:");
    foreach (var kvp in envVars.OrderBy(kvp => kvp.Key))
    {
        Information("{0}={1}", kvp.Key, kvp.Value);
    }

    var docFxBranch = EnvironmentVariable("DOCFX_SOURCE_BRANCH_NAME");
    if (docFxBranch != null)
    {
        Information("DocFx branch is {0}", docFxBranch);
    }

    Information("Reading framework settings");

    var xmlValue = XmlPeek(mainProject, "/Project/PropertyGroup/TargetFrameworks");
    if (string.IsNullOrEmpty(xmlValue))
    {
        xmlValue = XmlPeek(mainProject, "/Project/PropertyGroup/TargetFramework");
    }

    if (isWindows)
    {
        frameworks.AddRange(xmlValue.Split(';'));
    }
    else
    {
        frameworks.AddRange(xmlValue.Split(';').Where(v => !v.StartsWith("net4")));
    }

    Information("Frameworks: {0}", string.Join(", ", frameworks));

    if (isCi)
    {
        GitVersion(new GitVersionSettings
        {
            RepositoryPath = ".",
            OutputType = GitVersionOutput.BuildServer
        });
    }

    version = GitVersion(new GitVersionSettings { RepositoryPath = "." });

    Information("Version: {0} on {1}", version.FullSemVer, version.CommitDate);
    Information("Commit hash: {0}", version.Sha);
});

Teardown(ctx =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// HELPERS
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
    {
        Information("Cleaning output directories");
        CleanDirectory("./artifacts");
        CleanDirectory("./publish");
        DotNetCoreClean(solution);
        CleanDirectory("./src/Colore.Tests/TestResults");
        CreateDirectory("./artifacts/nuget");
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => DotNetCoreRestore(solution));

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var settings = new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            NoRestore = true,
            ArgumentCustomization = args => args
                .Append($"/p:AssemblyVersion={version.AssemblySemVer}")
                .Append($"/p:NuGetVersion={version.NuGetVersionV2}")
        };

        DotNetCoreBuild(project, settings);
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true,
            Settings = "tests/coverlet.runsettings",
            Loggers = new[] { "nunit" },
            ArgumentCustomization = args => args.Append("--collect:\"XPlat Code Coverage\"")
        };

        DotNetCoreTest(testProject, settings);

        var testResults = GetFiles("tests/Colore.Tests/TestResults/*/coverage.cobertura.xml");
        CopyFiles(testResults, "./artifacts");
        MoveFile("./artifacts/coverage.cobertura.xml", "./artifacts/coverage.xml");
    });

Task("CoverageReport")
    .IsDependentOn("Test")
    .Does(() =>
    {
        ReportGenerator((FilePath)"./artifacts/coverage.xml", "./artifacts/coverage-report");
        Zip("./artifacts/coverage-report", $"./artifacts/colore_{version.SemVer}_coverage.zip");
    });

Task("Dist")
    .IsDependentOn("Test")
    .Does(() =>
    {
        foreach (var framework in frameworks)
        {
            var dir = $"./src/Colore/bin/{configuration}/{framework}/";
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

            DotNetCorePublish("src/Colore", settings);

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
        MoveFiles(GetFiles("./src/**/*.nupkg"), "./artifacts/nuget");
        MoveFiles(GetFiles("./src/**/*.snupkg"), "./artifacts/nuget");
    });

Task("Docs")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DocFxMetadata("./docs/docfx.json");
        DocFxBuild("./docs/docfx.json");
        Zip("./docs/_site", $"./artifacts/colore_{version.SemVer}_docs.zip");
    });

Task("CI")
    .IsDependentOn("Dist")
    .IsDependentOn("Publish")
    .IsDependentOn("Pack")
    .IsDependentOn("Docs")
    .IsDependentOn("CoverageReport");

Task("GitHub").IsDependentOn("CI");

Task("Default").IsDependentOn("Test");

RunTarget(target);
