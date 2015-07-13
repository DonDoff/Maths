using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class LUDecomposition {
        public Matrix L { get; private set; }
        public Matrix U { get; private set; }

        public Matrix P { get; private set; }
        public double DetOfP { get; private set; }

        private Matrix M;

        public LUDecomposition(Matrix m) {
            DetOfP = 1;
            M = m.Copy();
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            if (!M.IsSquare()) {
                throw new MatrixException("The matrix is not square!");
            }

            L = Matrix.IdentityMatrix(M.Height);
            U = M.Copy();

            P = Matrix.IdentityMatrix(M.Height);

            ComplexNumber p = 0;
            ComplexNumber pom2;
            int k0 = 0;

            for (int k = 0; k < M.Width - 1; k++) {
                p = 0;
                // find the row with the biggest pivot
                for (int i = k; i < M.Height; i++) {
                    if (ComplexNumberMath.Abs(U[i, k]) > p) {
                        p = ComplexNumberMath.Abs(U[i, k]);
                        k0 = i;
                    }
                }
                if (p == 0) {
                    throw new MatrixException("The matrix is singular!");
                }

                // switch two rows in permutation matrix
                P = P.SwapRows(k, k0);

                for (int i = 0; i < k; i++) {
                    pom2 = L[k, i];
                    L[k, i] = L[k0, i];
                    L[k0, i] = pom2;
                }

                if (k != k0) {
                    DetOfP *= -1;
                }

                // Switch rows in U
                for (int i = 0; i < M.Width; i++) {
                    pom2 = U[k, i];
                    U[k, i] = U[k0, i];
                    U[k0, i] = pom2;
                }

                for (int i = k + 1; i < M.Height; i++) {
                    L[i, k] = U[i, k] / U[k, k];
                    for (int j = k; j < M.Width; j++) {
                        U[i, j] = U[i, j] - L[i, k] * U[k, j];
                    }
                }
            }
        }

        /// <summary>
        /// Function solves Ax = b in confirmity with solution vector "b"
        /// </summary>
        /// <param name="v"></param>
        /// <returns>x</returns>
        public Vector LinearSolver(Vector b) {
            if (M.Height != M.Width) {
                throw new MatrixException("The matrix is not square!");
            }
            if (M.Height != b.Height) {
                throw new MatrixException("Wrong number of results in solution vector!");
            }
            if (L == null) {
                MakeDecomposition();
            }

            Vector bp = (P * b).ToColumnVector();

            // Solves Ly = b
            Vector y = new Vector(L.Height);
            for (int i = 0; i < L.Height; i++) {
                y[i] = bp[i];
                for (int j = 0; j < i; j++) {
                    y[i] -= L[i, j] * y[j];
                }
                y[i] = y[i] / L[i, i];
            }

            // Solves Ux = y
            Vector x = new Vector(U.Height);
            for (int i = U.Height - 1; i > -1; i--) {
                x[i] = y[i];
                for (int j = U.Height - 1; j > i; j--) {
                    x[i] -= U[i, j] * x[j];
                }
                x[i] = x[i] / U[i, i];
            }
            return x;
        }
    }
}
