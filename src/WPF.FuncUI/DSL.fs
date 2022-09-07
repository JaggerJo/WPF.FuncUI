namespace WPF.FuncUI.DSL
open System.Windows.Controls
open System.Windows.Media
open System.Windows
open WPF.FuncUI.Library
open WPF.FuncUI.Builder
open WPF.FuncUI.Types
open System.Windows.Controls.Primitives

[<AutoOpen>]
module UIElement =  

    type UIElement with

        static member isEnabled<'t when 't :> UIElement>(value: bool) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<bool>(UIElement.IsEnabledProperty, value, ValueNone)

[<AutoOpen>]
module FrameworkElement =  

    type FrameworkElement with

        static member height<'t when 't :> FrameworkElement>(value: float) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<float>(FrameworkElement.HeightProperty, value, ValueNone)

        static member width<'t when 't :> FrameworkElement>(value: float) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<float>(FrameworkElement.WidthProperty, value, ValueNone)

        static member maxHeight<'t when 't :> FrameworkElement>(value: float) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<float>(FrameworkElement.MaxHeightProperty, value, ValueNone)

        static member maxWidth<'t when 't :> FrameworkElement>(value: float) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<float>(FrameworkElement.MaxWidthProperty, value, ValueNone)

        static member minHeight<'t when 't :> FrameworkElement>(value: float) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<float>(FrameworkElement.MinHeightProperty, value, ValueNone)

        static member minWidth<'t when 't :> FrameworkElement>(value: float) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<float>(FrameworkElement.MinWidthProperty, value, ValueNone)

        static member verticalAlignment<'t when 't :> FrameworkElement>(value: VerticalAlignment) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<VerticalAlignment>(FrameworkElement.VerticalAlignmentProperty, value, ValueNone)

        static member horizontalAlignment<'t when 't :> FrameworkElement>(value: HorizontalAlignment) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<HorizontalAlignment>(FrameworkElement.HorizontalAlignmentProperty, value, ValueNone)

        static member margin<'t when 't :> FrameworkElement>(value: Thickness) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Thickness>(FrameworkElement.MarginProperty, value, ValueNone)

        static member margin<'t when 't :> FrameworkElement>(left: float, top: float, right: float, bottom: float) : IAttr<'t> =
            (left, top, right, bottom) |> Thickness |> FrameworkElement.margin

        static member margin<'t when 't :> FrameworkElement>(value: float) : IAttr<'t> =
            value |> Thickness |> FrameworkElement.margin


[<AutoOpen>]
module Control =  

    type Control with

        static member padding<'t when 't :> Control>(value: Thickness) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Thickness>(Control.PaddingProperty, value, ValueNone)

        static member padding<'t when 't :> Control>(left: float, top: float, right: float, bottom: float) : IAttr<'t> =
            (left, top, right, bottom) |> Thickness |> Control.padding

        static member padding<'t when 't :> Control>(value: float) : IAttr<'t> =
            value |> Thickness |> Control.padding


[<AutoOpen>]
module Border =  

    type Border with

        static member padding<'t when 't :> Border>(value: Thickness) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Thickness>(Border.PaddingProperty, value, ValueNone)

        static member padding<'t when 't :> Border>(left: float, top: float, right: float, bottom: float) : IAttr<'t> =
            (left, top, right, bottom) |> Thickness |> Border.padding

        static member padding<'t when 't :> Border>(value: float) : IAttr<'t> =
            value |> Thickness |> Border.padding

