using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    // Based on https://jakevdp.github.io/blog/2013/08/28/understanding-the-fft/
    public class FFT {

        public static Vector CalculateFFT(Vector v) {
            if (Math.Log(v.Size, 2) % 1 > 0) {
                v = PadWithZeros(v);
            }
            return CFFT(v);
        }

        public static Vector CalculateIFFT(Vector v) {
            if (Math.Log(v.Size, 2) % 1 > 0) {
                v = PadWithZeros(v);
            }
            // The IFFT is the same as the FFT applied on the complex conjugate of the vector, divided by the size.
            return 1.0 / v.Size * CFFT(v.AppyToAllElements(x => x.Conjugate()).ToColumnVector());
        }

        private static Vector CFFT(Vector x) {
            int N = x.Size;

            if (N <= 32) {
                return DFT_Slow(x);
            }
            Vector evenIndices = Vector.Arrange(0, N, 2);
            Vector oddIndices = Vector.Arrange(1, N, 2);
            Vector X_even = CFFT(x[evenIndices]);
            Vector X_odd = CFFT(x[oddIndices]);
            Vector firstHalf = Vector.Arrange(N / 2);
            Vector secondHalf = Vector.Arrange(N / 2, N);

            Vector factor = MatrixMath.Exp(new Complex(0, 2) * Math.PI * Vector.Arrange(N) / N).ToColumnVector();

            return (X_even + factor[firstHalf].ElementMultiply(X_odd)).ConcatenateRows(
                    X_even + factor[secondHalf].ElementMultiply(X_odd)).ToColumnVector();
        }

        private static Vector DFT_Slow(Vector x) {
            int N = x.Size;
            Vector k = Vector.Arrange(N);
            Matrix n = k.Transpose();
            Matrix M = MatrixMath.Exp(new Complex(0, 2) * Math.PI * k * n / N);
            return (M * x).ToColumnVector();
        }

        private static Vector PadWithZeros(Vector x) {
            int N = x.Size;
            int pow2Size = (int)Math.Pow(2, Math.Ceiling(Math.Log(N) / Math.Log(2)));
            Vector newVec = new Vector(pow2Size);
            for (int i = 0; i < N; i++) {
                newVec[i] = x[i];
            }
            for (int i = N; i < pow2Size; i++) {
                newVec[i] = 0;
            }
            return newVec;
        }

    }
}
