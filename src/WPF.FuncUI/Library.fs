namespace WPF.FuncUI.Library

open System.Windows

module Observable =
    open System

    let subscribeWeakly (source: IObservable<'a>, callback: 'a -> unit, target: 'target) =
        let mutable sub: IDisposable = null
        let mutable disposed: bool = false
        let wr = WeakReference<'target>(target)

        let dispose() =
            lock (sub) (fun () ->
                if not disposed then sub.Dispose(); disposed <- true)

        let callback' x =
            let isAlive, _target = wr.TryGetTarget()
            if isAlive then callback x else dispose()

        sub <- Observable.subscribe callback' source
        sub

module internal Utils =
    let cast<'t>(a: obj) : 't =
        a :?> 't

[<AutoOpen>]
module internal Extensions =
    open System
    open System.Reactive.Linq
    open System.Windows.Media

    type IObservable<'a> with
        member this.SubscribeWeakly(callback: 'a -> unit, target) =
            Observable.subscribeWeakly(this, callback, target)


    type FrameworkElement with
        member this.GetObservable(routedEvent: RoutedEvent) : IObservable<RoutedEventArgs> =

            let sub = Func<IObserver<RoutedEventArgs>, IDisposable>(fun observer ->
                // push new update to subscribers
                let handler = RoutedEventHandler(fun _ e ->
                    observer.OnNext e
                )

                this.AddHandler(routedEvent, handler)

                { new IDisposable with

                    member _.Dispose () : unit =
                        this.RemoveHandler(routedEvent, handler)
                }
                
                // subscribe to event changes so they can be pushed to subscribers
                //this.AddDisposableHandler(routedEvent, handler, routedEvent.RoutingStrategies)
            )
            
            Observable.Create(sub)

    type Brush with
        
        static member Parse (color: string) : Brush =
            // might fail?
            BrushConverter().ConvertFromString(color) :?> Brush