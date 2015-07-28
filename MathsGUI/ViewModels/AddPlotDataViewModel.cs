using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Maths;
using MathsGUI.Models;

namespace MathsGUI.ViewModels {
    public class AddPlotDataViewModel : ViewModelBase {

        private PlotData plotData;
        public PlotData PlotData {
            get {
                return plotData;
            }
            set {
                plotData = value;
                RaisePropertyChanged(() => PlotData);
            }
        }

        public RelayCommand AddPlotDataHandler { get; set; }

        public AddPlotDataViewModel() {
            PlotData = new PlotData(new Vector(0), new Vector(0), "");
            AddPlotDataHandler = new RelayCommand(HandleAddPlotData);
        }

        private void HandleAddPlotData() {
            Vector x = Vector.ParseFrom(PlotData.XString);
            Vector y = Vector.ParseFrom(PlotData.YString);
            //Vector x = Vector.Arrange(10);
            //Vector y = MatrixFactory.Real(x.Size, 1, new Random()).ToColumnVector();
            string name = PlotData.Name;

            PlotData pd = new PlotData(x, y, name);
            new ViewModelLocator().PlotDatas.PlotDatas.Add(pd);
            MessengerInstance.Send<object>(null, MessengerToken.PlotDataAdded);
        }

    }
}

