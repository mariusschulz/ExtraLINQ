#r "packages/FAKE/tools/FakeLib.dll"
open Fake

RestorePackages()

let buildDir = "./build/"
let testDir = "./tests/"

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

Target "Default" (fun _ ->
    trace "Hello World from FAKE!"
)

"Clean"
    ==> "BuildApp"
    ==> "BuildTests"
    ==> "Default"

RunTargetOrDefault "Default"
