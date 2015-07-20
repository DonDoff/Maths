using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTests {
    [TestClass]
    public class MatrixTest {

        [TestMethod]
        public void MatrixTestCopy() {
            Matrix m1 = MatrixFactory.ParseFrom("1, 1; 1, 2");
            Matrix m2 = m1.Copy();
            m1[0, 0] = 2;
        
            Assert.AreNotEqual(m1, m2);
            Assert.AreEqual(m1[0, 0], new ComplexNumber(2, 0));
            Assert.AreEqual(m2[0, 0], new ComplexNumber(1, 0));
        }

        [TestMethod]
        public void MatrixTestAddition() {
            Matrix m1 = MatrixFactory.ParseFrom("1, 1; 1, 2");
            Matrix m2 = MatrixFactory.ParseFrom("2, 3; 4, 1");

            Matrix mActual = m1 + m2;
            Matrix mExpected = MatrixFactory.ParseFrom("3, 4; 5, 3");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestSubtraction() {
            Matrix m1 = MatrixFactory.ParseFrom("1, 1; 1, 2");
            Matrix m2 = MatrixFactory.ParseFrom("2, 3; 4 ,1");

            Matrix mActual = m1 - m2;
            Matrix mExpected = MatrixFactory.ParseFrom("-1, -2; -3, 1");
            Assert.AreEqual(mExpected, mActual);
        }
        
        [TestMethod]
        public void MatrixTestMultiplication() {
            Matrix m1 = MatrixFactory.ParseFrom("1, 1; 1, 2");
            Matrix m2 = MatrixFactory.ParseFrom("2 ,3; 4 ,1");

            Matrix mActual = m1 * m2;
            Matrix mExpected = MatrixFactory.ParseFrom("6 ,4; 10 ,5");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestDivision() {
            Matrix m1 = MatrixFactory.ParseFrom("4, 6; 0, 10 + 2i");
            ComplexNumber c1 = 2;

            Matrix mActual = m1 / c1;
            Matrix mExpected = MatrixFactory.ParseFrom("2 ,3; 0, 5+i");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestSwapRows() {
            Matrix m1 = MatrixFactory.ParseFrom("4, 6; 0, 5; 2, 3; 6, 2");

            Matrix mActual = m1.SwapRows(1, 2);
            Matrix mExpected = MatrixFactory.ParseFrom("4, 6; 2, 3; 0, 5; 6, 2");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestColumnIndexing() {
            Matrix m1 = MatrixFactory.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");

            Matrix mActual = m1[Vector.ParseFrom("0, 1, 2, 3"), 1];
            Matrix mExpected = MatrixFactory.ParseFrom("2; -3; 1; 1");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestRowIndexing() {
            Matrix m1 = MatrixFactory.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");

            Matrix mActual = m1[1, Vector.ParseFrom("0, 1, 2, 3")];
            Matrix mExpected = MatrixFactory.ParseFrom("2; -3; 1; 1");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestMatrixIndexing() {
            Matrix m1 = MatrixFactory.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 5, 1; 1, 1, 1, 2");

            Matrix mActual = m1[Vector.ParseFrom("1, 2"), Vector.ParseFrom("1, 2, 3")];
            Matrix mExpected = MatrixFactory.ParseFrom("-3, 1, 1; 1, 5, 1");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestEquals() {
            Matrix mActual = MatrixFactory.ParseFrom("1, 1; 1, 2");
            Matrix mExpected = MatrixFactory.ParseFrom("1, 1; 1, 2");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestToString() {
            string mActual = MatrixFactory.ParseFrom("1, 1; 1, 2").ToString();
            string mExpected = "1, 1\n1, 2";
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestDeterminant() {
            Matrix m1 = MatrixFactory.ParseFrom("3, -6; 2, 3");

            ComplexNumber mActual = m1.Determinant();
            ComplexNumber mExpected = 21;
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestInvert() {
            Matrix mActual = MatrixFactory.ParseFrom("1, 1; 1, 2").Invert();
            Matrix mExpected = MatrixFactory.ParseFrom("2, -1;-1, 1");

            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestPositiveDefinite() {
            // Hermitian and positive definite
            Matrix m1 = MatrixFactory.ParseFrom("25, 15, -5; 15, 18, 0; -5, 0, 11");
            Assert.IsTrue(m1.IsPositiveDefinite());

            // Hermitian but not positive definite
            m1 = MatrixFactory.ParseFrom("1, -4, 6; -4, 2, -2; 6, -2, 1");
            Assert.IsFalse(m1.IsPositiveDefinite());
        }

        [TestMethod]
        public void MatrixTestIsSymmetric() {
            // Hermitian and positive definite
            Matrix m1 = MatrixFactory.ParseFrom("25, 15, -5; 15, 18, 0; -5, 0, 11");
            Assert.IsTrue(m1.IsSymmetric());
        }
    }
}
