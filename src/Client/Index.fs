module Index

open Elmish
open Fable.Remoting.Client
open Fable.React
open Shared
open System
open Lectures
open Fable.Core
open Fable.Core.JsInterop
open Browser.Dom

type Model =
    { CurrentLecture : Lecture option
      Menu : string list list
      AllLectureMetaData : LectureMetaData list
    }

type Msg =
    | GetLecture of string
    | GotLecture of Lecture
    | GotAllLectureMetaData of LectureMetaData list
    | TypesetMathjax

let api =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IApi>

let init () : Model * Cmd<Msg> =
    let model =
        { CurrentLecture = None
          Menu = []
          AllLectureMetaData = []
        }

    let cmd =
        Cmd.OfAsync.perform api.GetAllLectureMetaData "c:/Users/rene/source/repos/DataScience/ttt" GotAllLectureMetaData
        //Cmd.OfAsync.perform api.GetLecture "c:/Users/rene/source/repos/DataScience/ttt/a.md" GotLecture

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | GetLecture path ->
        model, Cmd.OfAsync.perform api.GetLecture path GotLecture

    | GotLecture lecture ->
        { model with CurrentLecture = Some lecture }, Cmd.ofMsg TypesetMathjax

    | GotAllLectureMetaData allLectureMetaData ->
        { model with AllLectureMetaData = allLectureMetaData }, Cmd.none

    | TypesetMathjax ->
        window?MathJax?typeset()
        model, Cmd.none

open Feliz
open Feliz.Bulma

open Feliz
open type Feliz.Html
open Fable.Formatting.Markdown
open Fable.Core

let view (model: Model) (dispatch: Msg -> unit) =
    div [
        div [
            for x in model.AllLectureMetaData do
                div [
                    prop.onClick (fun _ -> dispatch <| GetLecture x.Path)
                    prop.children [
                        str (x.Menu |> String.concat " > ")
                    ]
                ]
        ]


        match model.CurrentLecture with
        | Some (lectureMetaData, lectureContent) ->
            let html = Markdown.ToHtml(lectureContent)
            div
                [ prop.dangerouslySetInnerHTML html
                ]

        | _ ->
            div [ prop.dangerouslySetInnerHTML ( Markdown.ToHtml """$ a^2 $""") ]



    ]
            




