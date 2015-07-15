using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTests {
    [TestClass]
    public class StatisticsTest {

        [TestMethod]
        public void StatisticsTestExpectedValue() {
            Vector v1 = Vector.ParseFrom("2, 4, 4, 4, 5, 5, 7, 9");
            VectorMath S = new VectorMath(v1);
            
            ComplexNumber mActual = S.ExpectedValue();
            ComplexNumber mExpected = 5;
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void StatisticsTestVariance() {
            Vector v1 = Vector.ParseFrom("2, 4, 4, 4, 5, 5, 7, 9");
            VectorMath S = new VectorMath(v1);
            
            ComplexNumber mActual = S.PopulationVariance();
            ComplexNumber mExpected = 4;
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void StatisticsTestCovariance() {
            Vector v1 = Vector.ParseFrom("1, 3, 7, 5, 6, 4");
            Vector v2 = Vector.ParseFrom("2, 7, 3, 2, 5, 1");

            ComplexNumber mActual = new VectorMath(v1).SampleCovariance(v2);
            ComplexNumber mExpected = 4.0/15;
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void StatisticsTestCorrelation() {
            Vector v1 = Vector.ParseFrom("1, 3, 7, 5, 6, 4");
            Vector v2 = Vector.ParseFrom("2, 7, 3, 2, 5, 1");

            ComplexNumber mActual = new VectorMath(v1).SampleCorrelation(v2);
            ComplexNumber mExpected = Math.Sqrt(2.0/665);
            Assert.AreEqual(mExpected, mActual);
        }
    }
}
