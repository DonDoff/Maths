using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTest {
    [TestClass]
    public class MatrixMathTest {

        [TestMethod]
        public void MatrixMathTestHouseHolder() {
            Matrix A = MatrixFactory.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");
            Matrix H = MatrixMath.CalculateHouseholderTransform(new Vector(A, 0));

            Assert.AreEqual(MatrixFactory.Zeros(A.Height - 1, 1), (H * A)[Vector.Arrange(1, A.Height), 0]);
        }

        [TestMethod]
        public void MatrixMathTestGivensRotation() {
            Matrix A = MatrixFactory.ParseFrom("4, 2, 1; 2, -3, 1; 2, 1, 1");
            Matrix G = MatrixFactory.IdentityMatrix(A.Height);
            G[Vector.Arrange(1, 3), Vector.Arrange(1, 3)] = MatrixMath.CalculatGivensRotation(A[1, 0], A[2, 0]);

            Assert.AreEqual(MatrixFactory.Zeros(1, 1), (G * A)[Vector.Arrange(A.Height - 1, A.Height), 0]);
        }
    }
}
