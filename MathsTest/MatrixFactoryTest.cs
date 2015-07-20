using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTest {
    [TestClass]
    public class MatrixFactoryTest {

        [TestMethod]
        public void MatrixFactoryTestParseFrom() {
            Matrix mActual = MatrixFactory.ParseFrom("2 +   i , 1 -i ; 1 + 3i, 2  -2i ");
            Matrix mExpected = new Matrix(2, 2);
            mExpected[0, 0] = new ComplexNumber(2, 1);
            mExpected[0, 1] = new ComplexNumber(1, -1);
            mExpected[1, 0] = new ComplexNumber(1, 3);
            mExpected[1, 1] = new ComplexNumber(2, -2);
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixFactoryTestSymmetric() {
            Matrix m = MatrixFactory.Symmetric(5);
            Assert.IsTrue(m.IsSymmetric());
        }

        [TestMethod]
        public void MatrixFactoryTestHankel() {
            Matrix mActual = MatrixFactory.Hankel(4, Vector.ParseFrom("1, 2, 3, 4"), Vector.ParseFrom("4, 5, 6, 7"));
            Matrix mExpected = MatrixFactory.ParseFrom("1, 2, 3, 4; 2, 3, 4, 5; 3, 4, 5, 6; 4, 5, 6, 7");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixFactoryTestZeros() {
            Matrix mActual = MatrixFactory.Zeros(3, 3);
            Matrix mExpected = MatrixFactory.ParseFrom("0, 0, 0; 0, 0, 0; 0, 0, 0");

            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixFactoryTestOnes() {
            Matrix mActual = MatrixFactory.Ones(3, 3);
            Matrix mExpected = MatrixFactory.ParseFrom("1, 1, 1; 1, 1, 1; 1, 1, 1");

            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixFactoryTestFill() {
            Matrix mActual = MatrixFactory.Fill(new Matrix(3), -1);
            Matrix mExpected = MatrixFactory.ParseFrom("-1, -1, -1; -1, -1, -1; -1, -1, -1");

            Assert.AreEqual(mExpected, mActual);
        }
    }
}
