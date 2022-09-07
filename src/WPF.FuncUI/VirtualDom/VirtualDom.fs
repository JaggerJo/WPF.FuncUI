namespace WPF.FuncUI.VirtualDom

open WPF.FuncUI.Types
open WPF.FuncUI.VirtualDom.Delta
open System.Windows
open System.Windows.Controls

module rec VirtualDom =

    let create (view: IView) : FrameworkElement =
        view
        |> ViewDelta.From
        |> Patcher.create

    let update (root: FrameworkElement, last: IView, next: IView) : unit =
        let delta = Differ.diff(last, next)
        Patcher.patch(root, delta)

    let updateRoot (host: ContentControl, last: IView option, next: IView option) =
        let root : Control voption =
            if host.Content <> null then
                match host.Content with
                | :? Control as control -> ValueSome control
                | _ -> ValueNone
            else
                ValueNone

        let delta : ViewDelta voption =
            match last with
            | Some last ->
                match next with
                | Some next -> Differ.diff (last, next) |> ValueSome
                | None -> ValueNone
            | None ->
                match next with
                | Some next -> ViewDelta.From next |> ValueSome
                | None -> ValueNone

        match root with
        | ValueSome control ->
            match delta with
            | ValueSome delta ->
                match control.GetType () = delta.ViewType && not delta.KeyDidChange with
                | true -> Patcher.patch (control, delta)
                | false -> host.Content <- Patcher.create delta
            | ValueNone ->
                host.Content <- null

        | ValueNone ->
            match delta with
            | ValueSome delta -> host.Content <- Patcher.create delta
            | ValueNone -> host.Content <- null

    // TODO: share code with updateRoot
    let internal updateBorderRoot (host: Border, last: IView option, next: IView option) =
        let root : UIElement voption =
            if host.Child <> null then
                ValueSome host.Child
            else
                ValueNone

        let delta : ViewDelta voption =
            match last with
            | Some last ->
                match next with
                | Some next -> Differ.diff (last, next) |> ValueSome
                | None -> ValueNone
            | None ->
                match next with
                | Some next -> ViewDelta.From next |> ValueSome
                | None -> ValueNone

        match root with
        | ValueSome control ->
            match delta with
            | ValueSome delta ->
                match control.GetType () = delta.ViewType && not delta.KeyDidChange with
                | true -> Patcher.patch (control :?> FrameworkElement, delta)
                | false -> host.Child <- Patcher.create delta
            | ValueNone ->
                host.Child <- null

        | ValueNone ->
            match delta with
            | ValueSome delta -> host.Child <- Patcher.create delta
            | ValueNone -> host.Child <- null