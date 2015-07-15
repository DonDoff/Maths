using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    // Based on https://jakevdp.github.io/blog/2013/08/28/understanding-the-fft/
    public class FFT {

        public static ColumnVector CalculateFFT(ColumnVector v) {
            if (Math.Log(v.Size, 2) % 1 > 0) {
                v = PadWithZeros(v);
            }
            return CFFT(v);
        }

        public static ColumnVector CalculateIFFT(ColumnVector v) {
            if (Math.Log(v.Size, 2) % 1 > 0) {
                v = PadWithZeros(v);
            }
            // The IFFT is the same as the FFT applied on the complex conjugate of the vector, divided by the size.
            return 1.0 / v.Size * CFFT(v.AppyToAllElements(x => x.Conjugate()).ToColumnVector());
        }

        private static ColumnVector CFFT(ColumnVector x) {
            int N = x.Size;

            if (N <= 32) {
                return DFT_Slow(x);
            }
            ColumnVector evenIndices = ColumnVector.Arrange(0, N, 2);
            ColumnVector oddIndices = ColumnVector.Arrange(1, N, 2);
            ColumnVector X_even = CFFT(x[evenIndices]);
            ColumnVector X_odd = CFFT(x[oddIndices]);
            ColumnVector firstHalf = ColumnVector.Arrange(N / 2);
            ColumnVector secondHalf = ColumnVector.Arrange(N / 2, N);

            ColumnVector factor = MatrixMath.Exp(new ComplexNumber(0, 2) * Math.PI * ColumnVector.Arrange(N) / N).ToColumnVector();

            return (X_even + factor[firstHalf].ElementMultiply(X_odd)).ConcatenateRows(
                    X_even + factor[secondHalf].ElementMultiply(X_odd)).ToColumnVector();
        }

        private static ColumnVector DFT_Slow(ColumnVector x) {
            int N = x.Size;
            ColumnVector k = ColumnVector.Arrange(N);
            Matrix n = k.Transpose();
            Matrix M = MatrixMath.Exp(new ComplexNumber(0, 2) * Math.PI * k * n / N);
            return (M * x).ToColumnVector();
        }

        private static ColumnVector PadWithZeros(ColumnVector x) {
            int N = x.Size;
            int pow2Size = (int)Math.Pow(2, Math.Ceiling(Math.Log(N) / Math.Log(2)));
            ColumnVector newVec = new ColumnVector(pow2Size);
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
