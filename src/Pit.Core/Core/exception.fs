namespace Pit

    // no support for proper inheritance and ctor initialization
    // so just having duplicate types everywhere
    // TODO: Implement inheritance ctors to reduce JS code gen
    type Exception =
        val message : string

        [<Js>]
        member x.Message
            with get()          = x.message

        [<Js>] new ()       = { message = ""; }
        [<Js>] new (msg)    = { message = msg;}

    type ArgumentException =
        val message : string

        [<Js>]
        member x.Message
            with get()          = x.message

        [<Js>] new ()       = { message = ""; }
        [<Js>] new (msg)    = { message = msg;}

    type InvalidOperationException =
        val message : string

        [<Js>]
        member x.Message
            with get()          = x.message

        [<Js>] new ()       = { message = ""; }
        [<Js>] new (msg)    = { message = msg;}

    type NotSupportedException =
        val message : string

        [<Js>]
        member x.Message
            with get()          = x.message

        [<Js>] new ()       = { message = ""; }
        [<Js>] new (msg)    = { message = msg;}