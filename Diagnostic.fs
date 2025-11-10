module Diagnostic

[<Struct>]
type Diagnostic =
    {
        pos: int
        msg: string
    }
