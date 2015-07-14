using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTests {
    [TestClass]
    public class MatrixTest {

        [TestMethod]
        public void MatrixTestAddition() {
            Matrix m1 = Matrix.ParseFrom("1, 1; 1, 2");
            Matrix m2 = Matrix.ParseFrom("2, 3; 4, 1");

            Matrix mActual = m1 + m2;
            Matrix mExpected = Matrix.ParseFrom("3, 4; 5, 3");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestSubtraction() {
            Matrix m1 = Matrix.ParseFrom("1, 1; 1, 2");
            Matrix m2 = Matrix.ParseFrom("2, 3; 4 ,1");

            Matrix mActual = m1 - m2;
            Matrix mExpected = Matrix.ParseFrom("-1, -2; -3, 1");
            Assert.AreEqual(mExpected, mActual);
        }
        
        [TestMethod]
        public void MatrixTestMultiplication() {
            Matrix m1 = Matrix.ParseFrom("1, 1; 1, 2");
            Matrix m2 = Matrix.ParseFrom("2 ,3; 4 ,1");

            Matrix mActual = m1 * m2;
            Matrix mExpected = Matrix.ParseFrom("6 ,4; 10 ,5");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestDivision() {
            Matrix m1 = Matrix.ParseFrom("4, 6; 0, 10 + 2i");
            ComplexNumber c1 = 2;

            Matrix mActual = m1 / c1;
            Matrix mExpected = Matrix.ParseFrom("2 ,3; 0, 5+i");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestSwapRows() {
            Matrix m1 = Matrix.ParseFrom("4, 6; 0, 5; 2, 3; 6, 2");

            Matrix mActual = m1.SwapRows(1, 2);
            Matrix mExpected = Matrix.ParseFrom("4, 6; 2, 3; 0, 5; 6, 2");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestEquals() {
            Matrix mActual = Matrix.ParseFrom("1, 1; 1, 2");
            Matrix mExpected = Matrix.ParseFrom("1, 1; 1, 2");
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestToString() {
            string mActual = Matrix.ParseFrom("1, 1; 1, 2").ToString();
            string mExpected = "1, 1\n1, 2";
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestParseConstructor() {
            Matrix mActual = Matrix.ParseFrom("2 +   i , 1 -i ; 1 + 3i, 2  -2i ");
            Matrix mExpected = new Matrix(2, 2);
            mExpected[0, 0] = new ComplexNumber(2, 1);
            mExpected[0, 1] = new ComplexNumber(1, -1);
            mExpected[1, 0] = new ComplexNumber(1, 3);
            mExpected[1, 1] = new ComplexNumber(2, -2);
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestInvert() {
            Matrix mActual = Matrix.ParseFrom("1, 1; 1, 2").Invert();
            Matrix mExpected = Matrix.ParseFrom("2, -1;-1, 1");

            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestLUDecomposition() {
            Matrix m1 = Matrix.ParseFrom("4, 2; 8, 3");
            LUDecomposition lu = m1.LU();

            Matrix mActual = lu.L;
            Matrix mExpected = Matrix.ParseFrom("1, 0; 0.5, 1");
            Assert.AreEqual(mExpected, mActual);

            mActual = lu.U;
            mExpected = Matrix.ParseFrom("8, 3; 0, 0.5");
            Assert.AreEqual(mExpected, mActual);

            mActual = lu.L * lu.U;
            mExpected = lu.P * m1;
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestCholeskyDecomposition() {
            Matrix m1 = Matrix.ParseFrom("25, 15, -5; 15, 18, 0; -5, 0, 11"); // the matrix needs to be symmetric and positive definite!
            CholeskyDecomposition chol = m1.Chol();

            // A = L*L.H
            Matrix mActual = chol.L * chol.L.ConjugateTranspose();
            Matrix mExpected = m1;
            Assert.AreEqual(mExpected, mActual);

            // L is lower triangular
            Assert.IsTrue(chol.L.IsLowerTriangular());
        }

        [TestMethod]
        public void MatrixTestPositiveDefinite() {
            // Hermitian and positive definite
            Matrix m1 = Matrix.ParseFrom("25, 15, -5; 15, 18, 0; -5, 0, 11");
            Assert.IsTrue(m1.IsPositiveDefinite());

            // Hermitian but not positive definite
            m1 = Matrix.ParseFrom("1, -4, 6; -4, 2, -2; 6, -2, 1");
            Assert.IsFalse(m1.IsPositiveDefinite());
        }

        [TestMethod]
        public void MatrixTestHouseHolder() {
            Matrix A = Matrix.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");
            Matrix H = MatrixMath.CalculateHouseholderTransform(new ColumnVector(A, 0));

            Assert.AreEqual((H * A)[ColumnVector.Arrange(1, A.Height), 0], Matrix.Zeros(A.Height - 1, 1));
        }

        [TestMethod]
        public void MatrixTestSVD() {
            Matrix A = Matrix.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");
            SVD svd = A.SVD();

            Assert.IsTrue(svd.U.IsUnitary());
            Assert.IsTrue(svd.D.IsDiagonal());
            Assert.IsTrue(svd.V.IsUnitary());
        }

        [TestMethod]
        public void MatrixTestBidiagonalization() {
            Matrix A = Matrix.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");
            Bidiagonalization bid = A.Bidiagonalization();

            Assert.IsTrue(bid.U.IsUnitary());
            Assert.IsTrue(bid.B.IsBiDiagonal());
            Assert.IsTrue(bid.V.IsUnitary());
        }

        [TestMethod]
        public void MatrixTestHessenbergDecomposition() {
            Matrix A = Matrix.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");
            HessenbergDecomposition HD = A.Hessen();

            Assert.IsTrue(HD.HM.IsUpperHessenberg());
        }

        [TestMethod]
        public void MatrixTestQRDecomposition() {
            Matrix mActual, mExpected;
            Matrix m1 = Matrix.ParseFrom("3, -6; 4, -8; 0, 1");
            QRDecomposition qr = m1.QR();

            // A = Q*R
            mActual = qr.Q * qr.R;
            mExpected = m1;
            Assert.AreEqual(mExpected, mActual);

            // Q is orthogonal
            mActual = qr.Q.Transpose() * qr.Q;
            mExpected = Matrix.IdentityMatrix(qr.Q.Height);
            Assert.AreEqual(mExpected, mActual);

            // R is upper triangular
            Assert.IsTrue(qr.R.IsUpperTriangular());



            m1 = Matrix.ParseFrom("3, -6, 4, 7");
            qr = m1.QR();

            // A = Q*R
            mActual = qr.Q * qr.R;
            mExpected = m1;
            Assert.AreEqual(mExpected, mActual);

            // Q is orthogonal
            mActual = qr.Q.Transpose() * qr.Q;
            mExpected = Matrix.IdentityMatrix(qr.Q.Height);
            Assert.AreEqual(mExpected, mActual);

            // R is upper triangular
            Assert.IsTrue(qr.R.IsUpperTriangular());

        }

        [TestMethod]
        public void MatrixTestDeterminant() {
            Matrix m1 = Matrix.ParseFrom("3, -6; 2, 3");

            ComplexNumber mActual = m1.Determinant();
            ComplexNumber mExpected = 21;
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixTestEigenvaluesAndEigenvectors() {
            Matrix m1 = Matrix.ParseFrom("1, -4, 6; -4, 2, -2; 6, -2, 1"); // the matrix needs to be symmetric!
            EigenvalueDecomposition eigen = m1.Eigen();
            Matrix mActual, mExpected;

            for (int i = 0; i < m1.Height; i++) {
                mActual = m1 * eigen.Eigenvectors.GetColumnVector(i) - eigen.Eigenvalues[i] * eigen.Eigenvectors.GetColumnVector(i);
                mExpected = Matrix.Zeros(m1.Height, 1);
                Assert.AreEqual(mExpected, mActual);
            }
        }

    }
}
