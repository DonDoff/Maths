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

        [Test, TestCaseSource(typeof(TestMatrices), "ComplexMatrices")]
        public void MatrixMathTestGivensRotationComplex(Matrix m) {
            Matrix G = MatrixFactory.IdentityMatrix(m.Height);
            G[Vector.Arrange(1, 3), Vector.Arrange(1, 3)] = MatrixMath.CalculatGivensRotation(m[1, 0], m[2, 0]);

            Assert.AreEqual(MatrixFactory.Zeros(1, 1), (G * m)[Vector.Arrange(m.Height - 1, m.Height), 0]);
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
