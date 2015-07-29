using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maths;
using GalaSoft.MvvmLight;
using OxyPlot;

namespace MathsGUI.Models {
    public class PlotData : ObservableObject {

        public Vector X { get; set; }
        public Vector Y { get; set; }
        public string Name { get; set; }
        public string XString { get; set; }
        public string YString { get; set; }
        public OxyColor Color { get; set; }

        public PlotData(Vector x, Vector y, string name) {
            this.X = x;
            this.Y = y;
            this.Name = name;
            XString = "";
            YString = "";
        }
    }
}
