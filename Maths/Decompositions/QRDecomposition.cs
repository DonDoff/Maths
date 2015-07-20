using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class QRDecomposition : IDecomposition {

        public Matrix Q { get; private set; }
        public Matrix R { get; private set; }

        private Matrix M;

        public QRDecomposition(Matrix m, bool usingGivens = false) {
            M = m;
            if (usingGivens) {
                MakeDecompositionUsingGivens();
            } else {
                MakeDecomposition();
            }
        }

        private void MakeDecomposition() {
            Matrix A = M.Copy();
            Q = MatrixFactory.IdentityMatrix(M.Height);

            for (int k = 0; k < Math.Min(M.Height - 1, M.Width); k++) {
                Vector x = new Vector(A, 0);
                Matrix Q_k = MatrixMath.CalculateHouseholderTransform(x);
                A = (Q_k * A).SubMatrix(1, A.Height, 1, A.Width);

                Matrix Q_temp = MatrixFactory.IdentityMatrix(M.Height);
                Q_temp[Vector.Arrange(k, M.Height), Vector.Arrange(k, M.Height)] = Q_k;

                Q *= Q_temp;
            }
            
            R = Q.ConjugateTranspose() * M;
        }

        private void MakeDecompositionUsingGivens() {
            Matrix A = M.Copy();
            Matrix G;
            Q = MatrixFactory.IdentityMatrix(M.Height);

            for (int j = 0; j < M.Width; j++) {
                for (int i = M.Height - 1; i > j; i--) {
                    if (A[i, j] != 0) {
                        G = MatrixFactory.IdentityMatrix(M.Height);
                        G[Vector.Arrange(i - 1, i + 1), Vector.Arrange(i - 1, i + 1)] = MatrixMath.CalculatGivensRotation(A[i - 1, j], A[i, j]);

                        A = G * A;
                        Q *= G.ConjugateTranspose();
                    }
                }
            }
            R = A;
        }

    }
}
