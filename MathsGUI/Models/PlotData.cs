using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maths;

namespace MathsGUI.Models {
    public class PlotData {

        public Vector X { get; set; }
        public Vector Y { get; set; }
        public string Name { get; set; }

        public PlotData(Vector x, Vector y, string name) {
            this.X = x;
            this.Y = y;
            this.Name = name;
        }

    }
}
