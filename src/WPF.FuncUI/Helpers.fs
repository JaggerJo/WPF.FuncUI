namespace WPF.FuncUI

[<AutoOpen>]
module Helpers =
    open WPF.FuncUI.Types
    
    let generalize (view: IView<'t>) : IView =
        view :> IView
        