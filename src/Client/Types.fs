namespace Lectures

open Fable.React

[<AutoOpen>]
module Types =

    type Lecture =
        { Title : string
          SubTitles : string * ReactElement
        }
