namespace WPF.FuncUI.VirtualDom

open System.Threading
open System
open System.Collections.Concurrent
open System.Windows

type ViewMetaData() =
    inherit DependencyObject()

    static let viewId = DependencyProperty.RegisterAttached("ViewId", typeof<Guid>, typeof<FrameworkElement>)

    /// Avalonia automatically adds subscriptions that are setup in XAML to a disposable bag (or something along the lines).
    /// This basically is what FuncUI uses instead to make sure it cancels subscriptions.
    static let viewSubscriptions = DependencyProperty.RegisterAttached("ViewSubscriptions", typeof<ConcurrentDictionary<string, CancellationTokenSource>>,  typeof<FrameworkElement>)

    static member ViewIdProperty = viewId

    static member ViewSubscriptionsProperty = viewSubscriptions

    static member GetViewId(control: FrameworkElement) : Guid =
        control.GetValue(ViewMetaData.ViewIdProperty) :?> Guid 

    static member SetViewId(control: FrameworkElement, value: Guid) : unit =
        control.SetValue(ViewMetaData.ViewIdProperty, value) |> ignore

    static member GetViewSubscriptions(control: FrameworkElement) : ConcurrentDictionary<string, CancellationTokenSource> =
        control.GetValue(ViewMetaData.ViewSubscriptionsProperty) :?> ConcurrentDictionary<_, _>

    static member SetViewSubscriptions(control: FrameworkElement, value) : unit =
        control.SetValue(ViewMetaData.ViewSubscriptionsProperty, value) |> ignore


