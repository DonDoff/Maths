using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTest {
    [TestClass]
    public class VectorMathTest {
        [TestMethod]
        public void VectorMathTestEuclidianNorm() {
            Vector v1 = Vector.ParseFrom("1, 3, 5, -2");

            Complex mActual = v1.EuclidianNorm();
            Complex mExpected = new Complex(39).Sqrt();
            Assert.AreEqual(mExpected, mActual);
        }
    }
}
