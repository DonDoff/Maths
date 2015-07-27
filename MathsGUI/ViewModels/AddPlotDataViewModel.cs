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
        RelayCommand AddPlotDataHandler { get; set; }

        public AddPlotDataViewModel() {
            AddPlotDataHandler = new RelayCommand(HandleAddPlotData);
        }

        private void HandleAddPlotData() {
            Vector x = Vector.ParseFrom("1, 2, 3, 4, 5");
            Vector y = x.ElementMultiply(x).ToColumnVector();
            string name = "Squared";

            PlotData pd = new PlotData(x, y, name);
            new ViewModelLocator().PlotData.PlotDatas.Add(pd);
        }

    }
}

