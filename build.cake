
/* ----------------------------------------------------------------------------
 * Build script for FileOperationScheduler
 * ----------------------------------------------------------------------------
 */

#addin nuget:?package=Cake.Figlet&version=2.0.1

#tool nuget:?package=GitVersion.CommandLine&version=5.10.1

 ///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var solution = "./src/FileOperationScheduler.sln";
var target = Argument("target", "Default");
var repoName = "FileOperationScheduler";
GitVersion gitVersion;

 ///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
    Information(Figlet($"{repoName}"));

    // https://gitversion.net/docs/usage/cli/arguments
    // https://cakebuild.net/api/Cake.Core.Tooling/ToolSettings/50AAB3A8
    gitVersion = GitVersion(new GitVersionSettings 
    { 
        OutputType = GitVersionOutput.Json,
        Verbosity = GitVersionVerbosity.Verbose,        
        ArgumentCustomization = args => args.Append("/updateprojectfiles")
    });
    var branchName = gitVersion.BranchName;

    // Information("Configuration             : {0}", configuration);
    Information("Branch                    : {0}", branchName);
    Information("Informational      Version: {0}", gitVersion.InformationalVersion);
    Information("SemVer             Version: {0}", gitVersion.SemVer);
    Information("AssemblySemVer     Version: {0}", gitVersion.AssemblySemVer);
    Information("AssemblySemFileVer Version: {0}", gitVersion.AssemblySemFileVer);
    Information("MajorMinorPatch    Version: {0}", gitVersion.MajorMinorPatch);
    Information("NuGet              Version: {0}", gitVersion.NuGetVersion); 
});

 ///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("clean")
    .Does(()=> {
        Information("Cleaning files...");
        var dirToDelete = GetDirectories("./**/obj")
                            .Concat(GetDirectories("./**/bin"))
                            .Concat(GetDirectories("./**/Output"))
                            .Concat(GetDirectories("./**/Publish"));
        DeleteDirectories(dirToDelete, new DeleteDirectorySettings{ Recursive = true, Force = true});

        DotNetTool(
            solution,
            "clean"
        );
});

Task("restore")
    .Does(()=>{

        DotNetTool(
            solution,
            "restore"
        );
});

Task("build")
    .Does(() => {        
        DotNetTool(
            solution,
            "build",
            "--no-restore -c release"
        );
});

Task("test")
    .Does(()=>{

        DotNetTool(
            solution,
            "test",
            "--no-restore --no-build -c release"
        );
});

Task("release-github")
    .Does(()=>{
        var token = EnvironmentVariable("CAKE_PUBLIC_GITHUB_TOKEN");
        var owner = EnvironmentVariable("CAKE_PUBLIC_GITHUB_USERNAME");

        var alphaVersions = new[] { "alpha", "beta" };
        var isPrerelease = alphaVersions.Any(x => gitVersion.SemVer.Contains(x));

        if(isPrerelease) {
                Information("This is a prerelease");
        }

        var parameters = $"create --token {token} -o {owner} -r {repoName} " +
                         $"--milestone {gitVersion.MajorMinorPatch} --name {gitVersion.SemVer} " +
                         $"{(isPrerelease ? "--pre" : "")} " +
                         $"--targetDirectory {Environment.CurrentDirectory} "
                         // + "--debug --verbose"
                         ;
        
        DotNetTool(
            solution, 
            "gitreleasemanager",
            parameters 
        );
});

Task("nuget")
    .Does(()=>{

        var version = gitVersion.NuGetVersion;
        var apiKey  = EnvironmentVariable("NUGET_TOKEN");
        var source  = "https://api.nuget.org/v3/index.json";
        
        var parameters = $"push \"../src/FileOperationScheduler/bin/Release/FileOperationScheduler.{version}.nupkg\" --api-key {apiKey} --source {source}";

        Warning("Parameters: {0}", parameters);

        DotNetTool(
            solution,
            "nuget",
            parameters 
        );
        
});

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

Task("default")
    .IsDependentOn("clean")
    .IsDependentOn("restore")
    .IsDependentOn("build")
    .IsDependentOn("test")
    ;

Task("relnote")
    .IsDependentOn("default")
    .IsDependentOn("release-github")
    ;

Task("release")
    .IsDependentOn("relnote")
    .IsDependentOn("nuget")
    ;

RunTarget(target);