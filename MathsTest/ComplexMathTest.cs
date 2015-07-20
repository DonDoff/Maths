using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTest {
    [TestClass]
    public class ComplexMathTest {

        [TestMethod]
        public void ComplexMathTestSinAndCos() {
            Complex c = new Complex("0.34 + 5i");

            Complex sin = ComplexMath.Sin(c);
            Complex cos = ComplexMath.Cos(c);
            Complex mActual = sin * sin + cos * cos;
            Complex mExpected = 1;

            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void ComplexMathTestPow() {
            // Real
            Complex cActual = ComplexMath.Pow(new Complex(3, 0), 2);
            Complex cExpected = new Complex(9, 0);
            Assert.AreEqual(cExpected, cActual);

            // Imaginary
            cActual = ComplexMath.Pow(new Complex(0, 1), 4);
            cExpected = new Complex(1, 0);
            Assert.AreEqual(cExpected, cActual);

            // Complex
            cActual = ComplexMath.Pow(new Complex(1, -1), 10);
            cExpected = new Complex(0, -32);
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexMathTestSqrt() {
            // Real
            Complex cActual = ComplexMath.Sqrt(new Complex(9, 0));
            Complex cExpected = new Complex(3, 0);
            Assert.AreEqual(cExpected, cActual);

            // Imaginary
            cActual = ComplexMath.Sqrt(new Complex(-1, 0));
            cExpected = new Complex(0, 1);
            Assert.AreEqual(cExpected, cActual);

            // Complex
            cActual = ComplexMath.Sqrt(new Complex(3, 4));
            cExpected = new Complex(2, 1);
            Assert.AreEqual(cExpected, cActual);
        }
    }
}
