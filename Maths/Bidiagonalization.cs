using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class Bidiagonalization {
        public Matrix U { get; private set; }
        public Matrix B { get; private set; }
        public Matrix V { get; private set; }

        private Matrix M;

        public Bidiagonalization(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            if (M.Height < M.Width) {
                throw new MatrixException("The number of rows should be equal or greater than the number of columns!");
            }

            Matrix A = M.Copy();
            Matrix[] Us = new Matrix[M.Width];
            Matrix[] Vs;
            if (M.Width > 1) {
                Vs = new Matrix[M.Width - 2];
            } else {
                Vs = new Matrix[0];
            }

            for (int k = 0; k < Us.Length; k++) {
                // Left-hand reduction
                Vector x = new Vector(A, 0);
                U = MatrixMath.CalculateHouseholderTransform(x);

                A = (U * A).SubMatrix(0, A.Height, 1, A.Width);

                Matrix U_k = Matrix.IdentityMatrix(M.Height);
                for (int i = 0; i < U.Height; i++) {
                    for (int j = 0; j < U.Width; j++) {
                        U_k[i + k, j + k] = U[i, j];
                    }
                }
                Us[k] = U_k;

                // Right-hand reduction
                if (k < Vs.Length) {
                    A = A.Transpose();
                    x = new Vector(A, 0);
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

            U = Us[0];
            for (int i = 1; i < Us.Length; i++) {
                U *= Us[i];
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
