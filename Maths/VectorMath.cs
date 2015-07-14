using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class VectorMath {

        public ColumnVector V { get; set; }

        public VectorMath(ColumnVector v) {
            V = v;
        }

        public ComplexNumber ExpectedValue() {
            return V.Sum()/((double) V.Size);
        }

        public ComplexNumber PopulationMean() {
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

        public ComplexNumber SampleCovariance(ColumnVector v2) {
            return Covariance(v2) / (V.Size - 1.0);
        }

        public ComplexNumber PopulationCovariance(ColumnVector v2) {
            return Covariance(v2) / (V.Size - 0.0);
        }

        public ComplexNumber SampleCorrelation(ColumnVector v2) {
            return SampleCovariance(v2)/(SampleStandardDeviation()*new VectorMath(v2).SampleStandardDeviation());
        }

        public ComplexNumber PopulationCorrelation(ColumnVector v2) {
            return PopulationCovariance(v2) / (PopulationStandardDeviation() * new VectorMath(v2).PopulationStandardDeviation());
        }

        private double Variance() {
            Matrix x = V - ExpectedValue();
            return (x.ConjugateTranspose() * x).ToComplexNumber().R;
        }

        private ComplexNumber Covariance(ColumnVector v) {
            ColumnVector x = V - ExpectedValue();
            ColumnVector y = v - new VectorMath(v).ExpectedValue();
            return (x.Transpose() * y).ToComplexNumber();
        }

        public static ColumnVector FFTShift(ColumnVector v) {
            return v.CircShift(v.Size / 2);
        }

        public static ColumnVector FFTNormalize(ColumnVector v) {
            double max = MatrixMath.Max(v).Abs();
            return v.AppyToAllElements(x => x.Abs() / max).ToColumnVector();
        }

        public static ColumnVector Convolution(ColumnVector v1, ColumnVector v2) {
            return FFT.CalculateIFFT((FFT.CalculateFFT(v1).ElementMultiply(FFT.CalculateFFT(v2))).ToColumnVector());
        }
    }
}
