﻿#pragma checksum "..\..\..\..\View\Windows\AuthWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B5C191CFE5CBE90D58FF3A9368B06D62BC267D0AA3843B1D4C34D87E6DCA7F39"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Otel.View.Windows;
using Otel.ViewModel;
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


namespace Otel.View.Windows {
    
    
    /// <summary>
    /// AuthWindow
    /// </summary>
    public partial class AuthWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 62 "..\..\..\..\View\Windows\AuthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox phoneTextBox;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\View\Windows\AuthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pswBox;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\View\Windows\AuthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pswTextBox;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\View\Windows\AuthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ShowPswCheckBox;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\..\View\Windows\AuthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label registrLabel;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\View\Windows\AuthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Otel;component/view/windows/authwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Windows\AuthWindow.xaml"
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
            this.phoneTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.pswBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 83 "..\..\..\..\View\Windows\AuthWindow.xaml"
            this.pswBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.pswBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pswTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ShowPswCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 103 "..\..\..\..\View\Windows\AuthWindow.xaml"
            this.ShowPswCheckBox.Click += new System.Windows.RoutedEventHandler(this.ShowPswCheckBox_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.registrLabel = ((System.Windows.Controls.Label)(target));
            
            #line 117 "..\..\..\..\View\Windows\AuthWindow.xaml"
            this.registrLabel.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.registrLabel_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 130 "..\..\..\..\View\Windows\AuthWindow.xaml"
            this.CancelButton.Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

