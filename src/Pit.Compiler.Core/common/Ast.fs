namespace Pit.Compiler

[<AutoOpen>]
module Ast =

    type Node =
        | Int of int option
        | Float of float option
        | BooleanNode of bool option
        | StringNode of string option
        | QuotedVariable of string
        | Variable of string
        | Addition of Node * Node
        | Subraction of Node * Node
        | Division of Node * Node
        | Multiply of Node * Node
        | Modulus of Node * Node
        | Negation of Node
        | Concatenate of Node * Node
        | LessThan of Node * Node
        | GreaterThan of Node * Node
        | LessThanOrEqual of Node * Node
        | GreaterThanOrEqual of Node * Node
        | NotEquals of Node * Node
        | Equality of Node * Node
        | And of Node * Node
        | Or of Node * Node
        | Not of Node
        | BitAnd of Node * Node
        | BitOr of Node * Node
        | BitXor of Node * Node
        | BitNot of Node
        | BitLeftShift of Node * Node
        | BitRightShift of Node * Node
        | DeclareStatement of Node * Node // variable and its RHS value
        | Block of Node[] //Block defines a set of execution statements
        | New of Node * Node[] // an identifier node with a list of args
        | Assign of Node * Node
        | MemberAccess of string * Node
        | MemberAccessNode of Node * Node
        | Class of string * string * Node * Node[] * Node[] * Node[] //Class Name Class info, ctor args, ctor variables, body
        | EnumNode of Node * string[] // member access for namespace and type, followed by the ENUM values
        | Function of string option * Node[] * Node //function name(if required to create a function with name), arguments, function container, return value
        | Return of Node
        | Call of Node * Node[] // method name & arguments
        | Closure of Node // closure function
        | NewTupleNode of Node[]
        | IfElse of Node * Node * Node option // condition, body, else part
        | While of Node * Node
        | ForIntegerUpLoop of Node * Node * Node * Node // variable, condition1, condition2, body -- "downto" keyword is not supported in F# quotations, may be in future so we have a "Up" indicator for postfix operators to always increment
        | StringLiteral of string
        | Try of Node * (Node * Node) option * Node option // try body / catch body / finally body
        | Throw of Node
        | NewArray of Node[]
        | IndexAccess of Node * Node
        | NewJsType of (string * Node)[] // used in js type notations
        | InstanceOf of Node * Node // js "instanceof" keyword
        | TypeOf of Node
        | Unit
        | Null
        | Empty
        | Ignore