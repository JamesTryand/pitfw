namespace Pit.Compiler
open Pit

module TypeParser =
    module Property =
        val createGet: bool -> System.Type -> System.Reflection.PropertyInfo -> Node
        val createSet: System.Type -> System.Reflection.PropertyInfo -> Node

    module Interface =
        module Empty =
            val getAst : System.Type -> Node[]

        val getAst : System.Type -> System.Type -> bool -> (Node * Node[]) option

    module Class =
        val getAst : System.Type -> Node[] -> Node[]

    module Module =
        val getAst : System.Type -> Node[] -> Node[]

    module Union =
        val getAst : System.Type -> Node[]

    val getAst : System.Type[] -> Node[]
    val getAstFromType : System.Type -> Node[]