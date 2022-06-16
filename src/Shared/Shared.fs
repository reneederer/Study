namespace Shared

open System


module Route =
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

type IApi =
    { GetLectures: unit -> Async<unit>
    }
