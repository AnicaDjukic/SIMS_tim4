﻿#pragma checksum "..\..\..\..\Forms\FormNapraviTerminLekar.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9CE55559011CB09F6D1070CC3D87D8F43AEEB3FA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Bolnica.Forms;
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


namespace Bolnica.Forms {
    
    
    /// <summary>
    /// FormNapraviTerminLekar
    /// </summary>
    public partial class FormNapraviTerminLekar : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textDatum;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textTrajanje;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textIme;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textPrezime;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelTextOperacija;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkOperacija;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox textOperacija;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Bolnica;component/forms/formnapraviterminlekar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.textDatum = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.textTrajanje = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.textIme = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textPrezime = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.labelTextOperacija = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.checkOperacija = ((System.Windows.Controls.CheckBox)(target));
            
            #line 40 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
            this.checkOperacija.Click += new System.Windows.RoutedEventHandler(this.isOperacija);
            
            #line default
            #line hidden
            return;
            case 7:
            this.textOperacija = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            
            #line 44 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PotvrdiIzmenu);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 45 "..\..\..\..\Forms\FormNapraviTerminLekar.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OtkaziIzmenu);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

