using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class Plot {
        public static void CreateMathsPlotWindow(Vector x, Vector y) {
            MathsPlotWindowModel vm = new MathsPlotWindowModel(new List<Vector>() { x }, new List<Vector>() { y });
            MathsPlotWindow w = new MathsPlotWindow(vm);
            w.Show();
        }

        public static void CreateMathsPlotWindow(List<Vector> xs, List<Vector> ys) {
            MathsPlotWindowModel vm = new MathsPlotWindowModel(xs, ys);
            MathsPlotWindow w = new MathsPlotWindow(vm);
            w.Show();
        }
    }
}
