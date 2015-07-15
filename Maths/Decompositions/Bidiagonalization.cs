using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class Bidiagonalization : IDecomposition {
        public Matrix U { get; private set; }
        public Matrix B { get; private set; }
        public Matrix V { get; private set; }

        private Matrix M;

        public Bidiagonalization(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            Matrix A = M.Copy();
            Matrix[] Us;
            Matrix[] Vs;

            if (M.Width == 1) {
                Us = new Matrix[M.Width];
                Vs = new Matrix[0];
            } else if (M.Height == 1) {
                Us = new Matrix[0];
                Vs = new Matrix[M.Height];
            } else {
                if (M.Height > M.Width) {
                    Us = new Matrix[M.Width];
                } else {
                    Us = new Matrix[M.Height - 1];
                }

                if (M.Height >= M.Width) {
                    Vs = new Matrix[M.Width - 2];
                } else if (M.Height == M.Width - 1) {
                    Vs = new Matrix[M.Height - 1];
                } else {
                    Vs = new Matrix[M.Height];
                }
            }

            for (int k = 0; k < Math.Max(Us.Length, Vs.Length); k++) {
                // Left-hand reduction
                if (k < Us.Length) {
                    ColumnVector x = new ColumnVector(A, 0);
                    U = MatrixMath.CalculateHouseholderTransform(x);

                    A = (U * A).SubMatrix(0, A.Height, 1, A.Width);

                    Matrix U_k = Matrix.IdentityMatrix(M.Height);
                    for (int i = 0; i < U.Height; i++) {
                        for (int j = 0; j < U.Width; j++) {
                            U_k[i + k, j + k] = U[i, j];
                        }
                    }
                    Us[k] = U_k;
                } else {
                    A = A.SubMatrix(0, A.Height, 1, A.Width);
                }

                // Right-hand reduction
                if (k < Vs.Length) {
                    A = A.Transpose();
                    ColumnVector x = new ColumnVector(A, 0);
                    V = MatrixMath.CalculateHouseholderTransform(x);
                    A = (V * A).SubMatrix(0, A.Height, 1, A.Width);
                    A = A.Transpose();

                    Matrix V_k = Matrix.IdentityMatrix(M.Width);
                    for (int i = 0; i < V.Height; i++) {
                        for (int j = 0; j < V.Width; j++) {
                            V_k[i + k + 1, j + k + 1] = V[i, j];
                        }
                    }
                    Vs[k] = V_k;
                } else {
                    A = A.SubMatrix(1, A.Height, 0, A.Width);
                }
            }

            if (Us.Length == 0) {
                U = Matrix.IdentityMatrix(M.Height);
            } else {
                U = Us[0];
                for (int i = 1; i < Us.Length; i++) {
                    U *= Us[i];
                }
            }

            if (Vs.Length == 0) {
                V = Matrix.IdentityMatrix(M.Width);
            } else {
                V = Vs[0];
                for (int i = 1; i < Vs.Length; i++) {
                    V *= Vs[i];
                }
            }
            B = U.ConjugateTranspose() * M * V;
        }
    }
}
