using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MathsGUI.Models;
using Maths;

namespace MathsGUI.ViewModels {
    public class EditPlotDataWindowViewModel : ViewModelBase, ICloseable {

        public event EventHandler<EventArgs> RequestClose;
        public RelayCommand<PlotData> EditPlotDataButtonHandler { get; private set; }

        public EditPlotDataWindowViewModel() {
            EditPlotDataButtonHandler = new RelayCommand<PlotData>(EditPlotData);
        }

        private void EditPlotData(PlotData pd) {
            //EditPlotDataViewModel editPlotDataViewModel = new ViewModelLocator().EditPlotData;
            //editPlotDataViewModel.PlotData = pd;
            //editPlotDataViewModel.XString = pd.X.ToStringAsInput();
            //editPlotDataViewModel.YString = pd.Y.ToStringAsInput();

            MessengerInstance.Send<object>(null, MessengerToken.PlotDataEdited);

            // Close the window
            EventHandler<EventArgs> handler = RequestClose;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }


    }
}
