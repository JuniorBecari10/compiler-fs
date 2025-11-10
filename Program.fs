open System

[<EntryPoint>]
let main _ =
    while true do
        printf "> "
        let input = Console.ReadLine()

        let span = ReadOnlySpan(input.ToCharArray())
        let res = Lexer.lex span

        printfn "%A" res
    0
