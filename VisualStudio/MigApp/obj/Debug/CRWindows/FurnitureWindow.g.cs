﻿#pragma checksum "..\..\..\CRWindows\FurnitureWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "809541A1FDD2004B88C9BF35E76152F12681F840BAD57E80F6CC06E1B957CE1F"
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
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace MigApp.CRWindows {
    
    
    /// <summary>
    /// FurnitureWindow
    /// </summary>
    public partial class FurnitureWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\CRWindows\FurnitureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InvNum;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\CRWindows\FurnitureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Type;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\CRWindows\FurnitureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TypeAdd;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\CRWindows\FurnitureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FurnitureName;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\CRWindows\FurnitureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Room;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\CRWindows\FurnitureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Comment;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\CRWindows\FurnitureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DoneButton;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\CRWindows\FurnitureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RecoveryButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\CRWindows\FurnitureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MigApp;component/crwindows/furniturewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\CRWindows\FurnitureWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.InvNum = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\..\CRWindows\FurnitureWindow.xaml"
            this.InvNum.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumOnly);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Type = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.TypeAdd = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\CRWindows\FurnitureWindow.xaml"
            this.TypeAdd.Click += new System.Windows.RoutedEventHandler(this.CreateNewType);
            
            #line default
            #line hidden
            return;
            case 4:
            this.FurnitureName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Room = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Comment = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.DoneButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\CRWindows\FurnitureWindow.xaml"
            this.DoneButton.Click += new System.Windows.RoutedEventHandler(this.SaveClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.RecoveryButton = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\CRWindows\FurnitureWindow.xaml"
            this.RecoveryButton.Click += new System.Windows.RoutedEventHandler(this.RecoveryClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.DeleteButton = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\CRWindows\FurnitureWindow.xaml"
            this.DeleteButton.Click += new System.Windows.RoutedEventHandler(this.DeleteClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

