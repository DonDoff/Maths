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

        public RelayCommand AddPlotDataHandler { get; set; }
        public RelayCommand GenerateRandomVectorButtonHandler { get; set; }
        public RelayCommand GenerateSinusoidButtonHandler { get; set; }
        private EditPlotDataViewModel editPlotDataViewModel;
        
        public AddPlotDataViewModel() {
            editPlotDataViewModel = new ViewModelLocator().EditPlotData;

            AddPlotDataHandler = new RelayCommand(HandleAddPlotData);
            GenerateRandomVectorButtonHandler = new RelayCommand(HandleGenerateRandomData);
            GenerateSinusoidButtonHandler = new RelayCommand(HandleSinusoid);
        }

        private void HandleAddPlotData() {
            Vector x = Vector.ParseFrom(editPlotDataViewModel.XString);
            Vector y = Vector.ParseFrom(editPlotDataViewModel.YString);
            string name = editPlotDataViewModel.PlotData.Name;

            PlotData pd = new PlotData(x, y, name);
            new ViewModelLocator().PlotDatas.PlotDatas.Add(pd);
            MessengerInstance.Send<object>(null, MessengerToken.PlotDataAdded);
        }

        private void HandleGenerateRandomData() {
            editPlotDataViewModel.PlotData.X = Vector.Arrange(10);
            editPlotDataViewModel.PlotData.Y = MatrixFactory.Real(10, 1, new Random()).ToColumnVector();
            editPlotDataViewModel.PlotData.Name = "Random data";

            editPlotDataViewModel.XString = editPlotDataViewModel.PlotData.X.ToStringAsInput();
            editPlotDataViewModel.YString = editPlotDataViewModel.PlotData.Y.ToStringAsInput();
        }

        private void HandleSinusoid() {
            editPlotDataViewModel.PlotData.X = Vector.Arrange(10);
            editPlotDataViewModel.PlotData.Y = MatrixMath.Sin(editPlotDataViewModel.PlotData.X).ToColumnVector();
            editPlotDataViewModel.PlotData.Name = "Sinusoid";

            editPlotDataViewModel.XString = editPlotDataViewModel.PlotData.X.ToStringAsInput();
            editPlotDataViewModel.YString = editPlotDataViewModel.PlotData.Y.ToStringAsInput();
        }

    }
}

