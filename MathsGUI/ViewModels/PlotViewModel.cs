using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Reporting;
using GalaSoft.MvvmLight;
using Maths;
using MathsGUI.Models;

namespace MathsGUI.ViewModels {
    public class PlotViewModel : ViewModelBase {

        //private PlotModel plotModel;
        //public PlotModel PlotModel {
        //    get {
        //        return plotModel;
        //    }
        //    set {
        //        plotModel = value;
        //        //RaisePropertyChanged(() => PlotModel);
        //    }
        //}

        public PlotModel PlotModel { get; set; }

        public PlotViewModel() {
            //PlotModel = new PlotModel();
            //SetUpModel();
        }

        //private void SetUpModel() {
        //    List<Vector> xs = new List<Vector>();
        //    List<Vector> ys = new List<Vector>();

        //    //ICollection<PlotData> pds = (new ViewModelLocator()).PlotData.PlotDatas;
        //    //foreach (PlotData p in pds) {
        //    //    xs.Add(p.X);
        //    //    ys.Add(p.Y);
        //    //}

        //    double minX = double.MaxValue, maxX = double.MinValue;
        //    double minY = double.MaxValue, maxY = double.MinValue;

        //    for (int i = 0; i < xs.Count; i++) {
        //        LineSeries l = new LineSeries();
        //        for (int j = 0; j < xs[i].Size; j++) {
        //            l.Points.Add(new DataPoint(xs[i][j].R, ys[i][j].R));
        //        }

        //        PlotModel.Series.Add(l);
        //    }

        //    for (int i = 0; i < xs.Count; i++) {
        //        if (MatrixMath.Min(xs[i]).R < minX) {
        //            minX = MatrixMath.Min(xs[i]).R;
        //        }
        //        if (MatrixMath.Max(xs[i]).R > maxX) {
        //            maxX = MatrixMath.Max(xs[i]).R;
        //        }
        //        if (MatrixMath.Min(ys[i]).R < minY) {
        //            minY = MatrixMath.Min(ys[i]).R;
        //        }
        //        if (MatrixMath.Max(ys[i]).R > maxY) {
        //            maxY = MatrixMath.Max(ys[i]).R;
        //        }
        //    }

        //    PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = minX, Maximum = maxX });
        //    PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = minY, Maximum = maxY });
        //}
    }
}
