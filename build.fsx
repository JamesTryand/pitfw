#r @"fake\FakeLib.dll"

open Fake

let release = @".\bin\release"
let debug   = @".\bin\debug"

let pitcore     = !! @"src\Pit.sln"
let pitcompiler = !! @"src\Pit.Compiler.sln"
let pitdbg      = !! @"src\Pit.dbg.sln"

Target? Clean <-
    fun () ->
        CleanDirs [release;debug]
        [@"src\scripts\pit.js";@"src\scripts\pit.core.min.js"]
        |> CopyTo debug

        [@"src\scripts\pit.js";@"src\scripts\pit.core.min.js"]
        |> CopyTo release

Target? BuildDebug <-
    fun () ->
        trace "Building Debug assemblies"

        MSBuildDebug debug "Clean" pitcore
        |> Log "Clean Debug Build:"
        MSBuildDebug debug "Clean" pitcompiler
        |> Log "Clean Debug Build:"
        MSBuildDebug debug "Clean" pitdbg
        |> Log "Clean Debug Build:"

        MSBuildDebug debug "Build" pitcore
        |> Log "Debug Build:"
        MSBuildDebug debug "Build" pitcompiler
        |> Log "Debug Build:"
        MSBuildDebug debug "Build" pitdbg
        |> Log "Debug Build:"

Target? Release <-
    fun () ->
        trace "Building Release assemblies"
        MSBuildRelease release "Clean" pitcore
        |> Log "Clean Release Build:"
        MSBuildRelease release "Clean" pitcompiler
        |> Log "Clean Release Build:"
        MSBuildRelease release "Clean" pitdbg
        |> Log "Clean Release Build:"

        MSBuildRelease release "Build" pitcore
        |> Log "Release Build:"
        MSBuildRelease release "Build" pitcompiler
        |> Log "Release Build:"
        MSBuildRelease release "Build" pitdbg
        |> Log "Release Build:"

Target? Default <-
    fun () -> trace "Finished..."

"Clean"
    ==> "Release" <=> "BuildDebug"
    ==> "Default"

Run? Default