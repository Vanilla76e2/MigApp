﻿#pragma checksum "..\..\..\..\..\..\MVVM\View\CRWindows\ComputersWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3E5A3783813D982B3DE817CA8A86BBE6209B9B41"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using MigApp.CustomElements;
using MigApp.MVVM.View.CRWindows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MigApp.MVVM.View.CRWindows {
    
    
    /// <summary>
    /// ComputersWindow
    /// </summary>
    public partial class ComputersWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\..\..\..\MVVM\View\CRWindows\ComputersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MigApp.CustomElements.IPAddressControl Test;
        
        #line default
        #line hidden
        
        
        #line 237 "..\..\..\..\..\..\MVVM\View\CRWindows\ComputersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Custom_MinimizeButton;
        
        #line default
        #line hidden
        
        
        #line 242 "..\..\..\..\..\..\MVVM\View\CRWindows\ComputersWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Custom_CloseButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MigApp;V2.0.0.12;component/mvvm/view/crwindows/computerswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\MVVM\View\CRWindows\ComputersWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Test = ((MigApp.CustomElements.IPAddressControl)(target));
            return;
            case 2:
            this.Custom_MinimizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 237 "..\..\..\..\..\..\MVVM\View\CRWindows\ComputersWindow.xaml"
            this.Custom_MinimizeButton.Click += new System.Windows.RoutedEventHandler(this.CustomUI_WindowControl);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Custom_CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 242 "..\..\..\..\..\..\MVVM\View\CRWindows\ComputersWindow.xaml"
            this.Custom_CloseButton.Click += new System.Windows.RoutedEventHandler(this.CustomUI_WindowControl);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

