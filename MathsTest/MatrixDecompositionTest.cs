using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths;

namespace MathsTest {
    [TestClass]
    public class MatrixDecompositionTest {
        [TestMethod]
        public void MatrixDecompositionTestLUDecomposition() {
            Matrix m1 = MatrixFactory.ParseFrom("4, 2; 8, 3");
            LUDecomposition lu = m1.LU();

            Assert.IsTrue(lu.L.IsLowerTriangular());
            Assert.IsTrue(lu.U.IsUpperTriangular());

            Matrix mActual = lu.L * lu.U;
            Matrix mExpected = lu.P * m1;
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixDecompositionTestCholeskyDecomposition() {
            Matrix m1 = MatrixFactory.ParseFrom("25, 15, -5; 15, 18, 0; -5, 0, 11"); // the matrix needs to be symmetric and positive definite!
            CholeskyDecomposition chol = m1.Chol();

            Assert.IsTrue(chol.L.IsLowerTriangular());

            Matrix mActual = chol.L * chol.L.ConjugateTranspose();
            Matrix mExpected = m1;
            Assert.AreEqual(mExpected, mActual);
        }        

        [TestMethod]
        public void MatrixDecompositionTestSVD() {
            Matrix A = MatrixFactory.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");
            SVD svd = A.SVD();

            Assert.IsTrue(svd.U.IsUnitary());
            Assert.IsTrue(svd.D.IsDiagonal());
            Assert.IsTrue(svd.V.IsUnitary());

            Matrix mActual = svd.U * svd.D * svd.V.ConjugateTranspose();
            Matrix mExpected = A;
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixDecompositionTestBidiagonalization() {
            Matrix A = MatrixFactory.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");
            Bidiagonalization bid = A.Bidiagonalization();

            Assert.IsTrue(bid.U.IsUnitary());
            Assert.IsTrue(bid.B.IsBiDiagonal());
            Assert.IsTrue(bid.V.IsUnitary());

            Matrix mActual = bid.U * bid.B * bid.V.ConjugateTranspose();
            Matrix mExpected = A;
            Assert.AreEqual(mExpected, mActual);
        }

        [TestMethod]
        public void MatrixDecompositionTestHessenbergDecomposition() {
            Matrix A = MatrixFactory.ParseFrom("4, 2, 2, 1; 2, -3, 1, 1; 2, 1, 3, 1; 1, 1, 1, 2");
            HessenbergDecomposition HD = A.Hessen();

            Assert.IsTrue(HD.HM.IsUpperHessenberg());
        }

        [TestMethod]
        public void MatrixDecompositionTestQRDecomposition() {
            Matrix mActual, mExpected;
            // Test with matrix
            Matrix m1 = MatrixFactory.ParseFrom("3, -6; 4, -8; 0, 1");
            QRDecomposition qr = m1.QR(true);

            mActual = qr.Q * qr.R;
            mExpected = m1;
            Assert.AreEqual(mExpected, mActual);
            Assert.IsTrue(qr.Q.IsUnitary());
            Assert.IsTrue(qr.R.IsUpperTriangular());

            // Test with vector
            m1 = MatrixFactory.ParseFrom("3, -6, 4, 7");
            qr = m1.QR(true);

            mActual = qr.Q * qr.R;
            mExpected = m1;
            Assert.AreEqual(mExpected, mActual);
            Assert.IsTrue(qr.Q.IsUnitary());
            Assert.IsTrue(qr.R.IsUpperTriangular());
        }

        [TestMethod]
        public void MatrixDecompositionTestEigenvaluesAndEigenvectors() {
            Matrix m1 = MatrixFactory.ParseFrom("1, -4, 6; -4, 2, -2; 6, -2, 1"); // the matrix needs to be symmetric!
            EigenvalueDecomposition eigen = m1.Eigen();
            Matrix mActual, mExpected;

            for (int i = 0; i < m1.Height; i++) {
                mActual = m1 * eigen.Eigenvectors.GetColumnVector(i) - eigen.Eigenvalues[i] * eigen.Eigenvectors.GetColumnVector(i);
                mExpected = MatrixFactory.Zeros(m1.Height, 1);
                Assert.AreEqual(mExpected, mActual);
            }
        }
    }
}
