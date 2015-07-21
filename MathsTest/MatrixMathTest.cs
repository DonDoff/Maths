using System;
using Maths;
using NUnit.Framework;

namespace MathsTest {
    [TestFixture]
    public class MatrixMathTest {

        [Test, TestCaseSource(typeof(TestMatrices), "RealMatrices")]
        public void MatrixMathTestHouseHolderReal(Matrix m) {
            Matrix H = MatrixMath.CalculateHouseholderTransform(new Vector(m, 0));

            Assert.AreEqual(MatrixFactory.Zeros(m.Height - 1, 1), (H * m)[Vector.Arrange(1, m.Height), 0]);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "ComplexMatrices")]
        public void MatrixMathTestHouseHolderComplex(Matrix m) {
            Matrix H = MatrixMath.CalculateHouseholderTransform(new Vector(m, 0));

            Assert.AreEqual(MatrixFactory.Zeros(m.Height - 1, 1), (H * m)[Vector.Arrange(1, m.Height), 0]);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "RealMatrices")]
        public void MatrixMathTestGivensRotationReal(Matrix m) {
            Vector v = m[Vector.Arrange(0, 2), 0];
            Matrix G = MatrixMath.CalculatGivensRotation(m[0, 0], m[1, 0]);

            Assert.AreEqual(new Complex(0, 0), (G * v)[1, 0]);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "ComplexMatrices")]
        public void MatrixMathTestGivensRotationComplex(Matrix m) {
            Vector v = m[Vector.Arrange(0, 2), 0];
            Matrix G = MatrixMath.CalculatGivensRotation(m[0, 0], m[1, 0]);

            Assert.AreEqual(new Complex(0, 0), (G * v)[1, 0]);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "ComplexMatrices")]
        public void MatrixMathTestSinAndCosComplex(Matrix m) {
            Matrix sin = MatrixMath.Sin(m);
            Matrix cos = MatrixMath.Cos(m);
            Matrix mActual = sin.ElementMultiply(sin) + cos.ElementMultiply(cos);
            Matrix mExpected = MatrixFactory.Ones(m.Height, m.Width);

            Assert.AreEqual(mExpected, mActual);
        }
    }
}
