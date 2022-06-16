module Index

open Elmish
open Fable.Remoting.Client
open Fable.React
open Shared
open System
open Lectures


type Model =
    { Lectures : (unit -> ReactElement) list
      CurrentLecture : int option
    }

type Msg =
    | GetLectures
    | GotLectures of unit

let api =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IApi>

let init () : Model * Cmd<Msg> =
    let model =
        { Lectures = [ _20220212_LinearAlgebra1.Lecture; _20220212_Analysis1.Lecture ]
          CurrentLecture = Some 0
        }

    let cmd =
        Cmd.OfAsync.perform api.GetLectures () GotLectures

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | GetLectures ->
        model, Cmd.none

    | GotLectures _ ->
        model, Cmd.none

open Feliz
open Feliz.Bulma

open Feliz
open type Feliz.Html

let view (model: Model) (dispatch: Msg -> unit) =
    let currentLecture = model.Lectures.[0]()
    currentLecture
