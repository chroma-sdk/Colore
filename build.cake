#addin nuget:?package=Cake.DocFx
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
// GLOBALS
///////////////////////////////////////////////////////////////////////////////

var isAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isTravis = TravisCI.IsRunningOnTravisCI;
var isCi = isAppVeyor || isTravis;
var isWindows = IsRunningOnWindows();

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

var mainProject = "./src/Corale.Colore/Corale.Colore.csproj";
var testProject = "./src/Corale.Colore.Tests/Corale.Colore.Tests.csproj";
var frameworks = new List<string>();

GitVersion version = null;

Setup(ctx =>
{
    Information("Reading framework settings");

    var xmlValue = XmlPeek(mainProject, "/Project/PropertyGroup/TargetFrameworks");
    if (string.IsNullOrEmpty(xmlValue))
    {
        xmlValue = XmlPeek(mainProject, "/Project/PropertyGroup/TargetFramework");
    }

    if (isWindows)
        frameworks.AddRange(xmlValue.Split(';'));
    else
        frameworks.AddRange(xmlValue.Split(';').Where(v => !v.StartsWith("net4")));

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

void FixNUnitLoggerPaths()
{
    Information("Fixing NUnitXML.TestLogger paths");
    var files = GetFiles("/home/travis/.nuget/packages/nunitxml.testlogger/*/build/_common/*.dll");

    foreach (var file in files)
    {
        if (file.GetFilenameWithoutExtension().ToString().Contains("Nunit"))
        {
            var full = file.ToString();
            var fix = full.Replace("Nunit", "NUnit");
            Information("{0} --> {1}", full, fix);
            MoveFile(full, fix);
        }
    }
}

void Build(string project, string framework = null)
{
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        ArgumentCustomization = args => args
            .Append($"/p:AssemblyVersion={version.AssemblySemVer}")
            .Append($"/p:NuGetVersion={version.NuGetVersionV2}")
    };

    if (!string.IsNullOrEmpty(framework))
        settings.Framework = framework;

    DotNetCoreBuild(project, settings);
}

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
        if (isTravis)
            FixNUnitLoggerPaths();

        if (isWindows)
        {
            Build(mainProject);
        }
        else
        {
            foreach (var framework in frameworks)
            {
                Build(mainProject, framework);
            }
        }

        Build(testProject);
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            NoBuild = true
        };

        if (AppVeyor.IsRunningOnAppVeyor)
        {
            settings.ArgumentCustomization = args => args
                .Append("--logger:AppVeyor");
        }
        else
        {
            settings.ArgumentCustomization = args => args
                .Append("--logger:nunit");
        }

        DotNetCoreTest(testProject, settings);

        if (AppVeyor.IsRunningOnAppVeyor)
        {
            return;
        }

        var testResults = GetFiles("src/Corale.Colore.Tests/TestResults/*.xml");
        CopyFiles(testResults, "./artifacts");
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

Task("Docs")
    .IsDependentOn("Build")
    .Does(() =>
    {
        if (isTravis)
        {
            DocFxBuild("./docs/docfx.json", new DocFxBuildSettings
            {
                ToolPath = "./docfx/docfx.exe"
            });
        }
        else
            DocFxBuild("./docs/docfx.json");

        Zip("./docs/_site", $"./artifacts/colore_{version.SemVer}_docs.zip");
    });

Task("CI")
    .IsDependentOn("Dist")
    .IsDependentOn("Publish")
    .IsDependentOn("Pack")
    .IsDependentOn("Docs");

Task("Default")
    .IsDependentOn("Test");

RunTarget(target);