[<AutoOpen>]
module TextBlock =  

    let create (attrs: IAttr<TextBlock> list): IView<TextBlock> =
        ViewBuilder.Create<TextBlock>(attrs)
    
    type TextBlock with
            
        static member text<'t when 't :> TextBlock>(value: string) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<string>(TextBlock.TextProperty, value, ValueNone)
            
        static member background<'t when 't :> TextBlock>(value: Brush) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Brush>(TextBlock.BackgroundProperty, value, ValueNone)
            
        static member background<'t when 't :> TextBlock>(color: string) : IAttr<'t> =
            color |> Brush.Parse |> TextBlock.background
        
        static member fontFamily<'t when 't :> TextBlock>(value: FontFamily) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<FontFamily>(TextBlock.FontFamilyProperty, value, ValueNone)
            
        static member fontSize<'t when 't :> TextBlock>(value: double) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<double>(TextBlock.FontSizeProperty, value, ValueNone)
            
        static member fontStyle<'t when 't :> TextBlock>(value: FontStyle) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<FontStyle>(TextBlock.FontStyleProperty, value, ValueNone)
            
        static member fontWeight<'t when 't :> TextBlock>(value: FontWeight) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<FontWeight>(TextBlock.FontWeightProperty, value, ValueNone)
            
        static member foreground<'t when 't :> TextBlock>(value: Brush) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Brush>(TextBlock.ForegroundProperty, value, ValueNone)
            
        static member foreground<'t when 't :> TextBlock>(color: string) : IAttr<'t> =
            color |> Brush.Parse |> TextBlock.foreground

        static member lineHeight<'t when 't :> TextBlock>(value: float) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<float>(TextBlock.LineHeightProperty, value, ValueNone)
            
        static member padding<'t when 't :> TextBlock>(value: Thickness) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Thickness>(TextBlock.PaddingProperty, value, ValueNone)

        static member padding<'t when 't :> TextBlock>(left: float, top: float, right: float, bottom: float) : IAttr<'t> =
            (left, top, right, bottom) |> Thickness |> TextBlock.padding

        static member padding<'t when 't :> TextBlock>(value: float) : IAttr<'t> =
            value |> Thickness |> TextBlock.padding

        static member textAlignment<'t when 't :> TextBlock>(alignment: TextAlignment) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<TextAlignment>(TextBlock.TextAlignmentProperty, alignment, ValueNone)

        static member textDecorations<'t when 't :> TextBlock>(value: TextDecorationCollection) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<TextDecorationCollection>(TextBlock.TextDecorationsProperty, value, ValueNone)

        static member textTrimming<'t when 't :> TextBlock>(value: TextTrimming) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<TextTrimming>(TextBlock.TextTrimmingProperty, value, ValueNone)
            
        static member textWrapping<'t when 't :> TextBlock>(value: TextWrapping) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<TextWrapping>(TextBlock.TextWrappingProperty, value, ValueNone)

[<AutoOpen>]
module DockPanel =  
    
    let create (attrs: IAttr<DockPanel> list): IView<DockPanel> =
        ViewBuilder.Create<DockPanel>(attrs)
    
    type FrameworkElement with
        static member dock<'t when 't :> FrameworkElement>(dock: Dock) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Dock>(DockPanel.DockProperty, dock, ValueNone)
    
    type DockPanel with
        static member lastChildFill<'t when 't :> DockPanel>(fill: bool) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<bool>(DockPanel.LastChildFillProperty, fill, ValueNone)

