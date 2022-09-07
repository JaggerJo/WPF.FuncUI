namespace WPF.FuncUI.Hosts

open WPF.FuncUI.Types
open WPF.FuncUI.VirtualDom
open System.Windows
open System.Windows.Controls

type IViewHost =
    abstract member Update: IView option -> unit

type HostWindow() as this =
    inherit Window()

    let mutable lastViewElement : IView option = None

    let update (nextViewElement : IView option) : unit =
        VirtualDom.updateRoot (this, lastViewElement, nextViewElement)
        lastViewElement <- nextViewElement

    interface IViewHost with
        member this.Update next =
            update next

type HostControl() as this =
    inherit ContentControl()

    let mutable lastViewElement : IView option = None

    let update (nextViewElement : IView option) : unit =
        VirtualDom.updateRoot (this, lastViewElement, nextViewElement)
        lastViewElement <- nextViewElement

    //interface IStyleable with
    //    member this.StyleKey = typeof<ContentControl>

    interface IViewHost with
        member this.Update next =
            update next