﻿#pragma checksum "..\..\SellReport.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "71B2E735E02C121CE1259771CE9ABAA641827B5F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LiveCharts.Wpf;
using Milestone;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Milestone {
    
    
    /// <summary>
    /// SellReport
    /// </summary>
    public partial class SellReport : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\SellReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtWelcome;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\SellReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtWelcome1;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\SellReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpStart;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\SellReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpEnd;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\SellReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.CartesianChart columnChart;
        
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
            System.Uri resourceLocater = new System.Uri("/Milestone;component/sellreport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SellReport.xaml"
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
            
            #line 9 "..\..\SellReport.xaml"
            ((Milestone.SellReport)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtWelcome = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtWelcome1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.dpStart = ((System.Windows.Controls.DatePicker)(target));
            
            #line 14 "..\..\SellReport.xaml"
            this.dpStart.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dpStart_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dpEnd = ((System.Windows.Controls.DatePicker)(target));
            
            #line 15 "..\..\SellReport.xaml"
            this.dpEnd.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dpEnd_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.columnChart = ((LiveCharts.Wpf.CartesianChart)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

