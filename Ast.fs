module Ast

type Expr =
    {
        pos: int
        kind: ExprKind
    }

and ExprKind =
    | Number of float
    | Binary of left: Expr * op: BinOp * right: Expr
    | Unary of expr: Expr * op: UnOp

and BinOp =
    | Plus
    | Minus
    | Times
    | Divide

and UnOp =
    Negate
