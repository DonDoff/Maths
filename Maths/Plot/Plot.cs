using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Maths {
    public class Plot {

        public static void CreateMathsPlotWindow(ColumnVector x, ColumnVector y) {
            CreateMathsPlotWindow(new List<ColumnVector> { x }, new List<ColumnVector> { y });
        }

        public static void CreateMathsPlotWindow(List<ColumnVector> xs, List<ColumnVector> ys) {
            Thread thread = new Thread(() => {
                MathsPlotWindowModel vm = new MathsPlotWindowModel(xs, ys);
                MathsPlotWindow w = new MathsPlotWindow(vm);
                w.Show();
                System.Windows.Threading.Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
