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
    public class PlotDatasViewModel : ViewModelBase {

        public PlotData SelectedPlotData { get; set; }

        private ObservableCollection<PlotData> plotDatas;
        public ObservableCollection<PlotData> PlotDatas {
            get {
                return plotDatas;
            }
            set {
                plotDatas = value;
                RaisePropertyChanged(() => PlotDatas);
            }
        }

        public RelayCommand AddPlotDataButtonHandler { get; set; }
        public RelayCommand RemovePlotDataButtonHandler { get; set; }

        public PlotDatasViewModel() {
            PlotDatas = new ObservableCollection<PlotData>();
            AddPlotDataButtonHandler = new RelayCommand(CreateAddPlotDataWindow);
            RemovePlotDataButtonHandler = new RelayCommand(RemovePlotData);
        }

        private void CreateAddPlotDataWindow() {
            AddPlotDataView w = new AddPlotDataView();
            w.Show();
        }

        private void RemovePlotData() {
            //ObservableCollection<PlotData> CopyOfPlotDatas = new ObservableCollection<PlotData>(PlotDatas);
            //for (int i = 0; i < CopyOfPlotDatas.Count; i++) {
            //    if (CopyOfPlotDatas[i] == SelectedPlotData) {
            //        CopyOfPlotDatas.RemoveAt(i);
            //    }
            //}
            //if (PlotDatas.Count > 0) {
                PlotDatas.Remove(SelectedPlotData);
                MessengerInstance.Send<object>(null, MessengerToken.PlotDataRemoved);
            //}
        }
    }
}
