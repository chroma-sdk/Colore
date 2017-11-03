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
                NoBuild = true
            });
    });

Task("Dist")
    .IsDependentOn("Test")
    .Does(() =>
    {
        var dir = $"./src/Corale.Colore/bin/{configuration}/netstandard1.6/";
        Zip(dir, $"./artifacts/colore_{version.SemVer}_anycpu.zip", $"{dir}**/*.*");
    });

Task("Publish")
    .IsDependentOn("Test")
    .Does(() =>
    {
        var settings = new DotNetCorePublishSettings
        {
            Configuration = configuration,
            OutputDirectory = "./publish/",
            ArgumentCustomization = args => args
                .Append($"/p:AssemblyVersion={version.AssemblySemVer}")
                .Append($"/p:NuGetVersion={version.NuGetVersionV2}")
        };

        DotNetCorePublish("src/Corale.Colore", settings);

        Zip($"./publish/", $"./artifacts/colore_{version.SemVer}_full.zip", "./publish/**/*.*");
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
