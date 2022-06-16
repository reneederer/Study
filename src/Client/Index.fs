module Index

open Elmish
open Fable.Remoting.Client
open Fable.React
open Shared
open System
open Lectures


type Model =
    { CurrentLecture : Lecture option
      Menu : string list list
    }

type Msg =
    | GetLecture of string
    | GotLecture of Lecture
    | GotAllLectureMetaData of LectureMetaData list

let api =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IApi>

let init () : Model * Cmd<Msg> =
    let model =
        { CurrentLecture = None
          Menu = []
        }

    let cmd =
        //Cmd.OfAsync.perform api.GetAllLectureMetaData "c:/Users/rene/source/repos/DataScience/ttt" GotAllLectureMetaData
        Cmd.OfAsync.perform api.GetLecture "c:/Users/rene/source/repos/DataScience/ttt/a.md" GotLecture

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | GetLecture path ->
        model, Cmd.OfAsync.perform api.GetLecture path GotLecture

    | GotLecture lecture ->
        { model with CurrentLecture = Some lecture }, Cmd.none

    | GotAllLectureMetaData allLectureMetaData ->
        model, Cmd.none

open Feliz
open Feliz.Bulma

open Feliz
open type Feliz.Html
open Fable.Formatting.Markdown

let view (model: Model) (dispatch: Msg -> unit) =
    match model.CurrentLecture with
    | Some (lectureMetaData, lectureContent) ->
        div
            [ prop.dangerouslySetInnerHTML (lectureContent |> Markdown.Parse |> Markdown.ToHtml)
            ]
    | None ->
        div []
        




