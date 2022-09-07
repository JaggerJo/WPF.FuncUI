namespace WPF.FuncUI

open System
open WPF.FuncUI

type internal UniqueValueReadOnly<'value>
  ( src: IReadable<'value> ) =

    let mutable current: 'value = src.Current

    interface IReadable<'value> with
        member this.InstanceId with get () = src.InstanceId
        member this.InstanceType with get () = InstanceType.Create src
        member this.ValueType with get () = typeof<'value>
        member this.Current with get () = current
        member this.Subscribe (handler: 'value -> unit) =
            src.Subscribe (fun value ->
                (* meh, we need a better equals implementation. *)
                if not (ComponentHelpers.safeFastEquals (current, value)) then
                    current <- value
                    handler value
            )
        member this.SubscribeAny (handler: obj -> unit) =
            (this :> IReadable<'value>).Subscribe handler

        member this.Dispose () =
            ()

type internal UniqueValue<'value>
  ( src: IWritable<'value> ) =

    inherit UniqueValueReadOnly<'value>(src)

    interface IWritable<'value> with
        member this.Set (newValue: 'value) =
            src.Set newValue

type internal ReadValueMapped<'a, 'b>
  ( src: IReadable<'a>,
    mapFunc: 'a -> 'b ) =

    let mutable current: 'b = mapFunc src.Current

    interface IReadable<'b> with
        member this.InstanceId with get () = src.InstanceId
        member this.InstanceType with get () = InstanceType.Create src
        member this.ValueType with get () = typeof<'b>
        member this.Current with get () = current
        member this.Subscribe (handler: 'b -> unit) =
            src.Subscribe (fun value ->
                current <- mapFunc value
                handler current
            )
        member this.SubscribeAny (handler: obj -> unit) =
            (this :> IReadable<'b>).Subscribe handler

        member this.Dispose () =
            ()

type internal ValueMapped<'a, 'b>
  ( src: IWritable<'a>,
    mapFunc: 'a -> 'b,
    mapBackFunc: 'b -> 'a ) =

    inherit ReadValueMapped<'a, 'b>(src, mapFunc)

    interface IWritable<'b> with
        member this.Set (value: 'b) : unit =
            src.Set (mapBackFunc value)

type internal ReadValueMap<'value, 'key when 'key : comparison>
  ( src: IReadable<'value list>,
    keyPath: 'value -> 'key ) =

    let disposable = new DisposableBag ()
    let value: IReadable<_> =
        let value = new UniqueValueReadOnly<_>(src)
        disposable.Add value
        value :> _

    let makeMap items =
        items
        |> Seq.map (fun item -> keyPath item, item)
        |> Map.ofSeq
    let mutable current: Map<'key, 'value> = makeMap value.Current

    interface IReadable<Map<'key, 'value>> with
        member this.InstanceId with get () = value.InstanceId
        member this.InstanceType with get () = InstanceType.Create src
        member this.ValueType with get () = typeof<Map<'key, 'value>>
        member this.Current with get () = current
        member this.Subscribe (handler: Map<'key, 'value> -> unit) =
            value.Subscribe (fun _ ->
                let current' = makeMap value.Current
                current <- current'
                handler current'
            )
        member this.SubscribeAny (handler: obj -> unit) =
            (this :> IReadable<_>).Subscribe handler

        member this.Dispose () =
            (disposable :> IDisposable).Dispose ()

type internal ValueMap<'value, 'key when 'key : comparison>
  ( src: IWritable<'value list>,
    keyPath: 'value -> 'key ) =

    inherit ReadValueMap<'value, 'key>(src, keyPath)

    interface IWritable<Map<'key, 'value>> with

        member this.Set (signal: Map<'key, 'value>) : unit =
            src.Current
            |> Seq.choose (fun item -> Map.tryFind (keyPath item) signal)
            |> Seq.toList
            |> src.Set

type internal ReadKeyFocusedValue<'value, 'key when 'key : comparison>
  ( src: IReadable<Map<'key, 'value>>,
    key: IReadable<'key> ) =

    let disposable = new DisposableBag ()
    let value: IReadable<_> =
        let value = new UniqueValueReadOnly<_>(src)
        disposable.Add value
        value :> _

    let key: IReadable<_> =
        let key = new UniqueValueReadOnly<_>(key)
        disposable.Add key
        key :> _

    let onChange = Event<'value option>()
    let mutable current = value.Current.TryFind key.Current

    let onKeyOrValueChanged () =
        let current' = src.Current.TryFind key.Current
        current <- current'
        onChange.Trigger current'

    do disposable.Add (key.Subscribe (ignore >> onKeyOrValueChanged))
    do disposable.Add (value.Subscribe (ignore >> onKeyOrValueChanged))

    interface IReadable<'value option> with
        member this.InstanceId with get () = value.InstanceId
        member this.InstanceType with get () =
            InstanceType.Create [
                "src", src :> IAnyReadable
                "key", key :> IAnyReadable
            ]
        member this.ValueType with get () = typeof<'value option>

        member this.Current with get () = current

        member this.Subscribe (handler: 'value option -> unit) =
            onChange.Publish.Subscribe handler

        member this.SubscribeAny (handler: obj -> unit) =
            (this :> IReadable<_>).Subscribe handler

        member this.Dispose () =
            (disposable :> IDisposable).Dispose()

type internal KeyFocusedValue<'value, 'key when 'key : comparison>
  ( src: IWritable<Map<'key, 'value>>,
    key: IReadable<'key> ) =

    inherit ReadKeyFocusedValue<'value, 'key>(src, key)

    interface IWritable<'value option> with
        member this.Set (signal: 'value option) : unit =
            match signal with
            | Some signal ->
                src.Current
                |> Map.add key.Current signal
                |> src.Set
            | None ->
                src.Current
                |> Map.remove key.Current
                |> src.Set

type internal FilteringValueList<'value, 'filter>
  ( src: IWritable<'value list>,
    filter: IReadable<'filter>,
    filterFunc: 'value -> 'filter -> bool ) =

    let disposable = new DisposableBag ()
    let value: IWritable<_> =
        let value = new UniqueValue<_>(src)
        disposable.Add value
        value :> _

    let filter: IReadable<_> =
        let key = new UniqueValueReadOnly<_>(filter)
        disposable.Add key
        key :> _

    let onChange = Event<'value list>()

    let evaluate () =
         value.Current
         |> List.filter (fun signal -> filterFunc signal filter.Current)

    let mutable current = evaluate ()

    let onKeyOrValueChange () =
        let current' = evaluate ()
        current <- current'
        onChange.Trigger current'

    do disposable.Add (filter.Subscribe (ignore >> onKeyOrValueChange))
    do disposable.Add (value.Subscribe (ignore >> onKeyOrValueChange))

    interface IReadable<'value list> with
        member this.InstanceId with get () = value.InstanceId
        member this.InstanceType with get () =
            InstanceType.Create [
                "src", src :> IAnyReadable
                "filter", filter :> IAnyReadable
            ]
        member this.ValueType with get () = typeof<'value list>
        member this.Current with get () = current
        member this.Subscribe (handler: 'value list -> unit) =
            onChange.Publish.Subscribe handler
        member this.SubscribeAny (handler: obj -> unit) =
            (this :> IReadable<_>).Subscribe handler
        member this.Dispose () =
            (disposable :> IDisposable).Dispose()

type internal TraversedValue<'value, 'key when 'key : comparison>
  ( wire: IWritable<Map<'key, 'value>>,
    key: 'key ) =

    interface IWritable<'value> with
        member this.InstanceId with get () = wire.InstanceId
        member this.InstanceType with get () = InstanceType.Create wire
        member this.ValueType with get () = typeof<'value>
        member this.Current with get () = wire.Current.[key]

        member this.Subscribe (handler: 'value -> unit) =
            wire.Subscribe (fun value ->
                match value.TryFind key with
                | Some value -> handler value
                | None -> ()
            )

        member this.SubscribeAny (handler: obj -> unit) =
            (this :> IReadable<_>).Subscribe handler

        member this.Set (signal: 'value) : unit =
            wire.Current
            |> Map.add key signal
            |> wire.Set

        member this.Dispose () =
            ()