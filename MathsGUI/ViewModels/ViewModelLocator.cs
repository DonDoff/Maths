/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MathsGUI"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MathsGUI.ViewModels {
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator() {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PlotViewModel>();
            SimpleIoc.Default.Register<PlotDataViewModel>();
            SimpleIoc.Default.Register<AddPlotDataViewModel>();
        }

        public MainViewModel Main {
            get {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public PlotViewModel Plot {
            get {
                return ServiceLocator.Current.GetInstance<PlotViewModel>();
            }
        }

        public PlotDataViewModel PlotData {
            get {
                return ServiceLocator.Current.GetInstance<PlotDataViewModel>();
            }
        }

        public AddPlotDataViewModel AddPlotData {
            get {
                return ServiceLocator.Current.GetInstance<AddPlotDataViewModel>();
            }
        }

        public static void Cleanup() {
            // TODO Clear the ViewModels
        }
    }
}