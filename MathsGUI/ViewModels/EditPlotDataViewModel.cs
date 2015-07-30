using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MathsGUI.Models;
using Maths;

namespace MathsGUI.ViewModels {
    public class EditPlotDataViewModel : ViewModelBase {

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

        private string xString;
        public string XString {
            get {
                return xString;
            }
            set {
                xString = value;
                RaisePropertyChanged(() => XString);
            }
        }

        private string yString;
        public string YString {
            get {
                return yString;
            }
            set {
                yString = value;
                RaisePropertyChanged(() => YString);
            }
        }

        public EditPlotDataViewModel() {
            PlotData = new PlotData(new Vector(0), new Vector(0), "");
        }

    }
}
