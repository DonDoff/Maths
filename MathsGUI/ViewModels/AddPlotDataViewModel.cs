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

        public RelayCommand AddPlotDataHandler { get; set; }
        public RelayCommand GenerateRandomVectorButtonHandler { get; set; }
        
        public AddPlotDataViewModel() {
            PlotData = new PlotData(new Vector(0), new Vector(0), "");
            AddPlotDataHandler = new RelayCommand(HandleAddPlotData);
            GenerateRandomVectorButtonHandler = new RelayCommand(HandleGenerateRandomData);
        }

        private void HandleAddPlotData() {
            Vector x = Vector.ParseFrom(XString);
            Vector y = Vector.ParseFrom(YString);
            string name = PlotData.Name;

            PlotData pd = new PlotData(x, y, name);
            new ViewModelLocator().PlotDatas.PlotDatas.Add(pd);
            MessengerInstance.Send<object>(null, MessengerToken.PlotDataAdded);
        }

        private void HandleGenerateRandomData() {
            PlotData.X = Vector.Arrange(10);
            PlotData.Y = MatrixFactory.Real(10, 1, new Random()).ToColumnVector();
            PlotData.Name = "Random data";

            XString = PlotData.X.ToStringAsInput();
            YString = PlotData.Y.ToStringAsInput();
        }

    }
}

