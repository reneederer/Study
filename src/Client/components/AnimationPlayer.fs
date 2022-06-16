namespace Components

[<AutoOpen>]
module AnimationPlayer =
    open Feliz
    open Feliz.UseElmish
    open Elmish

    type Msg =
        | Forward
        | Backward

    type State =
        { CurrentStep : int
          Steps : ReactElement list
        }

    let init steps =
        { CurrentStep = 0
          Steps = steps
        }, Cmd.none

    let update msg state =
        match msg with
        | Forward ->
            { state with CurrentStep = (state.CurrentStep + 1) % state.Steps.Length }, Cmd.none
        | Backward -> 
            { state with CurrentStep = if state.CurrentStep = 0 then state.Steps.Length - 1 else (state.CurrentStep - 1)}, Cmd.none

    [<ReactComponent>]
    let AnimationPlayer steps (width : int) (height : int) =
        let state, dispatch = React.useElmish((fun () -> init steps), update, [| |])
        Html.div [
            Html.div
                [ prop.style [ style.width width; style.height height]
                  prop.children [
                      for step in 0..state.CurrentStep do
                        steps.[step]
                  ]
                ]
            Html.button [
                prop.text "<"
                prop.onClick (fun _ -> dispatch Backward)
            ]
            Html.button [
                prop.text ">"
                prop.onClick (fun _ -> dispatch Forward)
            ]

        ]

