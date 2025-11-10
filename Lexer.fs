module Lexer

open System
open System.Globalization
open Diagnostic
open Token

type Source = ReadOnlySpan<char>

let make kind pos =
    {
        pos = pos
        kind = kind
    }

let diag msg pos =
    {
        pos = pos
        msg = msg
    }

// ---

let number (src: Source) i =
    let mutable pos = i

    while pos < src.Length && Char.IsDigit src.[pos] do
        pos <- pos + 1

    if pos < src.Length && src.[pos] = '.' then
        pos <- pos + 1

    while pos < src.Length && Char.IsDigit src.[pos] do
        pos <- pos + 1

    let slice = src.Slice(i, pos - i)
    let mutable value = 0.0
    
    if Double.TryParse(slice, NumberStyles.Float, CultureInfo.InvariantCulture, &value)
        then Ok (make (Number value) i, pos - i)
        else Error (diag "Invalid number literal." i) // should not happen

let token (src: Source) i =
    let make kind = Ok (make kind i, 1)
    let diag msg = Error (diag msg i)

    match src.[i] with
    | '+' -> make Plus
    | '-' -> make Minus
    | '*' -> make Star
    | '/' -> make Slash

    | '(' -> make LParen
    | ')' -> make RParen

    | d when Char.IsDigit d -> number src i
    | _ -> diag "Unknown character."

let skipWhitespace (src: Source) i =
    let mutable j = i

    while j < src.Length && Char.IsWhiteSpace src.[j] do
        j <- j + 1

    j

let lex (src: Source) =
    let mutable i = 0

    let tokens = ResizeArray<Token>()
    let diags = ResizeArray<Diagnostic>()

    while i < src.Length do
        i <- skipWhitespace src i

        if i < src.Length then
            match token src i with
            | Ok (tok, amount) ->
                tokens.Add tok
                i <- i + amount

            | Error diag ->
                diags.Add diag
                i <- i + 1

    if diags.Count > 0
        then Error (diags.ToArray())
        else Ok (tokens.ToArray())
