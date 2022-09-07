namespace WPF.FuncUI.DSL

open WPF.FuncUI.Types

[<AbstractClass; Sealed>]
type View () =

    static member createWithKey (key: string) (createView: IAttr<'view> list -> IView<'view>) (attrs: IAttr<'view> list) =
        let view = createView(attrs)

        { View.ViewType = typeof<'view>
          View.ViewKey = ValueSome key
          View.Attrs = view.Attrs
          View.ConstructorArgs = view.ConstructorArgs
          View.Outlet = ValueNone }
        :> IView<'view>

    static member createWithOutlet (outlet: 'view -> unit) (createView: IAttr<'view> list -> IView<'view>) (attrs: IAttr<'view> list) =
        let view = createView(attrs)

        { View.ViewType = typeof<'view>
          View.ViewKey = view.ViewKey
          View.Attrs = view.Attrs
          View.ConstructorArgs = view.ConstructorArgs
          View.Outlet = ValueSome (fun control -> outlet (control :?> 'view)) }
        :> IView<'view>

    static member withKey (key: string) (view: IView<'view>) : IView<'view> =
        { View.ViewType = view.ViewType
          View.ViewKey = ValueSome key
          View.Attrs = view.Attrs
          View.ConstructorArgs = view.ConstructorArgs
          View.Outlet = view.Outlet } :> _

    static member withOutlet (outlet: 'view -> unit) (view: IView<'view>) : IView<'view> =
        { View.ViewType = view.ViewType
          View.ViewKey = view.ViewKey
          View.Attrs = view.Attrs
          View.ConstructorArgs = view.ConstructorArgs
          View.Outlet = ValueSome (fun control -> outlet (control :?> 'view)) } :> _
        
    static member withConstructorArgs (constructorArgs: obj array) (view: IView<'view>) : IView<'view> =
        { View.ViewType = view.ViewType
          View.ViewKey = view.ViewKey
          View.Attrs = view.Attrs
          View.ConstructorArgs = constructorArgs
          View.Outlet = view.Outlet } :> _