using System.ComponentModel;
using Maths;
using JetBrains.Annotations;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Reporting;
using System.Collections.Generic;
using System;

namespace Maths {
    public class MathsPlotWindowModel : INotifyPropertyChanged {

        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }
        
        public MathsPlotWindowModel(List<ColumnVector> xs, List<ColumnVector> ys)
        {
            PlotModel = new PlotModel();
            SetUpModel(xs, ys);
        }
 
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetUpModel(List<ColumnVector> xs, List<ColumnVector> ys) {
            double minX = double.MaxValue, maxX = double.MinValue;
            double minY = double.MaxValue, maxY = double.MinValue;

            for (int i = 0; i < xs.Count; i++) {
                LineSeries l = new LineSeries();
                for (int j = 0; j < xs[i].Size; j++) {
                    l.Points.Add(new DataPoint(xs[i][j].R, ys[i][j].R));
                }
                //ScatterSeries l = new ScatterSeries();
                //for (int j = 0; j < xs[i].Size; j++) {
                //    l.Points.Add(new ScatterPoint(xs[i][j].R, ys[i][j].R));
                //}

                PlotModel.Series.Add(l);
            }

            for (int i = 0; i < xs.Count; i++) {
                if (MatrixMath.Min(xs[i]).R < minX) {
                    minX = MatrixMath.Min(xs[i]).R;
                }
                if (MatrixMath.Max(xs[i]).R > maxX) {
                    maxX = MatrixMath.Max(xs[i]).R;
                }
                if (MatrixMath.Min(ys[i]).R < minY) {
                    minY = MatrixMath.Min(ys[i]).R;
                }
                if (MatrixMath.Max(ys[i]).R > maxY) {
                    maxY = MatrixMath.Max(ys[i]).R;
                }
            }

            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = minX, Maximum = maxX });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = minY, Maximum = maxY });
        }
        
    }
}