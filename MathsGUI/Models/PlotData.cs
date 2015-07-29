using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maths;
using GalaSoft.MvvmLight;

namespace MathsGUI.Models {
    public class PlotData : ObservableObject {
        private Vector x;
        public Vector X {
            get {
                return x;
            }
            set {
                x = value;
                RaisePropertyChanged(() => X);
            }
        }

        private Vector y;
        public Vector Y {
            get {
                return y;
            }
            set {
                y = value;
                RaisePropertyChanged(() => Y);
            }
        }

        private string name;
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public PlotData(Vector x, Vector y, string name) {
            this.X = x;
            this.Y = y;
            this.Name = name;
        }
    }
}
