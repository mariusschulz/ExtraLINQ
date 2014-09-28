#r "packages/FAKE/tools/FakeLib.dll"
open Fake

let buildDir = "./build/"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "Build" (fun _ ->
    !! "src/app/**/*.csproj"
        |> MSBuildRelease buildDir "Build"
        |> Log "Build-Output: "
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE!"
)

"Clean"
    ==> "Build"

RunTargetOrDefault "Default"
