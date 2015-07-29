using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Maths;
using MathsGUI.Models;
using OxyPlot;
using System.Windows.Media;

namespace MathsGUI.ViewModels {
    public class PlotDataEditorViewModel : ViewModelBase {

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

        public RelayCommand EditPlotDataHandler { get; set; }

        public PlotDataEditorViewModel() {
            PlotData = new PlotData(new Vector(0), new Vector(0), "");
            EditPlotDataHandler = new RelayCommand(HandleEditPlotData);
        }

        public PlotDataEditorViewModel(PlotData pd) {
            PlotData = pd;
            EditPlotDataHandler = new RelayCommand(HandleEditPlotData);
        }

        private void HandleEditPlotData() {
            Vector x = Vector.ParseFrom(PlotData.XString);
            Vector y = Vector.ParseFrom(PlotData.YString);
            string name = PlotData.Name;

            PlotData pd = new PlotData(x, y, name);
            new ViewModelLocator().PlotDatas.PlotDatas.Add(pd);
            MessengerInstance.Send<object>(null, MessengerToken.PlotDataEdited);
        }

    }
}

