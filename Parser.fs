module Parser
open Token

type Precedence =
    | Lowest
    | Term
    | Factor

let getPrec = function
    | Plus | Minus -> Factor
    | Star | Slash -> Term
    | _ -> Lowest

type Parser(tokens: Token array) =
    let mutable pos = 0

    member private _.current() =
        if pos < tokens.Length
            then Some tokens.[pos]
            else None

    member private _.advance() =
        pos <- pos + 1

    // ---

    member private this.expression(prec: Precedence) =
        let leftPos = current().pos
        
