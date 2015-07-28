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
        private PlotModel plotModel;
        public PlotModel PlotModel {
            get {
                return plotModel;
            }
            set {
                plotModel = value;
                RaisePropertyChanged(() => PlotModel);
            }
        }

        public PlotViewModel() {
            PlotModel = new PlotModel();
            UpdateModel();
            MessengerInstance.Register<object>(this, MessengerToken.PlotDataAdded, o => UpdateModel());
            MessengerInstance.Register<object>(this, MessengerToken.PlotDataRemoved, o => UpdateModel());
        }

        private void UpdateModel() {
            PlotModel.Series.Clear();
            PlotModel.Axes.Clear();
            ICollection<PlotData> pds = (new ViewModelLocator()).PlotDatas.PlotDatas;

            if (pds.Count == 0) {
                PlotModel = new PlotModel();
                return;
            }

            double minX = double.MaxValue, maxX = double.MinValue;
            double minY = double.MaxValue, maxY = double.MinValue;

            foreach (PlotData pd in pds) {
                LineSeries l = new LineSeries();
                for (int i = 0; i < pd.X.Size; i++) {
                    l.Points.Add(new DataPoint(pd.X[i].R, pd.Y[i].R));
                    l.Title = pd.Name;

                    if (MatrixMath.Min(pd.X).R < minX) {
                        minX = MatrixMath.Min(pd.X).R;
                    }
                    if (MatrixMath.Max(pd.X).R > maxX) {
                        maxX = MatrixMath.Max(pd.X).R;
                    }
                    if (MatrixMath.Min(pd.Y).R < minY) {
                        minY = MatrixMath.Min(pd.Y).R;
                    }
                    if (MatrixMath.Max(pd.Y).R > maxY) {
                        maxY = MatrixMath.Max(pd.Y).R;
                    }
                }

                PlotModel.Series.Add(l);
            }

            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = minX, Maximum = maxX });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = minY, Maximum = maxY });

            PlotModel.InvalidatePlot(true);
        }
    }
}
