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
        public RelayCommand<PlotData> EditPlotDataButtonHandler { get; set; }

        public PlotDatasViewModel() {
            PlotDatas = new ObservableCollection<PlotData>();
            AddPlotDataButtonHandler = new RelayCommand(CreateAddPlotDataWindow);
            RemovePlotDataButtonHandler = new RelayCommand(RemovePlotData);
            EditPlotDataButtonHandler = new RelayCommand<PlotData>(EditPlotData);
        }

        private void CreateAddPlotDataWindow() {
            PlotDataEditorView w = new PlotDataEditorView();
            w.Show();
        }

        private void RemovePlotData() {
            PlotDatas.Remove(SelectedPlotData);
            MessengerInstance.Send<object>(null, MessengerToken.PlotDataRemoved);
        }

        private void EditPlotData(PlotData pd) {
            PlotDataEditorView w = new PlotDataEditorView();
            w.Show();
        }
    }
}
