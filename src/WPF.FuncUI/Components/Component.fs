namespace WPF.FuncUI

open System
open WPF.FuncUI
open WPF.FuncUI.Types
open WPF.FuncUI.VirtualDom
open System.Windows.Controls
open System.Windows.Threading
open System.Windows

[<AllowNullLiteral>]
type Component (render: IComponentContext -> IView) =
    inherit Border ()
    let context = new Context()
    let componentId = Guid.Unique

    let mutable lastViewElement : IView option = None
    let mutable lastViewAttrs: IAttr list = List.empty

    member internal this.Context with get () = context
    member internal this.ComponentId with get () = componentId

    member private this.Update () : unit =
        let _op = 
            Application.Current.Dispatcher.BeginInvoke (
                method = Action(fun _ ->
                    let nextViewElement = Some (render context)

                    // update view
                    VirtualDom.updateBorderRoot (this, lastViewElement, nextViewElement)
                    lastViewElement <- nextViewElement

                    let nextViewAttrs = context.ComponentAttrs

                    // update attrs
                    Patcher.patch (
                        this,
                        { Delta.ViewDelta.ViewType = typeof<Border>
                          Delta.ViewDelta.ConstructorArgs = null
                          Delta.ViewDelta.KeyDidChange = false
                          Delta.ViewDelta.Outlet = ValueNone
                          Delta.ViewDelta.Attrs = Differ.diffAttributes lastViewAttrs nextViewAttrs }
                    )

                    lastViewAttrs <- nextViewAttrs

                    context.EffectQueue.ProcessAfterRender ()
                ),
                priority = DispatcherPriority.Render
            )
        ()

    override this.OnInitialized (args) =
        base.OnInitialized (args)

        (context :> IComponentContext).trackDisposable (
            context.OnRender.Subscribe (fun _ ->
                this.Update ()
            )
        )

        this.Update ()

    override this.Finalize () =
        base.Finalize ()
        (context :> IDisposable).Dispose ()

    //interface IStyleable with
    //    member this.StyleKey = typeof<Border>

type Component with

    static member create(key: string, render: IComponentContext -> IView) : IView<Component> =
        { View.ViewType = typeof<Component>
          View.ViewKey = ValueSome key
          View.Attrs = list.Empty
          View.Outlet = ValueNone
          View.ConstructorArgs = [| render :> obj |] }
        :> IView<Component>