
/* ----------------------------------------------------------------------------
 * Build script for FileOperationScheduler
 * ----------------------------------------------------------------------------
 */

#addin nuget:?package=Cake.Figlet&version=2.0.1

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
});

Task("build")
    .Does(() => {  
        var settings = new DotNetBuildSettings {
            Configuration = "release"
        };
        DotNetBuild(solution, settings);        
});

Task("release-github")
    .Does(()=>{

        var token = EnvironmentVariable("CAKE_PUBLIC_GITHUB_TOKEN");
        var owner = EnvironmentVariable("CAKE_PUBLIC_GITHUB_USERNAME");
        var isPrerelease = gitVersion.SemVer.Contains("alpha");

        if(isPrerelease) {
                Information("This is a prerelease");
        }

        var parameters = $"create --token {token} -o {owner} -r {repoName} " +
                         $"--milestone {gitVersion.MajorMinorPatch} --name {gitVersion.SemVer} " +
                         $"--debug --verbose {(isPrerelease ? "--pre" : "")}";
        
        DotNetTool(
            solution, 
            "gitreleasemanager",
            parameters 
        );
});

Task("nuget")
    .Does(()=>{

        var version = gitVersion.NuGetVersion;
        var apiKey  = EnvironmentVariable("CAKE_PUBLIC_GITHUB_USERNAME");
        var source  = "https://nuget.pkg.github.com/jibedoubleve/index.json";
        
        var parameters = $"push \"../src/FileOperationScheduler/bin/Release/FileOperationScheduler.{version}.nupkg\" --api-key {apiKey} --source {source}";
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
    .IsDependentOn("build")
    ;

Task("release")
    .IsDependentOn("default")
    .IsDependentOn("nuget")
    .IsDependentOn("release-github")
    ;

RunTarget(target);