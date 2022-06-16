namespace Lectures

open Browser
open Components
open Fable.React


[<AutoOpen>]
module _20220212_LinearAlgebra1 =
    open Feliz
    open Feliz.UseElmish
    open Elmish
    open Fable.Core
    open Fable.Core.JsInterop
    let katex : obj = importDefault "katex"
    [<ReactComponent>]
    let Lecture() =
        let player =
            AnimationPlayer
                [ Html.div [
                    prop.dangerouslySetInnerHTML
                        (katex?renderToString @"\sqrt{x^2+1}")
                  ]
                  Html.div [
                    prop.dangerouslySetInnerHTML
                        (katex?renderToString @"x + \frac{2}{3}x^{\frac{3}{2}} + \frac{1}{x}\Biggr|_{1}^{9} \int_{-1}^{988}")
                  ]
                  Html.div [
                    prop.dangerouslySetInnerHTML
                        (katex?renderToString @"a^2 + b^2 = c^2")
                  ]
                ]
        Html.div [
            player
        ]
