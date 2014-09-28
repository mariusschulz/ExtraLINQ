#r "packages/FAKE/tools/FakeLib.dll"
open Fake

RestorePackages()

let buildDir = "./build/"
let testDir = "./tests/"

let nUnitPath = "./tools/NUnit"

Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
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
        |> NUnit (fun options ->
            { options with 
                ToolPath = nUnitPath;
                DisableShadowCopy = true;
                OutputFile = testDir + "TestResults.xml" })
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE!"
)

"Clean"
    ==> "BuildApp"
    ==> "BuildTests"
    ==> "Test"
    ==> "Default"

RunTargetOrDefault "Default"