[<AutoOpen>]
module Panel =  

    type Panel with
            
        static member children<'t when 't :> Panel>(value: IView list) : IAttr<'t> =
            let getter : ('t -> obj) = (fun control -> control.Children :> obj)
             
            AttrBuilder<'t>.CreateContentMultiple("Children", ValueSome getter, ValueNone, value)
            
        static member background<'t when 't :> Panel>(value: Brush) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Brush>(Panel.BackgroundProperty, value, ValueNone)
            
        static member background<'t when 't :> Panel>(color: string) : IAttr<'t> =
            color |> Brush.Parse |> Panel.background 

[<AutoOpen>]
module ButtonBase =
    open System.Windows.Input

    type ButtonBase with

        static member clickMode<'t when 't :> ButtonBase>(value: ClickMode) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<ClickMode>(ButtonBase.ClickModeProperty, value, ValueNone)

        static member command<'t when 't :> ButtonBase>(value: ICommand) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<ICommand>(ButtonBase.CommandProperty, value, ValueNone)

        static member commandParameter<'t when 't :> ButtonBase>(value: obj) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<obj>(ButtonBase.CommandParameterProperty, value, ValueNone)

        static member isPressed<'t when 't :> ButtonBase>(value: bool) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<bool>(ButtonBase.IsPressedProperty, value, ValueNone)

        static member onClick<'t when 't :> ButtonBase>(func: RoutedEventArgs -> unit, ?subPatchOptions) =
            AttrBuilder<'t>.CreateSubscription(ButtonBase.ClickEvent, func, ?subPatchOptions = subPatchOptions)

[<AutoOpen>]
module Button =

    let create (attrs: IAttr<Button> list): IView<Button> =
        ViewBuilder.Create<Button>(attrs)

    type Button with

        static member isDefault<'t when 't :> Button>(value: bool) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<bool>(Button.IsDefaultProperty, value, ValueNone)

[<AutoOpen>]
module ToggleButton =

    let create (attrs: IAttr<ToggleButton> list): IView<ToggleButton> =
        ViewBuilder.Create<ToggleButton>(attrs)

    type ToggleButton with

        static member isChecked<'t when 't :> ToggleButton>(value: System.Nullable<bool>) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<_>(ToggleButton.IsCheckedProperty, value, ValueNone)

        static member isThreeState<'t when 't :> ToggleButton>(value: bool) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<bool>(ToggleButton.IsThreeStateProperty, value, ValueNone)

        static member onChecked<'t when 't :> ToggleButton>(func: RoutedEventArgs -> unit, ?subPatchOptions) =
            AttrBuilder<'t>.CreateSubscription(ToggleButton.CheckedEvent, func, ?subPatchOptions = subPatchOptions)

        static member onUnchecked<'t when 't :> ToggleButton>(func: RoutedEventArgs -> unit, ?subPatchOptions) =
            AttrBuilder<'t>.CreateSubscription(ToggleButton.UncheckedEvent, func, ?subPatchOptions = subPatchOptions)
    
        static member onIndeterminate<'t when 't :> ToggleButton>(func: RoutedEventArgs -> unit, ?subPatchOptions) =
            AttrBuilder<'t>.CreateSubscription(ToggleButton.IndeterminateEvent, func, ?subPatchOptions = subPatchOptions)

[<AutoOpen>]
module CheckBox =

    let create (attrs: IAttr<CheckBox> list): IView<CheckBox> =
        ViewBuilder.Create<CheckBox>(attrs)

[<AutoOpen>]
module ContentControl =

    let create (attrs : IAttr<ContentControl> list) : IView<ContentControl> =
        ViewBuilder.Create<ContentControl>(attrs)

    type ContentControl with
        static member content<'t when 't :> ContentControl>(text: string) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<string>(ContentControl.ContentProperty, text, ValueNone)

        static member content<'t when 't :> ContentControl>(value: obj) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<obj>(ContentControl.ContentProperty, value, ValueNone)

        static member content<'t when 't :> ContentControl>(value: IView option) : IAttr<'t> =
            AttrBuilder<'t>.CreateContentSingle(ContentControl.ContentProperty, value)

        static member content<'t when 't :> ContentControl>(value: IView) : IAttr<'t> =
            value
            |> Some
            |> ContentControl.content

        static member horizontalAlignment<'t when 't :> ContentControl>(value: HorizontalAlignment) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<HorizontalAlignment>(ContentControl.HorizontalAlignmentProperty, value, ValueNone)

        static member verticalAlignment<'t when 't :> ContentControl>(value: VerticalAlignment) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<VerticalAlignment>(ContentControl.VerticalAlignmentProperty, value, ValueNone)

        static member horizontalContentAlignment<'t when 't :> ContentControl>(value: HorizontalAlignment) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<HorizontalAlignment>(ContentControl.HorizontalContentAlignmentProperty, value, ValueNone)

        static member verticalContentAlignment<'t when 't :> ContentControl>(value: VerticalAlignment) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<VerticalAlignment>(ContentControl.VerticalContentAlignmentProperty, value, ValueNone)


[<AutoOpen>]
module DataGrid =

    let create (attrs : IAttr<DataGrid> list) : IView<DataGrid> =
        ViewBuilder.Create<DataGrid>(attrs)


[<AutoOpen>]
module TextBoxBase =  
    
    type TextBoxBase with
            
        static member acceptsReturn<'t when 't :> TextBoxBase>(value: bool) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<bool>(TextBoxBase.AcceptsReturnProperty, value, ValueNone)

        static member acceptsTab<'t when 't :> TextBoxBase>(value: bool) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<bool>(TextBoxBase.AcceptsTabProperty, value, ValueNone)      

        static member isReadOnly<'t when 't :> TextBoxBase>(value: bool) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<bool>(TextBoxBase.IsReadOnlyProperty, value, ValueNone)      
            
        static member onTextChanged<'t when 't :> TextBoxBase>(func: RoutedEventArgs -> unit, ?subPatchOptions) =
            AttrBuilder<'t>.CreateSubscription(TextBoxBase.TextChangedEvent, func, ?subPatchOptions = subPatchOptions)

[<AutoOpen>]
module TextBox =  

    let create (attrs: IAttr<TextBox> list): IView<TextBox> =
        ViewBuilder.Create<TextBox>(attrs)
    
    
    type TextBox with

        static member text<'t when 't :> TextBox>(text: string) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<_>(TextBox.TextProperty, text, ValueNone)

        static member textAlignment<'t when 't :> TextBox>(alignment: TextAlignment) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<_>(TextBox.TextAlignmentProperty, alignment, ValueNone)
    
        static member textDecorations<'t when 't :> TextBox>(value: TextDecorationCollection) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<_>(TextBox.TextDecorationsProperty, value, ValueNone)
    
        static member textWrapping<'t when 't :> TextBox>(value: TextWrapping) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<_>(TextBox.TextWrappingProperty, value, ValueNone)

[<AutoOpen>]
module UniformGrid =  

    let create (attrs: IAttr<UniformGrid> list): IView<UniformGrid> =
        ViewBuilder.Create<UniformGrid>(attrs)
    
    
    type UniformGrid with

        static member columns<'t when 't :> UniformGrid>(value: int) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<_>(UniformGrid.ColumnsProperty, value, ValueNone)

        static member rows<'t when 't :> UniformGrid>(value: int) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<_>(UniformGrid.RowsProperty, value, ValueNone)