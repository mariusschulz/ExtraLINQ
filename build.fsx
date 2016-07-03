#r "packages/FAKE/tools/FakeLib.dll"
open System
open Fake
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper

RestorePackages()

let authors = ["Marius Schulz"]
let projectName = "ExtraLINQ"
let projectSummary = "ExtraLINQ provides a set of extension methods for various .NET sequence types."
let projectDescription = "ExtraLINQ provides a set of extension methods for various .NET sequence types."

let buildDir = "./build/"
let testDir = "./tests/"
let nugetDir = "./nuget/"

let xUnitPath = "./tools/xUnit/xunit.console.clr4.exe"

let releaseNotes = LoadReleaseNotes "RELEASE_NOTES.md"

Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir; nugetDir]
)

Target "SetAssemblyInfo" (fun _ ->
    let commonAttributes = [
        Attribute.Version releaseNotes.AssemblyVersion
        Attribute.InformationalVersion releaseNotes.AssemblyVersion
        Attribute.FileVersion releaseNotes.AssemblyVersion
        Attribute.Copyright ("Copyright " + authors.[0] + " " + DateTime.UtcNow.Year.ToString())]

    let appSpecificAttributes = [
        Attribute.Product "ExtraLINQ"
        Attribute.Title "ExtraLINQ"
        Attribute.Guid "8269e986-f931-4695-a8aa-eaf0f437b2c4"]

    let testsSpecificAttributes = [
        Attribute.Product "ExtraLINQ.Tests"
        Attribute.Title "ExtraLINQ Unit Tests"
        Attribute.Guid "032b2aa9-3158-4fc7-907e-1ded01fc9dd8"]

    appSpecificAttributes @ commonAttributes
        |> CreateCSharpAssemblyInfo "./src/app/ExtraLINQ/Properties/AssemblyInfo.cs"

    testsSpecificAttributes @ commonAttributes
        |> CreateCSharpAssemblyInfo "./src/tests/ExtraLINQ.Tests/Properties/AssemblyInfo.cs"
)

Target "BuildApp" (fun _ ->
    !! "src/app/**/*.csproj"
        |> MSBuildRelease buildDir "Build"
        |> Log "BuildApp-Output: "
)

Target "BuildTests" (fun _ ->
    !! "src/tests/ExtraLINQ.Tests/**/*.csproj"
        |> MSBuildRelease testDir "Build"
        |> Log "BuildTests-Output: "
)

Target "Test" (fun _ ->
    !! "tests/ExtraLINQ.Tests.dll"
        |> xUnit (fun options ->
            { options with ToolPath = xUnitPath })
)

Target "Package" (fun _ ->
    let libDir = nugetDir @@ "lib/"
    CreateDir libDir
    CopyFile libDir (buildDir @@ "ExtraLINQ.dll")

    NuGet(fun options ->
        { options with
            Authors = authors
            Project = projectName
            Version = releaseNotes.NugetVersion
            Description = projectDescription
            Summary = projectSummary
            WorkingDir = nugetDir
            ReleaseNotes = releaseNotes.Notes |> toLines
            AccessKey = getBuildParamOrDefault "nugetApiKey" ""
            Publish = hasBuildParam "nugetApiKey" })
            "ExtraLINQ.nuspec"
)

Target "Default" DoNothing

"Clean"
    ==> "SetAssemblyInfo"
    ==> "BuildApp"
    ==> "BuildTests"
    ==> "Test"
    ==> "Default"
    ==> "Package"

RunTargetOrDefault "Default"
