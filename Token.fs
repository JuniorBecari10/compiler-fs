module Token

[<Struct>]
type Token =
    {
        pos: int
        kind: TokenKind
    }

and TokenKind =
    | Number of float
    
    | Plus
    | Minus
    | Star
    | Slash

    | LParen
    | RParen

module TokenKind =
    let lexeme = function
    | Number n -> sprintf "%.2f" n
    
    | Plus -> "+"
    | Minus -> "-"
    | Star -> "*"
    | Slash -> "/"

    | LParen -> "("
    | RParen -> ")"
