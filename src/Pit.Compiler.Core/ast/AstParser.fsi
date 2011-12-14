namespace Pit.Compiler
open Microsoft.FSharp.Quotations

module AstParser =
    /// Returns the AST for the given quotation
    val getAst: Expr -> Node

    /// Applies projection / transformation to the AST. This is used to re-visting the nodes for any transformations after Expr is transformed to Node variant.
    val transformAst: Node -> (Node -> (Node -> Node) -> Node option) -> (Node-> (Node -> Node) -> Node) -> Node