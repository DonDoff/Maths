using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Maths;

namespace MathsTest {
    [TestFixture]
    public class MatrixDecompositionTest {

        [Test, TestCaseSource(typeof(TestMatrices), "RealMatrices")]
        public void MatrixDecompositionTestLUDecomposition(Matrix m) {
            LUDecomposition lu = m.LU();

            Assert.IsTrue(lu.L.IsLowerTriangular());
            Assert.IsTrue(lu.U.IsUpperTriangular());

            Matrix mActual = lu.L * lu.U;
            Matrix mExpected = lu.P * m;
            Assert.AreEqual(mExpected, mActual);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "SymmetricPositiveDefiniteMatrices")]
        public void MatrixDecompositionTestCholeskyDecomposition(Matrix m) {
            CholeskyDecomposition chol = m.Chol();

            Assert.IsTrue(chol.L.IsLowerTriangular());

            Matrix mActual = chol.L * chol.L.ConjugateTranspose();
            Matrix mExpected = m;
            Assert.AreEqual(mExpected, mActual);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "RealMatrices")]
        public void MatrixDecompositionTestSVDReal(Matrix m) {
            SVD svd = m.SVD();

            Assert.IsTrue(svd.U.IsUnitary(), "U is not unitary, U.H * U:\n" + svd.U.ConjugateTranspose() * svd.U);
            Assert.IsTrue(svd.D.IsDiagonal(), "D is not diagonal, D:\n" + svd.D);
            Assert.IsTrue(svd.V.IsUnitary(), "V is not unitary, V.H * V:\n" + svd.V.ConjugateTranspose() * svd.V);

            Matrix mActual = svd.U * svd.D * svd.V.ConjugateTranspose();
            Matrix mExpected = m;
            Assert.AreEqual(mExpected, mActual, "A != UDV.T:\n" + mActual);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "ComplexMatrices")]
        public void MatrixDecompositionTestSVDComplex(Matrix m) {
            SVD svd = m.SVD();

            Assert.IsTrue(svd.U.IsUnitary(), "U is not unitary, U.H * U:\n" + svd.U.ConjugateTranspose() * svd.U);
            Assert.IsTrue(svd.D.IsDiagonal(), "D is not diagonal, D:\n" + svd.D);
            Assert.IsTrue(svd.V.IsUnitary(), "V is not unitary, V.H * V:\n" + svd.V.ConjugateTranspose() * svd.V);

            Matrix mActual = svd.U * svd.D * svd.V.ConjugateTranspose();
            Matrix mExpected = m;
            Assert.AreEqual(mExpected, mActual, "A != UDV.T:\n" + mActual);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "RealMatrices")]
        public void MatrixDecompositionTestBidiagonalization(Matrix m) {
            Bidiagonalization bid = m.Bidiagonalization();

            Assert.IsTrue(bid.U.IsUnitary());
            Assert.IsTrue(bid.B.IsBiDiagonal());
            Assert.IsTrue(bid.V.IsUnitary());

            Matrix mActual = bid.U * bid.B * bid.V.ConjugateTranspose();
            Matrix mExpected = m;
            Assert.AreEqual(mExpected, mActual);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "ComplexMatrices")]
        public void MatrixDecompositionTestHessenbergDecomposition(Matrix m) {
            HessenbergDecomposition HD = m.Hessen();

            Assert.IsTrue(HD.H.IsUpperHessenberg());

            Matrix mActual = HD.P.ConjugateTranspose() * HD.H * HD.P;
            Matrix mExpected = m;
            Assert.AreEqual(mExpected, mActual);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "RealMatrices")]
        public void MatrixDecompositionTestQRDecompositionReal(Matrix m) {
            QRDecomposition qr = m.QR();

            Assert.IsTrue(qr.Q.IsUnitary(), "Q is not unitary, Q.H * Q:\n" + qr.Q.ConjugateTranspose() * qr.Q);
            Assert.IsTrue(qr.R.IsUpperTriangular(), "R is not upper triangular, R:\n" + qr.R);

            Matrix mActual = qr.Q * qr.R;
            Matrix mExpected = m;
            Assert.AreEqual(mExpected, mActual, "A != Q*R:\n" + mActual);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "ComplexMatrices")]
        public void MatrixDecompositionTestQRDecompositionComplex(Matrix m) {
            QRDecomposition qr = m.QR();

            Assert.IsTrue(qr.Q.IsUnitary(), "Q is not unitary, Q.H * Q:\n" + qr.Q.ConjugateTranspose() * qr.Q);
            Assert.IsTrue(qr.R.IsUpperTriangular(), "R is not upper triangular, R:\n" + qr.R);

            Matrix mActual = qr.Q * qr.R;
            Matrix mExpected = m;
            Assert.AreEqual(mExpected, mActual, "A != Q*R:\n" + mActual);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "RealMatrices")]
        public void MatrixDecompositionTestQRWithGivensDecompositionReal(Matrix m) {
            QRDecomposition qr = m.QR(true);

            Assert.IsTrue(qr.Q.IsUnitary(), "Q is not unitary, Q.H * Q:\n" + qr.Q.ConjugateTranspose() * qr.Q);
            Assert.IsTrue(qr.R.IsUpperTriangular(), "R is not upper triangular, R:\n" + qr.R);

            Matrix mActual = qr.Q * qr.R;
            Matrix mExpected = m;
            Assert.AreEqual(mExpected, mActual, "A != Q*R:\n" + mActual);
        }
        
        [Test, TestCaseSource(typeof(TestMatrices), "ComplexMatrices")]
        public void MatrixDecompositionTestQRWithGivensDecompositionComplex(Matrix m) {
            QRDecomposition qr = m.QR(true);

            Assert.IsTrue(qr.Q.IsUnitary(), "Q is not unitary, Q.H * Q:\n" + qr.Q.ConjugateTranspose() * qr.Q);
            Assert.IsTrue(qr.R.IsUpperTriangular(), "R is not upper triangular, R:\n" + qr.R);

            Matrix mActual = qr.Q * qr.R;
            Matrix mExpected = m;
            Assert.AreEqual(mExpected, mActual, "A != Q*R:\n" + mActual);
        }

        [Test, TestCaseSource(typeof(TestMatrices), "SymmetricPositiveDefiniteMatrices")]
        public void MatrixDecompositionTestEigenvaluesAndEigenvectors(Matrix m) {
            Matrix mActual, mExpected;
            EigenvalueDecomposition eigen = m.Eigen();

            for (int i = 0; i < m.Height; i++) {
                mActual = m * eigen.Eigenvectors.GetColumnVector(i) - eigen.Eigenvalues[i] * eigen.Eigenvectors.GetColumnVector(i);
                mExpected = MatrixFactory.Zeros(m.Height, 1);
                Assert.AreEqual(mExpected, mActual);
            }
        }
    }
}
