using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class HessenbergDecomposition : IDecomposition {
        public Matrix P { get; private set; }  // Hessenberg transform
        public Matrix H { get; private set; }  // Hessenberg matrix
        private Matrix M;

        /// <summary>
        /// Should only be called on square matrices, will throw an exception if the matrix is not square.
        /// </summary>
        /// <param name="m"></param>
        public HessenbergDecomposition(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            if (!M.IsSquare()) {
                throw new MatrixException("The matrix is not square!");
            }

            Matrix A = M.Copy();
            Matrix[] Hs = M.Height <= 2 ? new Matrix[0] : new Matrix[M.Height - 2];
            
            for (int k = 0; k < Hs.Length; k++) {
                Vector x = A[Vector.Arrange(k + 1, A.Height), k];
                P = MatrixMath.CalculateHouseholderTransform(x);

                Matrix H_k = MatrixFactory.IdentityMatrix(M.Height);
                H_k[Vector.Arrange(k + 1, M.Height), Vector.Arrange(k + 1, M.Height)] = P;
                Hs[k] = H_k;

                A = H_k * A * H_k.ConjugateTranspose();
            }

            if (Hs.Length == 0) {
                P = MatrixFactory.IdentityMatrix(M.Height);
            } else {
                P = Hs[Hs.Length - 1];
                for (int i = Hs.Length - 2; i >= 0; i--) {
                    P *= Hs[i];
                }
            }

            H = P * M * P.ConjugateTranspose();
        }

    }
}
