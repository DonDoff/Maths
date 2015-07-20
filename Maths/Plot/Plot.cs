using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Maths {
    public class Plot {

        public static void CreateMathsPlotWindow(Vector x, Vector y) {
            CreateMathsPlotWindow(new List<Vector> { x }, new List<Vector> { y });
        }

        public static void CreateMathsPlotWindow(List<Vector> xs, List<Vector> ys) {
            Thread thread = new Thread(() => {
                MathsPlotViewModel vm = new MathsPlotViewModel(xs, ys);
                MathsPlotWindow w = new MathsPlotWindow(vm);
                w.Show();
                System.Windows.Threading.Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
