using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class VectorMath {

        public Vector V { get; set; }

        public VectorMath(Vector v) {
            V = v;
        }

        public Complex ExpectedValue() {
            return V.Sum()/((double) V.Size);
        }

        public Complex PopulationMean() {
            return ExpectedValue();
        }

        public double SampleVariance() {
            return Variance() / (V.Size - 1.0);
        }

        public double PopulationVariance() {
            return Variance() / (V.Size - 0.0);
        }
        
        public double SampleStandardDeviation() {
            return Math.Sqrt(SampleVariance());
        }

        public double PopulationStandardDeviation() {
            return Math.Sqrt(PopulationVariance());
        }

        public Complex SampleCovariance(Vector v2) {
            return Covariance(v2) / (V.Size - 1.0);
        }

        public Complex PopulationCovariance(Vector v2) {
            return Covariance(v2) / (V.Size - 0.0);
        }

        public Complex SampleCorrelation(Vector v2) {
            return SampleCovariance(v2)/(SampleStandardDeviation()*new VectorMath(v2).SampleStandardDeviation());
        }

        public Complex PopulationCorrelation(Vector v2) {
            return PopulationCovariance(v2) / (PopulationStandardDeviation() * new VectorMath(v2).PopulationStandardDeviation());
        }

        private double Variance() {
            Matrix x = V - ExpectedValue();
            return (x.ConjugateTranspose() * x).ToComplexNumber().R;
        }

        private Complex Covariance(Vector v) {
            Vector x = V - ExpectedValue();
            Vector y = v - new VectorMath(v).ExpectedValue();
            return (x.Transpose() * y).ToComplexNumber();
        }

        public static Vector FFTShift(Vector v) {
            return v.CircShift(v.Size / 2);
        }

        public static Vector FFTNormalize(Vector v) {
            double max = MatrixMath.Max(v).Abs();
            return v.AppyToAllElements(x => x.Abs() / max).ToColumnVector();
        }

        public static Vector Convolution(Vector v1, Vector v2) {
            return FFT.CalculateIFFT((FFT.CalculateFFT(v1).ElementMultiply(FFT.CalculateFFT(v2))).ToColumnVector());
        }
    }
}
