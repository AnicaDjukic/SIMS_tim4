﻿#pragma checksum "..\..\..\..\Forms\FormNapraviTerminPacijent.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DB3F01CE86B5822BBF77C69934B34B7187EE7D80"
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
    /// FormNapraviTerminPacijent
    /// </summary>
    public partial class FormNapraviTerminPacijent : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\Forms\FormNapraviTerminPacijent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datumPicker;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\Forms\FormNapraviTerminPacijent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboSat;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Forms\FormNapraviTerminPacijent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboMinut;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Forms\FormNapraviTerminPacijent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboLekar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Bolnica;component/forms/formnapraviterminpacijent.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Forms\FormNapraviTerminPacijent.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.datumPicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 2:
            this.comboSat = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.comboMinut = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.comboLekar = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            
            #line 59 "..\..\..\..\Forms\FormNapraviTerminPacijent.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Potvrdi);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 60 "..\..\..\..\Forms\FormNapraviTerminPacijent.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Otkazi);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

