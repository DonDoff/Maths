﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    // Based on: https://math.dartmouth.edu/~m56s13/Southworth_proj.pdf
    public class SVD : IDecomposition {
        public Matrix U { get; private set; }
        public Matrix D { get; private set; }
        public Matrix V { get; private set; }
        private Matrix M;

        public SVD(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            Bidiagonalization bid = M.Bidiagonalization();
            Matrix B = bid.B.Copy();

            D = B * B.ConjugateTranspose();
            U = EigenDecomp(D);
            U = bid.U * U;

            D = B.ConjugateTranspose() * B;
            V = EigenDecomp(D);
            V = bid.V * V;

            Matrix CopyOfD = D.Copy();
            D = MatrixFactory.IdentityMatrix(D.Height);
            for (int i = 0; i < D.Height; i++) {
                D[i, i] = CopyOfD[i, i].Abs() < Constants.EPS ? 0 : ComplexMath.Sqrt(CopyOfD[i, i]);
            }

            if (M.Height > M.Width) {
                D = D.ConcatenateRows(MatrixFactory.Zeros(M.Height - D.Height, D.Width));
            } else if (M.Height < M.Width) {
                D = D.SubMatrix(0, M.Height, 0, D.Width);
            }


            //if (D.Height > 1) {
            //    V = EigenDecomp(D);
            //    V = bid.V * V;

            //    D = B * B.ConjugateTranspose();

            //    U = EigenDecomp(D);
            //    U = bid.U * U;
            //    if (U.Width > D.Height) {
            //        U = U.SubMatrix(0, U.Height, 0, D.Width);
            //    }

            //    Matrix CopyOfD = D.Copy(); 
            //    D = MatrixFactory.IdentityMatrix(D.Height);
            //    for (int i = 0; i < D.Height; i++) {
            //        D[i, i] = ComplexMath.Sqrt(CopyOfD[i, i]);
            //    }

            //    //U = new Matrix(M.Height, V.Width);
            //    //for (int i = 0; i < V.Width; i++) {
            //    //    U[Vector.Arrange(M.Height), i] = (M * V[Vector.Arrange(V.Height), i] / D[i, i]).ToColumnVector();
            //    //}
            //} else {
            //    V = bid.V;
            //    D = MatrixMath.Sqrt(D);
            //    U = new Matrix(M.Height);
            //    for (int i = 0; i < V.Width; i++) {
            //        U[Vector.Arrange(M.Height), i] = (M * V[Vector.Arrange(V.Height), i] / D[i, i]).ToColumnVector();
            //    }
            //}
        }

        private Matrix EigenDecomp(Matrix M) {
            Matrix shift = MatrixFactory.IdentityMatrix(M.Height) * ComputeShift(M);
            QRDecomposition qr = (M - shift).QR();
            Matrix Eigenvector = qr.Q;

            for (int i = 0; i < Constants.ITERATION_DEPTH; i++) {
                M = qr.R * qr.Q + shift;
                shift = MatrixFactory.IdentityMatrix(M.Height) * ComputeShift(M);
                qr = (M - shift).QR();

                Eigenvector *= qr.Q;

                if (M.IsDiagonal()) {
                    break;
                }
            }
            D = M.Copy();
            return Eigenvector;
        }

        /// <summary>
        /// The shift is generally taken to be the smallest eigenvalue of the 2 × 2 matrix in the bottom.
        /// </summary>
        /// <returns></returns>
        private Complex ComputeShift(Matrix mat) {
            try { 
                Matrix subMat = mat.SubMatrix(mat.Height - 2, mat.Height, mat.Width - 2, mat.Width);
                Complex trace = subMat.Trace();
                Complex det = subMat.Determinant();
                Complex eig1 = (trace + ComplexMath.Sqrt(trace * trace - 4 * det)) / 2;
                Complex eig2 = (trace - ComplexMath.Sqrt(trace * trace - 4 * det)) / 2;
                Complex eigen = ComplexMath.Min(eig1, eig2);
                return eigen;
            } catch (MatrixException) {
                return 0;
            }
        }


        //private Matrix QRSpecialGivens(Matrix B) {
        //    Matrix G;
        //    U = Matrix.IdentityMatrix(M.Height);
        //    V = Matrix.IdentityMatrix(M.Height);

        //    // B.Width > 1!

        //    ComplexNumber shift = ComputeShift(B);
        //    ComplexNumber t11 = B[0, 0] * B[0, 0] - shift;
        //    ComplexNumber t21 = B[0, 0] * B[0, 1];
        //    G = Matrix.IdentityMatrix(M.Height);
        //    G[Vector.Arrange(0, 2), Vector.Arrange(0, 2)] = MatrixMath.CalculatGivensRotation(t11, t21);
        //    B = B * G.ConjugateTranspose();
        //    V = G.ConjugateTranspose();

        //    for (int i = 1; i < B.Height; i++) {

        //        G = Matrix.IdentityMatrix(B.Height);
        //        G[Vector.Arrange(i - 1, i + 1), Vector.Arrange(i - 1, i + 1)] = MatrixMath.CalculatGivensRotation(B[i - 1, i - 1], B[i, i - 1]);
        //        B = G * B;
        //        U = G * U;

        //        if (i < B.Height - 1) {
        //            G = Matrix.IdentityMatrix(B.Height);
        //            G[Vector.Arrange(i, i + 2), Vector.Arrange(i, i + 2)] = MatrixMath.CalculatGivensRotation(B[i - 1, i], B[i - 1, i + 1]);
        //            B = B * G.ConjugateTranspose();
        //            V = V * G.ConjugateTranspose();
        //        }
        //    }
        //    return B;
        //}
    }
}
