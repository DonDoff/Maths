﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    // Based on: https://math.dartmouth.edu/~m56s13/Southworth_proj.pdf
    public class SVD {
        private static int ITERATION_DEPTH = 50;

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

            QRDecomposition qr = B.QR();
            V = qr.Q;

            for (int i = 0; i < ITERATION_DEPTH; i++) {

                Console.WriteLine("Q:\n" + qr.Q + "\n");
                Console.WriteLine("R:\n" + qr.R + "\n");

                B = qr.Q.ConjugateTranspose() * B * qr.Q;
                qr = B.QR();

                V *= qr.Q;

                if (D.IsDiagonal()) {
                    Console.WriteLine("Iterations stopped after: " + i);
                    break;
                }
            }

            V = bid.V * V;

            D = B;

            U = new Matrix(M.Height, V.Width);
            for (int i = 0; i < V.Width; i++) {
                U[Vector.Arrange(M.Height), i] = (M * V[Vector.Arrange(V.Height), i] / D[i, i]).ToColumnVector();
            }

            //D = B.ConjugateTranspose() * B;

            //if (D.Height > 1) {                
            //    V = EigenDecomp();
            //    V = bid.V * V;

            //    //D = B * B.ConjugateTranspose();
            //    //U = EigenDecomp();
            //    //U = bid.U * U;
            //    //D = D.SubMatrix(0, M.Height, 0, M.Width);

            //    D = MatrixMath.Sqrt(D);

            //    U = new Matrix(M.Height, V.Width);
            //    for (int i = 0; i < V.Width; i++) {
            //        U[ColumnVector.Arrange(M.Height), i] = (M * V[ColumnVector.Arrange(V.Height), i] / D[i, i]).ToColumnVector();
            //    }
            //} else {
            //    V = bid.V;
            //    D = MatrixMath.Sqrt(D);
            //    U = new Matrix(M.Height, V.Width);
            //    for (int i = 0; i < V.Width; i++) {
            //        U[ColumnVector.Arrange(M.Height), i] = (M * V[ColumnVector.Arrange(V.Height), i] / D[i, i]).ToColumnVector();
            //    }
            //}
        }

        private Matrix EigenDecomp() {
            Matrix shift = ComputeShift(D);
            QRDecomposition qr = (D - shift).QR();
            Matrix Eigenvector = qr.Q;

            for (int i = 0; i < ITERATION_DEPTH; i++) {
                D = qr.R * qr.Q + shift;
                shift = ComputeShift(D);
                qr = (D - shift).QR();

                Eigenvector *= qr.Q;

                if (D.IsDiagonal()) {
                    Console.WriteLine(i);
                    break;
                }
            }
            return Eigenvector;
        }

        /// <summary>
        /// The shift is generally taken to be the smallest eigenvalue of the 2 × 2 matrix in the bottom.
        /// </summary>
        /// <returns></returns>
        private Matrix ComputeShift(Matrix mat) {
            Matrix subMat = mat.SubMatrix(mat.Height - 2, mat.Height, mat.Width - 2, mat.Width);
            ComplexNumber trace = subMat.Trace();
            ComplexNumber det = subMat.Determinant();
            ComplexNumber eig1 = (trace + ComplexNumberMath.Sqrt(trace * trace - 4 * det)) / 2;
            ComplexNumber eig2 = (trace - ComplexNumberMath.Sqrt(trace * trace - 4 * det)) / 2;
            ComplexNumber smallest = ComplexNumberMath.Min(eig1, eig2);
            ComplexNumber eigenSquared = smallest * smallest;
            Matrix shift = eigenSquared * Matrix.IdentityMatrix(mat.Height);
            return shift;
        }


    }
}