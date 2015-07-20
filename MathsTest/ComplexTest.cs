using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTests {
    [TestClass]
    public class ComplexTest {

        [TestMethod]
        public void ComplexTestAddition() {
            Complex c1 = new Complex(-1, 2);
            Complex c2 = new Complex(2, -3);
            Complex cExpected = new Complex(1, -1);

            Complex cActual = c1 + c2;
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexTestSubtraction() {
            Complex c1 = new Complex(3, 4);
            Complex c2 = new Complex(2, -3);
            Complex cExpected = new Complex(1, 7);

            Complex cActual = c1 - c2;
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexTestImplicitConversion() {
            Complex cActual = 2;
            Complex cExpected = new Complex(2,0);

            Assert.AreEqual(cExpected, cActual);
        }
        
        [TestMethod]
        public void ComplexTestMultiplication() {
            Complex c1 = new Complex(1, 3);
            Complex c2 = new Complex(2, 3);
            Complex cExpected = new Complex(-7, 9);

            Complex cActual = c1 * c2;
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexTestDivision() {
            Complex c1 = new Complex(4, 2);
            Complex c2 = new Complex(3, -1);
            Complex cExpected = new Complex(1, 1);

            Complex cActual = c1 / c2;
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexTestEquals() {
            Complex cActual = new Complex(1, 2);
            Complex cExpected = new Complex(1, 2);
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexTestToString() {
            string sActual;
            string sExpected;

            sActual = new Complex(1.5, 0).ToString();
            sExpected = "1.5";
            Assert.AreEqual(sExpected, sActual);

            sActual = new Complex(5, 3).ToString();
            sExpected = "5+3i";
            Assert.AreEqual(sExpected, sActual);

            sActual = new Complex(5, -3).ToString();
            sExpected = "5-3i";
            Assert.AreEqual(sExpected, sActual);

            sActual = new Complex(0, 3).ToString();
            sExpected = "3i";
            Assert.AreEqual(sExpected, sActual);

            sActual = new Complex(3, 0).ToString();
            sExpected = "3";
            Assert.AreEqual(sExpected, sActual);

            sActual = new Complex(0, 1).ToString();
            sExpected = "i";
            Assert.AreEqual(sExpected, sActual);

            sActual = new Complex(0, -1).ToString();
            sExpected = "-i";
            Assert.AreEqual(sExpected, sActual);
        }

        [TestMethod]
        public void ComplexTestParseConstructor() {
            Complex cActual;
            Complex cExpected;

            cActual = new Complex("5+2i");
            cExpected = new Complex(5, 2);
            Assert.AreEqual(cActual, cExpected);

            cActual = new Complex("1");
            cExpected = new Complex(1, 0);
            Assert.AreEqual(cActual, cExpected);

            cActual = new Complex("2i");
            cExpected = new Complex(0, 2);
            Assert.AreEqual(cActual, cExpected);

            cActual = new Complex("-5+3i");
            cExpected = new Complex(-5, 3);
            Assert.AreEqual(cActual, cExpected);

            cActual = new Complex("-5");
            cExpected = new Complex(-5, 0);
            Assert.AreEqual(cActual, cExpected);

            cActual = new Complex("i");
            cExpected = new Complex(0, 1);
            Assert.AreEqual(cActual, cExpected);

            cActual = new Complex("-i");
            cExpected = new Complex(0, -1);
            Assert.AreEqual(cActual, cExpected);
                        
            cActual = new Complex("53i");
            cExpected = new Complex(0, 53);
            Assert.AreEqual(cActual, cExpected);
            
            cActual = new Complex("+5-i");
            cExpected = new Complex(5, -1);
            Assert.AreEqual(cActual, cExpected);

            cActual = new Complex("30");
            cExpected = new Complex(30, 0);
            Assert.AreEqual(cActual, cExpected);

            cActual = new Complex("20+50i");
            cExpected = new Complex(20, 50);
            Assert.AreEqual(cActual, cExpected);
        }
    }
}
