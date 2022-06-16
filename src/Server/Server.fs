module Server

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn
open Shared
open System
open System.IO
open System.Text.RegularExpressions

let getLecture path =
    let content = File.ReadAllText path
    let metaData =
        Regex.Replace(content, @"^\s*<!--\s*(.*?)\s*-->.*$", "$1", RegexOptions.Singleline).Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map (fun x ->
            let a = Regex.Match(x.Trim(), @"^(.*?):(.*?)$", RegexOptions.Multiline)
            a.Groups.[1].Value.Trim(), a.Groups.[2].Value.Trim())
        |> Map.ofSeq
    { Title = metaData.TryFind "title" |> Option.defaultValue "(No title)"
      Menu =
        metaData.TryFind "menu"
        |> Option.defaultValue "Other"
        |> fun menuStr -> Regex.Split(menuStr, @"\s*>\s*") |> List.ofSeq
      Date = metaData.TryFind "date" |> Option.defaultValue "(No date)"
      Tags = (metaData.TryFind "tags" |> Option.defaultValue "").Split "," |> List.ofSeq |> List.map (fun x -> x.Trim())
      Path = path
    }, content

let api =
    { GetAllLectureMetaData = fun dir -> async {
        let paths = Directory.GetFiles(dir, "*.md", SearchOption.AllDirectories)
        let allLectureMetaData =
            [ for path in paths do 
                getLecture path |> fst
            ]
        return allLectureMetaData }


      GetLecture = fun path -> async {
        return (getLecture path)
      }
    }

let webApp =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue api
    |> Remoting.buildHttpHandler

let app =
    application {
        url "http://0.0.0.0:8085"
        use_router webApp
        memory_cache
        use_static "public"
        use_gzip
    }

run app
