using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class CholeskyDecomposition : IDecomposition {

        public Matrix L { get; private set; }
        private Matrix M;

        /// <summary>
        /// An exception is thrown if the matrix is not hermitian.
        /// </summary>
        /// <param name="m"></param>
        public CholeskyDecomposition(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            if (!M.IsHermitian()) {
                throw new MatrixException("The matrix is not Hermitian!");
            }

            L = new Matrix(M.Height);
            for (int i = 0; i < M.Height; i++) {
                for (int j = 0; j < (i + 1); j++) {
                    Complex sum = 0;
                    for (int k = 0; k < j; k++) {
                        sum += L[i, k] * L[j, k].Conjugate();
                    }
                    L[i, j] = (i == j) ? ComplexMath.Sqrt(M[j, j] - sum) :
                        (1.0 / L[j, j] * (M[i, j] - sum));
                }
            }

            if (L * L.ConjugateTranspose() != M) {
                throw new MatrixException("Cholesky Decomposition failed, the matrix is not positive definite!");
            }

        }
    }
}
