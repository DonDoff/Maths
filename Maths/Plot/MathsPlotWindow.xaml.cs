﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using System.Threading;

namespace Maths {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MathsPlotWindow : Window {
        public MathsPlotViewModel ViewModel { get; set; }

        public MathsPlotWindow(List<Vector> xs, List<Vector> ys)
        {
            InitializeComponent();
            ViewModel = new MathsPlotViewModel(xs, ys);
            DataContext = ViewModel;
        }
    }
}
