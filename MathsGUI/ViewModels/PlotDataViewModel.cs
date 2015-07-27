using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using MathsGUI.Views;
using MathsGUI.Models;
using System.Threading;

namespace MathsGUI.ViewModels {
    public class PlotDataViewModel : ViewModelBase {
        public ObservableCollection<PlotData> PlotDatas { get; set; }
        public RelayCommand AddPlotDataButtonHandler { get; set; }
        
        public PlotDataViewModel() {
            PlotDatas = new ObservableCollection<PlotData>();
            AddPlotDataButtonHandler = new RelayCommand(CreateAddPlotDataWindow);
        }

        private void CreateAddPlotDataWindow() {
            AddPlotDataView w = new AddPlotDataView();
            w.Show();
        }
    }
}
