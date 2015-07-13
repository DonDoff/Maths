using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    class SimpleKalmanFilter {
        private static double Q = 0.001;
        private static double R = 0.01;
        private static double P = 1, X = 0, K;

        private static void measurementUpdate() {
            K = (P + Q) / (P + Q + R);
            P = R * (P + Q) / (R + P + Q);
        }

        public static double update(double measurement) {
            measurementUpdate();
            double result = X + (measurement - X) * K;
            X = result;
            return result;
        }


    }
}
