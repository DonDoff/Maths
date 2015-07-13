using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTests {
    [TestClass]
    public class ComplexNumberTest {

        [TestMethod]
        public void ComplexNumberTestAddition() {
            ComplexNumber c1 = new ComplexNumber(-1, 2);
            ComplexNumber c2 = new ComplexNumber(2, -3);
            ComplexNumber cExpected = new ComplexNumber(1, -1);

            ComplexNumber cActual = c1 + c2;
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexNumberTestSubtraction() {
            ComplexNumber c1 = new ComplexNumber(3, 4);
            ComplexNumber c2 = new ComplexNumber(2, -3);
            ComplexNumber cExpected = new ComplexNumber(1, 7);

            ComplexNumber cActual = c1 - c2;
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexNumberTestImplicitConversion() {
            ComplexNumber cActual = 2;
            ComplexNumber cExpected = new ComplexNumber(2,0);

            Assert.AreEqual(cExpected, cActual);
        }
        
        [TestMethod]
        public void ComplexNumberTestMultiplication() {
            ComplexNumber c1 = new ComplexNumber(1, 3);
            ComplexNumber c2 = new ComplexNumber(2, 3);
            ComplexNumber cExpected = new ComplexNumber(-7, 9);

            ComplexNumber cActual = c1 * c2;
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexNumberTestDivision() {
            ComplexNumber c1 = new ComplexNumber(4, 2);
            ComplexNumber c2 = new ComplexNumber(3, -1);
            ComplexNumber cExpected = new ComplexNumber(1, 1);

            ComplexNumber cActual = c1 / c2;
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexNumberTestPow() {
            // Real
            ComplexNumber cActual = ComplexNumberMath.Pow(new ComplexNumber(3, 0), 2);
            ComplexNumber cExpected = new ComplexNumber(9, 0);
            Assert.AreEqual(cExpected, cActual);

            // Imaginary
            cActual = ComplexNumberMath.Pow(new ComplexNumber(0, 1), 4);
            cExpected = new ComplexNumber(1, 0);
            Assert.AreEqual(cExpected, cActual);

            // Complex
            cActual = ComplexNumberMath.Pow(new ComplexNumber(1, -1), 10);
            cExpected = new ComplexNumber(0, -32);
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexNumberTestSqrt() {
            // Real
            ComplexNumber cActual = ComplexNumberMath.Sqrt(new ComplexNumber(9, 0));
            ComplexNumber cExpected = new ComplexNumber(3, 0);
            Assert.AreEqual(cExpected, cActual);

            // Imaginary
            cActual = ComplexNumberMath.Sqrt(new ComplexNumber(-1, 0));
            cExpected = new ComplexNumber(0, 1);
            Assert.AreEqual(cExpected, cActual);

            // Complex
            cActual = ComplexNumberMath.Sqrt(new ComplexNumber(3, 4));
            cExpected = new ComplexNumber(2, 1);
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexNumberTestEquals() {
            ComplexNumber cActual = new ComplexNumber(1, 2);
            ComplexNumber cExpected = new ComplexNumber(1, 2);
            Assert.AreEqual(cExpected, cActual);
        }

        [TestMethod]
        public void ComplexNumberTestToString() {
            string sActual;
            string sExpected;

            sActual = new ComplexNumber(1.5, 0).ToString();
            sExpected = "1.5";
            Assert.AreEqual(sExpected, sActual);

            sActual = new ComplexNumber(5, 3).ToString();
            sExpected = "5+3i";
            Assert.AreEqual(sExpected, sActual);

            sActual = new ComplexNumber(5, -3).ToString();
            sExpected = "5-3i";
            Assert.AreEqual(sExpected, sActual);

            sActual = new ComplexNumber(0, 3).ToString();
            sExpected = "3i";
            Assert.AreEqual(sExpected, sActual);

            sActual = new ComplexNumber(3, 0).ToString();
            sExpected = "3";
            Assert.AreEqual(sExpected, sActual);

            sActual = new ComplexNumber(0, 1).ToString();
            sExpected = "i";
            Assert.AreEqual(sExpected, sActual);

            sActual = new ComplexNumber(0, -1).ToString();
            sExpected = "-i";
            Assert.AreEqual(sExpected, sActual);
        }

        [TestMethod]
        public void ComplexNumberTestParseConstructor() {
            ComplexNumber cActual;
            ComplexNumber cExpected;

            cActual = new ComplexNumber("5+2i");
            cExpected = new ComplexNumber(5, 2);
            Assert.AreEqual(cActual, cExpected);

            cActual = new ComplexNumber("1");
            cExpected = new ComplexNumber(1, 0);
            Assert.AreEqual(cActual, cExpected);

            cActual = new ComplexNumber("2i");
            cExpected = new ComplexNumber(0, 2);
            Assert.AreEqual(cActual, cExpected);

            cActual = new ComplexNumber("-5+3i");
            cExpected = new ComplexNumber(-5, 3);
            Assert.AreEqual(cActual, cExpected);

            cActual = new ComplexNumber("-5");
            cExpected = new ComplexNumber(-5, 0);
            Assert.AreEqual(cActual, cExpected);

            cActual = new ComplexNumber("i");
            cExpected = new ComplexNumber(0, 1);
            Assert.AreEqual(cActual, cExpected);

            cActual = new ComplexNumber("-i");
            cExpected = new ComplexNumber(0, -1);
            Assert.AreEqual(cActual, cExpected);
                        
            cActual = new ComplexNumber("53i");
            cExpected = new ComplexNumber(0, 53);
            Assert.AreEqual(cActual, cExpected);
            
            cActual = new ComplexNumber("+5-i");
            cExpected = new ComplexNumber(5, -1);
            Assert.AreEqual(cActual, cExpected);

            cActual = new ComplexNumber("30");
            cExpected = new ComplexNumber(30, 0);
            Assert.AreEqual(cActual, cExpected);

            cActual = new ComplexNumber("20+50i");
            cExpected = new ComplexNumber(20, 50);
            Assert.AreEqual(cActual, cExpected);
        }
    }
}
