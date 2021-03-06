﻿using System;
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
        public RelayCommand<PlotData> RemovePlotDataButtonHandler { get; set; }
        public RelayCommand EditPlotDataWindowButtonHandler { get; set; }

        public PlotDatasViewModel() {
            PlotDatas = new ObservableCollection<PlotData>();
            AddPlotDataButtonHandler = new RelayCommand(CreateAddPlotDataWindow);
            RemovePlotDataButtonHandler = new RelayCommand<PlotData>(RemovePlotData);
            EditPlotDataWindowButtonHandler = new RelayCommand(EditPlotDataWindow);
        }

        private void CreateAddPlotDataWindow() {
            AddPlotDataView w = new AddPlotDataView();
            w.Show();
        }

        private void RemovePlotData(PlotData pd) {
            PlotDatas.Remove(pd);
            MessengerInstance.Send<object>(null, MessengerToken.PlotDataRemoved);
        }

        private void EditPlotDataWindow() {
            EditPlotDataWindowView w = new EditPlotDataWindowView();
            w.Show();
        }
    }
}
